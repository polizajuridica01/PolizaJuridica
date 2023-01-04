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
    public class FiadorFsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public FiadorFsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }
        // GET: FiadorFs
        public async Task<IActionResult> Index(int id)
        {
            var applicationDbContext = _context.FiadorF.Include(f => f.FisicaMoral).Where(f => f.FisicaMoralId == id);
            //sección del código que se puede mejorar
            var varSolicitudId = (await _context.FisicaMoral.SingleOrDefaultAsync(f => f.FisicaMoralId == id));
            var TipoRegimen = (await _context.Solicitud.SingleOrDefaultAsync(s => s.SolicitudId == varSolicitudId.SolicitudId));
            ViewBag.TipoRegimen = Int32.Parse(TipoRegimen.TipoArrendatario);
            //Fin de la sección
            ViewBag.Id = id;
            ViewData["TipoInmobiliarioId"] = new SelectList(_context.TipoInmobiliario, "TipoInmobiliarioId", "TipoInmobiliarioDesc");
            return View(await applicationDbContext.ToListAsync());
        }

        // POST: FiadorFs/Create
        public async Task<String> Insertar(int FiadorFId, string FiadorFNombres, string FiadorFApePaterno, string FiadorFApeMaterno, string FiadorFNacionalidad, string FiadorFCondicionMigratoria, string FiadorFParentesco, string FiadorFEstadoCivil, string FiadorFConvenioEC, string FiadorFDomicilio, string FiadorFColonia, string FiadorFDelegacion, string FiadorFTelefono, string FiadorFCelular, string FiadorFEmail, string FiadorFDomicilioGarantia, string FiadorFColoniaGarantia, string FiadorFDelegaciónGarantia, string FiadorFProfesion, string FiadorFEmpresa, string FiadorFTelefonoEmpresa, string FiadorFNombresConyuge, string FiadorFApePaternoConyuge, string FiadorFApeMaternoConyuge, int FisicaMoralId,string FiadorFEstado,string FiadorFEstadoGarantia,int FiadorFCodigoPostal,int FiadorFCodigoPostalGarantia, string EscrituraNumero, string Licenciado, string NumeroNotaria, string DistritoJudicial, string PartidaNumero, string PartidaVolumen, string PartidaLibro, string PartidaSeccion, DateTime PartidaFecha,string TipoIdentificacion,string NumeroIdentificacion,int TipoInmobiliarioId, FiadorF fiadorF)
        {
            bool isError = false;
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            string result = string.Empty;
            //Area
            var area = User.FindFirst("Area").Value;


            if (FiadorFNombres == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFNombres"));
                isError = true;
            }
            if (FiadorFApePaterno == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFApePaterno"));
                isError = true;
            }
            if (FiadorFApeMaterno == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFApeMaterno"));                
                isError = true;
            }
            if (FiadorFNacionalidad == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFNacionalidad"));
                isError = true;
            }
            if (FiadorFDomicilio == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFDomicilio"));
                isError = true;
            }
            if (FiadorFColonia == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFColonia"));
                isError = true;
            }
            if (FiadorFDelegacion == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFDelegacion"));
                isError = true;
            }
            if (FiadorFTelefono == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFTelefono"));
                isError = true;
            }
            if (FiadorFCelular == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFCelular"));
                isError = true;
            }
            if (FiadorFEmail == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFEmail"));
                isError = true;
            }



            //Validacion de domicilio en garantia
            var fisicamoral = _context.FisicaMoral.Include(f => f.Solicitud).Where(f => f.FisicaMoralId == FisicaMoralId).SingleOrDefault();

            if (fisicamoral.Solicitud.SolicitudInmuebleGaran == true)
            {
                if (FiadorFDomicilioGarantia == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("FiadorFDomicilioGarantia"));
                    isError = true;
                }
                if (FiadorFColoniaGarantia == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("FiadorFColoniaGarantia"));
                    isError = true;
                }
                if (FiadorFDelegaciónGarantia == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("FiadorFDelegaciónGarantia"));
                    isError = true;
                }
                if (FiadorFEstado == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("FiadorFEstado"));
                    isError = true;
                }
                if (FiadorFEstadoGarantia == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("FiadorFEstadoGarantia"));
                    isError = true;
                }
                if (FiadorFCodigoPostalGarantia == 0)
                {
                    Error.Add(Mensajes.ErroresAtributos("FiadorFCodigoPostalGarantia"));
                    isError = true;
                }
                if (FiadorFCodigoPostal == 0)
                {
                    Error.Add(Mensajes.ErroresAtributos("FiadorFCodigoPostal"));
                    isError = true;
                }

                if (TipoInmobiliarioId == 0)
                {
                    Error.Add(Mensajes.MensajesError("Favor de seleccionar Tipo de inmueble"));
                    isError = true;
                }
                //Validación de escritura
                if (area == "Procesos")
                {
                    if (EscrituraNumero == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("EscrituraNumero"));
                        isError = true;
                    }
                    if (Licenciado == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("Licenciado"));
                        isError = true;
                    }
                    if (NumeroNotaria == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("NumeroNotaria"));
                        isError = true;
                    }
                    if (DistritoJudicial == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("DistritoJudicial"));
                        isError = true;
                    }
                    if (PartidaNumero == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("PartidaNumero"));
                        isError = true;
                    }
                    if (PartidaVolumen == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("PartidaVolumen"));
                        isError = true;
                    }
                    if (PartidaLibro == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("PartidaLibro"));
                        isError = true;
                    }
                    if (PartidaSeccion == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("PartidaSeccion"));
                        isError = true;
                    }
                }
            }

            if (isError == false)
            {
                fiadorF = new FiadorF
                {
                    FiadorFid = FiadorFId,
                    FiadorFnombres = FiadorFNombres,
                    FiadorFapePaterno = FiadorFApePaterno,
                    FiadorFapeMaterno = FiadorFApeMaterno,
                    FiadorFnacionalidad = FiadorFNacionalidad,
                    FiadorFcondicionMigratoria = FiadorFCondicionMigratoria,
                    FiadorFparentesco = FiadorFParentesco,
                    FiadorFestadoCivil = FiadorFEstadoCivil,
                    FiadorFconvenioEc = FiadorFConvenioEC,
                    FiadorFdomicilio = FiadorFDomicilio,
                    FiadorFcolonia = FiadorFColonia,
                    FiadorFdelegacion = FiadorFDelegacion,
                    FiadorFtelefono = FiadorFTelefono,
                    FiadorFcelular = FiadorFCelular,
                    FiadorFemail = FiadorFEmail,
                    FiadorFdomicilioGarantia = FiadorFDomicilioGarantia,
                    FiadorFcoloniaGarantia = FiadorFColoniaGarantia,
                    FiadorFdelegacionGarantia = FiadorFDelegaciónGarantia,
                    FiadorFprofesion = FiadorFProfesion,
                    FiadorFempresa = FiadorFEmpresa,
                    FiadorFtelefonoEmpresa = FiadorFTelefonoEmpresa,
                    FiadorFnombresConyuge = FiadorFNombresConyuge,
                    FiadorFapePaternoConyuge = FiadorFApePaternoConyuge,
                    FiadorFapeMaternoConyuge = FiadorFApeMaternoConyuge,
                    FiadorFestado = FiadorFEstado,
                    FiadorFestadoGarantia = FiadorFEstadoGarantia,
                    FiadorFcodigoPostal = FiadorFCodigoPostal.ToString(),
                    FiadorFcodigoPostalGarantia = FiadorFCodigoPostalGarantia.ToString(),                    
                    FisicaMoralId = FisicaMoralId,
                    //Datos de escritura
                    EscrituraNumero = EscrituraNumero,
                    Licenciado = Licenciado,
                    NumeroNotaria = NumeroNotaria,
                    DistritoJudicial = DistritoJudicial,
                    PartidaNumero = PartidaNumero,
                    PartidaVolumen = PartidaVolumen,
                    PartidaLibro = PartidaLibro,
                    PartidaSeccion = PartidaSeccion,
                    PartidaFecha = PartidaFecha,
                    TipoIdentificacion = TipoIdentificacion,
                    NumeroIdentificacion = NumeroIdentificacion

                };
                _context.Add(fiadorF);
                var resulta = await _context.SaveChangesAsync();
                if (resulta > 0)
                {
                    Error.Add(Mensajes.Exitoso("Se agrego correctamente el fiador"));
                    result = JsonConvert.SerializeObject(Error);

                    //Agregmos a la persona para investigación
                    var inv = new InvestigacionsController(_context);
                    string nombre = string.Empty;
                    nombre = fiadorF.FiadorFnombres.Trim() + " " + fiadorF.FiadorFapePaterno.Trim();
                    if (fiadorF.FiadorFapeMaterno != null)
                        nombre = nombre + " " + fiadorF.FiadorFapeMaterno.Trim();

                    inv.InsertaCandidatos(fiadorF.FisicaMoralId, nombre);

                    bool isMatch = false;

                    InvestigacionListasNegras invest = new InvestigacionListasNegras(_context);
                    isMatch = invest.ListasNegras(fiadorF.FiadorFnombres, fiadorF.FiadorFapePaterno, fiadorF.FiadorFapeMaterno, null, null);

                    if (isMatch == true)
                    {
                        var fisicaMoral = _context.FisicaMoral.SingleOrDefault(f => f.FisicaMoralId == fiadorF.FisicaMoralId);
                        var s = _context.Solicitud.SingleOrDefault(so => so.SolicitudId == fisicaMoral.SolicitudId);
                        var proceso = _context.TipoProceso.SingleOrDefault(t => t.Orden == 99);
                        s.SolicitudEstatus = proceso.Descripcion;
                        var SistemaId = Int32.Parse(User.FindFirst("SistemaId").Value);

                        UsuariosSolicitud us = new UsuariosSolicitud()
                        {
                            SolicitudId = (int)s.SolicitudId,
                            TipoProcesoId = proceso.TipoProcesoId,
                            UsuariosId = SistemaId,
                            Fecha = DateTime.Now,
                            Observacion = "Se encontro en la lista negra a la siguiente persona: " + fiadorF.FiadorFnombres + " " + fiadorF.FiadorFapePaterno + " " + fiadorF.FiadorFapeMaterno
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
                                LogProceso = "FiadorF",
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

        // GET: FiadorFs/edit
        public async Task<List<FiadorF>> EditAjax(int? id)
        {
            List<FiadorF> fiador = new List<FiadorF>();
            var appFiador = await _context.FiadorF.SingleOrDefaultAsync(a => a.FiadorFid == id);
            fiador.Add(appFiador);
            return fiador;
        }
        // GET: FiadorFs/edit
        public async Task<List<FiadorF>> EditAjaxEdit(int? id)
        {
            List<FiadorF> fiador = new List<FiadorF>();
            var appFiador = await _context.FiadorF.SingleOrDefaultAsync(a => a.FiadorFid == id);
            fiador.Add(appFiador);
            return fiador;
        }
        //POST: Fiador/Edit
        public async Task<String> EditFiadorAjax(int FiadorFId, string FiadorFNombres, string FiadorFApePaterno, string FiadorFApeMaterno, string FiadorFNacionalidad, string FiadorFCondicionMigratoria, string FiadorFParentesco, string FiadorFEstadoCivil, string FiadorFConvenioEC, string FiadorFDomicilio, string FiadorFColonia, string FiadorFDelegacion, string FiadorFTelefono, string FiadorFCelular, string FiadorFEmail, string FiadorFDomicilioGarantia, string FiadorFColoniaGarantia, string FiadorFDelegaciónGarantia, string FiadorFProfesion, string FiadorFEmpresa, string FiadorFTelefonoEmpresa, string FiadorFNombresConyuge, string FiadorFApePaternoConyuge, string FiadorFApeMaternoConyuge, int FisicaMoralId, string FiadorFEstado, string FiadorFEstadoGarantia, int FiadorFCodigoPostal, int FiadorFCodigoPostalGarantia, string EscrituraNumero, string Licenciado, string NumeroNotaria, string DistritoJudicial, string PartidaNumero, string PartidaVolumen, string PartidaLibro, string PartidaSeccion, DateTime PartidaFecha, string TipoIdentificacion, string NumeroIdentificacion, int TipoInmobiliarioId, FiadorF fiadorF)
        {
            bool isError = false;
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            string result = string.Empty;
            //Area
            var area = User.FindFirst("Area").Value;
            var CError = "Edit";

            if (FiadorFNombres == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFNombres" + CError));
                isError = true;
            }
            if (FiadorFApePaterno == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFApePaterno" + CError));
                isError = true;
            }
            if (FiadorFApeMaterno == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFApeMaterno" + CError));
                isError = true;
            }
            if (FiadorFNacionalidad == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFNacionalidad" + CError));
                isError = true;
            }
            if (FiadorFDomicilio == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFDomicilio" + CError));
                isError = true;
            }
            if (FiadorFColonia == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFColonia" + CError));
                isError = true;
            }
            if (FiadorFDelegacion == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFDelegacion" + CError));
                isError = true;
            }
            if (FiadorFTelefono == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFTelefono" + CError));
                isError = true;
            }
            if (FiadorFCelular == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFCelular" + CError));
                isError = true;
            }
            if (FiadorFEmail == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FiadorFEmail" + CError));
                isError = true;
            }
            //Validacion de domicilio en garantia
            var fisicamoral = _context.FisicaMoral.Include(f => f.Solicitud).Where(f => f.FisicaMoralId == FisicaMoralId).SingleOrDefault();

            //if (fisicamoral.Solicitud.SolicitudInmuebleGaran == true)
            if(fisicamoral != null)
            if(fisicamoral.Solicitud.SolicitudInmuebleGaran == true)
            {
                if (FiadorFDomicilioGarantia == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("FiadorFDomicilioGarantia" + CError));
                    isError = true;
                }
                if (FiadorFColoniaGarantia == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("FiadorFColoniaGarantia" + CError));
                    isError = true;
                }
                if (FiadorFDelegaciónGarantia == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("FiadorFDelegaciónGarantia" + CError));
                    isError = true;
                }
                if (FiadorFEstado == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("FiadorFEstado" + CError));
                    isError = true;
                }
                if (FiadorFEstadoGarantia == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("FiadorFEstadoGarantia" + CError));
                    isError = true;
                }
                if (FiadorFCodigoPostalGarantia == 0)
                {
                    Error.Add(Mensajes.ErroresAtributos("FiadorFCodigoPostalGarantia" + CError));
                    isError = true;
                }
                if (FiadorFCodigoPostal == 0)
                {
                    Error.Add(Mensajes.ErroresAtributos("FiadorFCodigoPostal" + CError));
                    isError = true;
                }
                if (TipoInmobiliarioId == 0)
                {
                    Error.Add(Mensajes.MensajesError("Favor de seleccionar Tipo de inmueble"));
                    isError = true;
                }
                //Validación de escritura
                if (area == "Procesos")
                {
                    if (EscrituraNumero == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("EscrituraNumero" + CError));
                        isError = true;
                    }
                    if (Licenciado == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("Licenciado" + CError));
                        isError = true;
                    }
                    if (NumeroNotaria == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("NumeroNotaria" + CError));
                        isError = true;
                    }
                    if (DistritoJudicial == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("DistritoJudicial" + CError));
                        isError = true;
                    }
                    if (PartidaNumero == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("PartidaNumero" + CError));
                        isError = true;
                    }
                    if (PartidaVolumen == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("PartidaVolumen" + CError));
                        isError = true;
                    }
                    if (PartidaLibro == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("PartidaLibro" + CError));
                        isError = true;
                    }
                    if (PartidaSeccion == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("PartidaSeccion" + CError));
                        isError = true;
                    }
                }
            }

            if (isError == false)
            {
                fiadorF = new FiadorF
                {
                    FiadorFid = FiadorFId,
                    FiadorFnombres = FiadorFNombres,
                    FiadorFapePaterno = FiadorFApePaterno,
                    FiadorFapeMaterno = FiadorFApeMaterno,
                    FiadorFnacionalidad = FiadorFNacionalidad,
                    FiadorFcondicionMigratoria = FiadorFCondicionMigratoria,
                    FiadorFparentesco = FiadorFParentesco,
                    FiadorFestadoCivil = FiadorFEstadoCivil,
                    FiadorFconvenioEc = FiadorFConvenioEC,
                    FiadorFdomicilio = FiadorFDomicilio,
                    FiadorFcolonia = FiadorFColonia,
                    FiadorFdelegacion = FiadorFDelegacion,
                    FiadorFtelefono = FiadorFTelefono,
                    FiadorFcelular = FiadorFCelular,
                    FiadorFemail = FiadorFEmail,
                    FiadorFdomicilioGarantia = FiadorFDomicilioGarantia,
                    FiadorFcoloniaGarantia = FiadorFColoniaGarantia,
                    FiadorFdelegacionGarantia = FiadorFDelegaciónGarantia,
                    FiadorFprofesion = FiadorFProfesion,
                    FiadorFempresa = FiadorFEmpresa,
                    FiadorFtelefonoEmpresa = FiadorFTelefonoEmpresa,
                    FiadorFnombresConyuge = FiadorFNombresConyuge,
                    FiadorFapePaternoConyuge = FiadorFApePaternoConyuge,
                    FiadorFapeMaternoConyuge = FiadorFApeMaternoConyuge,
                    FiadorFestado = FiadorFEstado,
                    FiadorFestadoGarantia = FiadorFEstadoGarantia,
                    FiadorFcodigoPostal = FiadorFCodigoPostal.ToString(),
                    FiadorFcodigoPostalGarantia = FiadorFCodigoPostalGarantia.ToString(),
                    FisicaMoralId = FisicaMoralId,
                    EscrituraNumero = EscrituraNumero,
                    Licenciado = Licenciado,
                    NumeroNotaria = NumeroNotaria,
                    DistritoJudicial = DistritoJudicial,
                    PartidaNumero = PartidaNumero,
                    PartidaVolumen = PartidaVolumen,
                    PartidaLibro = PartidaLibro,
                    PartidaSeccion = PartidaSeccion,
                    PartidaFecha = PartidaFecha,
                    TipoIdentificacion = TipoIdentificacion,
                    NumeroIdentificacion = NumeroIdentificacion,
                    TipoInmuebleId = TipoInmobiliarioId
                };
                try
                {
                    _context.Update(fiadorF);
                    var resulta = await _context.SaveChangesAsync();
                    if (resulta > 0)
                    {

                        bool isMatch = false;

                        InvestigacionListasNegras invest = new InvestigacionListasNegras(_context);
                        isMatch = invest.ListasNegras(fiadorF.FiadorFnombres, fiadorF.FiadorFapePaterno, fiadorF.FiadorFapeMaterno, null, null);

                        if (isMatch == true)
                        {
                            var fisicaMoral = _context.FisicaMoral.SingleOrDefault(f => f.FisicaMoralId == fiadorF.FisicaMoralId);
                            var s = _context.Solicitud.SingleOrDefault(so => so.SolicitudId == fisicaMoral.SolicitudId);
                            var proceso = _context.TipoProceso.SingleOrDefault(t => t.Orden == 99);
                            s.SolicitudEstatus = proceso.Descripcion;
                            var SistemaId = Int32.Parse(User.FindFirst("SistemaId").Value);

                            UsuariosSolicitud us = new UsuariosSolicitud()
                            {
                                SolicitudId = (int)s.SolicitudId,
                                TipoProcesoId = proceso.TipoProcesoId,
                                UsuariosId = SistemaId,
                                Fecha = DateTime.Now,
                                Observacion = "Se encontro en la lista negra a la siguiente persona: " + fiadorF.FiadorFnombres + " " + fiadorF.FiadorFapePaterno + " " + fiadorF.FiadorFapeMaterno
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
                                    LogPantalla = "Edit",
                                    LogProceso = "FiadorF",
                                    LogObjetoOut = e.ToString()
                                };
                                _context.Add(log);
                                _context.SaveChanges();
                            }
                        }

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
                catch (Exception e)
                {
                    Log log = new Log()
                    {
                        LogObjetoIn = fiadorF.ToString(),
                        LogFecha = DateTime.Now,
                        LogPantalla = "Create",
                        LogProceso = "FiadorF",
                        LogObjetoOut = e.ToString()

                    };
                    _context.Add(log);
                    _context.SaveChanges();
                    Error.Add(Mensajes.MensajesError("Revisar Id: " + log.LogId.ToString()));
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
            var Fiador = await _context.FiadorF.SingleOrDefaultAsync(f => f.FiadorFid == id);
            _context.FiadorF.Remove(Fiador);
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
