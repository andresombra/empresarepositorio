using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerEmpresa.Domain.Entities
{
    [Table("Fornecedor")]
    public class Fornecedor
    {
        [Key]
        [Column("FOR_ID")]
        public int Id { get; set; }

        [Column("FOR_NOME", TypeName = "varchar(250)")]
        [StringLength(250)]
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Column("FOR_EMAIL", TypeName = "varchar(300)")]
        [StringLength(300)]
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        [Column("FOR_CPFCNPJ", TypeName = "varchar(20)")]
        [StringLength(20)]
        public string? Cnpj { get; set; } = string.Empty;

        [Column("FOR_DTCAD")]
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        [Column("FOR_CONTATO", TypeName = "varchar(50)")]
        [StringLength(50)]
        public string? Contato { get; set; } = string.Empty;

        [Column("FOR_ENDERECO", TypeName = "varchar(300)")]
        [StringLength(300)]
        public string? Endereco { get; set; } = string.Empty;
    }
}
