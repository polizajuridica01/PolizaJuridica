using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.Utilerias
{
    public class ConvertNumbertoText
    {
        public static string NumToLetter(string num, string moneda)
        {
            string res, dec, cantidad = string.Empty;
            decimal entero;
            decimal decimales;
            decimal nro;
            var tipoMoneda = string.Empty;
            var numeroformateado = num.Length;
            if (numeroformateado > 6)
            {
                if (numeroformateado == 7)
                {
                    cantidad = num.Substring(0, 1) + "," + num.Substring(1);
                }
                else if (numeroformateado == 8)
                {
                    cantidad = num.Substring(0, 2) + "," + num.Substring(2);
                }
                else if (numeroformateado == 9)
                {
                    cantidad = num.Substring(0, 3) + "," + num.Substring(3);
                }
                else if (numeroformateado == 10)
                {
                    cantidad = num.Substring(0, 1) + "," + num.Substring(0, 4) + "," + num.Substring(4);
                }//else if (numeroformateado == 11)
                //{
                //    cantidad = num.Substring(0, 4) + "," + num.Substring(4);
                //}else if (numeroformateado == 12)
                //{
                //    cantidad = num.Substring(0, 4) + "," + num.Substring(4);
                //}
            }
            else
            {
                cantidad = num;
            }

            try
            {
                nro = Convert.ToDecimal(num);
            }
            catch
            {
                return string.Empty;
            }

            entero = Math.Floor(nro);
            decimales = Math.Floor((nro - entero) * 100);

            if (moneda.ToUpper().Equals(string.Empty))
            {
                tipoMoneda = entero.CompareTo(1) == 0 ? "PESO" : "PESOS";
            }
            else
            {
                tipoMoneda = moneda.ToUpper();
            }
            dec = " PESOS 00/100 M.N.";//string.Format(" {0}, {1}/100 {2}", tipoMoneda, decimales.ToString().PadLeft(2, '0'), moneda.ToUpper().Equals(string.Empty) ? "MN" : string.Empty);
            res = cantidad + " ("+ ToText(entero) + dec + ")";
            return res;
        }

        private static string ToText(decimal value)
        {
            string num2Text = string.Empty;
            value = Math.Truncate(value);
            if (value == 0)
            {
                num2Text = "CERO";
            }
            else if (value == 1)
            {
                num2Text = "UN";
            }
            else if (value == 2)
            {
                num2Text = "DOS";
            }
            else if (value == 3)
            {
                num2Text = "TRES";
            }
            else if (value == 4)
            {
                num2Text = "CUATRO";
            }
            else if (value == 5)
            {
                num2Text = "CINCO";
            }
            else if (value == 6)
            {
                num2Text = "SEIS";
            }
            else if (value == 7)
            {
                num2Text = "SIETE";
            }
            else if (value == 8)
            {
                num2Text = "OCHO";
            }
            else if (value == 9)
            {
                num2Text = "NUEVE";
            }
            else if (value == 10)
            {
                num2Text = "DIEZ";
            }
            else if (value == 11)
            {
                num2Text = "ONCE";
            }
            else if (value == 12)
            {
                num2Text = "DOCE";
            }
            else if (value == 13)
            {
                num2Text = "TRECE";
            }
            else if (value == 14)
            {
                num2Text = "CATORCE";
            }
            else if (value == 15)
            {
                num2Text = "QUINCE";
            }
            else if (value < 20)
            {
                num2Text = "DIECI" + ToText(value - 10);
            }
            else if (value == 20)
            {
                num2Text = "VEINTE";
            }
            else if (value < 30)
            {
                num2Text = "VEINTI" + ToText(value - 20);
            }
            else if (value == 30)
            {
                num2Text = "TREINTA";
            }
            else if (value == 40)
            {
                num2Text = "CUARENTA";
            }
            else if (value == 50)
            {
                num2Text = "CINCUENTA";
            }
            else if (value == 60)
            {
                num2Text = "SESENTA";
            }
            else if (value == 70)
            {
                num2Text = "SETENTA";
            }
            else if (value == 80)
            {
                num2Text = "OCHENTA";
            }
            else if (value == 90)
            {
                num2Text = "NOVENTA";
            }
            else if (value < 100)
            {
                num2Text = ToText(Math.Truncate(value / 10) * 10) + " Y " + ToText(value % 10);
            }
            else if (value == 100)
            {
                num2Text = "CIEN";
            }
            else if (value < 200)
            {
                num2Text = "CIENTO " + ToText(value - 100);
            }
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800))
            {
                num2Text = ToText(Math.Truncate(value / 100)) + "CIENTOS";
            }
            else if (value == 500)
            {
                num2Text = "QUINIENTOS";
            }
            else if (value == 700)
            {
                num2Text = "SETECIENTOS";
            }
            else if (value == 900)
            {
                num2Text = "NOVECIENTOS";
            }
            else if (value < 1000)
            {
                num2Text = ToText(Math.Truncate(value / 100) * 100) + " " + ToText(value % 100);
            }
            else if (value == 1000)
            {
                num2Text = "MIL";
            }
            else if (value < 2000)
            {
                num2Text = "MIL " + ToText(value % 1000);
            }
            else if (value < 1000000)
            {
                num2Text = ToText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0)
                {
                    num2Text = num2Text + " " + ToText(value % 1000);
                }
            }
            else if (value == 1000000)
            {
                num2Text = "UN MILLON";
            }
            else if (value < 2000000)
            {
                num2Text = "UN MILLON " + ToText(value % 1000000);
            }
            else if (value < 1000000000000)
            {
                num2Text = ToText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - (Math.Truncate(value / 1000000) * 1000000)) > 0)
                {
                    num2Text = num2Text + " " + ToText(value - (Math.Truncate(value / 1000000) * 1000000));
                }
            }
            else if (value == 1000000000000)
            {
                num2Text = "UN BILLON";
            }
            else if (value < 2000000000000)
            {
                num2Text = "UN BILLON " + ToText(value - (Math.Truncate(value / 1000000000000) * 1000000000000));
            }
            else if (value < 1000000000000000000)
            {
                num2Text = ToText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - (Math.Truncate(value / 1000000000000) * 1000000000000)) > 0)
                {
                    num2Text = num2Text + " " + ToText(value - (Math.Truncate(value / 1000000000000) * 1000000000000));
                }
            }
            else if (value == 1000000000000000000)
            {
                num2Text = "UN TRILLON";
            }
            else if (value < 2000000000000000000)
            {
                num2Text = "UN TRILLON " + ToText(value - (Math.Truncate(value / 1000000000000000000) * 1000000000000000000));
            }
            else if (value < Convert.ToDecimal(1000000000000000000000000.00))
            {
                num2Text = ToText(Math.Truncate(value / 1000000000000000000)) + " TRILLONES";
                if ((value - (Math.Truncate(value / 1000000000000000000) * 1000000000000000000)) > 0)
                {
                    num2Text = num2Text + " " + ToText(value - (Math.Truncate(value / 1000000000000000000) * 1000000000000000000));
                }
            }
            return num2Text;
        }

    }
}
