using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Documentos
    {
        public int DocumentosId { get; set; }
        public string DocumentosImagen { get; set; }
        public string DocumentoDesc { get; set; }
        public int TipoDocumentoId { get; set; }
        public int FisicaMoralId { get; set; }

        public FisicaMoral FisicaMoral { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
    }
}
