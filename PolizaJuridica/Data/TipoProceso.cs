using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class TipoProceso
    {
        public TipoProceso()
        {
            UsuariosSolicitud = new HashSet<UsuariosSolicitud>();
        }

        public int TipoProcesoId { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }

        public ICollection<UsuariosSolicitud> UsuariosSolicitud { get; set; }
    }
}
