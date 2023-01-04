using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class FlujoSolicitud
    {
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public int SolicitudId { get; set; }

        public Usuarios Persona { get; set; }
        public Solicitud Solicitud { get; set; }
    }
}
