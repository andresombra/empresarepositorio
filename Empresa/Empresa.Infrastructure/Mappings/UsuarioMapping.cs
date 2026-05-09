using GerEmpresa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Infrastructure.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(e => e.Id)
                  .HasName("PK_Usuario");
            builder.Property(e => e.Id).HasColumnName("USU_ID");
            builder.Property(e => e.Email).HasColumnName("USU_EMAIL");
            builder.Property(e => e.Senha).HasColumnName("USU_SENHA");
            builder.Property(e => e.Data).HasColumnName("USU_DATA");
            builder.Property(e => e.Situacao).HasColumnName("USU_SITUACAO");
            builder.Property(e => e.Plano).HasColumnName("USU_PLANO");
            builder.Property(e => e.Adm).HasColumnName("USU_ADM");
            builder.Property(e => e.EmpresaId).HasColumnName("USU_EMPRESA_ID");
            // RELACIONAMENTO
            builder.HasOne(u => u.Empresa)
                  .WithMany(e => e.Usuarios)
                  .HasForeignKey(u => u.EmpresaId)
                  .HasConstraintName("FK_Usuario_Empresa");
        }
    }
}
