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
    public class AcuerdosController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public AcuerdosController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Acuerdos
        public async Task<IActionResult> Index(int Id)
        {
            
            
            var polizaJuridicaDbContext = _context.Acuerdos.Include(a => a.DetalleInvestigacion.Investigacion).Where(a => a.DetalleInvestigacionId == Id);
            ViewBag.Id = polizaJuridicaDbContext.FirstOrDefault().DetalleInvestigacion.Investigacion.FisicaMoralId;
            ViewBag.Detalle = polizaJuridicaDbContext.FirstOrDefault().DetalleInvestigacion;
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        // GET: Acuerdos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acuerdos = await _context.Acuerdos
                .Include(a => a.DetalleInvestigacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acuerdos == null)
            {
                return NotFound();
            }

            return View(acuerdos);
        }

        // GET: Acuerdos/Create
        public IActionResult Create()
        {
            ViewData["DetalleInvestigacionId"] = new SelectList(_context.DetalleInvestigacion, "Id", "Id");
            return View();
        }

        // POST: Acuerdos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Acuerdo,Juicio,Fecha,DetalleInvestigacionId")] Acuerdos acuerdos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acuerdos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DetalleInvestigacionId"] = new SelectList(_context.DetalleInvestigacion, "Id", "Id", acuerdos.DetalleInvestigacionId);
            return View(acuerdos);
        }

        // GET: Acuerdos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acuerdos = await _context.Acuerdos.FindAsync(id);
            if (acuerdos == null)
            {
                return NotFound();
            }
            ViewData["DetalleInvestigacionId"] = new SelectList(_context.DetalleInvestigacion, "Id", "Id", acuerdos.DetalleInvestigacionId);
            return View(acuerdos);
        }

        // POST: Acuerdos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Acuerdo,Juicio,Fecha,DetalleInvestigacionId")] Acuerdos acuerdos)
        {
            if (id != acuerdos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acuerdos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcuerdosExists(acuerdos.Id))
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
            ViewData["DetalleInvestigacionId"] = new SelectList(_context.DetalleInvestigacion, "Id", "Id", acuerdos.DetalleInvestigacionId);
            return View(acuerdos);
        }

        // GET: Acuerdos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acuerdos = await _context.Acuerdos
                .Include(a => a.DetalleInvestigacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acuerdos == null)
            {
                return NotFound();
            }

            return View(acuerdos);
        }

        // POST: Acuerdos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var acuerdos = await _context.Acuerdos.FindAsync(id);
            _context.Acuerdos.Remove(acuerdos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcuerdosExists(int id)
        {
            return _context.Acuerdos.Any(e => e.Id == id);
        }
    }
}
