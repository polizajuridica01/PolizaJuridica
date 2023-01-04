using PolizaJuridica.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolizaJuridica.Utilerias
{
    public class KeywordsFisicaMoral
    {
        public static String SolicitudArrendatario(string docText, FisicaMoral fisicaMoral, List<Arrendatario> arrendatarios)
        {
            string NombreArrendatario = string.Empty;
            string ArrendatarioApeMat = string.Empty;
            string TipoIdentificacionArrendatario = string.Empty;
            string NumeroIdentificacionArrendatario = string.Empty;
            string nacArrendatario = string.Empty;
            string pattern = string.Empty;

            if (fisicaMoral.SfisicaApeMat != null)
                ArrendatarioApeMat = fisicaMoral.SfisicaApeMat.ToUpper();

            if (fisicaMoral.TipoIdentificacion != null)
                TipoIdentificacionArrendatario = fisicaMoral.TipoIdentificacion.ToUpper();

            if (fisicaMoral.NumeroIdentificacion != null)
                NumeroIdentificacionArrendatario = fisicaMoral.NumeroIdentificacion;

            NombreArrendatario = fisicaMoral.SfisicaNombre.ToUpper() + " " + fisicaMoral.SfisicaApePat.ToUpper() + " " + ArrendatarioApeMat;

            if (fisicaMoral.SfisicaNacionallidad != null)
                nacArrendatario = fisicaMoral.SfisicaNacionallidad;

            //Sección del arrendatario

            if (arrendatarios.Count > 0)
            {
                string Arrendatario = string.Empty;
                string numeoidentificacion = string.Empty;
                string tipoidentificacion = string.Empty;
                string yoindistintamente = string.Empty;
                string apeMaterno = string.Empty;
                yoindistintamente = " Y/O INDISTINTAMENTE ";

                Arrendatario = NombreArrendatario.ToUpper().Trim() + yoindistintamente;
                numeoidentificacion = NumeroIdentificacionArrendatario + "/";
                tipoidentificacion = TipoIdentificacionArrendatario + "/";


                int contador = 0;
                foreach (var a in arrendatarios)
                {
                    contador++;
                    apeMaterno = String.Empty;
                    if (arrendatarios.Count == 1)
                    {
                        apeMaterno = String.IsNullOrEmpty(a.ApeMaterno) ? "" : a.ApeMaterno.ToUpper().Trim();
                        Arrendatario += " " + a.Nombre.ToUpper().Trim() + " " +
                            a.ApePaterno.ToUpper().Trim()
                            + " " + apeMaterno;
                        numeoidentificacion += a.NumeroIdentificacion;
                        tipoidentificacion = a.TipoIdentificacion + "/";
                    }
                    else
                    {
                        if (contador == arrendatarios.Count)
                        {
                            apeMaterno = String.IsNullOrEmpty(a.ApeMaterno) ? "" : a.ApeMaterno.ToUpper().Trim();
                            Arrendatario += " " + a.Nombre.ToUpper().Trim() + " " +
                                a.ApePaterno.ToUpper().Trim()
                                + " " + apeMaterno;
                            numeoidentificacion += a.NumeroIdentificacion;
                            tipoidentificacion = a.TipoIdentificacion + "/";
                        }
                        else
                        {
                            apeMaterno = String.IsNullOrEmpty(a.ApeMaterno) ? "" : a.ApeMaterno.ToUpper().Trim();
                            Arrendatario += a.Nombre.ToUpper().Trim() + " " + a.ApePaterno.ToUpper().Trim() + " " + apeMaterno + yoindistintamente;
                            numeoidentificacion += a.NumeroIdentificacion + "/";
                            tipoidentificacion = a.TipoIdentificacion + "/";
                        }
                    }
                }

                pattern = @"\b" + "NOMBREARRENDATARIO" + @"\b";
                docText = Regex.Replace(docText, pattern, Arrendatario);

                pattern = @"\b" + "NUMIDENTARRENDATARIO" + @"\b";
                docText = Regex.Replace(docText, pattern, NumeroIdentificacionArrendatario.ToUpper().Trim());

                pattern = @"\b" + "TIPOIDENTARRENDATARIO" + @"\b";
                docText = Regex.Replace(docText, pattern, TipoIdentificacionArrendatario.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + "NOMBREARRENDATARIO" + @"\b";
                docText = Regex.Replace(docText, pattern, NombreArrendatario.ToUpper().Trim());

                pattern = @"\b" + "NUMIDENTARRENDATARIO" + @"\b";
                docText = Regex.Replace(docText, pattern, NumeroIdentificacionArrendatario.ToUpper().Trim());

                pattern = @"\b" + "TIPOIDENTARRENDATARIO" + @"\b";
                docText = Regex.Replace(docText, pattern, TipoIdentificacionArrendatario.ToUpper().Trim());
            }


            pattern = @"\b" + "NACIONALIDADARRENDATARIO" + @"\b";
            docText = Regex.Replace(docText, pattern, nacArrendatario.ToUpper().Trim());

            /////////////////////////////
            ///


            if (fisicaMoral.AfianzadoraRl != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.AfianzadoraRl) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.AfianzadoraRl.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.AfianzadoraRl) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.AfianzadoRl == true)
            {
                pattern = @"\b" + nameof(fisicaMoral.AfianzadoRl) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.AfianzadoRl.ToString().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.AfianzadoRl) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.ColoniaRl != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.ColoniaRl) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.ColoniaRl.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.ColoniaRl) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.DeleMuni != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.DeleMuni) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.DeleMuni.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.DeleMuni) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.DomicilioRepresentanteLegal != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.DomicilioRepresentanteLegal) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.DomicilioRepresentanteLegal.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.DomicilioRepresentanteLegal) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.EstadoRl != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.EstadoRl) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.EstadoRl.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.EstadoRl) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.HorarioRl != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.HorarioRl) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.HorarioRl.Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.HorarioRl) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.IngresoMensualRl > 0)
            {
                pattern = @"\b" + nameof(fisicaMoral.IngresoMensualRl) + @"\b";
                docText = Regex.Replace(docText, pattern, ConvertNumbertoText.NumToLetter(fisicaMoral.IngresoMensualRl.ToString().Trim(), "MX").ToUpper());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.IngresoMensualRl) + @"\b";
                docText = Regex.Replace(docText, pattern, "0");
            }

            if (fisicaMoral.NumeroIdentificacion != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.NumeroIdentificacion) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.NumeroIdentificacion.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.NumeroIdentificacion) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.RequiereFacturaRl == true)
            {
                pattern = @"\b" + nameof(fisicaMoral.RequiereFacturaRl) + @"\b";
                docText = Regex.Replace(docText, pattern, "X");
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.RequiereFacturaRl) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.SfisicaAntiguedad != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaAntiguedad) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaAntiguedad.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaAntiguedad) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaApeMat != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaApeMat) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaApeMat.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaApeMat) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaApePat != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaApePat) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaApePat.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaApePat) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }
            docText = docText.Replace(nameof(fisicaMoral.SfisicaApePat), fisicaMoral.SfisicaApePat.ToUpper());

            if (fisicaMoral.SfisicaCelular != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaCelular) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaCelular.ToString().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaCelular) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaCodigoPostal != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaCodigoPostal) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaCodigoPostal.ToString().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaCodigoPostal) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaCodigoPostalTrabajo != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaCodigoPostalTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaCodigoPostalTrabajo.ToString().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaCodigoPostalTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaColonia != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaColonia) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaColonia.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaColonia) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.SfisicaColoniaTrabajo != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaColoniaTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaColoniaTrabajo.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaColoniaTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaCondMigratoria != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaCondMigratoria) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaCondMigratoria.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaCondMigratoria) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaDelegacionMunicipio != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaDelegacionMunicipio) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaDelegacionMunicipio.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaDelegacionMunicipio) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaDelegMuniTrabajo != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaDelegMuniTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaDelegMuniTrabajo.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaDelegMuniTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaDomicilio != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaDomicilio) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaDomicilio.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaDomicilio) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaDomicilio != null && fisicaMoral.SfisicaColonia != null && fisicaMoral.SfisicaDelegacionMunicipio != null && fisicaMoral.SfisicaCodigoPostal != null && fisicaMoral.SfisicaEstado != null)
            {
                var direccion = fisicaMoral.SfisicaDomicilio.ToUpper().Trim() + ", " + fisicaMoral.SfisicaColonia.ToUpper().Trim() + ", " + fisicaMoral.SfisicaDelegacionMunicipio.ToUpper().Trim() + ", CÓDIGO POSTAL:" + fisicaMoral.SfisicaCodigoPostal + ", " + fisicaMoral.SfisicaEstado.ToUpper().Trim();
                pattern = @"\b" + "ArrendatarioDireccionPrincipal" + @"\b";
                docText = Regex.Replace(docText, pattern, direccion);
            }
            else
            {
                pattern = @"\b" + "ArrendatarioDireccionPrincipal" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaDomicilioTrabajo != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaDomicilioTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaDomicilioTrabajo.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaDomicilioTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaEmail != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaEmail) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaEmail.ToLower().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaEmail) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaEmailJefe != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaEmailJefe) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaEmailJefe.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaEmailJefe) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaEstado != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaEstado) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaEstado.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaEstado) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaEstadoCivil != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaEstadoCivil) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaEstadoCivil.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaEstadoCivil) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaEstadoTrabajo != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaEstadoTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaEstadoTrabajo.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaEstadoTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaFactura == true)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaFactura) + @"\b";
                docText = Regex.Replace(docText, pattern, "X");
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaFactura) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaGiroTrabajo != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaGiroTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaGiroTrabajo.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaGiroTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaHorario != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaHorario) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaHorario.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaHorario) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaIngresoMensual > 0)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaIngresoMensual) + @"\b";
                docText = Regex.Replace(docText, pattern, ConvertNumbertoText.NumToLetter(fisicaMoral.SfisicaIngresoMensual.ToString().Trim(), "MX").ToUpper());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaIngresoMensual) + @"\b";
                docText = Regex.Replace(docText, pattern, "0");
            }

            if (fisicaMoral.SfisicaJefeTrabajo != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaJefeTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaJefeTrabajo.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaJefeTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaNacionallidad != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaNacionallidad) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaNacionallidad.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaNacionallidad) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaNombre != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaNombre) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaNombre.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaNombre) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaProfesion != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaProfesion) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaProfesion.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaProfesion) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaPuesto != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaPuesto) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaPuesto.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaPuesto) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaPuestoJefe != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaPuestoJefe) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaPuestoJefe.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaPuestoJefe) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.SfisicaRazonSocial != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaRazonSocial) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaRazonSocial.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaRazonSocial) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.SfisicaRfc != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaRfc) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaRfc.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaRfc) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.SfisicaTelefono != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaTelefono) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaTelefono.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaTelefono) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaHorario != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaHorario) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaHorario.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaHorario) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.SfisicaTelefonoTrabajo != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaTelefonoTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaTelefonoTrabajo.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaTelefonoTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.SfisicaTrabajo != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaTrabajo.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.SfisicaWebTrabajo != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaWebTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SfisicaWebTrabajo.Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SfisicaWebTrabajo) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.SindicadoRl != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.SindicadoRl) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.SindicadoRl.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.SindicadoRl) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.TelefonoRl != null)
            {
                pattern = @"\b" + nameof(fisicaMoral.TelefonoRl) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.TelefonoRl.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + nameof(fisicaMoral.TelefonoRl) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.TipoIdentificacion != null)
            {
                pattern = @"\b" + "Arrendatario" + nameof(fisicaMoral.TipoIdentificacion) + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.TipoIdentificacion.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + "Arrendatario" + nameof(fisicaMoral.TipoIdentificacion) + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.NumRppcons != null)
            {
                pattern = @"\b" + "ArrendatarioNumRPPCons" + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.NumRppcons.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + "ArrendatarioNumRPPCons" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.EscrituraNumero != null)
            {
                pattern = @"\b" + "ArrendatarioEscrituraNumero" + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.EscrituraNumero.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + "ArrendatarioEscrituraNumero" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.Licenciado != null)
            {
                pattern = @"\b" + "ArrendatarioLicenciado" + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.Licenciado.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + "ArrendatarioLicenciado" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.NumeroNotaria != null)
            {
                pattern = @"\b" + "ArrendatarioNumeroNotaria" + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.NumeroNotaria.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + "ArrendatarioNumeroNotaria" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.FechaRppcons.GetHashCode() != 0)
            {
                pattern = @"\b" + "ArrendatarioFechaRppcons" + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.FechaRppcons.ToString().Trim());
            }
            else
            {
                pattern = @"\b" + "ArrendatarioFechaRppcons" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.NumEscPoder != null)
            {
                pattern = @"\b" + "ArrendatarioNumEscPoder" + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.NumEscPoder.ToString().Trim());
            }
            else
            {
                pattern = @"\b" + "ArrendatarioNumEscPoder" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.TitularNotaPoder != null)
            {
                pattern = @"\b" + "ArrendatarioTitularNotariaPoder" + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.TitularNotaPoder.ToUpper().Trim());
            }
            else
            {
                pattern = @"\b" + "ArrendatarioTitularNotariaPoder" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }


            if (fisicaMoral.NumNotaria != null)
            {
                pattern = @"\b" + "ArrendatarioNumeNotaria" + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.NumNotaria.Trim());
            }
            else
            {
                pattern = @"\b" + "ArrendatarioNumeNotaria" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.FechaEmitePoder.GetHashCode() != 0)
            {
                pattern = @"\b" + "ArrendatarioFechaPoder" + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.FechaEmitePoder.ToString());
            }
            else
            {
                pattern = @"\b" + "ArrendatarioFechaPoder" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            if (fisicaMoral.FechaConstitutiva.GetHashCode() != 0)
            {
                pattern = @"\b" + "ArrendatarioFechaConstitutiva" + @"\b";
                docText = Regex.Replace(docText, pattern, fisicaMoral.FechaConstitutiva.ToString());
            }
            else
            {
                pattern = @"\b" + "ArrendatarioFechaConstitutiva" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }
            ///////////////
            ///
            pattern = @"\b" + nameof(fisicaMoral.TelefonoRl) + @"\b";
            docText = Regex.Replace(docText, pattern, fisicaMoral.TelefonoRl != null ? fisicaMoral.TelefonoRl.Trim() : "");
            pattern = String.Empty;

            pattern = @"\b" + nameof(fisicaMoral.EmailRl) + @"\b";
            docText = Regex.Replace(docText, pattern, fisicaMoral.EmailRl != null ? fisicaMoral.EmailRl.Trim() : "");
            pattern = String.Empty;

            return docText;
        }

    }
}
