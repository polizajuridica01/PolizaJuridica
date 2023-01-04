using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class MovimientoMaestro
    {
        public MovimientoMaestro()
        {
            Movimientos = new HashSet<Movimientos>();
        }

        public int Id { get; set; }
        public int? CuentaId { get; set; }
        public decimal? Entrada { get; set; }
        public decimal? Salida { get; set; }
        public DateTime? Fecha { get; set; }
        public int? UsuarioId { get; set; }

        public CuentasBancarias Cuenta { get; set; }
        public Usuarios Usuario { get; set; }
        public ICollection<Movimientos> Movimientos { get; set; }
    }
}
