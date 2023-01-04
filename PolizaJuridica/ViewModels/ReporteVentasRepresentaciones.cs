using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.ViewModels
{
    public class ReporteVentasRepresentaciones
    {

        public int SolicitudId { get; set; }
        public int PolizaId { get; set; }
        public String Representacion { get; set; }
        public String Ejecutivo { get; set; }

        public String Ubicacion { get; set; }
        public String Renovacion { get; set; }

        public Decimal PrecioPoliza { get; set; }

        public Decimal ImporteCobrado { get; set; }
        public Decimal ComisionAsesor { get; set; }

        public Decimal ComisionEjecutivo { get; set; }
        public Decimal UtilidadRepresentacion { get; set; }
        public int ReciboId { get; set; }
        public string FechaCobro { get; set; }

        public String Asesor { get; set; }
        public String Abogada { get; set; }
        public String EstatusPoliza { get; set; }
        public String EstatusSolicitud { get; set; }

        public DateTime FechaPolizaEmision { get; set; }
        public String Arrendador { get; set; }
        public String Arrendatario { get; set; }

        public DateTime VigenciaInicio { get; set; }
        public DateTime VigenciaFin { get; set; }
    }
}
