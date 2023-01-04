using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class ReferenciaComercial
    {
        public int ReferenciaComercialId { get; set; }
        public string Rcdetalle { get; set; }
        public string Rcrepresenta { get; set; }
        public int TipoRefComercialId { get; set; }
        public int FisicaMoralId { get; set; }

        public FisicaMoral FisicaMoral { get; set; }
        public TipoRefComercial TipoRefComercial { get; set; }
    }
}
