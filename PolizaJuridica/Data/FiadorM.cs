using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class FiadorM
    {
        public int FiadorMid { get; set; }
        public string FiaddorMrazonSocial { get; set; }
        public string FiadorMrfc { get; set; }
        public string FiadorMtelefono { get; set; }
        public string FiadorMgiro { get; set; }
        public string FiadorMweb { get; set; }
        public string FiadorMnombresRlegal { get; set; }
        public string FiadorMapePaternoRlegal { get; set; }
        public string FiadorMapeMaternoRlegal { get; set; }
        public string FiadorMpuestoRlegal { get; set; }
        public string FiadorMnacionalidadRlegal { get; set; }
        public string FiadorMmactaNo { get; set; }
        public string FiadorMpoderRepNo { get; set; }
        public string FiadorMtelefonoRlegat { get; set; }
        public string FiadorMcelularRlegal { get; set; }
        public string FiadorMemailRlegal { get; set; }
        public string FiadorMdomicilioGarantia { get; set; }
        public string FiadorMcoliniaGarantia { get; set; }
        public string FiadorMdelegacionGarantia { get; set; }
        public int FisicaMoralId { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string FiadorMnombreEscrituraGarantia { get; set; }
        public string FiadorMlicenciadoNotaria { get; set; }
        public string FiadorMnumNotaria { get; set; }
        public string FiadorMdistritoJudicial { get; set; }
        public string FiadorMnumPartida { get; set; }
        public string FiadorMvolumen { get; set; }
        public string FiadorMlibro { get; set; }
        public string FiadorMseccion { get; set; }
        public DateTime? FiadorMfechaPartida { get; set; }
        public string FiadorMcoloniaGarantia { get; set; }
        public string FiadorMestadoGarantia { get; set; }
        public string FiadorMcpgarantia { get; set; }
        public string FiadorMnumCons { get; set; }
        public DateTime? FiadorMfechaCons { get; set; }
        public string FiadorMlicenciadoCons { get; set; }
        public string FiadorMnumNotaCons { get; set; }
        public string FiadorMnumRpp { get; set; }
        public DateTime? FiadorMfechaRpp { get; set; }
        public string FiadorMnumEscPoder { get; set; }
        public DateTime? FiadorMfechaPoder { get; set; }
        public string FiadorMlicenciadoPoder { get; set; }
        public string FiadorMnumeroNotaPoder { get; set; }
        public string FiadorMcodigoPostalGarantia { get; set; }
        public string FiadorMcondicionMigratoria { get; set; }
        public string FiadorMdomicilioEmpresa { get; set; }
        public string FiadorMcoloniaEmpresa { get; set; }
        public string FiadorMdeleEmpresa { get; set; }
        public string FiadorMestadoEmpresa { get; set; }
        public string FiadorMcpempresa { get; set; }

        public FisicaMoral FisicaMoral { get; set; }
    }
}
