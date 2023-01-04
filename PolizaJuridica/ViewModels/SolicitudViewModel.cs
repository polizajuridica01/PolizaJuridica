using Newtonsoft.Json;
using PolizaJuridica.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.ViewModels
{
    public class SolicitudViewModel
    {

            [NotMapped]
            [JsonProperty("title")]
            public string Title { get; set; }

            [NotMapped]
            [JsonProperty("start")]
            public DateTime Start { get; set; }

            [NotMapped]
            [JsonProperty("end")]
            public DateTime End { get; set; }

            public int SolicitudId { get; set; }
            public string SolicitudTipoPoliza { get; set; }
            public string SolicitudSolicitudNombreCompania { get; set; }
            public string SolicitudPersonaSolicita { get; set; }
            public string SolicitudTelefono { get; set; }
            public string SolicitudCelular { get; set; }
            public string SolicitudEmail { get; set; }
            public DateTime SolicitudFechaSolicitud { get; set; }
            public DateTime SolicitudFechaFirma { get; set; }
            public DateTime SolicitudHoraFirma { get; set; }
            public string SolicitudLugarFirma { get; set; }
            public bool SolicitudAdmiInmueble { get; set; }
            public bool SolicitudEsAdminInmueble { get; set; }
            public string SolicitudRecibodePago { get; set; }
            public string SolicitudNombreProp { get; set; }
            public string SolicitudApePaternoProp { get; set; }
            public string SolicitudApeMaternoProp { get; set; }
            public string SolicitudNacionalidad { get; set; }
            public string SolicitudRazonSocial { get; set; }
            public string SolicitudRfc { get; set; }
            public string SolicitudRepresentanteLegal { get; set; }
            public string SolicitudApePaternoLegal { get; set; }
            public string SolicitudApeMaternoLegal { get; set; }
            public string SolicitudDomicilioProp { get; set; }
            public string SolicitudTelefonoProp { get; set; }
            public string SolicitudCelularProp { get; set; }
            public string SolicitudEmailProp { get; set; }
            public string SolicitudTipoDeposito { get; set; }
            public string SolicitudNombreCuenta { get; set; }
            public string SolicitudBanco { get; set; }
            public string SolicitudCuenta { get; set; }
            public string SolicitudClabe { get; set; }
            public string SolicitudUbicacionArrendado { get; set; }
            public bool SolicitudTelefonoInmueble { get; set; }
            public string SolicitudNumero { get; set; }
            public decimal SolicitudImporteMensual { get; set; }
            public decimal SolicitudCuotaMant { get; set; }
            public bool SolicitudIncluidaRenta { get; set; }
            public decimal SolicitudDepositoGarantia { get; set; }
            public DateTime SolicitudVigenciaContratoI { get; set; }
            public DateTime SolicitudVigenciaContratoF { get; set; }
            public bool SolicitudPagare { get; set; }
            public string SolicitudDestinoArrendamien { get; set; }
            public string SolicitudObservaciones { get; set; }
            public string SolicitudEstatus { get; set; }
            public int SolicitudTipoRegimen { get; set; }
            public int CentroCostosId { get; set; }
            public int EstadosId { get; set; }
            public bool SolicitudInmuebleGaran { get; set; }
            public bool SolicitudFiador { get; set; }
            public bool SolicitudCartaEntrega { get; set; }
            public string TipoArrendatario { get; set; }
            public string ArrendatarioNombre { get; set; }
            public string ArrendatarioApePat { get; set; }
            public string ArrendatarioApeMat { get; set; }
            public string ArrendatarioTelefono { get; set; }
            public string ArrendatarioCorreo { get; set; }
            public int TipoInmobiliarioId { get; set; }
            public string TextoVigenciaFechaIf { get; set; }
            public string TextoImporterenta { get; set; }
            public string TextoDeposito { get; set; }
            public string TextoCuotaGarantia { get; set; }
            public string TextoVigenciaFechaI { get; set; }
            public string TextoVigenciaFechaF { get; set; }
            public string TextoCostoPoliza { get; set; }
            public string TextoDiaPosteriorVencimiento { get; set; }
            public string DiaPago { get; set; }
            public int Representanteid { get; set; }
            public string RepresentanteNombre { get; set; }
            public int Asesorid { get; set; }
            public string AsesorNombre { get; set; }
            public int Creadorid { get; set; }
            public string CreadorNombre { get; set; }
            public string AlcaldiaMunicipioActual { get; set; }
            public string AlcaldiaMunicipioArrendar { get; set; }
            public string CodigoPostalActual { get; set; }
            public string CodigoPostalArrendar { get; set; }
            public string ColoniaActual { get; set; }
            public string ColoniaArrendar { get; set; }
            public string EstadoActual { get; set; }
            public string EstadoArrendar { get; set; }
        

        }
    }
