using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class TipoProcesoPo
    {
        public TipoProcesoPo()
        {
            UsuarioPoliza = new HashSet<UsuarioPoliza>();
        }

        public int TipoProcesoPoId { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }

        public ICollection<UsuarioPoliza> UsuarioPoliza { get; set; }
    }
}
