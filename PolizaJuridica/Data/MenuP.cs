using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class MenuP
    {
        public MenuP()
        {
            InverseMenuPpadre = new HashSet<MenuP>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Controlador { get; set; }
        public string Pantalla { get; set; }
        public int? MenuPpadreId { get; set; }
        public int AreaId { get; set; }

        public Area Area { get; set; }
        public MenuP MenuPpadre { get; set; }
        public ICollection<MenuP> InverseMenuPpadre { get; set; }
    }
}
