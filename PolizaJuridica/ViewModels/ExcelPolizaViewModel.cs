using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.ViewModels
{
    public class ExcelPolizaViewModel
    {
        public int Polizaid { get; set; }
        public string Asesor { get; set; }
        public string Direccion { get; set; }
        public decimal Costo { get; set; }
        public string Estatus { get; set; }
    }
}
