using PolizaJuridica.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolizaJuridica.Utilerias
{
    public class KeywordsFisicaF
    {
        public static String SolicitudFiadorFisico(string docText, FisicaMoral fisicaMoral, List<TipoInmobiliario> TipoInmueble)
        {
            //Seccion del fiador fisica
            string FiadorFNombres = string.Empty;
            string FiadorFApeMaterno = string.Empty;
            string FiadorFNacionalidad = string.Empty;
            string FiadorFCondicionMigratoria = string.Empty;
            string FiadorFParentesco = string.Empty;
            string FiadorFEstadoCivil = string.Empty;
            string FiadorFConvenioEC = string.Empty;
            string FiadorFDomicilio = string.Empty;
            string FiadorFColonia = string.Empty;
            string FiadorFDelegacion = string.Empty;
            string FiadorFEstado = string.Empty;
            string FiadorFCodigoPostal = string.Empty;
            string FiadorFTelefono = string.Empty;
            string FiadorFCelular = string.Empty;
            string FiadorFEmail = string.Empty;
            string FiadorFDomicilioGarantia = string.Empty;
            string FiadorFColoniaGarantia = string.Empty;
            string FiadorFDelegacionGarantia = string.Empty;
            string FiadorFEstadoGarantia = string.Empty;
            string FiadorFProfesion = string.Empty;
            string FiadorFEmpresa = string.Empty;
            string FiadorFTelefonoEmpresa = string.Empty;
            string FiadorFCodigoPostalGarantia = string.Empty;
            string FiadorFNombresConyuge = string.Empty;
            string FiadorFApePaternoConyuge = string.Empty;
            string FiadorFApeMaternoConyuge = string.Empty;
            string FisicaMoralId = string.Empty;
            string DistritoJudicial = string.Empty;
            string EscrituraNumero = string.Empty;
            string Licenciado = string.Empty;
            string NumeroNotaria = string.Empty;
            string PartidaFecha = string.Empty;
            string PartidaLibro = string.Empty;
            string PartidaNumero = string.Empty;
            string PartidaSeccion = string.Empty;
            string PartidaVolumen = string.Empty;
            string TipoIdentificacion = string.Empty;
            string NumeroIdentificacion = string.Empty;
            string identificador = string.Empty;
            string yoindistintamente = string.Empty;
            yoindistintamente = " Y/O INDISTINTAMENTE ";
            string pattern = string.Empty;

            string FiadorFNombresAgrupados = string.Empty;
            string TipoIdentificacionAgrupados = string.Empty;
            string NumeroIdentificacionAgrupados = string.Empty;
            int contador = 1;
            foreach (var fiador in fisicaMoral.FiadorF)
            {
                if (fiador.FiadorFapeMaterno != null)
                    FiadorFApeMaterno = fiador.FiadorFapeMaterno;

                FiadorFNombres = fiador.FiadorFnombres.ToUpper() + " " + fiador.FiadorFapePaterno + " " + FiadorFApeMaterno;

                if (fiador.TipoIdentificacion != null)
                    TipoIdentificacion = fiador.TipoIdentificacion;

                if (fiador.NumeroIdentificacion != null)
                    NumeroIdentificacion = fiador.NumeroIdentificacion;

                if (fisicaMoral.FiadorF.Count == 1)
                {
                    FiadorFNombresAgrupados += FiadorFNombres;
                    TipoIdentificacionAgrupados += TipoIdentificacion;
                    NumeroIdentificacionAgrupados += NumeroIdentificacion;
                }
                else
                {
                    if (fisicaMoral.FiadorF.Count == contador)
                    {
                        FiadorFNombresAgrupados += FiadorFNombres;
                        TipoIdentificacionAgrupados += TipoIdentificacion;
                        NumeroIdentificacionAgrupados += NumeroIdentificacion;
                    }
                    else
                    {
                        FiadorFNombresAgrupados += FiadorFNombres + yoindistintamente;
                        TipoIdentificacionAgrupados += TipoIdentificacion + "/";
                        NumeroIdentificacionAgrupados += NumeroIdentificacion + "/";
                    }
                }

                FiadorFNacionalidad = fiador.FiadorFnacionalidad;

                //Domicilio del fiador
                FiadorFDomicilio = fiador.FiadorFdomicilio.ToUpper() + ", " + fiador.FiadorFcolonia.ToUpper() + ", " + fiador.FiadorFdelegacion.ToUpper() + ", " + fiador.FiadorFestado.ToUpper() + ", CÓDIGO POSTAL: " + fiador.FiadorFcodigoPostal;
                //domicilio en garantia 
                if (fiador.FiadorFdomicilioGarantia != null)
                    FiadorFDomicilioGarantia = fiador.FiadorFdomicilioGarantia;

                if (fiador.FiadorFcoloniaGarantia != null)
                    FiadorFColoniaGarantia = fiador.FiadorFcoloniaGarantia;

                if (fiador.FiadorFdelegacionGarantia != null)
                    FiadorFDelegacionGarantia = fiador.FiadorFdelegacionGarantia;

                if (fiador.FiadorFestadoGarantia != null)
                    FiadorFEstadoGarantia = fiador.FiadorFestadoGarantia;

                if (fiador.FiadorFcodigoPostalGarantia != null)
                    FiadorFCodigoPostalGarantia = fiador.FiadorFcodigoPostalGarantia;

                if (fiador.FiadorFdomicilioGarantia != null)
                    FiadorFDomicilioGarantia = fiador.FiadorFdomicilioGarantia.ToUpper() + ", " + FiadorFColoniaGarantia.ToUpper() + ", " + FiadorFDelegacionGarantia.ToUpper() + ", " + FiadorFEstadoGarantia.ToUpper() + ", CÓDIGO POSTAL: " + FiadorFCodigoPostalGarantia;

                //Escritura
                if (fiador.EscrituraNumero != null)
                    EscrituraNumero = fiador.EscrituraNumero;

                if (fiador.Licenciado != null)
                    Licenciado = fiador.Licenciado;

                if (fiador.PartidaFecha.GetHashCode() != 0)
                    PartidaFecha = ConvertDateToText.DateToText(fiador.PartidaFecha).ToUpper();

                if (fiador.PartidaLibro != null)
                    PartidaLibro = fiador.PartidaLibro;

                if (fiador.PartidaNumero != null)
                    PartidaNumero = fiador.PartidaNumero;

                if (fiador.PartidaSeccion != null)
                    PartidaSeccion = fiador.PartidaSeccion;

                if (fiador.PartidaVolumen != null)
                    PartidaVolumen = fiador.PartidaVolumen;

                if (fiador.DistritoJudicial != null)
                    DistritoJudicial = fiador.DistritoJudicial;

                if (contador >= 1)
                    identificador = contador.ToString();

                //Sección de remplazo de keywords

                if (fiador.NumeroNotaria != null)
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFNumeroNotaria" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, fiador.NumeroNotaria.ToUpper());
                }
                else
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFNumeroNotaria" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }




                if (FiadorFDomicilio != null)
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFDomicilio" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, FiadorFDomicilio.ToUpper());
                    //docText = docText.Replace("FiadorFDomicilio" + identificador, FiadorFDomicilio.ToUpper());
                }
                else
                {
                    //docText = docText.Replace("FiadorFDomicilio" + identificador, "");
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFDomicilio" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (FiadorFDomicilioGarantia != null)
                {
                    pattern = string.Empty;
                    pattern = @"\b" + " " + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, FiadorFDomicilioGarantia.ToUpper());
                    //docText = docText.Replace("FiadorFDomicilioGarantia" + identificador, FiadorFDomicilioGarantia.ToUpper());
                }
                else
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFDomicilioGarantia" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace("FiadorFDomicilioGarantia" + identificador,"");
                }

                if (EscrituraNumero != null)
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFEscrituraNumero" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, EscrituraNumero.ToUpper());
                    //docText = docText.Replace("FiadorFEscrituraNumero" + identificador, EscrituraNumero.ToUpper());
                }
                else
                {
                    //docText = docText.Replace("FiadorFEscrituraNumero" + identificador, "");
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFEscrituraNumero" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (PartidaFecha != null)
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFPartidaFecha" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, PartidaFecha.ToUpper());
                    //docText = docText.Replace("FiadorFPartidaFecha" + identificador, PartidaFecha.ToUpper());
                }
                else
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFPartidaFecha" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace("FiadorFPartidaFecha" + identificador, "");
                }

                if (PartidaLibro != null)
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFPartidaLibro" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, PartidaLibro.ToUpper());
                    //docText = docText.Replace("FiadorFPartidaLibro" + identificador, PartidaLibro.ToUpper());
                }
                else
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFPartidaLibro" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace("FiadorFPartidaLibro" + identificador, "");
                }

                if (PartidaNumero != null)
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFPartidaNumero" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, PartidaNumero.ToUpper());
                    //docText = docText.Replace("FiadorFPartidaNumero" + identificador, PartidaNumero.ToUpper());
                }
                else
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFPartidaNumero" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace("FiadorFPartidaNumero" + identificador, "");
                }

                if (PartidaSeccion != null)//este bloque de codigo genera signos de mas o error en el docuemnto
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFPartidaSeccion" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, PartidaSeccion.ToUpper());
                    //docText = docText.Replace("FiadorFPartidaSeccion" + identificador, PartidaSeccion.ToUpper());
                }
                else
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFPartidaSeccion" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace("FiadorFPartidaSeccion" + identificador, "");
                }

                if (PartidaVolumen != null)
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFPartidaVolumen" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, PartidaVolumen.ToUpper());
                    //docText = docText.Replace("FiadorFPartidaVolumen" + identificador, PartidaVolumen.ToUpper());
                }
                else
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFPartidaVolumen" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace("FiadorFPartidaVolumen" + identificador, "");
                }

                if (DistritoJudicial != null)
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFDistritoJudicial" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, DistritoJudicial.ToUpper());
                    //docText = docText.Replace("FiadorFDistritoJudicial" + identificador, DistritoJudicial.ToUpper());
                }
                else
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFDistritoJudicial" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace("FiadorFDistritoJudicial" + identificador, "");
                }


                if (FiadorFNacionalidad != null)
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFNacionalidad" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, FiadorFNacionalidad.ToUpper());
                    //docText = docText.Replace("FiadorFNacionalidad" + identificador, FiadorFNacionalidad.ToUpper());
                }
                else
                {
                    pattern = string.Empty;
                    pattern = @"\b" + "FiadorFNacionalidad" + identificador.Trim() + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace("FiadorFNacionalidad" + identificador, "");
                }

                //Fin de seccion 
                string principio = @"\bFiadorF";
                string fin = @"\b";
                if (fiador.DistritoJudicial != null)
                {
                    pattern = $"{principio}{nameof(fiador.DistritoJudicial)}{identificador.Trim()}{fin}" + @"\b";
                    docText = Regex.Replace(docText, pattern, fiador.DistritoJudicial.ToUpper());
                }
                else
                {
                    pattern = $"{principio.Trim()}{nameof(fiador.DistritoJudicial)}{identificador.Trim()}{fin.Trim()}" + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.EscrituraNumero != null)
                {
                    pattern = $"{principio}{nameof(fiador.EscrituraNumero)}{identificador.Trim()}{fin}" + @"\b";
                    docText = Regex.Replace(docText, pattern, fiador.EscrituraNumero.ToUpper());
                }
                else
                {
                    pattern = $"{principio.Trim()}{nameof(fiador.EscrituraNumero)}{identificador.Trim()}{fin.Trim()}" + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFapeMaterno != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFapeMaterno)}{identificador.Trim()}{fin}" + @"\b";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFapeMaterno.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFapeMaterno)}{identificador.Trim()}{fin.Trim()}" + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFapeMaternoConyuge != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFapeMaternoConyuge)}{identificador.Trim()}{fin}" + @"\b";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFapeMaternoConyuge.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFapeMaternoConyuge)}{identificador.Trim()}{fin.Trim()}" + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFapePaterno != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFapePaterno)}{identificador.Trim()}{fin}" + @"\b";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFapePaterno.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFapePaterno)}{identificador.Trim()}{fin.Trim()}" + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFapePaternoConyuge != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFapePaternoConyuge)}{identificador.Trim()}{fin}" + @"\b";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFapePaternoConyuge.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFapePaternoConyuge)}{identificador.Trim()}{fin.Trim()}" + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFcelular != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFcelular)}{identificador.Trim()}{fin}";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFcelular.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFcelular)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFcodigoPostal != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFcodigoPostal)}{identificador.Trim()}{fin}" + @"\b";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFcodigoPostal.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFcodigoPostal)}{identificador.Trim()}{fin.Trim()}" + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFcodigoPostalGarantia != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFcodigoPostalGarantia)}{identificador.Trim()}{fin}" + @"\b";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFcodigoPostalGarantia.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFcodigoPostalGarantia)}{identificador.Trim()}{fin.Trim()}" + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFcolonia != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFcolonia)}{identificador.Trim()}{fin}" + @"\b";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFcolonia.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFcolonia)}{identificador.Trim()}{fin.Trim()}" + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFcoloniaGarantia != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFcoloniaGarantia)}{identificador.Trim()}{fin}" + @"\b";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFcoloniaGarantia.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFcoloniaGarantia)}{identificador.Trim()}{fin.Trim()}" + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFcondicionMigratoria != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFcondicionMigratoria)}{identificador.Trim()}{fin}" + @"\b";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFcondicionMigratoria.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFcondicionMigratoria)}{identificador.Trim()}{fin.Trim()}" + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                ///////
                if (fiador.FiadorFconvenioEc != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFconvenioEc)}{identificador.Trim()}{fin}" + @"\b";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFconvenioEc.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFconvenioEc)}{identificador.Trim()}{fin.Trim()}" + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFdelegacion != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFdelegacion)}{identificador.Trim()}{fin}" + @"\b";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFdelegacion.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFdelegacion)}{identificador.Trim()}{fin.Trim()}" + @"\b";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFdelegacionGarantia != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFdelegacionGarantia)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFdelegacionGarantia.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFdelegacionGarantia)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, "");
                }

                ////////
                ///
                if (fiador.FiadorFdomicilio != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFdomicilio)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFdomicilio.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFdomicilio)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFdomicilioGarantia != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFdomicilioGarantia)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFdomicilioGarantia.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFdomicilioGarantia)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFemail != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFemail)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFemail.ToLower());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFemail)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFempresa != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFempresa)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFempresa.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFempresa)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.TipoIdentificacion != null)
                {
                    pattern = @"\bFiadorF" + $"{nameof(fiador.TipoIdentificacion)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, fiador.TipoIdentificacion.ToUpper());
                }
                else
                {
                    pattern = @"\bFiadorF" + $"{nameof(fiador.TipoIdentificacion)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.Licenciado != null)
                {
                    pattern = @"\bFiadorF" + $"{nameof(fiador.Licenciado)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, fiador.Licenciado.ToUpper());
                }
                else
                {
                    pattern = @"\bFiadorF" + $"{nameof(fiador.Licenciado)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (fiador.FiadorFtelefono != null)
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFtelefono)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, fiador.FiadorFtelefono.ToUpper());
                }
                else
                {
                    pattern = @"\b" + $"{nameof(fiador.FiadorFtelefono)}{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (fiador.TipoInmuebleId > 0 || fiador.TipoInmuebleId != null)
                {
                    foreach (var t in TipoInmueble)
                    {
                        if (t.TipoInmobiliarioId == fiador.TipoInmuebleId)
                        {
                            pattern = @"\bFiadorFTipoInmueble" + $"{identificador.Trim()}{fin.Trim()}";
                            docText = Regex.Replace(docText, pattern, t.TipoInmobiliarioDesc.ToUpper().Trim());
                        }
                    }

                }
                else
                {
                    pattern = @"\bFiadorFTipoInmueble" + $"{identificador.Trim()}{fin.Trim()}";
                    docText = Regex.Replace(docText, pattern, "");
                }

                contador++;

            }

            if (FiadorFNombresAgrupados != null)
            {
                pattern = string.Empty;
                pattern = @"\b" + "FiadorFNombres1" + @"\b";
                docText = Regex.Replace(docText, pattern, FiadorFNombresAgrupados.ToUpper());
            }
            else
            {
                pattern = string.Empty;
                pattern = @"\b" + "FiadorFNombres1" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (TipoIdentificacionAgrupados != null)
            {
                pattern = string.Empty;
                pattern = @"\b" + "FiadorFTipoIdentificacion1" + @"\b";
                docText = Regex.Replace(docText, pattern, TipoIdentificacionAgrupados.ToUpper());
            }
            else
            {
                pattern = string.Empty;
                pattern = @"\b" + "FiadorFTTipoIdentificacion1" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }
            if (NumeroIdentificacionAgrupados != null)
            {
                pattern = @"\bFiadorF" + "NumeroIdentificacion1" + @"\b";
                docText = Regex.Replace(docText, pattern, NumeroIdentificacionAgrupados.ToUpper());
            }
            else
            {
                pattern = @"\bFiadorF" + "NumeroIdentificacion1" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            return docText;
        }
    }
}
