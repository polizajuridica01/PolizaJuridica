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
    public class CategoriaEsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public CategoriaEsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: CategoriaEs
        public async Task<IActionResult> Index()
        {
            var polizaJuridicaDbContext = _context.CategoriaEs.Include(c => c.CategoriaEspadre);
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        // GET: CategoriaEs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEs = await _context.CategoriaEs
                .Include(c => c.CategoriaEspadre)
                .FirstOrDefaultAsync(m => m.CategoriaEsid == id);
            if (categoriaEs == null)
            {
                return NotFound();
            }

            return View(categoriaEs);
        }

        // GET: CategoriaEs/Create
        public IActionResult Create()
        {
            ViewData["CategoriaEspadreId"] = new SelectList(_context.CategoriaEs, "CategoriaEsid", "Descripcion");
            return View();
        }

        // POST: CategoriaEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaEsid,Descripcion,Poliza,CategoriaEspadreId,TipoEs,CuentasXpagar")] CategoriaEs categoriaEs)
        {
            if (categoriaEs.CategoriaEspadreId == 0)
            {
                categoriaEs.CategoriaEspadreId = null;
            }

            if (categoriaEs.Descripcion != null)
            {
                _context.Add(categoriaEs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaEspadreId"] = new SelectList(_context.CategoriaEs, "CategoriaEsid", "Descripcion", categoriaEs.CategoriaEspadreId);
            return View(categoriaEs);
        }

        // GET: CategoriaEs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEs = await _context.CategoriaEs.FindAsync(id);
            if (categoriaEs == null)
            {
                return NotFound();
            }
            ViewData["CategoriaEspadreId"] = new SelectList(_context.CategoriaEs, "CategoriaEsid", "Descripcion", categoriaEs.CategoriaEspadreId);
            return View(categoriaEs);
        }

        // POST: CategoriaEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaEsid,Descripcion,Poliza,CategoriaEspadreId,TipoEs,CuentasXpagar")] CategoriaEs categoriaEs)
        {
            if (id != categoriaEs.CategoriaEsid)
            {
                return NotFound();
            }
            if (categoriaEs.CategoriaEspadreId == 0)
            {
                categoriaEs.CategoriaEspadreId = null;
            }

            if (categoriaEs.Descripcion != null)           
            {
                try
                {
                    _context.Update(categoriaEs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaEsExists(categoriaEs.CategoriaEsid))
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
            ViewData["CategoriaEspadreId"] = new SelectList(_context.CategoriaEs, "CategoriaEsid", "Descripcion", categoriaEs.CategoriaEspadreId);
            return View(categoriaEs);
        }

        // GET: CategoriaEs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEs = await _context.CategoriaEs
                .Include(c => c.CategoriaEspadre)
                .FirstOrDefaultAsync(m => m.CategoriaEsid == id);
            if (categoriaEs == null)
            {
                return NotFound();
            }

            return View(categoriaEs);
        }

        // POST: CategoriaEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaEs = await _context.CategoriaEs.FindAsync(id);
            _context.CategoriaEs.Remove(categoriaEs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaEsExists(int id)
        {
            return _context.CategoriaEs.Any(e => e.CategoriaEsid == id);
        }
    }
}
