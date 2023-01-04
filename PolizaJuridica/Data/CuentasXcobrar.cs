using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class CuentasXcobrar
    {
        public CuentasXcobrar()
        {
            Movimientos = new HashSet<Movimientos>();
        }

        public int Ccid { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? Importe { get; set; }
        public int? Numeral { get; set; }
        public decimal? ImporteIncr { get; set; }
        public int? PolizaId { get; set; }

        public Poliza Poliza { get; set; }
        public ICollection<Movimientos> Movimientos { get; set; }
    }
}
