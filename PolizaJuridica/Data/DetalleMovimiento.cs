using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class DetalleMovimiento
    {
        public DetalleMovimiento()
        {
            Movimientos = new HashSet<Movimientos>();
        }

        public int Id { get; set; }
        public int Tipo { get; set; }
        public decimal? Importe { get; set; }
        public string Referencias { get; set; }
        public string Origen { get; set; }
        public string Observaciones { get; set; }
        public DateTime? Fecha { get; set; }

        public ICollection<Movimientos> Movimientos { get; set; }
    }
}
