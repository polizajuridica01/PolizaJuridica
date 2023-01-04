using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Investigacion
    {
        public Investigacion()
        {
            DetalleInvestigacion = new HashSet<DetalleInvestigacion>();
        }

        public int InvestigacionId { get; set; }
        public string Criterio { get; set; }
        public int? FisicaMoralId { get; set; }
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string Nombre { get; set; }
        public string Entidad { get; set; }
        public int? Detalle { get; set; }
        public int? Cantidad { get; set; }

        public FisicaMoral FisicaMoral { get; set; }
        public ICollection<DetalleInvestigacion> DetalleInvestigacion { get; set; }
    }
}
