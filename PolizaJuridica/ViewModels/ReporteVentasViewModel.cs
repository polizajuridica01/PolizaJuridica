using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.ViewModels
{
    public class ReporteVentasViewModel
    {
        public int SolicitudId {get; set; }
        public int PolizaId { get; set; }
        public String Representacion { get; set; }
        public String Ejecutivo { get; set; }

        public String Ubicacion { get; set; }
        public String Renovacion { get; set; }
        public Decimal PrecioSinIVA { get; set; }
        public Decimal IVA { get; set; }
        public Decimal PrecioPoliza { get; set; }
        public Decimal IVAConcepto { get; set; }

        public Decimal Descuento { get; set; }
        public Decimal ImporteCobrado { get; set; }
        public Decimal ComisionAsesor { get; set; }
        
        public Decimal IngresoRepresentacion { get; set; }
        public Decimal Anticipo { get; set; }
        public Decimal Regalias { get; set; }        
        public Decimal ComisionEjecutivo { get; set; }
        public Decimal Firma { get; set; }
        public Decimal Translados { get; set; }
        //public Decimal PrecioNeto { get; set; }
        //public Decimal PorcentajeRepresentacion { get; set; }

        public Decimal OtrosOperativos { get; set; }
        public Decimal UtilidadRepresentacion { get; set; }
        public int ReciboId { get; set; }
        public string FechaCobro { get; set; }

        public String Asesor { get; set; }
        public String Abogada { get; set; }
        public String Capturada { get; set; }
        public String Investigada { get; set; }

        public String EstatusPoliza { get; set; }
        public String EstatusSolicitud { get; set; }

    }
}
