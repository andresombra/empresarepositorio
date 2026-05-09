using GerEmpresa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Empresa.Infrastructure.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedor");
            builder.HasKey(e => e.Id).HasName("PK_Fornecedor");
            builder.Property(e => e.Id).HasColumnName("FOR_ID");
            builder.Property(e => e.Nome).HasColumnName("FOR_NOME").IsRequired().HasMaxLength(250);
            builder.Property(e => e.Email).HasColumnName("FOR_EMAIL").HasMaxLength(300);
            builder.Property(e => e.Cnpj).HasColumnName("FOR_CPFCNPJ").HasMaxLength(20);
            builder.Property(e => e.DataCadastro).HasColumnName("FOR_DTCAD");
            builder.Property(e => e.Contato).HasColumnName("FOR_TEL_CELULAR").HasMaxLength(50);
            builder.Property(e => e.Endereco).HasColumnName("FOR_END_LOGRADOURO").HasMaxLength(300);
        }
    }
}
