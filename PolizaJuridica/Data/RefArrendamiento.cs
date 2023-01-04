using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class RefArrendamiento
    {
        public int RefArrendamientoId { get; set; }
        public string RefArrenNombres { get; set; }
        public string RefArrenApePaterno { get; set; }
        public string RefArrenApeMaterno { get; set; }
        public string RefArrenTelefono { get; set; }
        public string RefArrenDomicilio { get; set; }
        public decimal RefArrenMonto { get; set; }
        public string RefArrenMotivoCambio { get; set; }
        public int FisicaMoralId { get; set; }

        public FisicaMoral FisicaMoral { get; set; }
    }
}
