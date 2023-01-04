using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Arrendatario
    {
        public int ArrendatarioId { get; set; }
        public string Nacionalidad { get; set; }
        public string CondMigratoria { get; set; }
        public string EstadoCivil { get; set; }
        public string ConvenioEc { get; set; }
        public string Domicilio { get; set; }
        public string Colonia { get; set; }
        public string DelegacionMunicipio { get; set; }
        public string Estado { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Profesion { get; set; }
        public decimal IngresoMensual { get; set; }
        public string Trabajo { get; set; }
        public string Antiguedad { get; set; }
        public string Puesto { get; set; }
        public string TelefonoTrabajo { get; set; }
        public string Horario { get; set; }
        public string DomicilioTrabajo { get; set; }
        public string ColoniaTrabajo { get; set; }
        public string DelegMuniTrabajo { get; set; }
        public string EstadoTrabajo { get; set; }
        public string GiroTrabajo { get; set; }
        public string WebTrabajo { get; set; }
        public string JefeTrabajo { get; set; }
        public string PuestoJefe { get; set; }
        public string EmailJefe { get; set; }
        public bool Factura { get; set; }
        public string ActaConstitutiva { get; set; }
        public string PoderRepresentanteNo { get; set; }
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
        public int FisicaMoralId { get; set; }
        public string Rfc { get; set; }
        public string RazonSocial { get; set; }
        public string CodigoPostal { get; set; }
        public string CodigoPostalTrabajo { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public int? TipoRegimenFiscal { get; set; }

        public FisicaMoral FisicaMoral { get; set; }
    }
}
