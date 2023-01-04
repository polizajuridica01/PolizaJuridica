using PolizaJuridica.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.ViewModels
{
    public class PolizaViewModel
    {
            //Número de la Poliza
            public int PolizaId { get; set; }

            //Costo de la Póliza
            public string TextoCostoPoliza { get; set; }

            //Vigencia de la poliza
            public string TextoVigenciaFechaIF { get; set; }
            public string SolicitudFechaFirma { get; set; }
            public string TextoImporterenta { get; set; }
            public string AsesorNombre { get; set; }
            public string RepresentanteNombre { get; set; }

            //Datos del arrendador
            //Persona Fisica
            public string SolicitudNombreProp { get; set; }
            public string SolicitudDomicilioProp { get; set; }           
            public string SolicitudTelefonoProp { get; set; }
            public string SolicitudEmailProp { get; set; }

            //Persona Moral

            public string SolicitudRazonSocial { get; set; }
            public string SolicitudRfc { get; set; }
            public string SolicitudRepresentanteLegal { get; set; }

            //Direccion del domicilio arrendar
            public string SolicitudUbicacionArrendado { get; set; }

            //Tipo de Inmueble
            public string TipoInmobiliarioDesc { get; set; }

            //Datos del Arrendatario
            public string SfisicaNombre { get; set; }
            public string SfisicaTelefono { get; set; }
            public string SfisicaCelular { get; set; }
            public string SfisicaEmail { get; set; }

            //seccion faltante
            
            public string jurisdiccion { get; set; }
            public string polizasiniva { get; set; }
            public string iva { get; set; }
            public string oficina { get; set; }
            public string Inmobiliaria { get; set; }
        //Sección del representante
        public string RepresentanteFirma { get; set; }
        public string SolicitudTelefono { get; set; }
        public string SolicitudEmail { get; set; }

    }
}
