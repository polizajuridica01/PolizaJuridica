using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.ViewModels
{
    public class UserViewModel
    {
        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Representacón { get; set; }
        public int RepresentacionId { get; set;  }
        public int AreaId { get; set;}
        public string AreaDescripcion { get; set; }
        public string Dashboard { get; set; }

    }
}
