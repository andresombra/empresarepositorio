using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Domain.Entities
{
    public class Usuario
    {
        public Guid EmpresaId { get; set; }
        public Guid UserId { get; set; }
        public string UserNome { get; set; } = string.Empty;
        public string UserLogin { get; set; } = string.Empty;
        public string UserPass { get; set; } = string.Empty;
        public bool UserStatus { get; set; }
        public DateTime UserDateCreate { get; set; }
        public DateTime? UserDateUpdate { get; set; }
        public Guid? UserIdLog { get; set; }
    }
}
