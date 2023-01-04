using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class CuentasXpagar
    {
        public CuentasXpagar()
        {
            Movimientos = new HashSet<Movimientos>();
        }

        public int Cpid { get; set; }
        public int? CategoriaId { get; set; }
        public decimal? Importe { get; set; }
        public int? PolizaId { get; set; }
        public int? UsuarioId { get; set; }

        public CategoriaEs Categoria { get; set; }
        public Poliza Poliza { get; set; }
        public Usuarios Usuario { get; set; }
        public ICollection<Movimientos> Movimientos { get; set; }
    }
}
