namespace Empresa.Application.DTOs.Response
{
    public class UsuarioResponseDto
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public string Situacao { get; set; } = string.Empty;
        public int Plano { get; set; }
        public int Adm { get; set; }
    }
}