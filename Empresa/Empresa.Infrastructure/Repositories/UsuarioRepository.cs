using Empresa.Domain.Entities;
using Empresa.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Infrastructure.Repositories
{
    public class UsuarioRepository
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
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.UserLogin == login && u.UserPass == senha);
        }
    }
}
