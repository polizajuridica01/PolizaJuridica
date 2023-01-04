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
    public class ArrendatariosController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public ArrendatariosController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Arrendatarios
        public async Task<IActionResult> Index(int Id)
        {
            var polizaJuridicaDbContext = _context.Arrendatario.Where(a => a.FisicaMoralId == Id);
            var fisicamoral = _context.FisicaMoral.Where(f => f.FisicaMoralId == Id).Include(f => f.Solicitud).SingleOrDefault();
            ViewBag.TipoRegimen = Int32.Parse(fisicamoral.Solicitud.TipoArrendatario);
            ViewBag.Fiador = fisicamoral.Solicitud.SolicitudFiador;
            ViewBag.Id = Id;
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        // GET: Arrendatarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arrendatario = await _context.Arrendatario
                .Include(a => a.FisicaMoral)
                .FirstOrDefaultAsync(m => m.ArrendatarioId == id);
            if (arrendatario == null)
            {
                return NotFound();
            }

            return View(arrendatario);
        }

        // GET: Arrendatarios/Create
        public IActionResult Create(int id, int TipoId)
        {
            ViewBag.TipoRegimen = TipoId;
            ViewBag.Id = id;
            return View();
        }

        // POST: Arrendatarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArrendatarioId,Nacionalidad,CondMigratoria,EstadoCivil,ConvenioEc,Domicilio,Colonia,DelegacionMunicipio,Estado,Telefono,Celular,Email,Profesion,IngresoMensual,Trabajo,Antiguedad,Puesto,TelefonoTrabajo,Horario,DomicilioTrabajo,ColoniaTrabajo,DelegMuniTrabajo,EstadoTrabajo,GiroTrabajo,WebTrabajo,JefeTrabajo,PuestoJefe,EmailJefe,Factura,ActaConstitutiva,PoderRepresentanteNo,DomicilioRepresentanteLegal,ColoniaRl,DeleMuni,TelefonoRl,EstadoRl,EmailRl,HorarioRl,IngresoMensualRl,SindicadoRl,RequiereFacturaRl,AfianzadoRl,AfianzadoraRl,FisicaMoralId,Rfc,RazonSocial,CodigoPostal,CodigoPostalTrabajo,TipoIdentificacion,NumeroIdentificacion,Nombre,ApePaterno,ApeMaterno,TipoRegimenFiscal")] Arrendatario arrendatario)
        {
            bool isError = false;
            List<ErroresViewModel> Error = new List<ErroresViewModel>();

            switch (arrendatario.TipoRegimenFiscal)
            {
                case 1:
                    if (arrendatario.Celular == null)
                    {
                        Error.Add(Mensajes.MensajesError("Celular"));
                        isError = true;
                    }
                    if (arrendatario.Email == null)
                    {
                        Error.Add(Mensajes.MensajesError("Email"));
                        isError = true;
                    }
                    break;

                case 2:
                    if (arrendatario.Rfc == null)
                    {
                        Error.Add(Mensajes.MensajesError("Rfc"));
                        isError = true;
                    }
                    if (arrendatario.RazonSocial == null)
                    {
                        Error.Add(Mensajes.MensajesError("RazonSocial"));
                        isError = true;
                    }
                    if (arrendatario.ActaConstitutiva == null)
                    {
                        Error.Add(Mensajes.MensajesError("ActaConstitutiva"));
                        isError = true;
                    }
                    if (arrendatario.PoderRepresentanteNo == null)
                    {
                        Error.Add(Mensajes.MensajesError("PoderRepresentanteNo"));
                        isError = true;
                    }
                    if (arrendatario.DomicilioRepresentanteLegal == null)
                    {
                        Error.Add(Mensajes.MensajesError("DomicilioRepresentanteLegal"));
                        isError = true;
                    }
                    if (arrendatario.ColoniaRl == null)
                    {
                        Error.Add(Mensajes.MensajesError("ColoniaRL"));
                        isError = true;
                    }
                    if (arrendatario.DeleMuni == null)
                    {
                        Error.Add(Mensajes.MensajesError("DeleMuni"));
                        isError = true;
                    }
                    if (arrendatario.CodigoPostalTrabajo == null)
                    {
                        Error.Add(Mensajes.MensajesError("CodigoPostalTrabajo"));
                        isError = true;
                    }
                    if (arrendatario.Estado == null)
                    {
                        Error.Add(Mensajes.MensajesError("Estado"));
                        isError = true;
                    }
                    if (arrendatario.EmailRl == null)
                    {
                        Error.Add(Mensajes.MensajesError("EmailRL"));
                        isError = true;
                    }
                    if (arrendatario.TelefonoRl == null)
                    {
                        Error.Add(Mensajes.MensajesError("TelefonoRL"));
                        isError = true;
                    }
                    if (arrendatario.EstadoRl == null)
                    {
                        Error.Add(Mensajes.MensajesError("EstadoRL"));
                        isError = true;
                    }
                    if (arrendatario.ActaConstitutiva == null)
                    {
                        Error.Add(Mensajes.MensajesError("ActaConstitutiva"));
                        isError = true;
                    }
                    if (arrendatario.PoderRepresentanteNo == null)
                    {
                        Error.Add(Mensajes.MensajesError("PoderRepresentanteNo"));
                        isError = true;
                    }
                    if (arrendatario.DomicilioRepresentanteLegal == null)
                    {
                        Error.Add(Mensajes.MensajesError("DomicilioRepresentanteLegal"));
                        isError = true;
                    }
                    if (arrendatario.ColoniaRl == null)
                    {
                        Error.Add(Mensajes.MensajesError("ColoniaRL"));
                        isError = true;
                    }
                    if (arrendatario.DeleMuni == null)
                    {
                        Error.Add(Mensajes.MensajesError("DeleMuni"));
                        isError = true;
                    }
                    if (arrendatario.CodigoPostalTrabajo == null)
                    {
                        Error.Add(Mensajes.MensajesError("CodigoPostalTrabajo"));
                        isError = true;
                    }

                    break;
            }
            if (arrendatario.Estado == null)
            {
                Error.Add(Mensajes.MensajesError("Estado"));
                isError = true;
            }
            if (arrendatario.Nombre == null)
            {
                Error.Add(Mensajes.MensajesError("Nombre"));
                isError = true;
            }
            if (arrendatario.ApePaterno == null)
            {
                Error.Add(Mensajes.MensajesError("ApePaterno"));
                isError = true;
            }
            if (arrendatario.Domicilio == null)
            {
                Error.Add(Mensajes.MensajesError("Domicilio"));
                isError = true;
            }
            if (arrendatario.Colonia == null)
            {
                Error.Add(Mensajes.MensajesError("Colonia"));
                isError = true;
            }
            if (arrendatario.DelegacionMunicipio == null)
            {
                Error.Add(Mensajes.MensajesError("DelegacionMunicipio"));
                isError = true;
            }
            if (arrendatario.CodigoPostal == null)
            {
                Error.Add(Mensajes.MensajesError("CodigoPostal"));
                isError = true;
            }
            if (arrendatario.Nacionalidad == null)
            {
                Error.Add(Mensajes.MensajesError("Nacionalidad"));
                isError = true;
            }

            if (isError == true)
            {
                Error.Add(Mensajes.MensajesError("Favor de completar los campos que estan en rojo"));
                ViewBag.Error = JsonConvert.SerializeObject(Error);
                ViewBag.TipoRegimen = arrendatario.TipoRegimenFiscal;
                ViewBag.Id = arrendatario.FisicaMoralId;
                return View(arrendatario);
            }
            else
            {
                _context.Add(arrendatario);

                await _context.SaveChangesAsync();
                var inv = new InvestigacionsController(_context);
                string nombre = string.Empty;
                switch (arrendatario.TipoRegimenFiscal)
                {
                    case 1:
                        nombre = arrendatario.Nombre.Trim() + " " + arrendatario.ApePaterno.Trim();
                        if (arrendatario.ApeMaterno != null)
                            nombre = nombre + " " + arrendatario.ApeMaterno.Trim();

                        inv.InsertaCandidatos(arrendatario.FisicaMoralId, nombre);
                        break;

                    case 2:

                        if(arrendatario.RazonSocial != null)
                        inv.InsertaCandidatos(arrendatario.FisicaMoralId, arrendatario.RazonSocial.Trim());

                        //Representate legal
                        nombre = arrendatario.Nombre.Trim() + " " + arrendatario.ApePaterno.Trim();
                        if (arrendatario.ApeMaterno != null)
                            nombre = nombre + " " + arrendatario.ApeMaterno.Trim();

                        inv.InsertaCandidatos(arrendatario.FisicaMoralId, nombre);
                        break;

                }

                bool isMatch = false;

                InvestigacionListasNegras invest = new InvestigacionListasNegras(_context);
                isMatch = invest.ListasNegras(arrendatario.Nombre, arrendatario.ApePaterno, arrendatario.ApeMaterno, arrendatario.Rfc, arrendatario.RazonSocial);

                if (isMatch == true)
                {
                    var fisicaMoral = _context.FisicaMoral.SingleOrDefault(f => f.FisicaMoralId == arrendatario.FisicaMoralId);
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
                        Observacion = "Se encontro en la lista negra a la siguiente persona: " + arrendatario.Nombre + " " + arrendatario.ApePaterno + " " + arrendatario.ApeMaterno
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
                            LogProceso = "Arrendatarios",
                            LogObjetoOut = e.ToString()
                        };
                        _context.Add(log);
                        _context.SaveChanges();
                    }
                }

                return RedirectToAction("Index", new { Id = arrendatario.FisicaMoralId });
            }
        }

        // GET: Arrendatarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arrendatario = await _context.Arrendatario.FindAsync(id);
            if (arrendatario == null)
            {
                return NotFound();
            }
            ViewBag.TipoRegimen = arrendatario.TipoRegimenFiscal;
            ViewBag.Id = arrendatario.FisicaMoralId;
            ViewBag.ArrendatarioId = id;
            return View(arrendatario);
        }

        // POST: Arrendatarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArrendatarioId,Nacionalidad,CondMigratoria,EstadoCivil,ConvenioEc,Domicilio,Colonia,DelegacionMunicipio,Estado,Telefono,Celular,Email,Profesion,IngresoMensual,Trabajo,Antiguedad,Puesto,TelefonoTrabajo,Horario,DomicilioTrabajo,ColoniaTrabajo,DelegMuniTrabajo,EstadoTrabajo,GiroTrabajo,WebTrabajo,JefeTrabajo,PuestoJefe,EmailJefe,Factura,ActaConstitutiva,PoderRepresentanteNo,DomicilioRepresentanteLegal,ColoniaRl,DeleMuni,TelefonoRl,EstadoRl,EmailRl,HorarioRl,IngresoMensualRl,SindicadoRl,RequiereFacturaRl,AfianzadoRl,AfianzadoraRl,FisicaMoralId,Rfc,RazonSocial,CodigoPostal,CodigoPostalTrabajo,TipoIdentificacion,NumeroIdentificacion,Nombre,ApePaterno,ApeMaterno,TipoRegimenFiscal")] Arrendatario arrendatario)
        {
            if (id != arrendatario.ArrendatarioId)
            {
                return NotFound();
            }
            ViewBag.ArrendatarioId = id;
            bool isError = false;
            List<ErroresViewModel> Error = new List<ErroresViewModel>();

            switch (arrendatario.TipoRegimenFiscal)
            {
                case 1:
                    if (arrendatario.Celular == null)
                    {
                        Error.Add(Mensajes.MensajesError("Celular"));
                        isError = true;
                    }
                    if (arrendatario.Email == null)
                    {
                        Error.Add(Mensajes.MensajesError("Email"));
                        isError = true;
                    }
                    break;

                case 2:
                    if (arrendatario.Rfc == null)
                    {
                        Error.Add(Mensajes.MensajesError("Rfc"));
                        isError = true;
                    }
                    if (arrendatario.RazonSocial == null)
                    {
                        Error.Add(Mensajes.MensajesError("RazonSocial"));
                        isError = true;
                    }
                    if (arrendatario.ActaConstitutiva == null)
                    {
                        Error.Add(Mensajes.MensajesError("ActaConstitutiva"));
                        isError = true;
                    }
                    if (arrendatario.PoderRepresentanteNo == null)
                    {
                        Error.Add(Mensajes.MensajesError("PoderRepresentanteNo"));
                        isError = true;
                    }
                    if (arrendatario.DomicilioRepresentanteLegal == null)
                    {
                        Error.Add(Mensajes.MensajesError("DomicilioRepresentanteLegal"));
                        isError = true;
                    }
                    if (arrendatario.ColoniaRl == null)
                    {
                        Error.Add(Mensajes.MensajesError("ColoniaRL"));
                        isError = true;
                    }
                    if (arrendatario.DeleMuni == null)
                    {
                        Error.Add(Mensajes.MensajesError("DeleMuni"));
                        isError = true;
                    }
                    if (arrendatario.CodigoPostalTrabajo == null)
                    {
                        Error.Add(Mensajes.MensajesError("CodigoPostalTrabajo"));
                        isError = true;
                    }
                    if (arrendatario.Estado == null)
                    {
                        Error.Add(Mensajes.MensajesError("Estado"));
                        isError = true;
                    }
                    if (arrendatario.EmailRl == null)
                    {
                        Error.Add(Mensajes.MensajesError("EmailRL"));
                        isError = true;
                    }
                    if (arrendatario.TelefonoRl == null)
                    {
                        Error.Add(Mensajes.MensajesError("TelefonoRL"));
                        isError = true;
                    }
                    if (arrendatario.EstadoRl == null)
                    {
                        Error.Add(Mensajes.MensajesError("EstadoRL"));
                        isError = true;
                    }
                    if (arrendatario.ActaConstitutiva == null)
                    {
                        Error.Add(Mensajes.MensajesError("ActaConstitutiva"));
                        isError = true;
                    }
                    if (arrendatario.PoderRepresentanteNo == null)
                    {
                        Error.Add(Mensajes.MensajesError("PoderRepresentanteNo"));
                        isError = true;
                    }
                    if (arrendatario.DomicilioRepresentanteLegal == null)
                    {
                        Error.Add(Mensajes.MensajesError("DomicilioRepresentanteLegal"));
                        isError = true;
                    }
                    if (arrendatario.ColoniaRl == null)
                    {
                        Error.Add(Mensajes.MensajesError("ColoniaRL"));
                        isError = true;
                    }
                    if (arrendatario.DeleMuni == null)
                    {
                        Error.Add(Mensajes.MensajesError("DeleMuni"));
                        isError = true;
                    }
                    if (arrendatario.CodigoPostalTrabajo == null)
                    {
                        Error.Add(Mensajes.MensajesError("CodigoPostalTrabajo"));
                        isError = true;
                    }

                    break;
            }
            if (arrendatario.Estado == null)
            {
                Error.Add(Mensajes.MensajesError("Estado"));
                isError = true;
            }
            if (arrendatario.Nombre == null)
            {
                Error.Add(Mensajes.MensajesError("Nombre"));
                isError = true;
            }
            if (arrendatario.ApePaterno == null)
            {
                Error.Add(Mensajes.MensajesError("ApePaterno"));
                isError = true;
            }
            if (arrendatario.Domicilio == null)
            {
                Error.Add(Mensajes.MensajesError("Domicilio"));
                isError = true;
            }
            if (arrendatario.Colonia == null)
            {
                Error.Add(Mensajes.MensajesError("Colonia"));
                isError = true;
            }
            if (arrendatario.DelegacionMunicipio == null)
            {
                Error.Add(Mensajes.MensajesError("DelegacionMunicipio"));
                isError = true;
            }
            if (arrendatario.CodigoPostal == null)
            {
                Error.Add(Mensajes.MensajesError("CodigoPostal"));
                isError = true;
            }
            if (arrendatario.Nacionalidad == null)
            {
                Error.Add(Mensajes.MensajesError("Nacionalidad"));
                isError = true;
            }

            if (isError == true)
            {
                Error.Add(Mensajes.MensajesError("Favor de completar los campos que estan en rojo"));
                ViewBag.Error = JsonConvert.SerializeObject(Error);
                ViewBag.TipoRegimen = arrendatario.TipoRegimenFiscal;
                ViewBag.Id = arrendatario.FisicaMoralId;
                return View(arrendatario);
            }
            else
            {
                _context.Update(arrendatario);
                await _context.SaveChangesAsync();
                ViewBag.TipoRegimen = arrendatario.TipoRegimenFiscal;
                ViewBag.Id = arrendatario.FisicaMoralId;

                bool isMatch = false;

                InvestigacionListasNegras invest = new InvestigacionListasNegras(_context);
                isMatch = invest.ListasNegras(arrendatario.Nombre, arrendatario.ApePaterno, arrendatario.ApeMaterno, arrendatario.Rfc, arrendatario.RazonSocial);

                if (isMatch == true)
                {
                    var fisicaMoral = _context.FisicaMoral.SingleOrDefault(f => f.FisicaMoralId == arrendatario.FisicaMoralId);
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
                        Observacion = "Se encontro en la lista negra a la siguiente persona: " + arrendatario.Nombre + " " + arrendatario.ApePaterno + " " + arrendatario.ApeMaterno
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
                            LogProceso = "Arrendatarios",
                            LogObjetoOut = e.ToString()
                        };
                        _context.Add(log);
                        _context.SaveChanges();
                    }
                }

                return RedirectToAction("Index", new { Id = arrendatario.FisicaMoralId });
            }
        }

        // GET: Arrendatarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arrendatario = await _context.Arrendatario
                .Include(a => a.FisicaMoral)
                .FirstOrDefaultAsync(m => m.ArrendatarioId == id);
            if (arrendatario == null)
            {
                return NotFound();
            }
            ViewBag.TipoRegimen = arrendatario.TipoRegimenFiscal;
            ViewBag.Id = arrendatario.FisicaMoralId;
            ViewBag.ArrendatarioId = arrendatario.ArrendatarioId;
            return View(arrendatario);
        }

        // POST: Arrendatarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var arrendatario = await _context.Arrendatario.FindAsync(id);
            var fisicamoralid = arrendatario.FisicaMoralId;
            _context.Arrendatario.Remove(arrendatario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = fisicamoralid });
        }

        private bool ArrendatarioExists(int id)
        {
            return _context.Arrendatario.Any(e => e.ArrendatarioId == id);
        }
    }
}
