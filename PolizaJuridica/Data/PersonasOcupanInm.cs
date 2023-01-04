using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class PersonasOcupanInm
    {
        public int PersonasOcupanInmId { get; set; }
        public string PersonasOcupanInmNombre { get; set; }
        public int TipoParentescoId { get; set; }
        public int FisicaMoralId { get; set; }

        public FisicaMoral FisicaMoral { get; set; }
        public TipoParentesco TipoParentesco { get; set; }
    }
}
