using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.ViewModels
{
    public class RepresentantesCPCViewModel
    {
        public int RepresentanteId { get; set; }
        public string Nombre { get; set; }
        public int CantidadPoliza { get; set; }
        public decimal CostoVenta { get; set; }

    }
}
