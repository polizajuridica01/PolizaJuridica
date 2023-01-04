using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Firmas
    {
        public int Id { get; set; }
        public string Lugar { get; set; }
        public DateTime FechaFirma { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? PolizaId { get; set; }
        public int? CreadaPorId { get; set; }
        public int? FirmanteId { get; set; }

        public Usuarios CreadaPor { get; set; }
        public Usuarios Firmante { get; set; }
        public Poliza Poliza { get; set; }
    }
}
