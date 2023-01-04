using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class DetalleInvestigacion
    {
        public DetalleInvestigacion()
        {
            Acuerdos = new HashSet<Acuerdos>();
        }

        public int Id { get; set; }
        public string Entidad { get; set; }
        public string Tribunal { get; set; }
        public string Tipo { get; set; }
        public string Fuente { get; set; }
        public DateTime? Fecha { get; set; }
        public string Fuero { get; set; }
        public string Actor { get; set; }
        public string Juzgado { get; set; }
        public string Demandado { get; set; }
        public string Expediente { get; set; }
        public int? Circuitoid { get; set; }
        public int? Juzgadoid { get; set; }
        public int? InvestigacionId { get; set; }

        public Investigacion Investigacion { get; set; }
        public ICollection<Acuerdos> Acuerdos { get; set; }
    }
}
