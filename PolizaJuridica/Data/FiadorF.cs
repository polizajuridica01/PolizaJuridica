using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class FiadorF
    {
        public int FiadorFid { get; set; }
        public string FiadorFnombres { get; set; }
        public string FiadorFapePaterno { get; set; }
        public string FiadorFapeMaterno { get; set; }
        public string FiadorFnacionalidad { get; set; }
        public string FiadorFcondicionMigratoria { get; set; }
        public string FiadorFparentesco { get; set; }
        public string FiadorFestadoCivil { get; set; }
        public string FiadorFconvenioEc { get; set; }
        public string FiadorFdomicilio { get; set; }
        public string FiadorFcolonia { get; set; }
        public string FiadorFdelegacion { get; set; }
        public string FiadorFestado { get; set; }
        public string FiadorFcodigoPostal { get; set; }
        public string FiadorFtelefono { get; set; }
        public string FiadorFcelular { get; set; }
        public string FiadorFemail { get; set; }
        public string FiadorFdomicilioGarantia { get; set; }
        public string FiadorFcoloniaGarantia { get; set; }
        public string FiadorFdelegacionGarantia { get; set; }
        public string FiadorFestadoGarantia { get; set; }
        public string FiadorFprofesion { get; set; }
        public string FiadorFempresa { get; set; }
        public string FiadorFtelefonoEmpresa { get; set; }
        public string FiadorFcodigoPostalGarantia { get; set; }
        public string FiadorFnombresConyuge { get; set; }
        public string FiadorFapePaternoConyuge { get; set; }
        public string FiadorFapeMaternoConyuge { get; set; }
        public int FisicaMoralId { get; set; }
        public string DistritoJudicial { get; set; }
        public string EscrituraNumero { get; set; }
        public string Licenciado { get; set; }
        public string NumeroNotaria { get; set; }
        public DateTime PartidaFecha { get; set; }
        public string PartidaLibro { get; set; }
        public string PartidaNumero { get; set; }
        public string PartidaSeccion { get; set; }
        public string PartidaVolumen { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public int? TipoInmuebleId { get; set; }

        public FisicaMoral FisicaMoral { get; set; }
        public TipoInmobiliario TipoInmueble { get; set; }
    }
}
