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
    public class ProcesoSolucionesController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public ProcesoSolucionesController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: ProcesoSoluciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProcesoSoluciones.ToListAsync());
        }

        // GET: ProcesoSoluciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procesoSoluciones = await _context.ProcesoSoluciones
                .FirstOrDefaultAsync(m => m.ProcesoSolucionesId == id);
            if (procesoSoluciones == null)
            {
                return NotFound();
            }

            return View(procesoSoluciones);
        }

        // GET: ProcesoSoluciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProcesoSoluciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProcesoSolucionesId,Descripcion")] ProcesoSoluciones procesoSoluciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(procesoSoluciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(procesoSoluciones);
        }

        // GET: ProcesoSoluciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procesoSoluciones = await _context.ProcesoSoluciones.FindAsync(id);
            if (procesoSoluciones == null)
            {
                return NotFound();
            }
            return View(procesoSoluciones);
        }

        // POST: ProcesoSoluciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProcesoSolucionesId,Descripcion")] ProcesoSoluciones procesoSoluciones)
        {
            if (id != procesoSoluciones.ProcesoSolucionesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procesoSoluciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcesoSolucionesExists(procesoSoluciones.ProcesoSolucionesId))
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
            return View(procesoSoluciones);
        }

        // GET: ProcesoSoluciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procesoSoluciones = await _context.ProcesoSoluciones
                .FirstOrDefaultAsync(m => m.ProcesoSolucionesId == id);
            if (procesoSoluciones == null)
            {
                return NotFound();
            }

            return View(procesoSoluciones);
        }

        // POST: ProcesoSoluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var procesoSoluciones = await _context.ProcesoSoluciones.FindAsync(id);
            _context.ProcesoSoluciones.Remove(procesoSoluciones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcesoSolucionesExists(int id)
        {
            return _context.ProcesoSoluciones.Any(e => e.ProcesoSolucionesId == id);
        }
    }
}
