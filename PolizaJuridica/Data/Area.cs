using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class Area
    {
        public Area()
        {
            MenuP = new HashSet<MenuP>();
            Usuarios = new HashSet<Usuarios>();
        }

        public int AreaId { get; set; }
        public string AreaDescripcion { get; set; }
        public string Dashboard { get; set; }

        public ICollection<MenuP> MenuP { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
