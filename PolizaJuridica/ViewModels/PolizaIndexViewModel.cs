using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.ViewModels
{
    public class PolizaIndexViewModel
    {
        public int PolizaId { get; set; }
        public int SolicitudId { get; set; }

        public string SolicitudTipoPoliza { get; set; }

        public Decimal Costo { get; set; }

        public string RepresentacionNombre { get; set; }

        public string RepresentanteUsuarioNomCompleto { get; set; }

        public DateTime Creacion { get; set; }

        public string Estatus { get; set; }

        public bool IsRenovacion { get; set; }
    }
}
