using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Application.DTOs
{
    public class LoginRequestDto
    {
        public string UserLogin { get; set; } = string.Empty;
        public string UserPass { get; set; } = string.Empty;
    }
}
