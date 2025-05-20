using GerEmpresa.Domain.Entities;

public interface IEmpresaRepository
{
    Task<IEnumerable<GerEmpresa.Domain.Entities.Empresa>> GetAllAsync();
    Task<GerEmpresa.Domain.Entities.Empresa?> GetByIdAsync(int id);
    Task AddAsync(GerEmpresa.Domain.Entities.Empresa empresa);
    Task UpdateAsync(GerEmpresa.Domain.Entities.Empresa empresa);
    Task DeleteAsync(GerEmpresa.Domain.Entities.Empresa empresa);
}
