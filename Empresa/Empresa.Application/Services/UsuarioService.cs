using Empresa.Application.DTOs;
using Empresa.Application.DTOs.Response;
using Empresa.Application.Interfaces;
using Empresa.Domain.Entities;
using Empresa.Domain.Interfaces.Repositories;

namespace Empresa.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task CriarUsuarioAsync(UsuarioDto usuarioDto)
        {
            var usuario = new Usuario
            {
                Id = usuarioDto.UsuarioId,
                Email = usuarioDto.UserLogin,
                Senha = usuarioDto.UserPass,
                Situacao = "Ativo",
                Data = DateTime.UtcNow.ToString("yyyy-MM-dd"),
            };

            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task<LoginResponseDto?> AutenticarAsync(LoginRequestDto request)
        {
            var usuario = await _usuarioRepository.AutenticarAsync(request.UserLogin, request.UserPass);
            if (usuario == null) return null;

            return new LoginResponseDto
            {
                Token = "fake-jwt-token" // Substitua pela lógica de geração de token real
            };
        }
    }
}
