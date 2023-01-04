using PolizaJuridica.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace PolizaJuridica.Utilerias
{
    public class Keywords
    {
        public static String SolicitudOtorgamiento(string docText, FisicaMoral fisicaMoral)
        {
            Solicitud s = fisicaMoral.Solicitud;
            string ArrendadorApeMat = string.Empty;
            string NombreArrendador = string.Empty;
            string TipoIdentificacionArrendador = string.Empty;
            string NumeroIdentificacionArrendador = string.Empty;
            string domicilioArrendador = string.Empty;
            string DomicilioArrendar = string.Empty;
            string nacArrendador = string.Empty;

            string VigenciaContrato = ConvertDateToText.DateToText(fisicaMoral.Solicitud.SolicitudVigenciaContratoI).ToUpper() + " AL " + ConvertDateToText.DateToText(fisicaMoral.Solicitud.SolicitudVigenciaContratoF).ToUpper();
            string ImporteMensual = ConvertNumbertoText.NumToLetter(fisicaMoral.Solicitud.SolicitudImporteMensual.ToString().Trim(), "MX").ToUpper();
            string ImporteMantenimiento = ConvertNumbertoText.NumToLetter(fisicaMoral.Solicitud.SolicitudCuotaMant.ToString().Trim(), "MX").ToUpper();
            string DepGarantia = ConvertNumbertoText.NumToLetter(fisicaMoral.Solicitud.SolicitudDepositoGarantia.ToString().Trim(), "MX").ToUpper();
            string jurisdiccion = string.Empty;
            //datos de las fomas de pago
            string CUENTATRANS = string.Empty;
            string BANCOTRANS = string.Empty;
            string NOMBRECUENTATRANS = string.Empty;
            string CLABETRANS = string.Empty;
            string FechaFirma = string.Empty;
            string DIAPENALIDAD = ConvertDateToText.DateToText(fisicaMoral.Solicitud.SolicitudVigenciaContratoF.AddDays(1)).ToUpper();
            string DIAPAGO = fisicaMoral.Solicitud.SolicitudVigenciaContratoI.Day.ToString();
            string pattern = string.Empty;

            //Fecha para los pagares acorde a la vigencia del contrato
            int anos = s.SolicitudVigenciaContratoF.Year - s.SolicitudVigenciaContratoF.Year;
            if (anos == 0)
                anos = 1;
            int meses = Math.Abs(12 * anos);
            for (int i = 1; i <= meses; i++)
            {
                string fecha = string.Empty;
                string fechavalor = string.Empty;
                pattern = string.Empty;
                fecha = @"\bFECHAPAGARE" + i.ToString().Trim() + @"\b";
                fechavalor = ConvertDateToText.DateToText(s.SolicitudVigenciaContratoI.AddMonths(i - 1)).ToUpper().Trim();
                docText = Regex.Replace(docText, fecha, fechavalor);

                fecha = string.Empty;
                fechavalor = string.Empty;
            }

            if (fisicaMoral.Solicitud.SolicitudRepresentanteLegal != null && fisicaMoral.Solicitud.SolicitudApePaternoLegal != null)
            {
                var solicitudRepresentanteLegal = string.Empty;

                if (fisicaMoral.Solicitud.SolicitudApeMaternoLegal != null)
                {
                    solicitudRepresentanteLegal = fisicaMoral.Solicitud.SolicitudRepresentanteLegal + " " + fisicaMoral.Solicitud.SolicitudApePaternoLegal + " " + fisicaMoral.Solicitud.SolicitudApeMaternoLegal;
                }
                else
                {
                    solicitudRepresentanteLegal = fisicaMoral.Solicitud.SolicitudRepresentanteLegal + " " + fisicaMoral.Solicitud.SolicitudApePaternoLegal;
                }

                docText = docText.Replace("SolicitudRepresentanteLegal", solicitudRepresentanteLegal.ToUpper());
            }



            //datos del telefono e email del asesor inmobiliario

            if (fisicaMoral.Solicitud.Asesorid > 0)
            {
                if (fisicaMoral.Solicitud.Asesor.UsuarioTelefono != null)
                {
                    docText = docText.Replace("SolicitudAsesorTelefono", fisicaMoral.Solicitud.Asesor.UsuarioTelefono);
                }
                else
                {
                    docText = docText.Replace("SolicitudAsesorTelefono", "");
                }

                if (fisicaMoral.Solicitud.Asesor.UsurioEmail != null)
                {
                    docText = docText.Replace("SolicitudAsesorEmail", fisicaMoral.Solicitud.Asesor.UsurioEmail);
                }
                else
                {
                    docText = docText.Replace("SolicitudAsesorEmail", "");
                }
            }
            else
            {
                if (fisicaMoral.Solicitud.SolicitudAdmiInmueble == true)
                {
                    if (fisicaMoral.Solicitud.Asesor != null)
                    {
                        if (fisicaMoral.Solicitud.Asesor.UsuarioTelefono != null)
                        {
                            docText = docText.Replace("SolicitudAsesorTelefono", fisicaMoral.Solicitud.SolicitudCelularProp);
                        }
                        else
                        {
                            docText = docText.Replace("SolicitudAsesorTelefono", "");
                        }

                        if (fisicaMoral.Solicitud.Asesor.UsurioEmail != null)
                        {
                            docText = docText.Replace("SolicitudAsesorEmail", fisicaMoral.Solicitud.SolicitudEmailProp.ToLower());
                        }
                        else
                        {
                            docText = docText.Replace("SolicitudAsesorEmail", "");
                        }
                    }
                }
                else
                {
                    docText = docText.Replace("SolicitudAsesorTelefono", "");

                    docText = docText.Replace("SolicitudAsesorEmail", "");
                }


            }


            //fin de la sección

            if (fisicaMoral.Solicitud.SolicitudApeMaternoProp != null)
                ArrendadorApeMat = fisicaMoral.Solicitud.SolicitudApeMaternoProp.ToUpper();
            if (fisicaMoral.Solicitud.SolicitudNombreProp != null)
            {
                if (fisicaMoral.Solicitud.SolicitudApePaternoProp != null)
                    NombreArrendador = fisicaMoral.Solicitud.SolicitudNombreProp.ToUpper().Trim() + " " + fisicaMoral.Solicitud.SolicitudApePaternoProp.ToUpper().Trim() + " " + ArrendadorApeMat.Trim();
            }


            if (fisicaMoral.Solicitud.TipoIdentificacion != null)
                TipoIdentificacionArrendador = fisicaMoral.Solicitud.TipoIdentificacion.ToUpper();

            if (fisicaMoral.Solicitud.NumeroIdentificacion != null)
                NumeroIdentificacionArrendador = fisicaMoral.Solicitud.NumeroIdentificacion;

            if (fisicaMoral.Solicitud.SolicitudNacionalidad != null)
            {
                nacArrendador = fisicaMoral.Solicitud.SolicitudNacionalidad;
                docText = docText.Replace(nameof(fisicaMoral.Solicitud.SolicitudNacionalidad), fisicaMoral.Solicitud.SolicitudNacionalidad);
            }
            else
            {
                docText = docText.Replace(nameof(fisicaMoral.Solicitud.SolicitudNacionalidad), "");
            }



            domicilioArrendador = fisicaMoral.Solicitud.SolicitudDomicilioProp.ToUpper() + ", " + fisicaMoral.Solicitud.ColoniaActual.ToUpper() + ", " + fisicaMoral.Solicitud.AlcaldiaMunicipioActual.ToUpper() + ", " + fisicaMoral.Solicitud.EstadoActual.ToUpper() + ", CÓDIGO POSTAL : " + fisicaMoral.Solicitud.CodigoPostalActual.ToUpper();

            DomicilioArrendar = fisicaMoral.Solicitud.SolicitudUbicacionArrendado.ToUpper() + ", " + fisicaMoral.Solicitud.ColoniaArrendar.ToUpper() + ", " + fisicaMoral.Solicitud.AlcaldiaMunicipioArrendar.ToUpper() + ", " + fisicaMoral.Solicitud.Estados.EstadoNombre.ToUpper() + ", CÓDIGO POSTAL : " + fisicaMoral.Solicitud.CodigoPostalArrendar.ToUpper();

            //datos de formas de pago
            if (fisicaMoral.Solicitud.SolicitudCuenta != null)
                CUENTATRANS = fisicaMoral.Solicitud.SolicitudCuenta;

            if (fisicaMoral.Solicitud.SolicitudBanco != null)
                BANCOTRANS = fisicaMoral.Solicitud.SolicitudBanco;

            if (fisicaMoral.Solicitud.SolicitudNombreCuenta != null)
                NOMBRECUENTATRANS = fisicaMoral.Solicitud.SolicitudNombreCuenta;

            if (fisicaMoral.Solicitud.SolicitudClabe != null)
                CLABETRANS = fisicaMoral.Solicitud.SolicitudClabe;

            //seccion general 
            if (fisicaMoral.Solicitud.Jurisdiccion != null)
                jurisdiccion = fisicaMoral.Solicitud.Jurisdiccion.ToUpper();

            if (fisicaMoral.Solicitud.SolicitudFechaFirma != null)
                FechaFirma = ConvertDateToText.DateToText(fisicaMoral.Solicitud.SolicitudVigenciaContratoI).ToUpper().Trim();



            if (NombreArrendador != null)
            {
                docText = docText.Replace("NOMBREARRENDADOR", NombreArrendador.ToUpper());
            }
            else
            {
                docText = docText.Replace("NOMBREARRENDADOR", "");
            }

            if (TipoIdentificacionArrendador != null)
            {
                docText = docText.Replace("TIPOIDENTARRENDADOR", TipoIdentificacionArrendador.ToUpper());
            }
            else
            {
                docText = docText.Replace("TIPOIDENTARRENDADOR", "");
            }

            if (NumeroIdentificacionArrendador != null)
            {
                docText = docText.Replace("NUMIDENTARRENDADOR", NumeroIdentificacionArrendador.ToUpper());
            }
            else
            {
                docText = docText.Replace("NUMIDENTARRENDADOR", "");
            }

            if (domicilioArrendador != null)
            {
                docText = docText.Replace("DIRECCIONARRENDADOR", domicilioArrendador.ToUpper());
            }
            else
            {
                docText = docText.Replace("DIRECCIONARRENDADOR", "");
            }

            if (DomicilioArrendar != null)
            {
                docText = docText.Replace("DIRECCIONARRENDAR", DomicilioArrendar.ToUpper().Trim());
            }
            else
            {
                docText = docText.Replace("DIRECCIONARRENDAR", "");
            }

            if (nacArrendador != null)
            {
                docText = docText.Replace("NACIONALIDADARRENDADOR", nacArrendador.ToUpper());
            }
            else
            {
                docText = docText.Replace("NACIONALIDADARRENDADOR", "");
            }

            //datos de la forma de pago

            if (CUENTATRANS != null)
            {
                docText = docText.Replace("CUENTATRANS", CUENTATRANS.ToUpper());
            }
            else
            {
                docText = docText.Replace("CUENTATRANS", "");
            }

            if (BANCOTRANS != null)
            {
                docText = docText.Replace("BANCOTRANS", BANCOTRANS.ToUpper());
            }
            else
            {
                docText = docText.Replace("BANCOTRANS", "");
            }

            if (NOMBRECUENTATRANS != null)
            {
                docText = docText.Replace("NOMBRECUENTA", NOMBRECUENTATRANS.ToUpper());
            }
            else
            {
                docText = docText.Replace("NOMBRECUENTA", "");
            }

            if (CLABETRANS != null)
            {
                docText = docText.Replace("CLABETRANS", CLABETRANS.ToUpper().Trim());
            }
            else
            {
                docText = docText.Replace("CLABETRANS", "");
            }

            //datos generales 

            if (ImporteMensual != null)
            {
                docText = docText.Replace("IMPORTERENTA", ImporteMensual.ToUpper().Trim());
                docText = docText.Replace("IMPORTEVALOR", s.SolicitudImporteMensual.ToString().Trim());
            }
            else
            {
                docText = docText.Replace("IMPORTERENTA", "");
                docText = docText.Replace("IMPORTEVALOR", "");
            }

            if (ImporteMantenimiento != null)
            {
                docText = docText.Replace("IMPORTEMANTENIMIENTO", ImporteMantenimiento.ToUpper().Trim());
            }
            else
            {
                docText = docText.Replace("IMPORTEMANTENIMIENTO", "");
            }

            if (DepGarantia != null)
            {
                docText = docText.Replace("DEPOSITOGARANTIA", DepGarantia.ToUpper()).Trim();
            }
            else
            {
                docText = docText.Replace("DEPOSITOGARANTIA", "");
            }

            if (VigenciaContrato != null)
            {
                docText = docText.Replace("VIGENCIACONTRATO", VigenciaContrato.ToUpper().Trim());
            }
            else
            {
                docText = docText.Replace("VIGENCIACONTRATO", "");
            }

            if (jurisdiccion != null)
            {
                docText = docText.Replace("JURISDICCIONCONTRATO", jurisdiccion.ToUpper().Trim());
            }
            else
            {
                docText = docText.Replace("JURISDICCIONCONTRATO", "");
            }

            if (FechaFirma != null)
            {
                docText = docText.Replace("FECHAFIRMA", FechaFirma.ToUpper());
            }
            else
            {
                docText = docText.Replace("FECHAFIRMA", "");
            }

            if (DIAPENALIDAD != null)
            {
                docText = docText.Replace("DIAPENALIDAD", DIAPENALIDAD.ToUpper());
            }
            else
            {
                docText = docText.Replace("DIAPENALIDAD", "");
            }

            if (DIAPAGO != null)
            {
                docText = docText.Replace("DIAPAGO", DIAPAGO);
            }
            else
            {
                docText = docText.Replace("DIAPAGO", "");
            }

            if (s.AlcaldiaMunicipioActual != null)
            {
                docText = docText.Replace(nameof(s.AlcaldiaMunicipioActual), s.AlcaldiaMunicipioActual.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.AlcaldiaMunicipioActual), "");
            }

            if (s.AlcaldiaMunicipioArrendar != null)
            {
                docText = docText.Replace(nameof(s.AlcaldiaMunicipioArrendar), s.AlcaldiaMunicipioArrendar.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.AlcaldiaMunicipioArrendar), "");
            }

            if (s.ApeMat1 != null)
            {
                docText = docText.Replace(nameof(s.ApeMat1), s.ApeMat1.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.ApeMat1), "");
            }

            if (s.ApeMat2 != null)
            {
                docText = docText.Replace(nameof(s.ApeMat2), s.ApeMat2.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.ApeMat2), "");
            }


            if (s.ApeMat3 != null)
            {
                docText = docText.Replace(nameof(s.ApeMat3), s.ApeMat3.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.ApeMat3), "");
            }


            if (s.ApePat1 != null)
            {
                docText = docText.Replace(nameof(s.ApePat1), s.ApePat1.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.ApePat1), "");
            }


            if (s.ApePat2 != null)
            {
                docText = docText.Replace(nameof(s.ApePat2), s.ApePat2.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.ApePat2), "");
            }


            if (s.ApePat3 != null)
            {
                docText = docText.Replace(nameof(s.ApePat3), s.ApePat3.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.ApePat3), "");
            }


            if (s.ArrendatarioApeMat != null)
            {
                docText = docText.Replace(nameof(s.ArrendatarioApeMat), s.ArrendatarioApeMat.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.ArrendatarioApeMat), "");
            }


            if (s.ArrendatarioApePat != null)
            {
                docText = docText.Replace(nameof(s.ArrendatarioApePat), s.ArrendatarioApePat.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.ArrendatarioApePat), "");
            }

            if (s.ArrendatarioCorreo != null)
            {
                docText = docText.Replace(nameof(s.ArrendatarioCorreo), s.ArrendatarioCorreo.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.ArrendatarioCorreo), "");
            }


            if (s.ArrendatarioNombre != null)
            {
                docText = docText.Replace(nameof(s.ArrendatarioNombre), s.ArrendatarioNombre.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.ArrendatarioNombre), "");
            }

            if (s.ArrendatarioTelefono != null)
            {
                docText = docText.Replace(nameof(s.ArrendatarioTelefono), s.ArrendatarioTelefono.ToUpper().Trim());
            }
            else
            {
                docText = docText.Replace(nameof(s.ArrendatarioTelefono), "");
            }


            if (fisicaMoral.Solicitud.Asesorid != null)
            {
                docText = docText.Replace("AsesorUsuarioNomCompleto", fisicaMoral.Solicitud.Asesor.UsuarioNomCompleto.ToUpper());
            }
            else
            {
                if (fisicaMoral.Solicitud.SolicitudAdmiInmueble == true)
                {
                    docText = docText.Replace("AsesorUsuarioNomCompleto", "ES PROPIETARIO");
                }
                else
                {
                    docText = docText.Replace("AsesorUsuarioNomCompleto", "");
                }

            }



            if (s.CentroCostosId != null)
            {
                docText = docText.Replace(nameof(s.CentroCostos), ConvertNumbertoText.NumToLetter(s.CentroCostos.CentroCostosMonto.ToString().Trim(), "MX").ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.CentroCostos), ConvertNumbertoText.NumToLetter(s.CostoPoliza.ToString().Trim(), "MX").ToUpper());
            }


            if (s.CodigoPostalActual != null)
            {
                docText = docText.Replace(nameof(s.CodigoPostalActual), s.CodigoPostalActual.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.CodigoPostalActual), "");
            }

            if (s.CodigoPostalArrendar != null)
            {
                docText = docText.Replace(nameof(s.CodigoPostalArrendar), s.CodigoPostalArrendar.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.CodigoPostalArrendar), "");
            }

            if (s.EstadoActual != null)
            {
                docText = docText.Replace(nameof(s.EstadoActual), s.EstadoActual.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.EstadoActual), "");
            }

            if (s.Estados.EstadoNombre != null)
            {
                docText = docText.Replace(nameof(s.Estados.EstadoNombre), s.Estados.EstadoNombre.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.Estados.EstadoNombre), "");
            }


            if (s.Inmobiliaria != null)
            {
                docText = docText.Replace("InmobiliariaSolicitud", s.Inmobiliaria.ToUpper());
            }
            else
            {
                if (s.SolicitudAdmiInmueble == true)
                {
                    docText = docText.Replace("InmobiliariaSolicitud", "ARRENDADOR");
                }
                else
                {
                    docText = docText.Replace("InmobiliariaSolicitud", "");
                }

            }


            if (s.Jurisdiccion != null)
            {
                docText = docText.Replace(nameof(s.Jurisdiccion), s.Jurisdiccion.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.Jurisdiccion), "");
            }


            if (s.Nombre1 != null)
            {
                docText = docText.Replace(nameof(s.Nombre1), s.Nombre1.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.Nombre1), "");
            }


            if (s.Nombre2 != null)
            {
                docText = docText.Replace(nameof(s.Nombre2), s.Nombre2.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.Nombre2), "");
            }


            if (s.Nombre3 != null)
            {
                docText = docText.Replace(nameof(s.Nombre3), s.Nombre3.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.Nombre3), "");
            }


            if (s.NumeroIdentificacion != null)
            {
                docText = docText.Replace("Solicitud" + nameof(s.NumeroIdentificacion), s.NumeroIdentificacion.ToUpper());
            }
            else
            {
                docText = docText.Replace("Solicitud" + nameof(s.NumeroIdentificacion), "");
            }

            if (s.NumIdent1 != null)
            {
                docText = docText.Replace(nameof(s.NumIdent1), s.NumIdent1.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.NumIdent1), "");
            }


            if (s.NumIdent2 != null)
            {
                docText = docText.Replace(nameof(s.NumIdent2), s.NumIdent2.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.NumIdent2), "");
            }



            if (s.NumIdent3 != null)
            {
                docText = docText.Replace(nameof(s.NumIdent3), s.NumIdent3.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.NumIdent3), "");
            }


            if (fisicaMoral.Solicitud.Representanteid != null)
            {
                docText = docText.Replace("RepresentanteUsuarioNomCompleto", fisicaMoral.Solicitud.Representante.UsuarioNomCompleto.ToUpper());
            }
            else
            {
                docText = docText.Replace("RepresentanteUsuarioNomCompleto", "");
            }


            docText = docText.Replace(nameof(s.SolicitudAdmiInmueble), s.SolicitudAdmiInmueble.ToString().ToUpper());

            if (s.SolicitudApeMaternoLegal != null)
            {
                docText = docText.Replace(nameof(s.SolicitudApeMaternoLegal), s.SolicitudApeMaternoLegal.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudApeMaternoLegal), "");
            }


            if (s.SolicitudApeMaternoProp != null)
            {
                docText = docText.Replace(nameof(s.SolicitudApeMaternoProp), s.SolicitudApeMaternoProp.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudApeMaternoProp), "");
            }


            if (s.SolicitudApePaternoLegal != null)
            {
                docText = docText.Replace(nameof(s.SolicitudApePaternoLegal), s.SolicitudApePaternoLegal.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudApePaternoLegal), "");
            }


            if (s.SolicitudApePaternoProp != null)
            {
                docText = docText.Replace(nameof(s.SolicitudApePaternoProp), s.SolicitudApePaternoProp.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudApePaternoProp), "");
            }


            if (s.SolicitudBanco != null)
            {
                docText = docText.Replace(nameof(s.SolicitudBanco), s.SolicitudBanco.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudBanco), "");
            }


            docText = docText.Replace(nameof(s.SolicitudCartaEntrega), s.SolicitudCartaEntrega.ToString().ToUpper());

            if (s.SolicitudCelularProp != null)
            {
                docText = docText.Replace(nameof(s.SolicitudCelularProp), s.SolicitudCelularProp.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudCelularProp), "");
            }

            if (s.SolicitudCelular != null)
            {
                docText = docText.Replace(nameof(s.SolicitudCelular) + "Asesor", s.SolicitudCelular.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudCelular) + "Asesor", "");
            }

            if (s.SolicitudClabe != null)
            {
                docText = docText.Replace(nameof(s.SolicitudClabe), s.SolicitudClabe.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudClabe), "");
            }


            if (s.SolicitudCuenta != null)
            {
                docText = docText.Replace(nameof(s.SolicitudCuenta), s.SolicitudCuenta.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudCuenta), "");
            }


            if (s.SolicitudCuotaMant > 0)
            {
                docText = docText.Replace(nameof(s.SolicitudCuotaMant), ConvertNumbertoText.NumToLetter(s.SolicitudCuotaMant.ToString().Trim(), "MX").ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudCuotaMant), "0");
            }


            if (s.SolicitudDepositoGarantia > 0)
            {
                docText = docText.Replace(nameof(s.SolicitudDepositoGarantia), ConvertNumbertoText.NumToLetter(s.SolicitudDepositoGarantia.ToString().Trim(), "MX").ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudDepositoGarantia), "0");
            }

            if (s.SolicitudDestinoArrendamien != null)
            {
                docText = docText.Replace(nameof(s.SolicitudDestinoArrendamien), s.SolicitudDestinoArrendamien.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudDestinoArrendamien), "");
            }


            if (s.SolicitudDomicilioProp != null)
            {
                docText = docText.Replace(nameof(s.SolicitudDomicilioProp), s.SolicitudDomicilioProp.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudDomicilioProp), "");
            }


            if (s.SolicitudEmail != null)
            {
                docText = docText.Replace(nameof(s.SolicitudEmail) + "Asesor", s.SolicitudEmail.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudEmail) + "Asesor", "");
            }


            if (s.SolicitudEmailProp != null)
            {
                docText = docText.Replace(nameof(s.SolicitudEmailProp), s.SolicitudEmailProp.ToLower());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudEmailProp), "");
            }

            docText = docText.Replace(nameof(s.SolicitudEsAdminInmueble), s.SolicitudEsAdminInmueble.ToString().ToUpper());

            if (s.SolicitudFechaFirma.GetHashCode() != 0)
                docText = docText.Replace(nameof(s.SolicitudFechaFirma), ConvertDateToText.DateToText(s.SolicitudFechaFirma).ToUpper().ToUpper());

            if (s.SolicitudFechaSolicitud.GetHashCode() != 0)
                docText = docText.Replace(nameof(s.SolicitudFechaSolicitud), ConvertDateToText.DateToText(s.SolicitudFechaSolicitud).ToUpper().ToUpper());

            docText = docText.Replace(nameof(s.SolicitudFiador), s.SolicitudFiador.ToString().ToUpper());

            if (s.SolicitudHoraFirma.GetHashCode() != 0)
                docText = docText.Replace(nameof(s.SolicitudHoraFirma), s.SolicitudHoraFirma.ToLocalTime().ToString().ToUpper());

            if (s.SolicitudImporteMensual > 0)
            {
                docText = docText.Replace(nameof(s.SolicitudImporteMensual), ConvertNumbertoText.NumToLetter(s.SolicitudImporteMensual.ToString().Trim(), "MX").ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudImporteMensual), "");
            }


            if (s.SolicitudIncluidaRenta == true)
            {
                docText = docText.Replace(nameof(s.SolicitudIncluidaRenta), "X");
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudIncluidaRenta), "");
            }



            if (s.SolicitudInmuebleGaran == true)
            {
                docText = docText.Replace(nameof(s.SolicitudInmuebleGaran), s.SolicitudInmuebleGaran.ToString().ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudInmuebleGaran), "");
            }

            if (s.SolicitudLugarFirma != null)
            {
                docText = docText.Replace(nameof(s.SolicitudLugarFirma), s.SolicitudLugarFirma.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudLugarFirma), "");
            }

            if (s.SolicitudNombreCuenta != null)
            {
                docText = docText.Replace(nameof(s.SolicitudNombreCuenta), s.SolicitudNombreCuenta.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudNombreCuenta), "");
            }


            if (s.SolicitudNombreProp != null)
            {
                docText = docText.Replace(nameof(s.SolicitudNombreProp), s.SolicitudNombreProp.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudNombreProp), "");
            }


            if (s.SolicitudNumero != null)
            {
                docText = docText.Replace(nameof(s.SolicitudNumero), s.SolicitudNumero.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudNumero), "");
            }


            if (s.SolicitudObservaciones != null)
            {
                docText = docText.Replace(nameof(s.SolicitudObservaciones), s.SolicitudObservaciones.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudObservaciones), "");
            }


            if (s.SolicitudPagare == true)
            {
                docText = docText.Replace(nameof(s.SolicitudPagare), "X");
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudPagare), "");
            }


            if (s.SolicitudPersonaSolicita != null)
            {
                docText = docText.Replace(nameof(s.SolicitudPersonaSolicita), s.SolicitudPersonaSolicita.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudPersonaSolicita), "");
            }


            if (s.SolicitudRazonSocial != null)
            {
                docText = docText.Replace(nameof(s.SolicitudRazonSocial), s.SolicitudRazonSocial.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudRazonSocial), "");
            }


            if (s.SolicitudRecibodePago != null)
            {
                docText = docText.Replace(nameof(s.SolicitudRecibodePago), s.SolicitudRecibodePago.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudRecibodePago), "");
            }


            if (s.SolicitudRepresentanteLegal != null)
            {
                docText = docText.Replace(nameof(s.SolicitudRepresentanteLegal), s.SolicitudRepresentanteLegal.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudRepresentanteLegal), "");
            }


            if (s.SolicitudRfc != null)
            {
                docText = docText.Replace(nameof(s.SolicitudRfc), s.SolicitudRfc.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudRfc), "");
            }

            if (s.SolicitudTelefonoProp != null)
            {
                docText = docText.Replace(nameof(s.SolicitudTelefonoProp), s.SolicitudTelefonoProp);
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudTelefonoProp), "");
            }

            if (s.SolicitudTelefono != null)
            {
                docText = docText.Replace(nameof(s.SolicitudTelefono), s.SolicitudTelefono.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudTelefono), "");
            }

            if (s.SolicitudTelefonoInmueble == true)
            {
                docText = docText.Replace(nameof(s.SolicitudTelefonoInmueble), "X");
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudTelefonoInmueble), "");
            }

            if (s.SolicitudTipoDeposito != null)
            {
                docText = docText.Replace(nameof(s.SolicitudTipoDeposito), s.SolicitudTipoDeposito.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudTipoDeposito), "");
            }


            if (s.SolicitudTipoPoliza != null)
            {
                docText = docText.Replace(nameof(s.SolicitudTipoPoliza), s.SolicitudTipoPoliza.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudTipoPoliza), "");
            }

            if (s.SolicitudUbicacionArrendado != null)
            {
                docText = docText.Replace(nameof(s.SolicitudUbicacionArrendado), s.SolicitudUbicacionArrendado.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.SolicitudUbicacionArrendado), "");
            }

            if (s.TipoArrendatario != null)
            {
                docText = docText.Replace(nameof(s.TipoArrendatario), s.TipoArrendatario.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.TipoArrendatario), "");
            }


            if (s.TipoIdent1 != null)
            {
                docText = docText.Replace(nameof(s.TipoIdent1), s.TipoIdent1.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.TipoIdent1), "");
            }


            if (s.TipoIdent2 != null)
            {
                docText = docText.Replace(nameof(s.TipoIdent2), s.TipoIdent2.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.TipoIdent2), "");
            }

            if (s.TipoIdent3 != null)
            {
                docText = docText.Replace(nameof(s.TipoIdent3), s.TipoIdent3.ToUpper());
            }
            else
            {
                docText = docText.Replace(nameof(s.TipoIdent3), "");
            }


            if (s.TipoIdentificacion != null)
            {
                docText = docText.Replace("Solicitud" + nameof(s.TipoIdentificacion), s.TipoIdentificacion.ToUpper());
            }
            else
            {
                docText = docText.Replace("Solicitud" + nameof(s.TipoIdentificacion), "");
            }


            if (fisicaMoral.Solicitud.TipoInmobiliario.TipoInmobiliarioId > 0)
                docText = docText.Replace(nameof(s.TipoInmobiliario.TipoInmobiliarioDesc), s.TipoInmobiliario.TipoInmobiliarioDesc.ToUpper());

            if (fisicaMoral.Solicitud.Representanteid > 0)
            {
                if (fisicaMoral.Solicitud.Representante.Representacion.OficinaEmisora != null)
                {
                    docText = docText.Replace("OficinaEmisora", fisicaMoral.Solicitud.Representante.Representacion.OficinaEmisora.ToUpper());
                }
                else
                {
                    docText = docText.Replace("OficinaEmisora", "");
                }
            }

            if (s.SolicitudVigenciaContratoI.GetHashCode() != 0)
                docText = docText.Replace(nameof(s.SolicitudVigenciaContratoI), ConvertDateToText.DateToText(s.SolicitudVigenciaContratoI).ToUpper());

            if (s.SolicitudVigenciaContratoF.GetHashCode() != 0)
                docText = docText.Replace(nameof(s.SolicitudVigenciaContratoF), ConvertDateToText.DateToText(s.SolicitudVigenciaContratoF).ToUpper());

            if (s.Estados.EstadoNombre != null)
            {
                docText = docText.Replace("EstadoArrendamiento", s.Estados.EstadoNombre.Trim());
            }
            else
            {
                docText = docText.Replace("EstadoArrendamiento", "");
            }

            if (s.NumRppcons != null)
            {
                docText = docText.Replace("SolicitudNumRPPCons", s.NumRppcons.Trim());
            }
            else
            {
                docText = docText.Replace("SolicitudNumRPPCons", "");
            }

            if (s.EscrituraNumero != null)
            {
                docText = docText.Replace("SolicitudEscrituraNumero", s.EscrituraNumero);
            }
            else
            {
                docText = docText.Replace("SolicitudEscrituraNumero", "");
            }

            if (s.Licenciado != null)
            {
                docText = docText.Replace("SolicitudLicenciado", s.Licenciado);
            }
            else
            {
                docText = docText.Replace("SolicitudLicenciado", "");
            }

            if (s.NumeroNotaria != null)
            {
                docText = docText.Replace("SolicitudNumeNotaria", s.NumeroNotaria);
            }
            else
            {
                docText = docText.Replace("SolicitudNumeNotaria", "");
            }

            if (s.FechaRppcons.GetHashCode() != 0)
            {
                docText = docText.Replace("SolicitudFechaRppcons", s.FechaRppcons.ToString());
            }
            else
            {
                docText = docText.Replace("SolicitudFechaRppcons", "");
            }

            if (s.NumEscPoder != null)
            {
                docText = docText.Replace("SolicitudNumEscPoder", s.NumEscPoder);
            }
            else
            {
                docText = docText.Replace("SolicitudNumEscPoder", "");
            }

            if (s.TitularNotaPoder != null)
            {
                docText = docText.Replace("SolicitudTitularNotariaPoder", s.TitularNotaPoder);
            }
            else
            {
                docText = docText.Replace("SolicitudTitularNotariaPoder", "");
            }

            if (s.NumeroNotaria != null)
            {
                docText = docText.Replace("SolicitudNumNotPoder", s.NumNotaria);
            }
            else
            {
                docText = docText.Replace("SolicitudNumNotPoder", "");
            }

            if (s.FechaEmitePoder.GetHashCode() != 0)
            {
                docText = docText.Replace("SolicitudFechaPoder", s.FechaEmitePoder.ToString());
            }
            else
            {
                docText = docText.Replace("SolicitudFechaPoder", "");
            }

            if (s.FechaConstitutiva.GetHashCode() != 0)
            {
                pattern = @"\b" + "SolicitudFechaConstitutiva" + @"\b";
                docText = Regex.Replace(docText, pattern, s.FechaConstitutiva.ToString());
            }
            else
            {
                pattern = @"\b" + "SolicitudFechaConstitutiva" + @"\b";
                docText = Regex.Replace(docText, pattern, "");
            }

            return docText;
        }
    }
}
