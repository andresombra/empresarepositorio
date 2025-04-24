using Empresa.Application.DTOs;
using Empresa.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task CriarUsuarioAsync(UsuarioDto usuarioDto);
        Task<LoginResponseDto?> AutenticarAsync(LoginRequestDto request);
    }
}
