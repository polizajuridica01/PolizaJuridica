using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class FisicaMoral
    {
        public FisicaMoral()
        {
            Arrendatario = new HashSet<Arrendatario>();
            Calendario = new HashSet<Calendario>();
            Documentos = new HashSet<Documentos>();
            FiadorF = new HashSet<FiadorF>();
            FiadorM = new HashSet<FiadorM>();
            Investigacion = new HashSet<Investigacion>();
            PersonasOcupanInm = new HashSet<PersonasOcupanInm>();
            Poliza = new HashSet<Poliza>();
            RefArrendamiento = new HashSet<RefArrendamiento>();
            ReferenciaComercial = new HashSet<ReferenciaComercial>();
            ReferenciaPersonal = new HashSet<ReferenciaPersonal>();
            ReporteInvst = new HashSet<ReporteInvst>();
        }

        public int FisicaMoralId { get; set; }
        public string SfisicaNacionallidad { get; set; }
        public string SfisicaCondMigratoria { get; set; }
        public string SfisicaEstadoCivil { get; set; }
        public string SfisicaConvenioEc { get; set; }
        public string SfisicaDomicilio { get; set; }
        public string SfisicaColonia { get; set; }
        public string SfisicaDelegacionMunicipio { get; set; }
        public string SfisicaEstado { get; set; }
        public string SfisicaTelefono { get; set; }
        public string SfisicaCelular { get; set; }
        public string SfisicaEmail { get; set; }
        public string SfisicaProfesion { get; set; }
        public decimal SfisicaIngresoMensual { get; set; }
        public string SfisicaTrabajo { get; set; }
        public string SfisicaAntiguedad { get; set; }
        public string SfisicaPuesto { get; set; }
        public string SfisicaTelefonoTrabajo { get; set; }
        public string SfisicaHorario { get; set; }
        public string SfisicaDomicilioTrabajo { get; set; }
        public string SfisicaColoniaTrabajo { get; set; }
        public string SfisicaDelegMuniTrabajo { get; set; }
        public string SfisicaEstadoTrabajo { get; set; }
        public string SfisicaGiroTrabajo { get; set; }
        public string SfisicaWebTrabajo { get; set; }
        public string SfisicaJefeTrabajo { get; set; }
        public string SfisicaPuestoJefe { get; set; }
        public string SfisicaEmailJefe { get; set; }
        public bool SfisicaFactura { get; set; }
        public string DomicilioRepresentanteLegal { get; set; }
        public string ColoniaRl { get; set; }
        public string DeleMuni { get; set; }
        public string TelefonoRl { get; set; }
        public string EstadoRl { get; set; }
        public string EmailRl { get; set; }
        public string HorarioRl { get; set; }
        public decimal IngresoMensualRl { get; set; }
        public string SindicadoRl { get; set; }
        public bool RequiereFacturaRl { get; set; }
        public bool AfianzadoRl { get; set; }
        public string AfianzadoraRl { get; set; }
        public int SolicitudId { get; set; }
        public string SfisicaNombre { get; set; }
        public string SfisicaApePat { get; set; }
        public string SfisicaApeMat { get; set; }
        public string SfisicaRfc { get; set; }
        public string SfisicaRazonSocial { get; set; }
        public string SfisicaCodigoPostal { get; set; }
        public string SfisicaCodigoPostalTrabajo { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
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

        public Solicitud Solicitud { get; set; }
        public ICollection<Arrendatario> Arrendatario { get; set; }
        public ICollection<Calendario> Calendario { get; set; }
        public ICollection<Documentos> Documentos { get; set; }
        public ICollection<FiadorF> FiadorF { get; set; }
        public ICollection<FiadorM> FiadorM { get; set; }
        public ICollection<Investigacion> Investigacion { get; set; }
        public ICollection<PersonasOcupanInm> PersonasOcupanInm { get; set; }
        public ICollection<Poliza> Poliza { get; set; }
        public ICollection<RefArrendamiento> RefArrendamiento { get; set; }
        public ICollection<ReferenciaComercial> ReferenciaComercial { get; set; }
        public ICollection<ReferenciaPersonal> ReferenciaPersonal { get; set; }
        public ICollection<ReporteInvst> ReporteInvst { get; set; }
    }
}
