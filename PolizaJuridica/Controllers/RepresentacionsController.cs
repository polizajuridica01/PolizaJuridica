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
    public class RepresentacionsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public RepresentacionsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Representacions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Representacion.ToListAsync());
        }

        // GET: Representacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representacion = await _context.Representacion
                .FirstOrDefaultAsync(m => m.RepresentacionId == id);
            if (representacion == null)
            {
                return NotFound();
            }

            return View(representacion);
        }

        // GET: Representacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Representacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RepresentacionId,RepresentacionNombre,Direccion,TelefonoOficina,OficinaEmisora,Porcentaje,PorcentajeAsesor,PorcentajeEjecutivo")] Representacion representacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(representacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(representacion);
        }

        // GET: Representacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representacion = await _context.Representacion.FindAsync(id);
            if (representacion == null)
            {
                return NotFound();
            }
            return View(representacion);
        }

        // POST: Representacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RepresentacionId,RepresentacionNombre,Direccion,TelefonoOficina,OficinaEmisora,Porcentaje,PorcentajeAsesor,PorcentajeEjecutivo")] Representacion representacion)
        {
            if (id != representacion.RepresentacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(representacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepresentacionExists(representacion.RepresentacionId))
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
            return View(representacion);
        }

        // GET: Representacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representacion = await _context.Representacion
                .FirstOrDefaultAsync(m => m.RepresentacionId == id);
            if (representacion == null)
            {
                return NotFound();
            }

            return View(representacion);
        }

        // POST: Representacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var representacion = await _context.Representacion.FindAsync(id);
            _context.Representacion.Remove(representacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepresentacionExists(int id)
        {
            return _context.Representacion.Any(e => e.RepresentacionId == id);
        }
    }
}
