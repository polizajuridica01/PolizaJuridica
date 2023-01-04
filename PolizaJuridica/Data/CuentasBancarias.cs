using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class CuentasBancarias
    {
        public CuentasBancarias()
        {
            MovimientoMaestro = new HashSet<MovimientoMaestro>();
        }

        public int CuentaId { get; set; }
        public string Nombre { get; set; }
        public string Clabe { get; set; }
        public string Banco { get; set; }
        public sbyte? Estatus { get; set; }
        public decimal? Saldo { get; set; }

        public ICollection<MovimientoMaestro> MovimientoMaestro { get; set; }
    }
}
