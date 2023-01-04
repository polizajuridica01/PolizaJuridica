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
    public class DetalleMovimientoesController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public DetalleMovimientoesController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: DetalleMovimientoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DetalleMovimiento.ToListAsync());
        }

        // GET: DetalleMovimientoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleMovimiento = await _context.DetalleMovimiento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleMovimiento == null)
            {
                return NotFound();
            }

            return View(detalleMovimiento);
        }

        // GET: DetalleMovimientoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetalleMovimientoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo,Importe,Referencias,Origen,Fecha,Observaciones")] DetalleMovimiento detalleMovimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleMovimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detalleMovimiento);
        }

        // GET: DetalleMovimientoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleMovimiento = await _context.DetalleMovimiento.FindAsync(id);
            if (detalleMovimiento == null)
            {
                return NotFound();
            }
            return View(detalleMovimiento);
        }

        // POST: DetalleMovimientoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,Importe,Referencias,Origen,Fecha,Observaciones")] DetalleMovimiento detalleMovimiento)
        {
            if (id != detalleMovimiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleMovimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleMovimientoExists(detalleMovimiento.Id))
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
            return View(detalleMovimiento);
        }

        // GET: DetalleMovimientoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleMovimiento = await _context.DetalleMovimiento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleMovimiento == null)
            {
                return NotFound();
            }

            return View(detalleMovimiento);
        }

        // POST: DetalleMovimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleMovimiento = await _context.DetalleMovimiento.FindAsync(id);
            _context.DetalleMovimiento.Remove(detalleMovimiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleMovimientoExists(int id)
        {
            return _context.DetalleMovimiento.Any(e => e.Id == id);
        }
    }
}
