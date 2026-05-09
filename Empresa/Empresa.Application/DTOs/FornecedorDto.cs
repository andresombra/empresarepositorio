using System.ComponentModel.DataAnnotations;

namespace Empresa.Application.DTOs
{
    public class FornecedorDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(250, ErrorMessage = "O nome deve ter até 250 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "O e-mail deve ter até 300 caracteres.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string? Email { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "O CNPJ deve ter até 20 caracteres.")]
        public string? Cnpj { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        [StringLength(50, ErrorMessage = "O contato deve ter até 50 caracteres.")]
        public string? Contato { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "O endereço deve ter até 300 caracteres.")]
        public string? Endereco { get; set; } = string.Empty;
    }
}
