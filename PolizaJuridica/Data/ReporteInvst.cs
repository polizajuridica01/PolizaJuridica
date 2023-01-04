using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class ReporteInvst
    {
        public int Id { get; set; }
        public string Texto1 { get; set; }
        public string Texto2 { get; set; }
        public int FisicaMoralId { get; set; }

        public FisicaMoral FisicaMoral { get; set; }
    }
}
