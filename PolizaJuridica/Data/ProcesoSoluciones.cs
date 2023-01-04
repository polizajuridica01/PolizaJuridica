using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class ProcesoSoluciones
    {
        public ProcesoSoluciones()
        {
            Soluciones = new HashSet<Soluciones>();
            UsuariosSoluciones = new HashSet<UsuariosSoluciones>();
        }

        public int ProcesoSolucionesId { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Soluciones> Soluciones { get; set; }
        public ICollection<UsuariosSoluciones> UsuariosSoluciones { get; set; }
    }
}
