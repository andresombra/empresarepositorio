using Empresa.Domain.Interfaces.Repositories;
using GerEmpresa.Domain.Entities;
using Empresa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Infrastructure.Repositories
{
    public class FornecedorRepository : BaseRepository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(EmpresaDbContext context) : base(context)
        {
        }
    }
}
