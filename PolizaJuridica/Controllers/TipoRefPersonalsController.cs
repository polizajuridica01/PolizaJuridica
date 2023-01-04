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
    public class TipoRefPersonalsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public TipoRefPersonalsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: TipoRefPersonals
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoRefPersonal.ToListAsync());
        }

        // GET: TipoRefPersonals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoRefPersonal = await _context.TipoRefPersonal
                .FirstOrDefaultAsync(m => m.TipoRefPersonalId == id);
            if (tipoRefPersonal == null)
            {
                return NotFound();
            }

            return View(tipoRefPersonal);
        }

        // GET: TipoRefPersonals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoRefPersonals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoRefPersonalId,TipoRefPersonalDesc")] TipoRefPersonal tipoRefPersonal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoRefPersonal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoRefPersonal);
        }

        // GET: TipoRefPersonals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoRefPersonal = await _context.TipoRefPersonal.FindAsync(id);
            if (tipoRefPersonal == null)
            {
                return NotFound();
            }
            return View(tipoRefPersonal);
        }

        // POST: TipoRefPersonals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoRefPersonalId,TipoRefPersonalDesc")] TipoRefPersonal tipoRefPersonal)
        {
            if (id != tipoRefPersonal.TipoRefPersonalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoRefPersonal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoRefPersonalExists(tipoRefPersonal.TipoRefPersonalId))
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
            return View(tipoRefPersonal);
        }

        // GET: TipoRefPersonals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoRefPersonal = await _context.TipoRefPersonal
                .FirstOrDefaultAsync(m => m.TipoRefPersonalId == id);
            if (tipoRefPersonal == null)
            {
                return NotFound();
            }

            return View(tipoRefPersonal);
        }

        // POST: TipoRefPersonals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoRefPersonal = await _context.TipoRefPersonal.FindAsync(id);
            _context.TipoRefPersonal.Remove(tipoRefPersonal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoRefPersonalExists(int id)
        {
            return _context.TipoRefPersonal.Any(e => e.TipoRefPersonalId == id);
        }
    }
}
