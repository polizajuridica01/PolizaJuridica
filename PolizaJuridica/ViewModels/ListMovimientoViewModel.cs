using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.ViewModels
{

        public class Dp
        {
            public string id { get; set; }
            public string polizaid { get; set; }
            public string descripcion { get; set; }
            public string flujo { get; set; }
            public string importe { get; set; }
        }

        public class Cxp
        {
            public string id { get; set; }
            public string polizaid { get; set; }
            public string descripcion { get; set; }
            public string flujo { get; set; }
            public string importe { get; set; }
        }

        public class Cxc
        {
            public string id { get; set; }
            public string polizaid { get; set; }
            public string importe { get; set; }
            public string numeral { get; set; }
            public string importeIncr { get; set; }
        }

        public class Tran
        {
            public string id { get; set; }
            public string tipo { get; set; }
            public string importe { get; set; }
            public string referencias { get; set; }
            public string origen { get; set; }
        }

        public class ListMovimientoViewModel
        {
            public List<Dp> dp { get; set; }
            public List<Cxp> cxp { get; set; }
            public List<Cxc> cxc { get; set; }
            public List<Tran> trans { get; set; }
            public int Cuenta { get; set; }
        }
}
