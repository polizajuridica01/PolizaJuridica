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
    public class TipoProcesoesController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public TipoProcesoesController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: TipoProcesoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoProceso.ToListAsync());
        }

        // GET: TipoProcesoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProceso = await _context.TipoProceso
                .FirstOrDefaultAsync(m => m.TipoProcesoId == id);
            if (tipoProceso == null)
            {
                return NotFound();
            }

            return View(tipoProceso);
        }

        // GET: TipoProcesoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoProcesoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoProcesoId,Descripcion,Orden")] TipoProceso tipoProceso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoProceso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoProceso);
        }

        // GET: TipoProcesoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProceso = await _context.TipoProceso.FindAsync(id);
            if (tipoProceso == null)
            {
                return NotFound();
            }
            return View(tipoProceso);
        }

        // POST: TipoProcesoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoProcesoId,Descripcion,Orden")] TipoProceso tipoProceso)
        {
            if (id != tipoProceso.TipoProcesoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoProceso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoProcesoExists(tipoProceso.TipoProcesoId))
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
            return View(tipoProceso);
        }

        // GET: TipoProcesoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProceso = await _context.TipoProceso
                .FirstOrDefaultAsync(m => m.TipoProcesoId == id);
            if (tipoProceso == null)
            {
                return NotFound();
            }

            return View(tipoProceso);
        }

        // POST: TipoProcesoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoProceso = await _context.TipoProceso.FindAsync(id);
            _context.TipoProceso.Remove(tipoProceso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoProcesoExists(int id)
        {
            return _context.TipoProceso.Any(e => e.TipoProcesoId == id);
        }
    }
}
