using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class UsuariosSoluciones
    {
        public int UsuariosSolulucionesId { get; set; }
        public int UsuariosId { get; set; }
        public int SolucionesId { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public int? ProcesoSolucionesId { get; set; }

        public ProcesoSoluciones ProcesoSoluciones { get; set; }
        public Soluciones Soluciones { get; set; }
        public Usuarios Usuarios { get; set; }
    }
}
