using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class TipoParentesco
    {
        public TipoParentesco()
        {
            PersonasOcupanInm = new HashSet<PersonasOcupanInm>();
        }

        public int TipoParentescoId { get; set; }
        public string TipoParentescoDesc { get; set; }

        public ICollection<PersonasOcupanInm> PersonasOcupanInm { get; set; }
    }
}
