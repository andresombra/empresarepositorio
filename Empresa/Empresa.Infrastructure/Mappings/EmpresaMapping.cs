using GE = GerEmpresa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Empresa.Infrastructure.Mappings
{
    public class EmpresaMapping : IEntityTypeConfiguration<GE.Empresa>
    {
        public void Configure(EntityTypeBuilder<GE.Empresa> builder)
        {
            builder.ToTable("Empresa");
            builder.HasKey(e => e.Id).HasName("PK_Empresa");

            builder.Property(e => e.Id)
                   .HasColumnName("EMP_ID");

            builder.Property(e => e.Nome)
                   .HasColumnName("EMP_NOME")
                   .IsRequired()
                   .HasMaxLength(250)
                   .HasColumnType("varchar(250)");

            builder.Property(e => e.Email)
                   .HasColumnName("EMP_EMAIL")
                   .HasMaxLength(300)
                   .HasColumnType("varchar(300)");

            builder.Property(e => e.DataCadastro)
                   .HasColumnName("EMP_DTCAD");

            builder.Property(e => e.Contato)
                   .HasColumnName("EMP_CONATO")
                   .HasMaxLength(15)
                   .HasColumnType("varchar(15)");

            builder.Property(e => e.Endereco)
                   .HasColumnName("EMP_ENDERECO")
                   .HasMaxLength(300)
                   .HasColumnType("varchar(300)");

            // Relationship 1:N with Usuario
            builder.HasMany(e => e.Usuarios)
                   .WithOne(u => u.Empresa)
                   .HasForeignKey(u => u.EmpresaId)
                   .HasConstraintName("FK_Usuario_Empresa");
        }
    }
}
