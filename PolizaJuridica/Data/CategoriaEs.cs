using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class CategoriaEs
    {
        public CategoriaEs()
        {
            CuentasXpagarNavigation = new HashSet<CuentasXpagar>();
            DetallePoliza = new HashSet<DetallePoliza>();
            InverseCategoriaEspadre = new HashSet<CategoriaEs>();
        }

        public int CategoriaEsid { get; set; }
        public string Descripcion { get; set; }
        public bool? Poliza { get; set; }
        public int? CategoriaEspadreId { get; set; }
        public int? TipoEs { get; set; }
        public bool? CuentasXpagar { get; set; }

        public CategoriaEs CategoriaEspadre { get; set; }
        public ICollection<CuentasXpagar> CuentasXpagarNavigation { get; set; }
        public ICollection<DetallePoliza> DetallePoliza { get; set; }
        public ICollection<CategoriaEs> InverseCategoriaEspadre { get; set; }
    }
}
