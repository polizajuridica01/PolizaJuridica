using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.ViewModels
{
    public class SolicitudIndexViewModel
    {
        public int SolicitudId { get; set; }
        public string SolicitudTipoPoliza { get; set; }
        public string SolicitudNombreProp { get; set; }
        public string SolicitudApePaternoProp { get; set; }
        public string SolicitudApeMaternoProp { get; set; }
        public string SolicitudRazonSocial { get; set; }

        public string SolicitudUbicacionArrendado { get; set; }

        public DateTime SolicitudFechaSolicitud { get; set; }

        public string CreadorNombreCompleto { get; set; }
        public string RepresentanteNombreCompleto { get; set; }
        public string AsesorNombreCompleto { get; set; }

        public string SolicitudEstatus { get; set; }
        public bool IsRenovacion { get; set; }

        public int? Representanteid { get; set; }
        public int? Asesorid { get; set; }
        public int Creadorid { get; set; }

        public int? FisicaMoralId { get; set; }
        public int SolicitudTipoRegimen { get; set; }

        public int? RepresentacionId { get; set; }
    }

}
