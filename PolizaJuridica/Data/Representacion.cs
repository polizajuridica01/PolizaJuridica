using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Representacion
    {
        public Representacion()
        {
            Usuarios = new HashSet<Usuarios>();
        }

        public int RepresentacionId { get; set; }
        public string RepresentacionNombre { get; set; }
        public string Direccion { get; set; }
        public string TelefonoOficina { get; set; }
        public string OficinaEmisora { get; set; }
        public decimal? Porcentaje { get; set; }
        public decimal? PorcentajeAsesor { get; set; }
        public decimal? PorcentajeEjecutivo { get; set; }
        public bool? Foranea { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public bool? Activa { get; set; }

        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
