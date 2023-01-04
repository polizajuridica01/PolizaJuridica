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
    public class CentroCostosController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public CentroCostosController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: CentroCostos
        public async Task<IActionResult> Index()
        {
            return View(await _context.CentroCostos.ToListAsync());
        }

        // GET: CentroCostos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroCostos = await _context.CentroCostos
                .FirstOrDefaultAsync(m => m.CentroCostosId == id);
            if (centroCostos == null)
            {
                return NotFound();
            }

            return View(centroCostos);
        }

        // GET: CentroCostos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CentroCostos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CentroCostosId,CentroCostosTipo,CentroCostosMonto,CentroCostosRentaInicial,CentroCostosRentaFinal")] CentroCostos centroCostos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(centroCostos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(centroCostos);
        }

        // GET: CentroCostos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroCostos = await _context.CentroCostos.FindAsync(id);
            if (centroCostos == null)
            {
                return NotFound();
            }
            return View(centroCostos);
        }

        // POST: CentroCostos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CentroCostosId,CentroCostosTipo,CentroCostosMonto,CentroCostosRentaInicial,CentroCostosRentaFinal")] CentroCostos centroCostos)
        {
            if (id != centroCostos.CentroCostosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(centroCostos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CentroCostosExists(centroCostos.CentroCostosId))
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
            return View(centroCostos);
        }

        // GET: CentroCostos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroCostos = await _context.CentroCostos
                .FirstOrDefaultAsync(m => m.CentroCostosId == id);
            if (centroCostos == null)
            {
                return NotFound();
            }

            return View(centroCostos);
        }

        // POST: CentroCostos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var centroCostos = await _context.CentroCostos.FindAsync(id);
            _context.CentroCostos.Remove(centroCostos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CentroCostosExists(int id)
        {
            return _context.CentroCostos.Any(e => e.CentroCostosId == id);
        }
    }
}
