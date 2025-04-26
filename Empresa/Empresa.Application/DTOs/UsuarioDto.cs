using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Application.DTOs
{
    public class UsuarioDto
    {
        public Guid EmpresaId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string UserNome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string UserLogin { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string UserPass { get; set; } = string.Empty;
    }
}
