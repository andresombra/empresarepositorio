using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerEmpresa.Domain.Entities
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("USU_ID")]
        public int Id { get; set; }

        

        [Column("USU_EMAIL", TypeName = "longtext")]
        public string Email { get; set; } = string.Empty;

        [Column("USU_SENHA", TypeName = "longtext")]
        public string Senha { get; set; } = string.Empty;

        [Column("USU_DATA", TypeName = "longtext")]
        public string Data { get; set; } = string.Empty;

        [Column("USU_SITUACAO", TypeName = "longtext")]
        public string Situacao { get; set; } = string.Empty;

        [Column("USU_PLANO")]
        public int Plano { get; set; }

        [Column("USU_ADM")]
        public int Adm { get; set; }

        // FK
        [Column("USU_EMPRESA_ID")]
        public virtual int EmpresaId { get; set; }

        // Navegação
        public virtual Empresa? Empresa { get; set; }
    }
}
