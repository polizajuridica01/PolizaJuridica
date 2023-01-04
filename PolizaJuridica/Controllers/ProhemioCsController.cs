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
    public class ProhemioCsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public ProhemioCsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: ProhemioCs
        public async Task<IActionResult> Index(int id)
        {
            var polizaJuridicaDbContext = _context.ProhemioC.Include(p => p.Prohemio).Where( p => p.ProhemioId == id);
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        // GET: ProhemioCs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prohemioC = await _context.ProhemioC
                .Include(p => p.Prohemio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prohemioC == null)
            {
                return NotFound();
            }

            return View(prohemioC);
        }

        // GET: ProhemioCs/Create
        public IActionResult Create(int id)
        {
            ViewBag.Id = id;
            ViewData["ProhemioId"] = new SelectList(_context.Prohemio, "Id", "Id");
            return View();
        }

        // POST: ProhemioCs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo,PersonalidadJuridica,Cantidad,ProhemioId")] ProhemioC prohemioC)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prohemioC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProhemioId"] = new SelectList(_context.Prohemio, "Id", "Id", prohemioC.ProhemioId);
            return View(prohemioC);
        }

        // GET: ProhemioCs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prohemioC = await _context.ProhemioC.FindAsync(id);
            if (prohemioC == null)
            {
                return NotFound();
            }
            ViewData["ProhemioId"] = new SelectList(_context.Prohemio, "Id", "Id", prohemioC.ProhemioId);
            return View(prohemioC);
        }

        // POST: ProhemioCs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,PersonalidadJuridica,Cantidad,ProhemioId")] ProhemioC prohemioC)
        {
            if (id != prohemioC.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prohemioC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProhemioCExists(prohemioC.Id))
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
            ViewData["ProhemioId"] = new SelectList(_context.Prohemio, "Id", "Id", prohemioC.ProhemioId);
            return View(prohemioC);
        }

        // GET: ProhemioCs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prohemioC = await _context.ProhemioC
                .Include(p => p.Prohemio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prohemioC == null)
            {
                return NotFound();
            }

            return View(prohemioC);
        }

        // POST: ProhemioCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prohemioC = await _context.ProhemioC.FindAsync(id);
            _context.ProhemioC.Remove(prohemioC);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProhemioCExists(int id)
        {
            return _context.ProhemioC.Any(e => e.Id == id);
        }
    }
}
