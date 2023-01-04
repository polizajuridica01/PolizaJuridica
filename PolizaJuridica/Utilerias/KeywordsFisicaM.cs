using PolizaJuridica.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolizaJuridica.Utilerias
{
    public class KeywordsFisicaM
    {
        public static String SolicitudFiadorMoral(string docText, FisicaMoral fisicaMoral)
        {
            int contador = 1;
            string identificador = string.Empty;
            string prefijo = @"\bFiadorM";
            string FiaddorMrazonSocialAgrupador = string.Empty;
            string representantelegalAgrupador = string.Empty;
            string yoindistintamente = string.Empty;
            yoindistintamente = " Y/O INDISTINTAMENTE ";
            string pattern = string.Empty;
            string representantelegal = String.Empty;
            foreach (var f in fisicaMoral.FiadorM)
            {
                identificador = contador.ToString();

                if (f.FiadorMapeMaternoRlegal != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMapeMaternoRlegal) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMapeMaternoRlegal.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMapeMaternoRlegal) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMapePaternoRlegal != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMapePaternoRlegal) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMapePaternoRlegal.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMapePaternoRlegal) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }


                //Keyword representate legal

                if (f.FiadorMnombresRlegal != null && f.FiadorMapePaternoRlegal != null)
                {
                    if (f.FiadorMapeMaternoRlegal != null)
                    {
                        representantelegal = f.FiadorMnombresRlegal + " " + f.FiadorMapePaternoRlegal + " " + f.FiadorMapeMaternoRlegal;
                    }
                    else
                    {
                        representantelegal = f.FiadorMnombresRlegal + " " + f.FiadorMapePaternoRlegal;
                    }
                }

                if (fisicaMoral.FiadorM.Count == 1)
                {
                    FiaddorMrazonSocialAgrupador += f.FiaddorMrazonSocial;
                    representantelegalAgrupador += representantelegal;

                }
                else
                {
                    if (fisicaMoral.FiadorM.Count == contador)
                    {
                        FiaddorMrazonSocialAgrupador += f.FiaddorMrazonSocial;
                        representantelegalAgrupador += representantelegal;
                    }
                    else
                    {
                        FiaddorMrazonSocialAgrupador += f.FiaddorMrazonSocial + yoindistintamente;
                        representantelegalAgrupador += representantelegal + yoindistintamente;
                    }

                }

                //fin de keyword
                if (f.FiadorMcelularRlegal != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMcelularRlegal) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMcelularRlegal.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMcelularRlegal) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMcoliniaGarantia != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMcoliniaGarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMcoliniaGarantia.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMcoliniaGarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMdelegacionGarantia != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMdelegacionGarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMdelegacionGarantia.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMdelegacionGarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMdomicilioGarantia != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMdomicilioGarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMdomicilioGarantia.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMdomicilioGarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMemailRlegal != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMemailRlegal) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMemailRlegal.ToLower());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMemailRlegal) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMgiro != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMgiro) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMgiro.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMgiro) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMmactaNo != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMmactaNo) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMmactaNo.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMmactaNo) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMnacionalidadRlegal != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMnacionalidadRlegal) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMnacionalidadRlegal.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMnacionalidadRlegal) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMnombresRlegal != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMnombresRlegal) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMnombresRlegal.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMnombresRlegal) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMpoderRepNo != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMpoderRepNo) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMpoderRepNo.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMpoderRepNo) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMpuestoRlegal != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMpuestoRlegal) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMpuestoRlegal.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMpuestoRlegal) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMrfc != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMrfc) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMrfc.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMrfc) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMtelefono != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMtelefono) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMtelefono.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMtelefono) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMtelefonoRlegat != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMtelefonoRlegat) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMtelefonoRlegat.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMtelefonoRlegat) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.FiadorMweb != null)
                {
                    pattern = @"\b" + nameof(f.FiadorMweb) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMweb.ToUpper());
                }
                else
                {
                    pattern = @"\b" + nameof(f.FiadorMweb) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.NumeroIdentificacion != null)
                {
                    pattern = prefijo + nameof(f.NumeroIdentificacion) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.NumeroIdentificacion.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.NumeroIdentificacion) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (f.TipoIdentificacion != null)
                {
                    pattern = prefijo + nameof(f.TipoIdentificacion) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.TipoIdentificacion.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.TipoIdentificacion) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }
                prefijo = @"\b";
                //nuevos campos
                if (f.FiadorMnombreEscrituraGarantia != null)
                {
                    pattern = prefijo + nameof(f.FiadorMnombreEscrituraGarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMnombreEscrituraGarantia.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMnombreEscrituraGarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMlicenciadoNotaria != null)
                {
                    pattern = prefijo + nameof(f.FiadorMlicenciadoNotaria) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMlicenciadoNotaria.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMlicenciadoNotaria) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMnumNotaria != null)
                {
                    pattern = prefijo + nameof(f.FiadorMnumNotaria) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMnumNotaria.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMnumNotaria) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMdistritoJudicial != null)
                {
                    pattern = prefijo + nameof(f.FiadorMdistritoJudicial) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMdistritoJudicial.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMdistritoJudicial) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMnumPartida != null)
                {
                    pattern = prefijo + nameof(f.FiadorMnumPartida) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMnumPartida.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMnumPartida) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMvolumen != null)
                {
                    pattern = prefijo + nameof(f.FiadorMvolumen) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMvolumen.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMvolumen) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMlibro != null)
                {
                    pattern = prefijo + nameof(f.FiadorMlibro) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMlibro.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMlibro) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMseccion != null)
                {
                    pattern = prefijo + nameof(f.FiadorMseccion) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMseccion.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMseccion) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMfechaPartida.GetHashCode() != 0)
                {
                    pattern = prefijo + nameof(f.FiadorMfechaPartida) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMfechaPartida.ToString());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMfechaPartida) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMcoloniaGarantia != null)
                {
                    pattern = prefijo + nameof(f.FiadorMcoloniaGarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMcoloniaGarantia.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMcoloniaGarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMestadoGarantia != null)
                {
                    pattern = prefijo + nameof(f.FiadorMestadoGarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMestadoGarantia.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMestadoGarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMcpgarantia != null)
                {
                    pattern = prefijo + nameof(f.FiadorMcpgarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMcpgarantia.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMcpgarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMnumCons != null)
                {
                    pattern = prefijo + nameof(f.FiadorMnumCons) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMnumCons.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMnumCons) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMfechaCons.GetHashCode() != 0)
                {
                    pattern = prefijo + nameof(f.FiadorMfechaCons) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMfechaCons.ToString());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMfechaCons) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMlicenciadoCons != null)
                {
                    pattern = prefijo + nameof(f.FiadorMlicenciadoCons) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMlicenciadoCons.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMlicenciadoCons) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMnumNotaCons != null)
                {
                    pattern = prefijo + nameof(f.FiadorMnumNotaCons) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMnumNotaCons.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMnumNotaCons) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMnumRpp != null)
                {
                    pattern = prefijo + nameof(f.FiadorMnumRpp) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMnumRpp.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMnumRpp) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMfechaRpp.GetHashCode() != 0)
                {
                    pattern = prefijo + nameof(f.FiadorMfechaRpp) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMfechaRpp.ToString());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMfechaRpp) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMnumEscPoder != null)
                {
                    pattern = prefijo + nameof(f.FiadorMnumEscPoder) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMnumEscPoder.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMnumEscPoder) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMfechaPoder.GetHashCode() != 0)
                {
                    pattern = prefijo + nameof(f.FiadorMfechaPoder) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMfechaPoder.ToString());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMfechaPoder) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMlicenciadoPoder != null)
                {
                    pattern = prefijo + nameof(f.FiadorMlicenciadoPoder) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMlicenciadoPoder.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMlicenciadoPoder) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMnumeroNotaPoder != null)
                {
                    pattern = prefijo + nameof(f.FiadorMnumeroNotaPoder) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMnumeroNotaPoder.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMnumeroNotaPoder) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMcodigoPostalGarantia != null)
                {
                    pattern = prefijo + nameof(f.FiadorMcodigoPostalGarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMcodigoPostalGarantia.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMcodigoPostalGarantia) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMcondicionMigratoria != null)
                {
                    pattern = prefijo + nameof(f.FiadorMcondicionMigratoria) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMcondicionMigratoria.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMcondicionMigratoria) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMdomicilioEmpresa != null)
                {
                    pattern = prefijo + nameof(f.FiadorMdomicilioEmpresa) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMdomicilioEmpresa.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMdomicilioEmpresa) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMcoloniaEmpresa != null)
                {
                    pattern = prefijo + nameof(f.FiadorMcoloniaEmpresa) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMcoloniaEmpresa.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMcoloniaEmpresa) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMdeleEmpresa != null)
                {
                    pattern = prefijo + nameof(f.FiadorMdeleEmpresa) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMdeleEmpresa.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMdeleEmpresa) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMestadoEmpresa != null)
                {
                    pattern = prefijo + nameof(f.FiadorMestadoEmpresa) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMestadoEmpresa.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMestadoEmpresa) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (f.FiadorMcpempresa != null)
                {
                    pattern = prefijo + nameof(f.FiadorMcpempresa) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, f.FiadorMcpempresa.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(f.FiadorMcpempresa) + identificador + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }


                //En esta sección meto los nuevo


                //fin de sección
                contador++;
            }

            if (!String.IsNullOrEmpty(representantelegal))
            {
                pattern = @"\bFiadorMRepresentanteLegal" + @"\b";
                docText = Regex.Replace(docText, pattern, representantelegal.ToUpper());
            }
            else
            {
                pattern = @"\bFiadorMRepresentanteLegal" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }
            if (!String.IsNullOrEmpty(FiaddorMrazonSocialAgrupador))
            {
                pattern = @"\b" + "FiaddorMrazonSocial" + identificador + @"\b";
                docText = Regex.Replace(docText, pattern, FiaddorMrazonSocialAgrupador.ToUpper());
            }
            else
            {
                pattern = @"\b" + "FiaddorMrazonSocial" + identificador + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            return docText;
        }

    }
}
