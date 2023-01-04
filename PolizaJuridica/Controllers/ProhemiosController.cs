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
    public class ProhemiosController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public ProhemiosController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Prohemios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prohemio.ToListAsync());
        }

        // GET: Prohemios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prohemio = await _context.Prohemio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prohemio == null)
            {
                return NotFound();
            }

            return View(prohemio);
        }

        // GET: Prohemios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prohemios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Texto")] Prohemio prohemio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prohemio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prohemio);
        }

        // GET: Prohemios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prohemio = await _context.Prohemio.FindAsync(id);
            if (prohemio == null)
            {
                return NotFound();
            }
            return View(prohemio);
        }

        // POST: Prohemios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Texto")] Prohemio prohemio)
        {
            if (id != prohemio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prohemio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProhemioExists(prohemio.Id))
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
            return View(prohemio);
        }

        // GET: Prohemios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prohemio = await _context.Prohemio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prohemio == null)
            {
                return NotFound();
            }

            return View(prohemio);
        }

        // POST: Prohemios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prohemio = await _context.Prohemio.FindAsync(id);
            _context.Prohemio.Remove(prohemio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProhemioExists(int id)
        {
            return _context.Prohemio.Any(e => e.Id == id);
        }
    }
}
