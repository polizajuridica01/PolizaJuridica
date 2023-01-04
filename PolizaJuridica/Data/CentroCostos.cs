using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class CentroCostos
    {
        public CentroCostos()
        {
            Solicitud = new HashSet<Solicitud>();
        }

        public int CentroCostosId { get; set; }
        public string CentroCostosTipo { get; set; }
        public decimal CentroCostosMonto { get; set; }
        public int CentroCostosRentaInicial { get; set; }
        public int CentroCostosRentaFinal { get; set; }

        public ICollection<Solicitud> Solicitud { get; set; }
    }
}
