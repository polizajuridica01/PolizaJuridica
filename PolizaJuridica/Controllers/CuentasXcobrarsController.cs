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
    public class CuentasXcobrarsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public CuentasXcobrarsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: CuentasXcobrars
        public async Task<IActionResult> Index()
        {
            var polizaJuridicaDbContext = _context.CuentasXcobrar.Include(c => c.Poliza);
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        // GET: CuentasXcobrars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentasXcobrar = await _context.CuentasXcobrar
                .Include(c => c.Poliza)
                .FirstOrDefaultAsync(m => m.Ccid == id);
            if (cuentasXcobrar == null)
            {
                return NotFound();
            }

            return View(cuentasXcobrar);
        }

        // GET: CuentasXcobrars/Create
        public IActionResult Create()
        {
            ViewData["PolizaId"] = new SelectList(_context.Poliza, "PolizaId", "PolizaId");
            return View();
        }

        // POST: CuentasXcobrars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ccid,Fecha,Importe,Numeral,ImporteIncr,PolizaId")] CuentasXcobrar cuentasXcobrar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuentasXcobrar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PolizaId"] = new SelectList(_context.Poliza, "PolizaId", "PolizaId", cuentasXcobrar.PolizaId);
            return View(cuentasXcobrar);
        }

        // GET: CuentasXcobrars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentasXcobrar = await _context.CuentasXcobrar.FindAsync(id);
            if (cuentasXcobrar == null)
            {
                return NotFound();
            }
            ViewData["PolizaId"] = new SelectList(_context.Poliza, "PolizaId", "PolizaId", cuentasXcobrar.PolizaId);
            return View(cuentasXcobrar);
        }

        // POST: CuentasXcobrars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Ccid,Fecha,Importe,Numeral,ImporteIncr,PolizaId")] CuentasXcobrar cuentasXcobrar)
        {
            if (id != cuentasXcobrar.Ccid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuentasXcobrar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuentasXcobrarExists(cuentasXcobrar.Ccid))
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
            ViewData["PolizaId"] = new SelectList(_context.Poliza, "PolizaId", "PolizaId", cuentasXcobrar.PolizaId);
            return View(cuentasXcobrar);
        }

        // GET: CuentasXcobrars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentasXcobrar = await _context.CuentasXcobrar
                .Include(c => c.Poliza)
                .FirstOrDefaultAsync(m => m.Ccid == id);
            if (cuentasXcobrar == null)
            {
                return NotFound();
            }

            return View(cuentasXcobrar);
        }

        // POST: CuentasXcobrars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuentasXcobrar = await _context.CuentasXcobrar.FindAsync(id);
            _context.CuentasXcobrar.Remove(cuentasXcobrar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuentasXcobrarExists(int id)
        {
            return _context.CuentasXcobrar.Any(e => e.Ccid == id);
        }
    }
}
