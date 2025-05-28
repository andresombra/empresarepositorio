using Empresa.Application.DTOs;
using Empresa.Application.DTOs.Response;

namespace Empresa.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task CriarUsuarioAsync(UsuarioDto usuarioDto);
        Task<LoginResponseDto?> AutenticarAsync(LoginRequestDto request);
    }
}
