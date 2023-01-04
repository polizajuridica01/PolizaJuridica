using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PolizaJuridica.Data;
using PolizaJuridica.ViewModels;

namespace PolizaJuridica.Controllers
{
    public class PolizasController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public PolizasController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Polizas
        public async Task<IActionResult> Index(DateTime FechaInicio, DateTime FechaFin)
        {
            int usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            string areaDescripcion = User.FindFirst("Area").Value;
            int representacionid = Int32.Parse(User.FindFirst("RepresentacionId").Value);
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            if (FechaInicio.GetHashCode() == 0)
                FechaInicio = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            if (FechaFin.GetHashCode() == 0)
                FechaFin = FechaInicio.AddMonths(1).AddDays(-1);
            List<PolizaIndexViewModel> polizaJuridicaDbContext = new List<PolizaIndexViewModel>();

            List<CentroCostos>  listCentro = new List<CentroCostos>();
            listCentro = _context.CentroCostos.ToList();

            //meter el estatus de la poliza
            if (areaDescripcion == "Asesor" || areaDescripcion == "Representante")
            {
                if (areaDescripcion == "Asesor")
                {
                    polizaJuridicaDbContext = await _context.Poliza
                   .Include(p => p.FisicaMoral.Solicitud)
                   .Include(p => p.FisicaMoral.Solicitud.Representante.Representacion)
                   .Include(p => p.UsuarioPoliza).ThenInclude(t => t.TipoProcesoPo).OrderByDescending(p => p.PolizaId)
                   .Include(p => p.Firmas)
                   .Where(p => p.Creacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                          p.Creacion.Date.CompareTo(FechaFin.Date) <= 0 && p.FisicaMoral.Solicitud.Asesorid == usuarioid || p.FisicaMoral.Solicitud.Creadorid == usuarioid)
                   .Select(sl => new PolizaIndexViewModel
                   {
                       PolizaId = sl.PolizaId,
                       SolicitudId = sl.FisicaMoral.SolicitudId,
                       SolicitudTipoPoliza = sl.FisicaMoral.Solicitud.SolicitudTipoPoliza,
                       Costo = sl.FisicaMoral.Solicitud.CentroCostosId > 0 ? CentroCosto(listCentro, (int)sl.FisicaMoral.Solicitud.CentroCostosId) : (decimal)sl.FisicaMoral.Solicitud.CostoPoliza,
                       RepresentacionNombre = sl.FisicaMoral.Solicitud.Representante.Representacion.RepresentacionNombre,
                       RepresentanteUsuarioNomCompleto = sl.FisicaMoral.Solicitud.Representante.UsuarioNomCompleto,
                       Creacion = sl.Creacion,
                       Estatus = sl.UsuarioPoliza.OrderByDescending(u => u.UsuarioPolizaId).FirstOrDefault().TipoProcesoPo.Descripcion,
                       IsRenovacion = sl.FisicaMoral.Solicitud.IsRenovacion
                   }).ToListAsync();
                }
                if (areaDescripcion == "Representante")
                {
                    polizaJuridicaDbContext = await _context.Poliza
                   .Include(p => p.FisicaMoral.Solicitud)
                   .Include(p => p.FisicaMoral.Solicitud.Representante.Representacion)
                   .Include(p => p.UsuarioPoliza).ThenInclude(t => t.TipoProcesoPo).OrderByDescending(p => p.PolizaId)
                   .Include(p => p.Firmas)
                   .Where(p => p.Creacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                          p.Creacion.Date.CompareTo(FechaFin.Date) <= 0 &&
                          p.FisicaMoral.Solicitud.Representante.RepresentacionId == representacionid)
                   .Select(sl => new PolizaIndexViewModel
                   {
                       PolizaId = sl.PolizaId,
                       SolicitudId = sl.FisicaMoral.SolicitudId,
                       SolicitudTipoPoliza = sl.FisicaMoral.Solicitud.SolicitudTipoPoliza,
                       Costo = sl.FisicaMoral.Solicitud.CentroCostosId > 0 ? CentroCosto(listCentro, (int)sl.FisicaMoral.Solicitud.CentroCostosId) : (decimal)sl.FisicaMoral.Solicitud.CostoPoliza,
                       RepresentacionNombre = sl.FisicaMoral.Solicitud.Representante.Representacion.RepresentacionNombre,
                       RepresentanteUsuarioNomCompleto = sl.FisicaMoral.Solicitud.Representante.UsuarioNomCompleto,
                       Creacion = sl.Creacion,
                       Estatus = sl.UsuarioPoliza.OrderByDescending(u => u.UsuarioPolizaId).FirstOrDefault().TipoProcesoPo.Descripcion,
                       IsRenovacion = sl.FisicaMoral.Solicitud.IsRenovacion
                   }).ToListAsync();
                }
            }
            else
            {
                polizaJuridicaDbContext = await _context.Poliza
               .Include(p => p.FisicaMoral.Solicitud)
               .Include(p => p.FisicaMoral.Solicitud.Representante.Representacion)
               .Include(p => p.UsuarioPoliza).ThenInclude(t => t.TipoProcesoPo).OrderByDescending(p => p.PolizaId)
               .Include(p => p.Firmas)
               .Where(p => p.Creacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                      p.Creacion.Date.CompareTo(FechaFin.Date) <= 0)
               .Select(sl => new PolizaIndexViewModel
                      {
                          PolizaId = sl.PolizaId,
                          SolicitudId = sl.FisicaMoral.SolicitudId,
                          SolicitudTipoPoliza = sl.FisicaMoral.Solicitud.SolicitudTipoPoliza,
                          Costo = sl.FisicaMoral.Solicitud.CentroCostosId > 0 ? CentroCosto(listCentro,(int)sl.FisicaMoral.Solicitud.CentroCostosId) : (decimal)sl.FisicaMoral.Solicitud.CostoPoliza,
                          RepresentacionNombre = sl.FisicaMoral.Solicitud.Representante.Representacion.RepresentacionNombre,
                          RepresentanteUsuarioNomCompleto = sl.FisicaMoral.Solicitud.Representante.UsuarioNomCompleto,
                          Creacion = sl.Creacion,
                          Estatus = sl.UsuarioPoliza.OrderByDescending(u => u.UsuarioPolizaId).FirstOrDefault().TipoProcesoPo.Descripcion,
                          IsRenovacion = sl.FisicaMoral.Solicitud.IsRenovacion
                      }).ToListAsync();                
            }

            ViewBag.FiltroFecha = "Fecha de: " + FechaInicio.ToShortDateString() + " Al " + FechaFin.ToShortDateString();

            int[] areaid = { 3, 12 };
            ViewBag.Usuarios = new SelectList(_context.Usuarios.Where(p => areaid.Contains(p.AreaId) && p.Activo == true), "UsuariosId", "UsuarioNomCompleto");

            return View(polizaJuridicaDbContext);
        }

        public decimal CentroCosto(List<CentroCostos> listCentro,int ccId) {
            var cc = listCentro.FirstOrDefault(c => c.CentroCostosId == ccId);
            return cc != null ? cc.CentroCostosMonto : 0 ;
        }
        // GET: Polizas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poliza = await _context.Poliza
                .Include(p => p.FisicaMoral)
                //.Include(p => p.PolizaEstatus)
                .FirstOrDefaultAsync(m => m.PolizaId == id);
            if (poliza == null)
            {
                return NotFound();
            }

            return View(poliza);
        }

        // GET: Polizas/Create
        public IActionResult Create()
        {
            ViewData["FiadorFid"] = new SelectList(_context.FiadorF, "FiadorFid", "FiadorFcodigoPostal");
            ViewData["FiadorMid"] = new SelectList(_context.FiadorM, "FiadorMid", "FiadorMid");
            ViewData["FisicaMoralId"] = new SelectList(_context.FisicaMoral, "FisicaMoralId", "FisicaMoralId");
            //ViewData["PolizaEstatusId"] = new SelectList(_context.PolizaEstatus, "PolizaEstatusId", "PolizaEstatusId");
            return View();
        }

        // POST: Polizas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolizaId,Creacion,PolizaEstatusId,FechaEstatus,FiadorFid,FiadorMid,FisicaMoralId")] Poliza poliza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poliza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FisicaMoralId"] = new SelectList(_context.FisicaMoral, "FisicaMoralId", "FisicaMoralId", poliza.FisicaMoralId);
            //ViewData["PolizaEstatusId"] = new SelectList(_context.PolizaEstatus, "PolizaEstatusId", "PolizaEstatusId", poliza.PolizaEstatusId);
            return View(poliza);
        }

        // GET: Polizas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poliza = await _context.Poliza.FindAsync(id);
            if (poliza == null)
            {
                return NotFound();
            }
            ViewData["FisicaMoralId"] = new SelectList(_context.FisicaMoral, "FisicaMoralId", "FisicaMoralId", poliza.FisicaMoralId);
            //ViewData["PolizaEstatusId"] = new SelectList(_context.PolizaEstatus, "PolizaEstatusId", "PolizaEstatusId", poliza.PolizaEstatusId);
            return View(poliza);
        }

        // POST: Polizas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PolizaId,Creacion,PolizaEstatusId,FechaEstatus,FiadorFid,FiadorMid,FisicaMoralId")] Poliza poliza)
        {
            if (id != poliza.PolizaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poliza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolizaExists(poliza.PolizaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FisicaMoralId"] = new SelectList(_context.FisicaMoral, "FisicaMoralId", "FisicaMoralId", poliza.FisicaMoralId);
            //ViewData["PolizaEstatusId"] = new SelectList(_context.PolizaEstatus, "PolizaEstatusId", "PolizaEstatusId", poliza.PolizaEstatusId);
            return View(poliza);
        }

        // GET: Polizas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poliza = await _context.Poliza
                .Include(p => p.FisicaMoral)
                //.Include(p => p.PolizaEstatus)
                .FirstOrDefaultAsync(m => m.PolizaId == id);
            if (poliza == null)
            {
                return NotFound();
            }

            return View(poliza);
        }

        // POST: Polizas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poliza = await _context.Poliza.FindAsync(id);
            _context.Poliza.Remove(poliza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolizaExists(int id)
        {
            return _context.Poliza.Any(e => e.PolizaId == id);
        }

        // GET: Find
        public async Task<string> Find(int? id)
        {
            string result = string.Empty;
            var p = await _context.Poliza.Include(po => po.FisicaMoral.Solicitud).SingleOrDefaultAsync(po => po.PolizaId == id);

            FirmaViewModel f = new FirmaViewModel();

            f.PolizaId = p.PolizaId;
            f.direccion = p.FisicaMoral.Solicitud.SolicitudUbicacionArrendado;


            result = JsonConvert.SerializeObject(f);
            return result;
        }

    }
}
