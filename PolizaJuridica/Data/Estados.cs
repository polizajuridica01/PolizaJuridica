using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Estados
    {
        public Estados()
        {
            DocumentoPlantilla = new HashSet<DocumentoPlantilla>();
            EventoUsuarios = new HashSet<EventoUsuarios>();
            Solicitud = new HashSet<Solicitud>();
            Soluciones = new HashSet<Soluciones>();
        }

        public int EstadosId { get; set; }
        public string EstadoNombre { get; set; }
        public string CodigoPostal { get; set; }

        public ICollection<DocumentoPlantilla> DocumentoPlantilla { get; set; }
        public ICollection<EventoUsuarios> EventoUsuarios { get; set; }
        public ICollection<Solicitud> Solicitud { get; set; }
        public ICollection<Soluciones> Soluciones { get; set; }
    }
}
