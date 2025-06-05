using Empresa.Application.DTOs;
using Empresa.Application.DTOs.Response;

public interface IEmpresaService
{
    Task<IEnumerable<EmpresaResponseDto>> GetAllAsync();
    Task<EmpresaResponseDto?> GetByIdAsync(int id);
    Task<EmpresaResponseDto> CreateAsync(EmpresaDto dto);
    Task<bool> UpdateAsync(int id, EmpresaDto dto);
    Task<bool> DeleteAsync(int id);
}
