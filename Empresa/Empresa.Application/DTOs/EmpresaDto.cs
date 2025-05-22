using System.ComponentModel.DataAnnotations;

namespace Empresa.Application.DTOs
{
    public class EmpresaDto
    {
        public int EmpresaId { get; set; }

        [Required(ErrorMessage = "O nome � obrigat�rio.")]
        [StringLength(250, ErrorMessage = "O nome deve ter at� 250 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "O e-mail deve ter at� 300 caracteres.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail v�lido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de cadastro � obrigat�ria.")]
        public DateTime DataCadastro { get; set; }

        [StringLength(15, ErrorMessage = "O contato deve ter at� 15 caracteres.")]
        public string Contato { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "O endere�o deve ter at� 300 caracteres.")]
        public string Endereco { get; set; } = string.Empty;
    }
}
