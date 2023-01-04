using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class EventoUsuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string ApellidoPaterno { get; set; }
        public int? RepresentanteId { get; set; }
        public int? EstadoId { get; set; }
        public int? EventosId { get; set; }

        public Estados Estado { get; set; }
        public Eventos Eventos { get; set; }
        public Usuarios Representante { get; set; }
    }
}
