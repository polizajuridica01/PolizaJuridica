using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class TipoDocumento
    {
        public TipoDocumento()
        {
            Documentos = new HashSet<Documentos>();
        }

        public int TipoDocumentoId { get; set; }
        public string TipoDocumentoDesc { get; set; }

        public ICollection<Documentos> Documentos { get; set; }
    }
}
