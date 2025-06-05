using System.ComponentModel.DataAnnotations;

namespace Empresa.Application.DTOs
{
    public class UsuarioDto
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Para usuário deve ser informado o ID da empresa a qual ele pertence.")]
        public int UsuarioEmpresaId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        public string UserNome { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string UserLogin { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string UserPass { get; set; } = string.Empty;

    }
}
