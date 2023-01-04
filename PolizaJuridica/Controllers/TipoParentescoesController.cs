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
    public class TipoParentescoesController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public TipoParentescoesController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: TipoParentescoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoParentesco.ToListAsync());
        }

        // GET: TipoParentescoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoParentesco = await _context.TipoParentesco
                .FirstOrDefaultAsync(m => m.TipoParentescoId == id);
            if (tipoParentesco == null)
            {
                return NotFound();
            }

            return View(tipoParentesco);
        }

        // GET: TipoParentescoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoParentescoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoParentescoId,TipoParentescoDesc")] TipoParentesco tipoParentesco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoParentesco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoParentesco);
        }

        // GET: TipoParentescoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoParentesco = await _context.TipoParentesco.FindAsync(id);
            if (tipoParentesco == null)
            {
                return NotFound();
            }
            return View(tipoParentesco);
        }

        // POST: TipoParentescoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoParentescoId,TipoParentescoDesc")] TipoParentesco tipoParentesco)
        {
            if (id != tipoParentesco.TipoParentescoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoParentesco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoParentescoExists(tipoParentesco.TipoParentescoId))
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
            return View(tipoParentesco);
        }

        // GET: TipoParentescoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoParentesco = await _context.TipoParentesco
                .FirstOrDefaultAsync(m => m.TipoParentescoId == id);
            if (tipoParentesco == null)
            {
                return NotFound();
            }

            return View(tipoParentesco);
        }

        // POST: TipoParentescoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoParentesco = await _context.TipoParentesco.FindAsync(id);
            _context.TipoParentesco.Remove(tipoParentesco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoParentescoExists(int id)
        {
            return _context.TipoParentesco.Any(e => e.TipoParentescoId == id);
        }
    }
}
