using PolizaJuridica.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolizaJuridica.Utilerias
{
    public class KeywordReporteInvest
    {
        public static String Generar(string docText, ReporteInvst reporte)
        {
            string pattern = String.Empty;

            string FECHAHOY = ConvertDateToText.DateToText(DateTime.Now).ToUpper();

            pattern = @"\bFECHAHOY\b";
            docText = Regex.Replace(docText, pattern, FECHAHOY.Trim());


            if (reporte.Texto1 != null)
            {
                pattern = @"\b" + nameof(reporte.Texto1) + @"\b";
                docText = Regex.Replace(docText, pattern, reporte.Texto1.ToString().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(reporte.Texto1) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (reporte.Texto2 != null)
            {
                pattern = @"\b" + nameof(reporte.Texto2) + @"\b";
                docText = Regex.Replace(docText, pattern, reporte.Texto2.ToString().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(reporte.Texto2) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            return docText;
        }
    }
}
