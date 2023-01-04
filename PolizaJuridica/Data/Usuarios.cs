using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Calendario = new HashSet<Calendario>();
            ClienteUsuario = new HashSet<ClienteUsuario>();
            CuentasXpagar = new HashSet<CuentasXpagar>();
            EventoUsuarios = new HashSet<EventoUsuarios>();
            FirmasCreadaPor = new HashSet<Firmas>();
            FirmasFirmante = new HashSet<Firmas>();
            FlujoSolicitud = new HashSet<FlujoSolicitud>();
            InverseUsuarioPadre = new HashSet<Usuarios>();
            MovimientoMaestro = new HashSet<MovimientoMaestro>();
            SolicitudAsesor = new HashSet<Solicitud>();
            SolicitudCreador = new HashSet<Solicitud>();
            SolicitudRepresentante = new HashSet<Solicitud>();
            SolucionDetalle = new HashSet<SolucionDetalle>();
            Soluciones = new HashSet<Soluciones>();
            UsuarioPoliza = new HashSet<UsuarioPoliza>();
            UsuariosSolicitud = new HashSet<UsuariosSolicitud>();
            UsuariosSoluciones = new HashSet<UsuariosSoluciones>();
        }

        public int UsuariosId { get; set; }
        public string UsurioEmail { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioApellidoPaterno { get; set; }
        public string UsuarioApellidoMaterno { get; set; }
        public string UsuarioTelefono { get; set; }
        public string UsuarioContrasenia { get; set; }
        public string UsuarioNomCompleto { get; set; }
        public string UsuarioInmobiliaria { get; set; }
        public int AreaId { get; set; }
        public int RepresentacionId { get; set; }
        public string UsuarioCelular { get; set; }
        public bool IsResponsanle { get; set; }
        public int? UsuarioPadreId { get; set; }
        public bool? Activo { get; set; }
        public string ContraEncrypt { get; set; }
        public string Imagen { get; set; }
        public string Titulo { get; set; }

        public Area Area { get; set; }
        public Representacion Representacion { get; set; }
        public Usuarios UsuarioPadre { get; set; }
        public ICollection<Calendario> Calendario { get; set; }
        public ICollection<ClienteUsuario> ClienteUsuario { get; set; }
        public ICollection<CuentasXpagar> CuentasXpagar { get; set; }
        public ICollection<EventoUsuarios> EventoUsuarios { get; set; }
        public ICollection<Firmas> FirmasCreadaPor { get; set; }
        public ICollection<Firmas> FirmasFirmante { get; set; }
        public ICollection<FlujoSolicitud> FlujoSolicitud { get; set; }
        public ICollection<Usuarios> InverseUsuarioPadre { get; set; }
        public ICollection<MovimientoMaestro> MovimientoMaestro { get; set; }
        public ICollection<Solicitud> SolicitudAsesor { get; set; }
        public ICollection<Solicitud> SolicitudCreador { get; set; }
        public ICollection<Solicitud> SolicitudRepresentante { get; set; }
        public ICollection<SolucionDetalle> SolucionDetalle { get; set; }
        public ICollection<Soluciones> Soluciones { get; set; }
        public ICollection<UsuarioPoliza> UsuarioPoliza { get; set; }
        public ICollection<UsuariosSolicitud> UsuariosSolicitud { get; set; }
        public ICollection<UsuariosSoluciones> UsuariosSoluciones { get; set; }
    }
}
