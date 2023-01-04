using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PolizaJuridica.Data;
using PolizaJuridica.Utilerias;

namespace PolizaJuridica.Controllers
{
    public class FisicaMoralsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public FisicaMoralsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: FisicaMorals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FisicaMoral.Include(f => f.Solicitud);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FisicaMorals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fisicaMoral = await _context.FisicaMoral
                .Include(f => f.Solicitud)
                .FirstOrDefaultAsync(m => m.FisicaMoralId == id);
            if (fisicaMoral == null)
            {
                return NotFound();
            }

            return View(fisicaMoral);
        }

        // GET: FisicaMorals/Create
        public async Task<IActionResult> Create(int id)
        {
            
            var solicitud = await _context.Solicitud.SingleOrDefaultAsync(m => m.SolicitudId == id);
            ViewBag.SolicitudId = id;
            ViewBag.TipoRegimen = Int32.Parse(solicitud.TipoArrendatario);
            ViewBag.Nombre = solicitud.ArrendatarioNombre;
            ViewBag.ApePat = solicitud.ArrendatarioApePat;
            ViewBag.ApeMat = solicitud.ArrendatarioApeMat;
            ViewBag.Id = id;
            ViewBag.Email = solicitud.ArrendatarioCorreo;
            ViewBag.Telefono = solicitud.ArrendatarioTelefono;
            return View();
        }

        // POST: FisicaMorals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FisicaMoralId,SfisicaNacionallidad,SfisicaCondMigratoria,SfisicaEstadoCivil,SfisicaConvenioEc,SfisicaDomicilio,SfisicaColonia,SfisicaDelegacionMunicipio,SfisicaEstado,SfisicaTelefono,SfisicaCelular,SfisicaEmail,SfisicaProfesion,SfisicaIngresoMensual,SfisicaTrabajo,SfisicaAntiguedad,SfisicaPuesto,SfisicaTelefonoTrabajo,SfisicaHorario,SfisicaDomicilioTrabajo,SfisicaColoniaTrabajo,SfisicaDelegMuniTrabajo,SfisicaEstadoTrabajo,SfisicaGiroTrabajo,SfisicaWebTrabajo,SfisicaJefeTrabajo,SfisicaPuestoJefe,SfisicaEmailJefe,SfisicaFactura,ActaConstitutiva,PoderRepresentanteNo,DomicilioRepresentanteLegal,ColoniaRl,DeleMuni,TelefonoRl,EstadoRl,EmailRl,HorarioRl,IngresoMensualRl,SindicadoRl,RequiereFacturaRl,AfianzadoRl,AfianzadoraRl,SolicitudId,SfisicaNombre,SfisicaApePat,SfisicaApeMat,SfisicaRfc,SfisicaRazonSocial,SfisicaCodigoPostal,SfisicaCodigoPostalTrabajo,TipoIdentificacion,NumeroIdentificacion,NumRppcons,EscrituraNumero,Licenciado,NumeroNotaria,FechaRppcons,NumEscPoder,TitularNotaPoder,NumNotaria,FechaEmitePoder,FechaConstitutiva")] FisicaMoral fisicaMoral)
        {
            bool isError = false;
            List<string> CamposError = new List<string>();
            List<string> Errores = new List<string>();
            var CError = "Error";
            int opc = Int32.Parse(_context.Solicitud.SingleOrDefault(s => s.SolicitudId == fisicaMoral.SolicitudId).TipoArrendatario);
            switch (opc)
            {
                case 1:
                    if (fisicaMoral.SfisicaCelular == null)
                    {
                        CamposError.Add("SfisicaCelular" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.SfisicaEmail == null)
                    {
                        CamposError.Add("SfisicaEmail" + CError);
                        isError = true;
                    }
                    break;

                case 2:
                    if (fisicaMoral.SfisicaRfc == null)
                    {
                        CamposError.Add("SFisicaRFC" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.SfisicaRazonSocial == null)
                    {
                        CamposError.Add("SFisicaRazonSocial" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.DomicilioRepresentanteLegal == null)
                    {
                        CamposError.Add("DomicilioRepresentanteLegal" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.ColoniaRl == null)
                    {
                        CamposError.Add("ColoniaRL" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.DeleMuni == null)
                    {
                        CamposError.Add("DeleMuni" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.SfisicaCodigoPostalTrabajo == null)
                    {
                        CamposError.Add("SFisicaCodigoPostalTrabajo" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.SfisicaEstado == null)
                    {
                        CamposError.Add("SFisicaCodigoPostalTrabajo" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.EmailRl == null)
                    {
                        CamposError.Add("EmailRL" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.TelefonoRl == null)
                    {
                        CamposError.Add("TelefonoRL" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.EstadoRl == null)
                    {
                        CamposError.Add("EstadoRL" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.DomicilioRepresentanteLegal == null)
                    {
                        CamposError.Add("DomicilioRepresentanteLegal" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.ColoniaRl == null)
                    {
                        CamposError.Add("ColoniaRL" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.DeleMuni == null)
                    {
                        CamposError.Add("DeleMuni" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.SfisicaCodigoPostalTrabajo == null)
                    {
                        CamposError.Add("SFisicaCodigoPostalTrabajo" + CError);
                        isError = true;
                    }

                    break;
            }
            if (fisicaMoral.SfisicaEstado == null)
            {
                CamposError.Add("SFisicaEstado" + CError);
                isError = true;
            }
            if (fisicaMoral.SfisicaNombre == null)
            {
                CamposError.Add("SFisicaNombre" + CError);
                isError = true;
            }
            if (fisicaMoral.SfisicaApePat == null)
            {
                CamposError.Add("SFisicaApePat" + CError);
                isError = true;
            }
            //if (fisicaMoral.SfisicaApeMat == null)
            //{
            //    CamposError.Add("SFisicaApeMat" + CError);
            //    isError = true;
            //}
            if (fisicaMoral.SfisicaDomicilio == null)
            {
                CamposError.Add("SFisicaDomicilio" + CError);
                isError = true;
            }
            if (fisicaMoral.SfisicaColonia == null)
            {
                CamposError.Add("SfisicaColonia" + CError);
                isError = true;
            }
            if (fisicaMoral.SfisicaDelegacionMunicipio == null)
            {
                CamposError.Add("SFisicaDelegacionMunicipio" + CError);
                isError = true;
            }
            if (fisicaMoral.SfisicaCodigoPostal == null)
            {
                CamposError.Add("SFisicaCodigoPostal" + CError);
                isError = true;
            }
            if (fisicaMoral.SfisicaNacionallidad == null)
            {
                CamposError.Add("SfisicaNacionallidad" + CError);
                isError = true;
            }
            
            if (isError == true)
            {
                if (CamposError.Count > 0)
                {
                    Errores.Add("Favor de completar los campos que estan en rojo");
                }
                ViewData["CamposError"] = CamposError;
                int id = fisicaMoral.SolicitudId;
                var solicitud = await _context.Solicitud.SingleOrDefaultAsync(m => m.SolicitudId == id);
                ViewBag.SolicitudId = id;
                ViewBag.TipoRegimen = Int32.Parse(solicitud.TipoArrendatario);
                ViewBag.Nombre = solicitud.ArrendatarioNombre;
                ViewBag.ApePat = solicitud.ArrendatarioApePat;
                ViewBag.ApeMat = solicitud.ArrendatarioApeMat;
                ViewBag.Email = solicitud.ArrendatarioCorreo;
                ViewBag.Telefono = solicitud.ArrendatarioTelefono;
                ViewBag.Id = id;
                return View(fisicaMoral);
            }
            else
            {
                _context.Add(fisicaMoral);
                await _context.SaveChangesAsync();
                var Id = fisicaMoral.SolicitudId;
                var inv = new InvestigacionsController(_context);
                string nombre = string.Empty;
                bool isMatch = false;

                InvestigacionListasNegras invest = new InvestigacionListasNegras(_context);
                isMatch = invest.ListasNegras(fisicaMoral.SfisicaNombre, fisicaMoral.SfisicaApePat, fisicaMoral.SfisicaApeMat, fisicaMoral.SfisicaRfc, fisicaMoral.SfisicaRazonSocial);

                if (isMatch == true)
                {
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
                        Observacion = "Se encontro en la lista negra a la siguiente persona: " + fisicaMoral.SfisicaNombre + " " + fisicaMoral.SfisicaApePat + " " + fisicaMoral.SfisicaApeMat
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
                            LogProceso = "FisicaMoral",
                            LogObjetoOut = e.ToString()
                        };
                        _context.Add(log);
                        _context.SaveChanges();
                    }
                }

                //adicional agregamos a la persona o empresa

                //if (fisicaMoral.Solicitud.TipoArrendatario == "1")//persona fisica
                //{
                    
                //    nombre = fisicaMoral.SfisicaNombre.Trim() + " " + fisicaMoral.SfisicaApePat.Trim();
                //    if (fisicaMoral.SfisicaApeMat != null)
                //        nombre = nombre + " " + fisicaMoral.SfisicaApePat.Trim();

                //    inv.InsertaCandidatos(fisicaMoral.FisicaMoralId,nombre);
                //}

                //if (fisicaMoral.Solicitud.TipoArrendatario == "2")//persona Moral
                //{
                //    if(fisicaMoral.SfisicaRazonSocial != null)
                //    inv.InsertaCandidatos(fisicaMoral.FisicaMoralId, fisicaMoral.SfisicaRazonSocial.Trim());

                //    //Representante Legal
                //    nombre = fisicaMoral.SfisicaNombre.Trim() + " " + fisicaMoral.SfisicaApePat.Trim();
                //    if (fisicaMoral.SfisicaApeMat != null)
                //        nombre = nombre + " " + fisicaMoral.SfisicaApePat.Trim();

                //    inv.InsertaCandidatos(fisicaMoral.FisicaMoralId, nombre);
                //}


                return RedirectToAction("Edit", new { Id, origen = 1 });
            }
        }


        // GET: SFisicas/Edit/5
        public async Task<IActionResult> Edit(int? id, int? origen)
        {    //1 Solicitud
             //0 FisicaMoral
            if (id == null)
            {
                return NotFound();
            }
            FisicaMoral fisicaMoral = null;
            if (origen == 1)
            {
                fisicaMoral = (await _context.FisicaMoral.SingleOrDefaultAsync(s => s.SolicitudId == id));
                //Fin de la sección             
            }
            else
            {
                fisicaMoral = (await _context.FisicaMoral.SingleOrDefaultAsync(f => f.FisicaMoralId == id));

                //Fin de la sección
            }
            //sección del código que se puede mejorar
            var TipoRegimen = (await _context.Solicitud.SingleOrDefaultAsync(s => s.SolicitudId == fisicaMoral.SolicitudId));
            ViewBag.TipoRegimen = Int32.Parse(TipoRegimen.TipoArrendatario);
            ViewBag.Fiador = fisicaMoral.Solicitud.SolicitudFiador;
            if (fisicaMoral == null)
            {
                return NotFound();
            }
            return View(fisicaMoral);
        }

        // POST: SFisicas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FisicaMoralId,SfisicaNacionallidad,SfisicaCondMigratoria,SfisicaEstadoCivil,SfisicaConvenioEc,SfisicaDomicilio,SfisicaColonia,SfisicaDelegacionMunicipio,SfisicaEstado,SfisicaTelefono,SfisicaCelular,SfisicaEmail,SfisicaProfesion,SfisicaIngresoMensual,SfisicaTrabajo,SfisicaAntiguedad,SfisicaPuesto,SfisicaTelefonoTrabajo,SfisicaHorario,SfisicaDomicilioTrabajo,SfisicaColoniaTrabajo,SfisicaDelegMuniTrabajo,SfisicaEstadoTrabajo,SfisicaGiroTrabajo,SfisicaWebTrabajo,SfisicaJefeTrabajo,SfisicaPuestoJefe,SfisicaEmailJefe,SfisicaFactura,ActaConstitutiva,PoderRepresentanteNo,DomicilioRepresentanteLegal,ColoniaRl,DeleMuni,TelefonoRl,EstadoRl,EmailRl,HorarioRl,IngresoMensualRl,SindicadoRl,RequiereFacturaRl,AfianzadoRl,AfianzadoraRl,SolicitudId,SfisicaNombre,SfisicaApePat,SfisicaApeMat,SfisicaRfc,SfisicaRazonSocial,SfisicaCodigoPostal,SfisicaCodigoPostalTrabajo,TipoIdentificacion,NumeroIdentificacion,NumRppcons,EscrituraNumero,Licenciado,NumeroNotaria,FechaRppcons,NumEscPoder,TitularNotaPoder,NumNotaria,FechaEmitePoder,FechaConstitutiva")] FisicaMoral fisicaMoral)
        {
            if (id != fisicaMoral.FisicaMoralId)
            {
                return NotFound();
            }
            bool isError = false;
            bool isMatch = false;
            List<string> CamposError = new List<string>();
            List<string> Errores = new List<string>();
            var CError = "Error";
            int opc = Int32.Parse(_context.Solicitud.SingleOrDefault(s => s.SolicitudId == fisicaMoral.SolicitudId).TipoArrendatario);
            var solicitud = _context.Solicitud.SingleOrDefault(s => s.SolicitudId == fisicaMoral.SolicitudId);
            ViewBag.TipoRegimen = Int32.Parse(solicitud.TipoArrendatario);
            ViewBag.Fiador = solicitud.SolicitudFiador;
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            switch (opc)
            {
                case 1:
                    if (fisicaMoral.SfisicaCelular == null)
                    {
                        CamposError.Add("SfisicaCelular" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.SfisicaEmail == null)
                    {
                        CamposError.Add("SfisicaEmail" + CError);
                        isError = true;
                    }
                    break;

                case 2:
                    if (fisicaMoral.SfisicaRfc == null)
                    {
                        CamposError.Add("SFisicaRFC" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.SfisicaRazonSocial == null)
                    {
                        CamposError.Add("SFisicaRazonSocial" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.DomicilioRepresentanteLegal == null)
                    {
                        CamposError.Add("DomicilioRepresentanteLegal" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.ColoniaRl == null)
                    {
                        CamposError.Add("ColoniaRL" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.DeleMuni == null)
                    {
                        CamposError.Add("DeleMuni" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.SfisicaCodigoPostalTrabajo == null)
                    {
                        CamposError.Add("SFisicaCodigoPostalTrabajo" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.SfisicaEstado == null)
                    {
                        CamposError.Add("SFisicaCodigoPostalTrabajo" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.EmailRl == null)
                    {
                        CamposError.Add("EmailRL" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.TelefonoRl == null)
                    {
                        CamposError.Add("TelefonoRL" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.EstadoRl == null)
                    {
                        CamposError.Add("EstadoRL" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.DomicilioRepresentanteLegal == null)
                    {
                        CamposError.Add("DomicilioRepresentanteLegal" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.ColoniaRl == null)
                    {
                        CamposError.Add("ColoniaRL" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.DeleMuni == null)
                    {
                        CamposError.Add("DeleMuni" + CError);
                        isError = true;
                    }
                    if (fisicaMoral.SfisicaCodigoPostalTrabajo == null)
                    {
                        CamposError.Add("SFisicaCodigoPostalTrabajo" + CError);
                        isError = true;
                    }

                    break;
            }
            if (fisicaMoral.SfisicaEstado == null)
            {
                CamposError.Add("SFisicaEstado" + CError);
                isError = true;
            }
            if (fisicaMoral.SfisicaNombre == null)
            {
                CamposError.Add("SFisicaNombre" + CError);
                isError = true;
            }
            if (fisicaMoral.SfisicaApePat == null)
            {
                CamposError.Add("SFisicaApePat" + CError);
                isError = true;
            }
            if (fisicaMoral.SfisicaApeMat == null)
            {
                CamposError.Add("SFisicaApeMat" + CError);
                isError = true;
            }
            if (fisicaMoral.SfisicaDomicilio == null)
            {
                CamposError.Add("SFisicaDomicilio" + CError);
                isError = true;
            }
            if (fisicaMoral.SfisicaColonia == null)
            {
                CamposError.Add("SfisicaColonia" + CError);
                isError = true;
            }
            if (fisicaMoral.SfisicaDelegacionMunicipio == null)
            {
                CamposError.Add("SFisicaDelegacionMunicipio" + CError);
                isError = true;
            }
            if (fisicaMoral.SfisicaCodigoPostal == null)
            {
                CamposError.Add("SFisicaCodigoPostal" + CError);
                isError = true;
            }
            if (fisicaMoral.SfisicaNacionallidad == null)
            {
                CamposError.Add("SfisicaNacionallidad" + CError);
                isError = true;
            }
            if (isError == true)
            {
                if (CamposError.Count > 0)
                {
                    Errores.Add("Favor de completar los campos que estan en rojo");
                }
                ViewData["CamposError"] = CamposError;
                return View(fisicaMoral);
            }
            else
            {

                    InvestigacionListasNegras inv = new InvestigacionListasNegras(_context);
                    isMatch = inv.ListasNegras(fisicaMoral.SfisicaNombre, fisicaMoral.SfisicaApePat, fisicaMoral.SfisicaApeMat, fisicaMoral.SfisicaRfc, fisicaMoral.SfisicaRazonSocial);

                if(isMatch == true)
                {
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
                        Observacion = "Se encontro en la lista negra a la siguiente persona: " + fisicaMoral.SfisicaNombre + " " + fisicaMoral.SfisicaApePat + " " + fisicaMoral.SfisicaApeMat
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
                            LogProceso = "FisicaMoral",
                            LogObjetoOut = e.ToString()
                        };
                        _context.Add(log);
                        _context.SaveChanges();
                    }
                }
                

                try
                {
                    _context.Update(fisicaMoral);
                    await _context.SaveChangesAsync();
                    ViewData["Exitoso"] = "Ok";
                    return View(fisicaMoral);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FisicaMoralExists((int)fisicaMoral.FisicaMoralId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            
        }

        // GET: FisicaMorals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fisicaMoral = await _context.FisicaMoral
                .Include(f => f.Solicitud)
                .FirstOrDefaultAsync(m => m.FisicaMoralId == id);
            if (fisicaMoral == null)
            {
                return NotFound();
            }

            return View(fisicaMoral);
        }

        // POST: FisicaMorals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fisicaMoral = await _context.FisicaMoral.FindAsync(id);
            _context.FisicaMoral.Remove(fisicaMoral);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FisicaMoralExists(int id)
        {
            return _context.FisicaMoral.Any(e => e.FisicaMoralId == id);
        }
    }
}
