using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class ProhemioC
    {
        public int Id { get; set; }
        public int? Tipo { get; set; }
        public int? PersonalidadJuridica { get; set; }
        public int? Cantidad { get; set; }
        public int? ProhemioId { get; set; }

        public Prohemio Prohemio { get; set; }
    }
}
