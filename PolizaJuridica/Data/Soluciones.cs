using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Soluciones
    {
        public Soluciones()
        {
            SolucionDetalle = new HashSet<SolucionDetalle>();
            UsuariosSoluciones = new HashSet<UsuariosSoluciones>();
        }

        public int SolucionesId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? PolizaAnterior { get; set; }
        public int? PolizaId { get; set; }
        public string NombrePro { get; set; }
        public string ApellidoPatPro { get; set; }
        public string ApellidoMatPro { get; set; }
        public string DireccionInmueble { get; set; }
        public string ColoniaInmueble { get; set; }
        public string AlcaldiaMunicipioInmueble { get; set; }
        public int? EstadoId { get; set; }
        public string Cpinmueble { get; set; }
        public string DescripcionProblema { get; set; }
        public DateTime? FechaArrendatario { get; set; }
        public string Argumenta { get; set; }
        public DateTime? FechaContratoI { get; set; }
        public int UsuarioId { get; set; }
        public string CelularProp { get; set; }
        public string EmailProp { get; set; }
        public string NombreArren { get; set; }
        public string ApellidoPatArren { get; set; }
        public string ApellidoMatArren { get; set; }
        public string CelularArrem { get; set; }
        public string EmailArren { get; set; }
        public string NombreFiador { get; set; }
        public string ApellidoPatFiador { get; set; }
        public string ApellidoMatFiador { get; set; }
        public string CelularFiador { get; set; }
        public string EmailFiador { get; set; }
        public int ProcesoSolucionesId { get; set; }
        public DateTime? FechaContratoF { get; set; }
        public string TipoPoliza { get; set; }

        public Estados Estado { get; set; }
        public Poliza Poliza { get; set; }
        public ProcesoSoluciones ProcesoSoluciones { get; set; }
        public Usuarios Usuario { get; set; }
        public ICollection<SolucionDetalle> SolucionDetalle { get; set; }
        public ICollection<UsuariosSoluciones> UsuariosSoluciones { get; set; }
    }
}
