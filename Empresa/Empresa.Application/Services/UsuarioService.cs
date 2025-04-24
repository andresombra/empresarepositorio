using Empresa.Application.DTOs;
using Empresa.Application.DTOs.Response;
using Empresa.Application.Interfaces;
using Empresa.Domain.Entities;
using Empresa.Infrastructure.Repositories;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public UsuarioService(UsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task CriarUsuarioAsync(UsuarioDto usuarioDto)
        {
            var usuario = usuarioDto.Adapt<Usuario>();
            usuario.UserId = Guid.NewGuid();
            usuario.UserDateCreate = DateTime.UtcNow;
            usuario.UserStatus = true;
            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task<LoginResponseDto?> AutenticarAsync(LoginRequestDto request)
        {
            var usuario = await _usuarioRepository.AutenticarAsync(request.UserLogin, request.UserPass);
            if (usuario == null || !usuario.UserStatus) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.UserId.ToString()),
                    new Claim("EmpresaId", usuario.EmpresaId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new LoginResponseDto { Token = tokenHandler.WriteToken(token) };
        }
    }
}
