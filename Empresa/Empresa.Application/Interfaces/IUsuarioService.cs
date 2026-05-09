using Empresa.Application.DTOs;
using Empresa.Application.DTOs.Response;
using Empresa.Application.DTOs.Usuario;
using GerEmpresa.Domain.Entities;

namespace Empresa.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task CriarUsuarioAsync(Usuario usuarioDto);
        Task<IList<ResponseUsuarioDto>> ListarAsync();
        Task<LoginResponseDto?> AutenticarAsync(LoginRequestDto request);
    }
}
