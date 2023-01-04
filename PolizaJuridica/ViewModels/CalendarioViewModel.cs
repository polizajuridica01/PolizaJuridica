using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.ViewModels
{
    public class CalendarioViewModel
    {
        // propiedades extendidas FullCalendar

        public int SolicitudId { get; set; }
        public string SolicitudTipoPoliza { get; set; }

        public int SolicitudTipoRegimen { get; set; }

        public string SolicitudNombreProp { get; set; }
        public string SolicitudApePaternoProp { get; set; }
        public string SolicitudApeMaternoProp { get; set; }

        // DATOS REPRESENTANTE LEGAL
        public string SolicitudRepresentanteLegal { get; set; }
        public string SolicitudApePaternoLegal { get; set; }
        public string SolicitudApeMaternoLegal { get; set; }

        // DATOS ARRENDATARIO
        public string ArrendatarioNombre { get; set; }
        public string ArrendatarioApePat { get; set; }
        public string ArrendatarioApeMat { get; set; }

        public string SolicitudLugarFirma { get; set; }
        public string AsesorNombre { get; set; }
        public string RepresentanteNombre { get; set; }


        // propiedades generales FullCalendar  no mover
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("start")]
        public DateTimeOffset Start { get; set; }
        //public DateTime Start { get; set; }

        [JsonProperty("end")]
        public DateTimeOffset End { get; set; }
        // public DateTime End { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("borderColor")]
        public string BorderColor { get; set; }

        [JsonProperty("textColor")]
        public string TextColor { get; set; }

        [JsonProperty("backgroundColor")]
        public string BackgroundColor { get; set; }

        [JsonProperty("className")]
        public ICollection<string> ClassName { get; set; }

    }
}
