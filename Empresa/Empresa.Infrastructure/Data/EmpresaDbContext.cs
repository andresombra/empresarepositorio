using Empresa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Infrastructure.Data
{
    public class EmpresaDbContext : DbContext
    {
        public EmpresaDbContext(DbContextOptions<EmpresaDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<GerEmpresa.Domain.Entities.Empresa> Empresas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GerEmpresa.Domain.Entities.Empresa>()
                .ToTable("Empresa", schema: "andresombra");
        }


    }
}
