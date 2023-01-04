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
    public class MovimientoMaestroesController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public MovimientoMaestroesController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: MovimientoMaestroes
        public async Task<IActionResult> Index()
        {
            var polizaJuridicaDbContext = _context.MovimientoMaestro.Include(m => m.Cuenta);
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        // GET: MovimientoMaestroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimientoMaestro = await _context.MovimientoMaestro
                .Include(m => m.Cuenta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimientoMaestro == null)
            {
                return NotFound();
            }

            return View(movimientoMaestro);
        }

        // GET: MovimientoMaestroes/Create
        public IActionResult Create()
        {
            ViewData["CuentaId"] = new SelectList(_context.CuentasBancarias, "CuentasBancarias1", "CuentasBancarias1");
            return View();
        }

        // POST: MovimientoMaestroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CuentaId,Entrada,Salida,Fecha")] MovimientoMaestro movimientoMaestro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimientoMaestro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CuentaId"] = new SelectList(_context.CuentasBancarias, "CuentasBancarias1", "CuentasBancarias1", movimientoMaestro.CuentaId);
            return View(movimientoMaestro);
        }

        // GET: MovimientoMaestroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimientoMaestro = await _context.MovimientoMaestro.FindAsync(id);
            if (movimientoMaestro == null)
            {
                return NotFound();
            }
            ViewData["CuentaId"] = new SelectList(_context.CuentasBancarias, "CuentasBancarias1", "CuentasBancarias1", movimientoMaestro.CuentaId);
            return View(movimientoMaestro);
        }

        // POST: MovimientoMaestroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CuentaId,Entrada,Salida,Fecha")] MovimientoMaestro movimientoMaestro)
        {
            if (id != movimientoMaestro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimientoMaestro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimientoMaestroExists(movimientoMaestro.Id))
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
            ViewData["CuentaId"] = new SelectList(_context.CuentasBancarias, "CuentasBancarias1", "CuentasBancarias1", movimientoMaestro.CuentaId);
            return View(movimientoMaestro);
        }

        // GET: MovimientoMaestroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimientoMaestro = await _context.MovimientoMaestro
                .Include(m => m.Cuenta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimientoMaestro == null)
            {
                return NotFound();
            }

            return View(movimientoMaestro);
        }

        // POST: MovimientoMaestroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimientoMaestro = await _context.MovimientoMaestro.FindAsync(id);
            _context.MovimientoMaestro.Remove(movimientoMaestro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimientoMaestroExists(int id)
        {
            return _context.MovimientoMaestro.Any(e => e.Id == id);
        }
    }
}
