
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.Utilerias
{
    public class ConvertDateToText
    {
        public static string DateToText(DateTime date)
        {
            string fechaTexto, TextoMes = string.Empty;
            string Textodia = string.Empty;
            int mes = date.Month;
            int dia = date.Day;
           
            switch (mes)
            {
                case 1:
                    TextoMes = "ENERO";
                    break;
                case 2:
                    TextoMes = "FEBRERO";
                    break;
                case 3:
                    TextoMes = "MARZO";
                    break;
                case 4:
                    TextoMes = "ABRIL";
                    break;
                case 5:
                    TextoMes = "MAYO";
                    break;
                case 6:
                    TextoMes = "JUNIO";
                    break;
                case 7:
                    TextoMes = "JULIO";
                    break;
                case 8:
                    TextoMes = "AGOSTO";
                    break;
                case 9:
                    TextoMes = "SEPTIEMBRE";
                    break;
                case 10:
                    TextoMes = "OCTUBRE";
                    break;
                case 11:
                    TextoMes = "NOVIEMBRE";
                    break;
                case 12:
                    TextoMes = "DICIEMBRE";
                    break;
            }

            switch (dia)
            {
                case 1:
                    Textodia = "01";
                break;
                case 2:
                    Textodia = "02";
                break;
                case 3:
                    Textodia = "03";
                break;
                case 4:
                    Textodia = "04";
                break;
                case 5:
                    Textodia = "05";
                break;
                case 6:
                    Textodia = "06";
                break;
                case 7:
                    Textodia = "07";
                break;
                case 8:
                    Textodia = "08";
                break;
                case 9:
                    Textodia = "09";
                break;

                default:
                    Textodia = date.Day.ToString();
                    break;

            }

            fechaTexto = Textodia + " DE " + TextoMes + " DEL " + date.Year.ToString();

            return fechaTexto;
        }

    }
}
