using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class PolizaEstatus
    {
        public PolizaEstatus()
        {
            Poliza = new HashSet<Poliza>();
        }

        public int PolizaEstatusId { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }

        public ICollection<Poliza> Poliza { get; set; }
    }
}
