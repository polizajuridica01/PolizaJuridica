using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Poliza
    {
        public Poliza()
        {
            CuentasXcobrar = new HashSet<CuentasXcobrar>();
            CuentasXpagar = new HashSet<CuentasXpagar>();
            DetallePoliza = new HashSet<DetallePoliza>();
            Soluciones = new HashSet<Soluciones>();
            UsuarioPoliza = new HashSet<UsuarioPoliza>();
        }

        public int PolizaId { get; set; }
        public DateTime Creacion { get; set; }
        public int? FisicaMoralId { get; set; }
        public string PolizaAnterior { get; set; }

        public FisicaMoral FisicaMoral { get; set; }
        public Firmas Firmas { get; set; }
        public ICollection<CuentasXcobrar> CuentasXcobrar { get; set; }
        public ICollection<CuentasXpagar> CuentasXpagar { get; set; }
        public ICollection<DetallePoliza> DetallePoliza { get; set; }
        public ICollection<Soluciones> Soluciones { get; set; }
        public ICollection<UsuarioPoliza> UsuarioPoliza { get; set; }
    }
}
