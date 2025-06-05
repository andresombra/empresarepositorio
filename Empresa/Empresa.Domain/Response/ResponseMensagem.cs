namespace Empresa.Domain.Response
{
    public class ResponseMensagem
    {
        public int ResultCode { get; set; }
        public List<string> Validacoes { get; set; } = new();
    }
}
