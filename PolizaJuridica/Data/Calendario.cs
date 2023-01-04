using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Calendario
    {
        public int CalendarioId { get; set; }
        public DateTime CalendarioFechaFirma { get; set; }
        public string CalendarioUbicacion { get; set; }
        public string CalendarioEstatus { get; set; }
        public string CalendarioDescripcion { get; set; }
        public int? UsuariosId { get; set; }
        public int FisicaMoralId { get; set; }

        public FisicaMoral FisicaMoral { get; set; }
        public Usuarios Usuarios { get; set; }
    }
}
