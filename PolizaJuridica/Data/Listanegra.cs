using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Listanegra
    {
        public int ListaNegraId { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string RazonSocial { get; set; }
        public string Rfc { get; set; }
        public string Observaciones { get; set; }
        public sbyte? Estatus { get; set; }
    }
}
