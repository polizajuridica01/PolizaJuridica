using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PolizaJuridica.Data;

namespace PolizaJuridica.Controllers
{
    public class FlujoSolicitudsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public FlujoSolicitudsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: FlujoSolicituds
        public async Task<IActionResult> Index(int Id)
        {
            if (Id == 0 || Id == null)
                return NotFound();

            ViewBag.Id = Id;
            var polizaJuridicaDbContext = _context.FlujoSolicitud.Include(f => f.Persona.Area).Include(f => f.Solicitud).Where(f => f.SolicitudId == Id);
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        // GET: FlujoSolicituds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flujoSolicitud = await _context.FlujoSolicitud
                .Include(f => f.Persona)
                .Include(f => f.Solicitud)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flujoSolicitud == null)
            {
                return NotFound();
            }

            return View(flujoSolicitud);
        }

        // GET: FlujoSolicituds/Create
        public IActionResult Create(int Id)
        {
            int[] area = { 7, 9, 10, 11, 12 };
            ViewData["PersonaId"] = new SelectList(_context.Usuarios.Where(u => area.Contains( u.AreaId)), "UsuariosId", "UsuarioNomCompleto");
            ViewBag.SolicitudId = Id;
            return View();
        }

        // POST: FlujoSolicituds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonaId,FechaAsignacion,SolicitudId")] FlujoSolicitud flujoSolicitud)
        {
            flujoSolicitud.Id = 0;
            flujoSolicitud.FechaAsignacion = DateTime.Now;
            if (flujoSolicitud.PersonaId > 0)
            {
                _context.Add(flujoSolicitud);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new { id = flujoSolicitud.SolicitudId});
            }
            int[] area = { 7, 9, 10, 11, 12 };
            ViewData["PersonaId"] = new SelectList(_context.Usuarios.Where(u => area.Contains(u.AreaId)), "UsuariosId", "UsuarioNomCompleto");
            ViewBag.SolicitudId = flujoSolicitud.SolicitudId;
            return View(flujoSolicitud);
        }

        // GET: FlujoSolicituds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flujoSolicitud = await _context.FlujoSolicitud.FindAsync(id);
            if (flujoSolicitud == null)
            {
                return NotFound();
            }
            int[] area = { 7, 9, 10, 11, 12 };
            ViewData["PersonaId"] = new SelectList(_context.Usuarios.Where(u => area.Contains(u.AreaId)), "UsuariosId", "UsuarioNomCompleto");
            return View(flujoSolicitud);
        }

        // POST: FlujoSolicituds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonaId,FechaAsignacion,SolicitudId")] FlujoSolicitud flujoSolicitud)
        {
            flujoSolicitud.FechaAsignacion = DateTime.Now;
            if (id != flujoSolicitud.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flujoSolicitud);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlujoSolicitudExists(flujoSolicitud.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),new { id = flujoSolicitud.SolicitudId});
            }
            int[] area = { 7, 9, 10, 11, 12 };
            ViewData["PersonaId"] = new SelectList(_context.Usuarios.Where(u => area.Contains(u.AreaId)), "UsuariosId", "UsuarioNomCompleto");
            return View(flujoSolicitud);
        }

        // GET: FlujoSolicituds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flujoSolicitud = await _context.FlujoSolicitud
                .Include(f => f.Persona)
                .Include(f => f.Solicitud)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flujoSolicitud == null)
            {
                return NotFound();
            }

            return View(flujoSolicitud);
        }

        // POST: FlujoSolicituds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flujoSolicitud = await _context.FlujoSolicitud.FindAsync(id);
            _context.FlujoSolicitud.Remove(flujoSolicitud);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new { id = flujoSolicitud.SolicitudId});
        }

        private bool FlujoSolicitudExists(int id)
        {
            return _context.FlujoSolicitud.Any(e => e.Id == id);
        }
    }
}
