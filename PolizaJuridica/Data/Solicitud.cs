using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Solicitud
    {
        public Solicitud()
        {
            FisicaMoral = new HashSet<FisicaMoral>();
            FlujoSolicitud = new HashSet<FlujoSolicitud>();
            UsuariosSolicitud = new HashSet<UsuariosSolicitud>();
        }

        public int SolicitudId { get; set; }
        public string SolicitudTipoPoliza { get; set; }
        public string Inmobiliaria { get; set; }
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
        public int? CentroCostosId { get; set; }
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
        public int? Representanteid { get; set; }
        public int? Asesorid { get; set; }
        public int Creadorid { get; set; }
        public string AlcaldiaMunicipioActual { get; set; }
        public string AlcaldiaMunicipioArrendar { get; set; }
        public string CodigoPostalActual { get; set; }
        public string CodigoPostalArrendar { get; set; }
        public string ColoniaActual { get; set; }
        public string ColoniaArrendar { get; set; }
        public string EstadoActual { get; set; }
        public string Jurisdiccion { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Nombre1 { get; set; }
        public string ApePat1 { get; set; }
        public string ApeMat1 { get; set; }
        public string TipoIdent1 { get; set; }
        public string NumIdent1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApePat2 { get; set; }
        public string ApeMat2 { get; set; }
        public string TipoIdent2 { get; set; }
        public string NumIdent2 { get; set; }
        public string Nombre3 { get; set; }
        public string ApePat3 { get; set; }
        public string ApeMat3 { get; set; }
        public string TipoIdent3 { get; set; }
        public string NumIdent3 { get; set; }
        public string NumRppcons { get; set; }
        public string EscrituraNumero { get; set; }
        public string Licenciado { get; set; }
        public string NumeroNotaria { get; set; }
        public DateTime? FechaRppcons { get; set; }
        public string NumEscPoder { get; set; }
        public string TitularNotaPoder { get; set; }
        public string NumNotaria { get; set; }
        public DateTime? FechaEmitePoder { get; set; }
        public DateTime? FechaConstitutiva { get; set; }
        public bool IsRenovacion { get; set; }
        public decimal? CostoPoliza { get; set; }
        public bool EsAmueblado { get; set; }

        public Usuarios Asesor { get; set; }
        public CentroCostos CentroCostos { get; set; }
        public Usuarios Creador { get; set; }
        public Estados Estados { get; set; }
        public Usuarios Representante { get; set; }
        public TipoInmobiliario TipoInmobiliario { get; set; }
        public ICollection<FisicaMoral> FisicaMoral { get; set; }
        public ICollection<FlujoSolicitud> FlujoSolicitud { get; set; }
        public ICollection<UsuariosSolicitud> UsuariosSolicitud { get; set; }
    }
}
