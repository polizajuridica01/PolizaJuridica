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
    public class TipoRefComercialsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public TipoRefComercialsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: TipoRefComercials
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoRefComercial.ToListAsync());
        }

        // GET: TipoRefComercials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoRefComercial = await _context.TipoRefComercial
                .FirstOrDefaultAsync(m => m.TipoRefComercialId == id);
            if (tipoRefComercial == null)
            {
                return NotFound();
            }

            return View(tipoRefComercial);
        }

        // GET: TipoRefComercials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoRefComercials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoRefComercialId,TipoRepresentaRC,TipoDetalleRC")] TipoRefComercial tipoRefComercial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoRefComercial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoRefComercial);
        }

        // GET: TipoRefComercials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoRefComercial = await _context.TipoRefComercial.FindAsync(id);
            if (tipoRefComercial == null)
            {
                return NotFound();
            }
            return View(tipoRefComercial);
        }

        // POST: TipoRefComercials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoRefComercialId,TipoRepresentaRC,TipoDetalleRC")] TipoRefComercial tipoRefComercial)
        {
            if (id != tipoRefComercial.TipoRefComercialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoRefComercial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoRefComercialExists(tipoRefComercial.TipoRefComercialId))
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
            return View(tipoRefComercial);
        }

        // GET: TipoRefComercials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoRefComercial = await _context.TipoRefComercial
                .FirstOrDefaultAsync(m => m.TipoRefComercialId == id);
            if (tipoRefComercial == null)
            {
                return NotFound();
            }

            return View(tipoRefComercial);
        }

        // POST: TipoRefComercials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoRefComercial = await _context.TipoRefComercial.FindAsync(id);
            _context.TipoRefComercial.Remove(tipoRefComercial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoRefComercialExists(int id)
        {
            return _context.TipoRefComercial.Any(e => e.TipoRefComercialId == id);
        }
    }
}
