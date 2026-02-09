using Empresa.Application.DTOs;
using Empresa.Application.DTOs.Response;
using GerEmpresa.Domain.Entities;

namespace Empresa.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task CriarUsuarioAsync(Usuario usuarioDto);
        Task<IList<Usuario>> ListarAsync();
        Task<LoginResponseDto?> AutenticarAsync(LoginRequestDto request);
    }
}
