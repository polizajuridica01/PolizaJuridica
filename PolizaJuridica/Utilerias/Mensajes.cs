using PolizaJuridica.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.Utilerias
{
    public class Mensajes
    {
        public static ErroresViewModel MensajesError(string texto)
        {

            return new ErroresViewModel()
            {
                mensaje = texto,
                tipo = 1
            };
        }
        public static ErroresViewModel ErroresAtributos(string texto)
        {
            var error = "Error";

            return new ErroresViewModel()
            {
                mensaje = texto + error,
                tipo = 2
            };
        }
        public static ErroresViewModel Exitoso(string texto)
        {

            return new ErroresViewModel()
            {
                mensaje = texto,
                tipo = 3
            };
        }
        public static ErroresViewModel Informativo(string texto)
        {

            return new ErroresViewModel()
            {
                mensaje = texto,
                tipo = 4
            };
        }
    }
}
