using Empresa.Domain.Entities;
using Empresa.Domain.Interfaces.Repositories;
using Empresa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EmpresaDbContext _context;

        public UsuarioRepository(EmpresaDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> AutenticarAsync(string login, string senha)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == login && u.Senha == senha);
        }
    }
}
