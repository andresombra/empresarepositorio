using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Domain.Response
{
    public class ResponseMensagem
    {
        public int ResultCode { get; set; }
        public List<string> Validacoes { get; set; } = new();
    }
}
