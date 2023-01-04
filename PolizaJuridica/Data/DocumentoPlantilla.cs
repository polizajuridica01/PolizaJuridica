using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class DocumentoPlantilla
    {
        public int DocumentoPlantillaId { get; set; }
        public string DocumentoPlantillaTipo { get; set; }
        public string DocumentoPlantillaNombre { get; set; }
        public string DocumentoPlantillaXml { get; set; }
        public string DocumentoOriginal { get; set; }
        public bool DocumentoPagare { get; set; }
        public bool DocumentoFiador { get; set; }
        public bool DocumentoInmueble { get; set; }
        public bool DocumentoCarta { get; set; }
        public int? EstadosId { get; set; }

        public Estados Estados { get; set; }
    }
}
