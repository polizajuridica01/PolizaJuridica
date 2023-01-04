using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class KeywordStructura
    {
        public int Id { get; set; }
        public string Estructura { get; set; }
        public string Comentarios { get; set; }
        public string Keyword { get; set; }
        public int? TipoInmobiliarioId { get; set; }
        public int? Orden { get; set; }

        public TipoInmobiliario TipoInmobiliario { get; set; }
    }
}
