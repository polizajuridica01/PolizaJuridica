using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class TipoRefPersonal
    {
        public TipoRefPersonal()
        {
            ReferenciaPersonal = new HashSet<ReferenciaPersonal>();
        }

        public int TipoRefPersonalId { get; set; }
        public string TipoRefPersonalDesc { get; set; }

        public ICollection<ReferenciaPersonal> ReferenciaPersonal { get; set; }
    }
}
