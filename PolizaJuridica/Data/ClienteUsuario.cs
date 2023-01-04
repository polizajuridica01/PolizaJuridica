using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class ClienteUsuario
    {
        public int ClienteUsuario1 { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public int UsuariosId { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Usuarios Usuarios { get; set; }
    }
}
