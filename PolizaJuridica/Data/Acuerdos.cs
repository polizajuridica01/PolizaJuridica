using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Acuerdos
    {
        public int Id { get; set; }
        public string Acuerdo { get; set; }
        public string Juicio { get; set; }
        public DateTime? Fecha { get; set; }
        public int? DetalleInvestigacionId { get; set; }

        public DetalleInvestigacion DetalleInvestigacion { get; set; }
    }
}
