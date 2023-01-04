using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Movimientos
    {
        public Movimientos()
        {
            InverseIdPadreNavigation = new HashSet<Movimientos>();
        }

        public int Id { get; set; }
        public int? DetallePolizaId { get; set; }
        public int? Mmid { get; set; }
        public int? IdPadre { get; set; }
        public int? Ccid { get; set; }
        public int? Cpid { get; set; }
        public int? Dmid { get; set; }

        public CuentasXcobrar Cc { get; set; }
        public CuentasXpagar Cp { get; set; }
        public DetallePoliza DetallePoliza { get; set; }
        public DetalleMovimiento Dm { get; set; }
        public Movimientos IdPadreNavigation { get; set; }
        public MovimientoMaestro Mm { get; set; }
        public ICollection<Movimientos> InverseIdPadreNavigation { get; set; }
    }
}
