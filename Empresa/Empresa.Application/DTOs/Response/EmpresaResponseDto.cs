namespace Empresa.Application.DTOs.Response
{
    public class EmpresaResponseDto
    {
        public int EmpresaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public string Contato { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
    }
}
