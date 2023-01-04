using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class ReferenciaPersonal
    {
        public int ReferenciaPersonalId { get; set; }
        public string Rpnombres { get; set; }
        public string RpapePaterno { get; set; }
        public string RpApeMaterno { get; set; }
        public string Rptelefono { get; set; }
        public int TipoRefPersonalId { get; set; }
        public int FisicaMoralId { get; set; }

        public FisicaMoral FisicaMoral { get; set; }
        public TipoRefPersonal TipoRefPersonal { get; set; }
    }
}
