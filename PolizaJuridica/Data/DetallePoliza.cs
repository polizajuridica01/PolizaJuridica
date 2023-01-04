using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class DetallePoliza
    {
        public DetallePoliza()
        {
            Movimientos = new HashSet<Movimientos>();
        }

        public int DetallePolizaId { get; set; }
        public decimal? Importe { get; set; }
        public int? PolizaId { get; set; }
        public int CategoriaEsid { get; set; }

        public CategoriaEs CategoriaEs { get; set; }
        public Poliza Poliza { get; set; }
        public ICollection<Movimientos> Movimientos { get; set; }
    }
}
