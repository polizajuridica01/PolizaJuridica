using PolizaJuridica.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaJuridica.Utilerias
{
    public class KeywordsPoliza
    {
        public static String GenerarPoliza(string docText, FisicaMoral fisicaMoral)
        {
            Poliza p = fisicaMoral.Poliza.SingleOrDefault();

            double iva = 1.16;
            double costo = 0;           

            if (p.FisicaMoral.Solicitud.CentroCostosId <= 0 || p.FisicaMoral.Solicitud.CentroCostosId == null)
            {
                costo = Convert.ToDouble(p.FisicaMoral.Solicitud.CostoPoliza);
            }
            else
            {
                costo = Convert.ToDouble(p.FisicaMoral.Solicitud.CentroCostos.CentroCostosMonto); 
            }

            double siniva = costo / iva;
            double resta = costo - siniva;
            string PolizaConIVA = ConvertNumbertoText.NumToLetter(resta.ToString().Trim(), "MX").ToUpper();
            string PolizaSinIVA = ConvertNumbertoText.NumToLetter(siniva.ToString().Trim(), "MX").ToUpper();


            if (p.PolizaId > 0)
            {
                docText = docText.Replace(nameof(p.PolizaId), p.PolizaId.ToString());
            }

            if (PolizaConIVA != null)
            {
                docText = docText.Replace("PolizaConIVA", PolizaConIVA);
            }

            if (PolizaSinIVA != null)
            {
                docText = docText.Replace("PolizaSinIVA", PolizaSinIVA);
            }

            

            return docText;
        }
    }
}
