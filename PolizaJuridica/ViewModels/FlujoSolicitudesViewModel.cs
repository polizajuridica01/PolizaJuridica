using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace PolizaJuridica.ViewModels
{
    public class FlujoSolicitudesViewModel
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }

        public string TipoPoliza { get; set;  }
        public int SolicitudId { get; set; }
        public string Representante { get; set;  }

        public string Direccion { get; set; }

        public string Estatus { get; set; }
    }
}
