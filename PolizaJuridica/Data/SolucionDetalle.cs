using System;
using System.Collections.Generic;

namespace PolizaJuridica.Data
{
    public partial class SolucionDetalle
    {
        public int SolucionDetalleId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Observaciones { get; set; }
        public int SolucionesId { get; set; }
        public int UsuarioId { get; set; }
        public string DocumentosImagen { get; set; }
        public string DocumentoDesc { get; set; }

        public Soluciones Soluciones { get; set; }
        public Usuarios Usuario { get; set; }
    }
}
