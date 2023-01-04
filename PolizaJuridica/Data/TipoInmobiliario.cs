using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class TipoInmobiliario
    {
        public TipoInmobiliario()
        {
            FiadorF = new HashSet<FiadorF>();
            KeywordStructura = new HashSet<KeywordStructura>();
            Solicitud = new HashSet<Solicitud>();
        }

        public int TipoInmobiliarioId { get; set; }
        public string TipoInmobiliarioDesc { get; set; }
        public string Clausula { get; set; }

        public ICollection<FiadorF> FiadorF { get; set; }
        public ICollection<KeywordStructura> KeywordStructura { get; set; }
        public ICollection<Solicitud> Solicitud { get; set; }
    }
}
