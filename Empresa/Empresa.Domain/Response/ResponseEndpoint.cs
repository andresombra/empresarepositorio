using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Domain.Response
{
    public class ResponseEndpoint<T>
    {
        public T? Data { get; set; }
        public ResponseMensagem Mensagem { get; set; } = new();
        public HttpStatusCode StatusCode { get; set; }
    }
}
