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
    public class TipoProcesoPoesController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public TipoProcesoPoesController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: TipoProcesoPoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoProcesoPo.ToListAsync());
        }

        // GET: TipoProcesoPoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProcesoPo = await _context.TipoProcesoPo
                .FirstOrDefaultAsync(m => m.TipoProcesoPoId == id);
            if (tipoProcesoPo == null)
            {
                return NotFound();
            }

            return View(tipoProcesoPo);
        }

        // GET: TipoProcesoPoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoProcesoPoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoProcesoPoId,Descripcion,Orden")] TipoProcesoPo tipoProcesoPo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoProcesoPo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoProcesoPo);
        }

        // GET: TipoProcesoPoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProcesoPo = await _context.TipoProcesoPo.FindAsync(id);
            if (tipoProcesoPo == null)
            {
                return NotFound();
            }
            return View(tipoProcesoPo);
        }

        // POST: TipoProcesoPoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoProcesoPoId,Descripcion,Orden")] TipoProcesoPo tipoProcesoPo)
        {
            if (id != tipoProcesoPo.TipoProcesoPoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoProcesoPo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoProcesoPoExists(tipoProcesoPo.TipoProcesoPoId))
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
            return View(tipoProcesoPo);
        }

        // GET: TipoProcesoPoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProcesoPo = await _context.TipoProcesoPo
                .FirstOrDefaultAsync(m => m.TipoProcesoPoId == id);
            if (tipoProcesoPo == null)
            {
                return NotFound();
            }

            return View(tipoProcesoPo);
        }

        // POST: TipoProcesoPoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoProcesoPo = await _context.TipoProcesoPo.FindAsync(id);
            _context.TipoProcesoPo.Remove(tipoProcesoPo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoProcesoPoExists(int id)
        {
            return _context.TipoProcesoPo.Any(e => e.TipoProcesoPoId == id);
        }
    }
}
