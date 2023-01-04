using PolizaJuridica.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolizaJuridica.Utilerias
{
    public class KeywordsArrendatarios
    {
        public static String SolicitudArrendatarios(string docText, FisicaMoral fisicaMoral)
        {

            int contador = 0;
            string identificador = string.Empty;
            string prefijo = @"\bArrendatarios";
            string prefijofin = @"\b";
            string pattern = string.Empty;
            foreach (var a in fisicaMoral.Arrendatario)
            {

                identificador = contador.ToString();

                if (a.ActaConstitutiva != null)
                {
                    pattern = prefijo + nameof(a.ActaConstitutiva) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.ActaConstitutiva.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.ActaConstitutiva) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }
                if (a.AfianzadoraRl != null)
                {
                    pattern = prefijo + nameof(a.AfianzadoraRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.AfianzadoraRl.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.AfianzadoraRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.AfianzadoRl == true)
                {
                    pattern = prefijo + nameof(a.AfianzadoRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "X");
                }
                else
                {
                    pattern = prefijo + nameof(a.AfianzadoRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.Antiguedad != null)
                {
                    pattern = prefijo + nameof(a.Antiguedad) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.Antiguedad.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.Antiguedad) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.ApeMaterno != null)
                {
                    pattern = prefijo + nameof(a.ApeMaterno) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.ApeMaterno.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.ApeMaterno) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.ApePaterno != null)
                {
                    pattern = prefijo + nameof(a.ApePaterno) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.ApePaterno.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.ApePaterno) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.Celular != null)
                {
                    pattern = prefijo + nameof(a.Celular) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.Celular.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.Celular) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.CodigoPostal != null)
                {
                    pattern = prefijo + nameof(a.CodigoPostal) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.CodigoPostal.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.CodigoPostal) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.CodigoPostalTrabajo != null)
                {
                    pattern = prefijo + nameof(a.CodigoPostalTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.CodigoPostalTrabajo.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.CodigoPostalTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.Colonia != null)
                {
                    pattern = prefijo + nameof(a.Colonia) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.Colonia.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.Colonia) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.ColoniaRl != null)
                {
                    pattern = prefijo + nameof(a.ColoniaRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.ColoniaRl.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.ColoniaRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }


                if (a.ColoniaTrabajo != null)
                {
                    pattern = prefijo + nameof(a.ColoniaTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.ColoniaTrabajo.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.ColoniaTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.CondMigratoria != null)
                {
                    pattern = prefijo + nameof(a.CondMigratoria) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.CondMigratoria.ToUpper());

                }
                else
                {
                    pattern = prefijo + nameof(a.CondMigratoria) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.ConvenioEc != null)
                {
                    pattern = prefijo + nameof(a.ConvenioEc) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.ConvenioEc.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.ConvenioEc) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.DelegacionMunicipio != null)
                {
                    pattern = prefijo + nameof(a.DelegacionMunicipio) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.DelegacionMunicipio.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.DelegacionMunicipio) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.DelegMuniTrabajo != null)
                {
                    pattern = prefijo + nameof(a.DelegMuniTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.DelegMuniTrabajo.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.DelegMuniTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }


                if (a.DeleMuni != null)
                {
                    pattern = prefijo + nameof(a.DeleMuni) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.DeleMuni.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.DeleMuni) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.Domicilio != null)
                {
                    pattern = prefijo + nameof(a.Domicilio) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.Domicilio.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.Domicilio) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.DomicilioRepresentanteLegal != null)
                {
                    pattern = prefijo + nameof(a.DomicilioRepresentanteLegal) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.DomicilioRepresentanteLegal.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.DomicilioRepresentanteLegal) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.DomicilioTrabajo != null)
                {
                    pattern = prefijo + nameof(a.DomicilioTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.DomicilioTrabajo.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.DomicilioTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.Email != null)
                {
                    pattern = prefijo + nameof(a.Email) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.Email.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.Email) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.EmailJefe != null)
                {
                    pattern = prefijo + nameof(a.EmailJefe) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.EmailJefe.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.EmailJefe) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.EmailRl != null)
                {
                    pattern = prefijo + nameof(a.EmailRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.EmailRl.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.EmailRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.Estado != null)
                {
                    pattern = prefijo + nameof(a.Estado) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.Estado.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.Estado) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.EstadoCivil != null)
                {
                    pattern = prefijo + nameof(a.EstadoCivil) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.EstadoCivil.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.EstadoCivil) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.EstadoRl != null)
                {
                    pattern = prefijo + nameof(a.EstadoRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.EstadoRl.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.EstadoRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.EstadoTrabajo != null)
                {
                    pattern = prefijo + nameof(a.EstadoTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.EstadoTrabajo.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.EstadoTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.Factura == true)
                {
                    pattern = prefijo + nameof(a.Factura) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }
                else
                {
                    pattern = prefijo + nameof(a.Factura) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.GiroTrabajo != null)
                {
                    pattern = prefijo + nameof(a.GiroTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.GiroTrabajo.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.GiroTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.Horario != null)
                {
                    pattern = prefijo + nameof(a.Horario) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.Horario.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.Horario) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.HorarioRl != null)
                {
                    pattern = prefijo + nameof(a.HorarioRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.HorarioRl.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.HorarioRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.IngresoMensual > 0)
                {
                    pattern = prefijo + nameof(a.IngresoMensual) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, ConvertNumbertoText.NumToLetter(a.IngresoMensual.ToString().Trim(), "MX"));
                }
                else
                {
                    pattern = prefijo + nameof(a.IngresoMensual) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.IngresoMensualRl > 0)
                {
                    pattern = prefijo + nameof(a.IngresoMensualRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, ConvertNumbertoText.NumToLetter(a.IngresoMensualRl.ToString().Trim(), "MX"));
                }
                else
                {
                    pattern = prefijo + nameof(a.IngresoMensualRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.JefeTrabajo != null)
                {
                    pattern = prefijo + nameof(a.JefeTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.JefeTrabajo.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.JefeTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.Nacionalidad != null)
                {
                    pattern = prefijo + nameof(a.Nacionalidad) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.Nacionalidad.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.Nacionalidad) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.Nombre != null)
                {
                    pattern = prefijo + nameof(a.Nombre) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.Nombre.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.Nombre) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.NumeroIdentificacion != null)
                {
                    pattern = prefijo + nameof(a.NumeroIdentificacion) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.NumeroIdentificacion.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.NumeroIdentificacion) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.PoderRepresentanteNo != null)
                {
                    pattern = prefijo + nameof(a.PoderRepresentanteNo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.PoderRepresentanteNo.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.PoderRepresentanteNo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.Profesion != null)
                {
                    pattern = prefijo + nameof(a.Profesion) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.Profesion.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.Profesion) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.Puesto != null)
                {
                    pattern = prefijo + nameof(a.Puesto) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.Puesto.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.Puesto) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.PuestoJefe != null)
                {
                    pattern = prefijo + nameof(a.PuestoJefe) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.PuestoJefe.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.PuestoJefe) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.RazonSocial != null)
                {
                    pattern = prefijo + nameof(a.RazonSocial) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.RazonSocial.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.RazonSocial) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.RequiereFacturaRl == true)
                {
                    pattern = prefijo + nameof(a.RequiereFacturaRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "X");
                }
                else
                {
                    pattern = prefijo + nameof(a.RequiereFacturaRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.Rfc != null)
                {
                    pattern = prefijo + nameof(a.Rfc) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.Rfc.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.Rfc) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                }

                if (a.SindicadoRl != null)
                {
                    pattern = prefijo + nameof(a.SindicadoRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.SindicadoRl.ToUpper());
                    //docText = docText.Replace(nameof(a.SindicadoRl) + identificador, a.SindicadoRl.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.SindicadoRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace(nameof(a.SindicadoRl) + identificador, "");
                }

                if (a.Telefono != null)
                {
                    pattern = prefijo + nameof(a.Telefono) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.Telefono.ToUpper());
                    //docText = docText.Replace(nameof(a.Telefono) + identificador, a.Telefono.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.Telefono) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace(nameof(a.Telefono) + identificador, "");
                }

                if (a.TelefonoRl != null)
                {
                    pattern = prefijo + nameof(a.TelefonoRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.TelefonoRl.ToUpper());
                    //docText = docText.Replace(nameof(a.TelefonoRl) + identificador, a.TelefonoRl.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.TelefonoRl) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace(nameof(a.TelefonoRl) + identificador, "");
                }

                if (a.TelefonoTrabajo != null)
                {
                    pattern = prefijo + nameof(a.TelefonoTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.TelefonoTrabajo.ToUpper());
                    //docText = docText.Replace(nameof(a.TelefonoTrabajo) + identificador, a.TelefonoTrabajo.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.TelefonoTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace(nameof(a.TelefonoTrabajo) + identificador, "");
                }

                if (a.TipoIdentificacion != null)
                {
                    pattern = prefijo + nameof(a.TipoIdentificacion) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.TipoIdentificacion.ToUpper());
                    //docText = docText.Replace("Arrendatarios" + nameof(a.TipoIdentificacion) + identificador, a.TipoIdentificacion.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.TipoIdentificacion) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace("Arrendatarios" + nameof(a.TipoIdentificacion) + identificador, "");
                }

                if (a.Trabajo != null)
                {
                    pattern = prefijo + nameof(a.Trabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.Trabajo.ToUpper());
                    //docText = docText.Replace(nameof(a.Trabajo) + identificador, a.Trabajo.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.Trabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace(nameof(a.Trabajo) + identificador, "");
                }

                if (a.WebTrabajo != null)
                {
                    pattern = prefijo + nameof(a.WebTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, a.WebTrabajo.ToUpper());
                    //docText = docText.Replace(nameof(a.WebTrabajo) + identificador, a.WebTrabajo.ToUpper());
                }
                else
                {
                    pattern = prefijo + nameof(a.WebTrabajo) + identificador + prefijofin;
                    docText = Regex.Replace(docText, pattern, "");
                    //docText = docText.Replace(nameof(a.WebTrabajo) + identificador, "");
                }


                contador++;
            }
            return docText;
        }
    }
}
