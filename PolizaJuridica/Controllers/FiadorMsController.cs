using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PolizaJuridica.Data;
using PolizaJuridica.Utilerias;
using PolizaJuridica.ViewModels;

namespace PolizaJuridica.Controllers
{
    public class FiadorMsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public FiadorMsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }
        // GET: FiadorMs
        public async Task<IActionResult> Index(int id)
        {
            var applicationDbContext = _context.FiadorM.Include(f => f.FisicaMoral).Where(f => f.FisicaMoralId == id);
            //sección del código que se puede mejorar
            var varSolicitudId = (await _context.FisicaMoral.SingleOrDefaultAsync(f => f.FisicaMoralId == id));
            var TipoRegimen = (await _context.Solicitud.SingleOrDefaultAsync(s => s.SolicitudId == varSolicitudId.SolicitudId));
            ViewBag.TipoRegimen = Int32.Parse(TipoRegimen.TipoArrendatario);
            //Fin de la sección
            ViewBag.Id = id;
            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<String> InsertarActualizar( int FiadorMid, string FiaddorMrazonSocial, string FiadorMrfc, string FiadorMtelefono, string FiadorMgiro, string FiadorMweb, string FiadorMnombresRlegal, string FiadorMapePaternoRlegal, string FiadorMapeMaternoRlegal, string FiadorMpuestoRlegal, string FiadorMnacionalidadRlegal, string FiadorMpoderRepNo, string FiadorMtelefonoRlegat, string FiadorMcelularRlegal, string FiadorMdomicilioGarantia, string FiadorMcoliniaGarantia, string FiadorMdelegacionGarantia, int FisicaMoralId, string TipoIdentificacion, string NumeroIdentificacion, string FiadorMnombreEscrituraGarantia, string FiadorMlicenciadoNotaria, string FiadorMnumNotaria, string FiadorMdistritoJudicial, string FiadorMnumPartida, string FiadorMvolumen, string FiadorMlibro, string FiadorMseccion, DateTime FiadorMfechaPartida, string FiadorMcoloniaGarantia, string FiadorMestadoGarantia, string FiadorMcpgarantia, string FiadorMnumCons, DateTime FiadorMfechaCons, string FiadorMlicenciadoCons, string FiadorMnumNotaCons, string FiadorMnumRpp, DateTime FiadorMfechaRpp, string FiadorMnumEscPoder, DateTime FiadorMfechaPoder, string FiadorMlicenciadoPoder, string FiadorMnumeroNotaPoder, string FiadorMCodigoPostalGarantia, string FiadorMCondicionMigratoria, string FiadorMDomicilioEmpresa, string FiadorMColoniaEmpresa, string FiadorMDeleEmpresa, string FiadorMEstadoEmpresa, string FiadorMCPEmpresa, FiadorM fiadorM)
        {
            bool isError = false;
            string result = string.Empty;
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            var SistemaId = Int32.Parse(User.FindFirst("SistemaId").Value);
            // validaciones por campo

            if (fiadorM.FiadorMnombresRlegal == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorMnombresRlegal"));
                isError = true;
            }
            if (fiadorM.FiadorMapePaternoRlegal == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorMapePaternoRlegal"));
                isError = true;
            }
            if (fiadorM.FiaddorMrazonSocial == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiaddorMrazonSocial"));
                isError = true;
            }

            // Fin de validaciones
            if (isError == false)
            {
                    _context.Add(fiadorM);

                var resulta = await _context.SaveChangesAsync();
                if (resulta > 0)
                {
                    Error.Add(Mensajes.Exitoso("Se agrego correctamente el fiador"));
                    result = JsonConvert.SerializeObject(Error);

                    //Agregmos a la persona para investigación
                    var inv = new InvestigacionsController(_context);
                    string nombre = string.Empty;
                    nombre = fiadorM.FiadorMnombresRlegal.Trim() + " " + fiadorM.FiadorMapePaternoRlegal.Trim();
                    if (fiadorM.FiadorMapeMaternoRlegal != null)
                        nombre = nombre + " " + fiadorM.FiadorMapeMaternoRlegal.Trim();

                    inv.InsertaCandidatos(fiadorM.FisicaMoralId, nombre);

                    if(fiadorM.FiaddorMrazonSocial != null)
                        inv.InsertaCandidatos(fiadorM.FisicaMoralId, fiadorM.FiaddorMrazonSocial.Trim());


                    bool isMatch = false;

                    InvestigacionListasNegras invest = new InvestigacionListasNegras(_context);
                    isMatch = invest.ListasNegras(fiadorM.FiadorMnombresRlegal, fiadorM.FiadorMapePaternoRlegal, fiadorM.FiadorMapeMaternoRlegal, fiadorM.FiadorMrfc, fiadorM.FiaddorMrazonSocial);

                    if (isMatch == true)
                    {
                        var fisicaMoral = _context.FisicaMoral.SingleOrDefault(f => f.FisicaMoralId == fiadorM.FisicaMoralId);
                        var s = _context.Solicitud.SingleOrDefault(so => so.SolicitudId == fisicaMoral.SolicitudId);
                        var proceso = _context.TipoProceso.SingleOrDefault(t => t.Orden == 99);
                        s.SolicitudEstatus = proceso.Descripcion;

                        UsuariosSolicitud us = new UsuariosSolicitud()
                        {
                            SolicitudId = s.SolicitudId,
                            TipoProcesoId = proceso.TipoProcesoId,
                            UsuariosId = SistemaId,
                            Fecha = DateTime.Now,
                            Observacion = "Se encontro en la lista negra a la siguiente persona: " + fiadorM.FiadorMnombresRlegal + " " + fiadorM.FiadorMapePaternoRlegal + " " + fiadorM.FiadorMapeMaternoRlegal
                        };
                        _context.Add(us);
                        await _context.SaveChangesAsync();

                        try
                        {
                            _context.Update(s);
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception e)
                        {
                            Log log = new Log()
                            {
                                LogObjetoIn = s.ToString(),
                                LogFecha = DateTime.Now,
                                LogPantalla = "Create",
                                LogProceso = "FiadorM",
                                LogObjetoOut = e.ToString()
                            };
                            _context.Add(log);
                            _context.SaveChanges();
                        }
                    }

                    return result;
                }
                else
                {
                    Error.Add(Mensajes.MensajesError("Error, favor de copiar el error y mandarlo al da" + result.ToString()));
                    result = JsonConvert.SerializeObject(Error);
                    return result;
                }
            }
            else
            {
                Error.Add(Mensajes.MensajesError("Favor de completar los campos en rojo"));
                result = JsonConvert.SerializeObject(Error);
                return result;
            }
        }

        // GET: FiadorMs/Edit/5
        public async Task<List<FiadorM>> EditAjax(int? id)
        {
            List<FiadorM> fiador = new List<FiadorM>();
            var appFiador = await _context.FiadorM.SingleOrDefaultAsync(a => a.FiadorMid == id);
            fiador.Add(appFiador);
            return fiador;
        }

        // POST: FiadorMs/Edit
        public async Task<String> EditFiadorAjax(int FiadorMid, string FiaddorMrazonSocial, string FiadorMrfc, string FiadorMtelefono, string FiadorMgiro, string FiadorMweb, string FiadorMnombresRlegal, string FiadorMapePaternoRlegal, string FiadorMapeMaternoRlegal, string FiadorMpuestoRlegal, string FiadorMnacionalidadRlegal, string FiadorMpoderRepNo, string FiadorMtelefonoRlegat, string FiadorMcelularRlegal, string FiadorMdomicilioGarantia, string FiadorMcoliniaGarantia, string FiadorMdelegacionGarantia, int FisicaMoralId, string TipoIdentificacion, string NumeroIdentificacion, string FiadorMnombreEscrituraGarantia, string FiadorMlicenciadoNotaria, string FiadorMnumNotaria, string FiadorMdistritoJudicial, string FiadorMnumPartida, string FiadorMvolumen, string FiadorMlibro, string FiadorMseccion, DateTime FiadorMfechaPartida, string FiadorMcoloniaGarantia, string FiadorMestadoGarantia, string FiadorMcpgarantia, string FiadorMnumCons, DateTime FiadorMfechaCons, string FiadorMlicenciadoCons, string FiadorMnumNotaCons, string FiadorMnumRpp, DateTime FiadorMfechaRpp, string FiadorMnumEscPoder, DateTime FiadorMfechaPoder, string FiadorMlicenciadoPoder, string FiadorMnumeroNotaPoder, string FiadorMCodigoPostalGarantia, string FiadorMCondicionMigratoria, string FiadorMDomicilioEmpresa, string FiadorMColoniaEmpresa, string FiadorMDeleEmpresa, string FiadorMEstadoEmpresa, string FiadorMCPEmpresa, FiadorM fiadorM)
        {
            bool isError = false;
            string result = string.Empty;
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            // validaciones por campo


            // Fin de validaciones
            if (isError == false)
            {
                _context.Update(fiadorM);

                var resulta = await _context.SaveChangesAsync();
                if (resulta > 0)
                {
                    Error.Add(Mensajes.Exitoso("Se agrego correctamente el fiador"));
                    result = JsonConvert.SerializeObject(Error);
                    return result;
                }
                else
                {
                    Error.Add(Mensajes.MensajesError("Error, favor de copiar el error y mandarlo al da" + result.ToString()));
                    result = JsonConvert.SerializeObject(Error);
                    return result;
                }
            }
            else
            {
                Error.Add(Mensajes.MensajesError("Favor de completar los campos en rojo"));
                result = JsonConvert.SerializeObject(Error);
                return result;
            }
        }
        //Post: FiadorF/DeleteComirmed
        public async Task<String> DeleteConfirmed(int id)
        {
            var fiador = await _context.FiadorM.SingleOrDefaultAsync(f => f.FiadorMid == id);
            _context.FiadorM.Remove(fiador);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return "Save";
            }
            else
            {
                return "Error, favor de intentarlo nuevamente";
            }

        }
    }
}
