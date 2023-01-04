using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.ViewModels
{
    public class BuholegalViewModel
    {

        public class BuhoLegalResponse
        {
            public Query query { get; set; }
            public Results results { get; set; }
        }

        public class Query
        {
            public string entidad { get; set; }
            public string parte { get; set; }
            public string fecha_final { get; set; }
            public string fecha_inicial { get; set; }
            public string criterio { get; set; }
            public int numero_resultados { get; set; }
            public string nombre { get; set; }
            public string detalle { get; set; }
        }


        public class Results : Dictionary<string, EntidadFederativa[]>
        {

        }


        public class EntidadFederativa
        {
            public string entidad { get; set; }
            public string tribunal { get; set; }
            public string tipo { get; set; }
            public string fuente { get; set; }
            public string fecha { get; set; }
            public string fuero { get; set; }
            public string actor { get; set; }
            public string juzgado { get; set; }
            public string demandado { get; set; }
            public string expediente { get; set; }
            public int circuito_id { get; set; }
            public int juzgado_id { get; set; }

            public Acuerdos[] acuerdos { get; set; }
        }

        public class Acuerdos
        {
            public string acuerdo { get; set; }
            public string juicio { get; set; }
            public string fecha { get; set; }
        }
    }
}
