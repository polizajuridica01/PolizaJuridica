using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.ViewModels
{
    public class FirmaViewModel
    {
        public int Id { get; set; }
        public int PolizaId { get; set; }
        public int SolicitudId { get; set; }
        public string direccion { get; set; }
        public string creadorpor {get; set;}
        public string lugar { get; set; }
        public DateTime fechafirma { get; set; }
        public int FirmanteID { get; set; }
    }
}
