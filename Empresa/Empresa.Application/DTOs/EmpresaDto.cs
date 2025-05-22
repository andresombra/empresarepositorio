using System.ComponentModel.DataAnnotations;

namespace Empresa.Application.DTOs
{
    public class EmpresaDto
    {
        public int EmpresaId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(250, ErrorMessage = "O nome deve ter até 250 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "O e-mail deve ter até 300 caracteres.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de cadastro é obrigatória.")]
        public DateTime DataCadastro { get; set; }

        [StringLength(15, ErrorMessage = "O contato deve ter até 15 caracteres.")]
        public string Contato { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "O endereço deve ter até 300 caracteres.")]
        public string Endereco { get; set; } = string.Empty;
    }
}
