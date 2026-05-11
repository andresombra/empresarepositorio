using Empresa.Application.DTOs;
using Empresa.Application.DTOs.Response;
using Empresa.Application.Interfaces;
using GerEmpresa.Domain.Entities;
using Empresa.Domain.Interfaces.Repositories;
using Mapster;
using Empresa.Application.DTOs.Usuario;

namespace Empresa.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        //private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository _usuarioRepository)
        {
           _usuarioRepository = usuarioRepository;
        }

        public async Task CriarUsuarioAsync(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            usuario.Situacao = "Ativo";
            usuario.Data = DateTime.UtcNow.ToString("yyyy-MM-dd");
            
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

        public async Task<IList<ResponseUsuarioDto>> ListarAsync() // Return DTOs mapped with Mapster
        {
            var lista = await _usuarioRepository.ListaAsync();
            if (lista == null || lista.Count == 0)
            {
                return new List<ResponseUsuarioDto>();
            }

            var dtoList = lista.Adapt<IList<ResponseUsuarioDto>>();
            return dtoList;
        }
    }
}
