using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PolizaJuridica.Data;

namespace PolizaJuridica.Controllers
{
    public class ReporteInvstsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public ReporteInvstsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: ReporteInvsts
        public async Task<IActionResult> Index(int id)
        {
            var polizaJuridicaDbContext = _context.ReporteInvst.Where(r => r.FisicaMoralId == id).Include(r => r.FisicaMoral);
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        // GET: ReporteInvsts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporteInvst = await _context.ReporteInvst
                .Include(r => r.FisicaMoral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reporteInvst == null)
            {
                return NotFound();
            }

            var fm = _context.FisicaMoral.Include(f => f.Solicitud).SingleOrDefault(f => f.FisicaMoralId == reporteInvst.FisicaMoralId);
            ViewBag.Arrendatario = fm;
            ViewData["ff"] = _context.FiadorF.Where(f => f.FisicaMoralId == reporteInvst.FisicaMoralId).ToList();
            ViewData["fm"] = _context.FiadorM.Where(f => f.FisicaMoralId == reporteInvst.FisicaMoralId).ToList();
            ViewBag.Id = id;
            ViewData["PersonasOcupan"] = _context.PersonasOcupanInm.Include(p => p.TipoParentesco).Where(p => p.FisicaMoralId == id).ToList();

            return View(reporteInvst);
        }

        // GET: ReporteInvsts/Create
        public IActionResult Create(int? id)
        {

                var reporte = _context.ReporteInvst.SingleOrDefault( r => r.FisicaMoral.SolicitudId == id);
                if(reporte != null)
                {
                    return RedirectToAction("Edit",new { id = reporte.Id } );
                }
            

            var fm = _context.FisicaMoral.Include( f => f.Solicitud).SingleOrDefault(f => f.SolicitudId == id);
            ViewBag.Arrendatario = fm;
            ViewData["ff"] = _context.FiadorF.Where(f => f.FisicaMoral.SolicitudId == id).ToList();
            ViewData["fm"] = _context.FiadorM.Where(f => f.FisicaMoral.SolicitudId == id).ToList();
            ViewBag.Id = fm == null ? 0 : fm.FisicaMoralId;
            ViewData["PersonasOcupan"] = _context.PersonasOcupanInm.Include(p => p.TipoParentesco).Where(p => p.FisicaMoral.SolicitudId == id).ToList();
            return View();
        }

        // POST: ReporteInvsts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Texto1,Texto2,FisicaMoralId")] ReporteInvst reporteInvst)
        {
                _context.Add(reporteInvst);
                await _context.SaveChangesAsync();
                var solicitudid = _context.FisicaMoral.SingleOrDefault( f => f.FisicaMoralId == reporteInvst.FisicaMoralId).SolicitudId;
                return RedirectToAction(nameof(Edit),new { id = solicitudid });
        }

        // GET: ReporteInvsts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporteInvst = await _context.ReporteInvst.Include(r => r.FisicaMoral.Solicitud).SingleOrDefaultAsync(r => r.FisicaMoral.SolicitudId == id);
            if (reporteInvst == null)
            {
                return NotFound();
            }
            var fm = _context.FisicaMoral.Include(f => f.Solicitud).SingleOrDefault(f => f.FisicaMoralId == reporteInvst.FisicaMoralId);
            ViewBag.Arrendatario = fm;
            ViewData["ff"] = _context.FiadorF.Where(f => f.FisicaMoralId == reporteInvst.FisicaMoralId).ToList();
            ViewData["fm"] = _context.FiadorM.Where(f => f.FisicaMoralId == reporteInvst.FisicaMoralId).ToList();
            ViewBag.Id = fm.FisicaMoralId;
            ViewData["PersonasOcupan"] = _context.PersonasOcupanInm.Include(p => p.TipoParentesco).Where(p => p.FisicaMoralId == reporteInvst.FisicaMoralId).ToList();
            return View(reporteInvst);
        }

        // POST: ReporteInvsts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Texto1,Texto2,FisicaMoralId")] ReporteInvst reporteInvst)
        {
            if (id != reporteInvst.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reporteInvst);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReporteInvstExists(reporteInvst.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var solicitudId = _context.FisicaMoral.SingleOrDefault(f => f.FisicaMoralId == reporteInvst.FisicaMoralId).SolicitudId;
                return RedirectToAction(nameof(Edit),new { id = solicitudId });
            }
            var fm = _context.FisicaMoral.Include(f => f.Solicitud).SingleOrDefault(f => f.FisicaMoralId == reporteInvst.FisicaMoralId);
            ViewBag.Arrendatario = fm;
            ViewData["ff"] = _context.FiadorF.Where(f => f.FisicaMoralId == reporteInvst.FisicaMoralId).ToList();
            ViewData["fm"] = _context.FiadorM.Where(f => f.FisicaMoralId == reporteInvst.FisicaMoralId).ToList();
            ViewData["PersonasOcupan"] = _context.PersonasOcupanInm.Include(p => p.TipoParentesco).Where(p => p.FisicaMoralId == reporteInvst.FisicaMoralId).ToList();
            return View(reporteInvst);
        }

        // GET: ReporteInvsts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporteInvst = await _context.ReporteInvst
                .Include(r => r.FisicaMoral)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reporteInvst == null)
            {
                return NotFound();
            }

            return View(reporteInvst);
        }

        // POST: ReporteInvsts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reporteInvst = await _context.ReporteInvst.FindAsync(id);
            _context.ReporteInvst.Remove(reporteInvst);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReporteInvstExists(int id)
        {
            return _context.ReporteInvst.Any(e => e.Id == id);
        }
    }
}
