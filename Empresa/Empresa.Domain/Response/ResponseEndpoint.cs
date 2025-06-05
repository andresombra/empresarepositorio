using System.Net;

namespace Empresa.Domain.Response
{
    public class ResponseEndpoint<T>
    {
        public T? Data { get; set; }
        public ResponseMensagem Mensagem { get; set; } = new();
        public HttpStatusCode StatusCode { get; set; }
    }
}
