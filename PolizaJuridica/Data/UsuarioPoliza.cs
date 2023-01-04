using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class UsuarioPoliza
    {
        public int UsuarioPolizaId { get; set; }
        public int UsuariosId { get; set; }
        public int PolizaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public int TipoProcesoPoId { get; set; }

        public Poliza Poliza { get; set; }
        public TipoProcesoPo TipoProcesoPo { get; set; }
        public Usuarios Usuarios { get; set; }
    }
}
