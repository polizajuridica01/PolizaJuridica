using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Prohemio
    {
        public Prohemio()
        {
            ProhemioC = new HashSet<ProhemioC>();
        }

        public int Id { get; set; }
        public string Texto { get; set; }

        public ICollection<ProhemioC> ProhemioC { get; set; }
    }
}
