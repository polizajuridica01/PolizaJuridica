using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Eventos
    {
        public Eventos()
        {
            EventoUsuarios = new HashSet<EventoUsuarios>();
        }

        public int EventosId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Lugar { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public bool Activo { get; set; }
        public string ReunionIde { get; set; }
        public string ReunionUr { get; set; }
        public string ReunionPass { get; set; }
        public string Imagen { get; set; }

        public ICollection<EventoUsuarios> EventoUsuarios { get; set; }
    }
}
