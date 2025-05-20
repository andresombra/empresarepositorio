using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerEmpresa.Domain.Entities
{
    [Table("Empresa")]
    public class Empresa
    {
        [Key]
        [Column("EMP_ID")]
        public int Id { get; set; }

        [Column("EMP_NOME", TypeName = "varchar(250)")]
        [StringLength(250, ErrorMessage = "O nome deve ter até 250 caracteres.")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Column("EMP_EMAIL", TypeName = "varchar(300)")]
        [StringLength(300, ErrorMessage = "O e-mail deve ter até 300 caracteres.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        [Column("EMP_DTCAD")]
        [Required(ErrorMessage = "A data de cadastro é obrigatória.")]
        public DateTime DataCadastro { get; set; }

        [Column("EMP_CONATO", TypeName = "varchar(15)")]
        [StringLength(15, ErrorMessage = "O contato deve ter até 15 caracteres.")]
        public string Contato { get; set; } = string.Empty;

        [Column("EMP_ENDERECO", TypeName = "varchar(300)")]
        [StringLength(300, ErrorMessage = "O endereço deve ter até 300 caracteres.")]
        public string Endereco { get; set; } = string.Empty;
    }
}
