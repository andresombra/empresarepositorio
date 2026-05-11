using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Application.DTOs.Empresa
{
    public class CreateEmpresaDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(250, ErrorMessage = "O nome deve ter até 250 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "O e-mail deve ter até 300 caracteres.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(15, ErrorMessage = "O contato deve ter até 15 caracteres.")]
        public string Contato { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "O endereço deve ter até 300 caracteres.")]
        public string Endereco { get; set; } = string.Empty;
    }
}
