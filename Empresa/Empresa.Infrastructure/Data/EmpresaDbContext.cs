using Empresa.Infrastructure.Mappings;
using GerEmpresa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Infrastructure.Data
{
    public class EmpresaDbContext : DbContext
    {
        public EmpresaDbContext(DbContextOptions<EmpresaDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<GerEmpresa.Domain.Entities.Empresa> Empresas { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            OnModelCreatingUsuario(modelBuilder);

            modelBuilder.Entity<GerEmpresa.Domain.Entities.Empresa>()
                .ToTable("Empresa", schema: "andresombra");

            modelBuilder.Entity<GerEmpresa.Domain.Entities.Empresa>().HasKey(e => e.Id)
                .HasName("PK_Empresa");

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.HasKey(e => e.Id)
                      .HasName("PK_Usuario");

                entity.Property(e => e.Id).HasColumnName("USU_ID");
                entity.Property(e => e.Email).HasColumnName("USU_EMAIL");
                entity.Property(e => e.Senha).HasColumnName("USU_SENHA");
                entity.Property(e => e.Data).HasColumnName("USU_DATA");
                entity.Property(e => e.Situacao).HasColumnName("USU_SITUACAO");
                entity.Property(e => e.Plano).HasColumnName("USU_PLANO");
                entity.Property(e => e.Adm).HasColumnName("USU_ADM");
                entity.Property(e => e.EmpresaId).HasColumnName("USU_EMPRESA_ID");

                // RELACIONAMENTO
                entity.HasOne(u => u.Empresa)
                      .WithMany(e => e.Usuarios)
                      .HasForeignKey(u => u.EmpresaId)
                      .HasConstraintName("FK_Usuario_Empresa");
            });

            // Aplicar mapeamento do Fornecedor
            modelBuilder.ApplyConfiguration(new FornecedorMapping());
        }

        private static void OnModelCreatingUsuario(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
        }

        public async Task BeginTransaction()
        {
            await Database.BeginTransactionAsync();
        }

        public async Task<bool> Commit()
        {
            var retorno = await SaveChangesAsync() > 0;

            await Database.CommitTransactionAsync();

            return retorno;
        }

        public async Task Rollback()
        {
            await Database.RollbackTransactionAsync();
        }   

    }
}
