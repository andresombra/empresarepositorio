using Empresa.Domain.Interfaces.Repositories;
using Empresa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Infrastructure.Repositories;
public class EmpresaRepository : BaseRepository<GerEmpresa.Domain.Entities.Empresa>, IEmpresaRepository
{
    public EmpresaRepository(EmpresaDbContext context) : base(context)
    {
    }
}
