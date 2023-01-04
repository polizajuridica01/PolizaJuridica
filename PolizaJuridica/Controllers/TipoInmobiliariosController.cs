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
    public class TipoInmobiliariosController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public TipoInmobiliariosController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: TipoInmobiliarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoInmobiliario.ToListAsync());
        }

        // GET: TipoInmobiliarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoInmobiliario = await _context.TipoInmobiliario
                .FirstOrDefaultAsync(m => m.TipoInmobiliarioId == id);
            if (tipoInmobiliario == null)
            {
                return NotFound();
            }

            return View(tipoInmobiliario);
        }

        // GET: TipoInmobiliarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoInmobiliarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoInmobiliarioId,TipoInmobiliarioDesc,Clausula")] TipoInmobiliario tipoInmobiliario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoInmobiliario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoInmobiliario);
        }

        // GET: TipoInmobiliarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoInmobiliario = await _context.TipoInmobiliario.FindAsync(id);
            if (tipoInmobiliario == null)
            {
                return NotFound();
            }
            return View(tipoInmobiliario);
        }

        // POST: TipoInmobiliarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoInmobiliarioId,TipoInmobiliarioDesc,Clausula")] TipoInmobiliario tipoInmobiliario)
        {
            if (id != tipoInmobiliario.TipoInmobiliarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoInmobiliario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoInmobiliarioExists(tipoInmobiliario.TipoInmobiliarioId))
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
            return View(tipoInmobiliario);
        }

        // GET: TipoInmobiliarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoInmobiliario = await _context.TipoInmobiliario
                .FirstOrDefaultAsync(m => m.TipoInmobiliarioId == id);
            if (tipoInmobiliario == null)
            {
                return NotFound();
            }

            return View(tipoInmobiliario);
        }

        // POST: TipoInmobiliarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoInmobiliario = await _context.TipoInmobiliario.FindAsync(id);
            _context.TipoInmobiliario.Remove(tipoInmobiliario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoInmobiliarioExists(int id)
        {
            return _context.TipoInmobiliario.Any(e => e.TipoInmobiliarioId == id);
        }
    }
}
