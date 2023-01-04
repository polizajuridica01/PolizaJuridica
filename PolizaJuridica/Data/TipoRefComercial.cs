using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class TipoRefComercial
    {
        public TipoRefComercial()
        {
            ReferenciaComercial = new HashSet<ReferenciaComercial>();
        }

        public int TipoRefComercialId { get; set; }
        public string TipoRepresentaRc { get; set; }
        public string TipoDetalleRc { get; set; }

        public ICollection<ReferenciaComercial> ReferenciaComercial { get; set; }
    }
}
