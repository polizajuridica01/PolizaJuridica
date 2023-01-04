using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class UsuariosSolicitud
    {
        public int UsuariosSolicitudId { get; set; }
        public int UsuariosId { get; set; }
        public int SolicitudId { get; set; }
        public DateTime Fecha { get; set; }
        public string Proceso { get; set; }
        public string Observacion { get; set; }
        public int? TipoProcesoId { get; set; }

        public Solicitud Solicitud { get; set; }
        public TipoProceso TipoProceso { get; set; }
        public Usuarios Usuarios { get; set; }
    }
}
