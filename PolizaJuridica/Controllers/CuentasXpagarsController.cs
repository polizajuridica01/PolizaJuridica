using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PolizaJuridica.Data;

namespace PolizaJuridica.Controllers
{
    public class CuentasXpagarsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public CuentasXpagarsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: CuentasXpagars
        public async Task<IActionResult> Index()
        {
            var polizaJuridicaDbContext = _context.CuentasXpagar.Include(c => c.Categoria).Include(c => c.Poliza);
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        // GET: CuentasXpagars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentasXpagar = await _context.CuentasXpagar
                .Include(c => c.Categoria)
                .Include(c => c.Poliza)
                .FirstOrDefaultAsync(m => m.Cpid == id);
            if (cuentasXpagar == null)
            {
                return NotFound();
            }

            return View(cuentasXpagar);
        }

        // GET: CuentasXpagars/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.CategoriaEs, "CategoriaEsid", "Descripcion");
            ViewData["PolizaId"] = new SelectList(_context.Poliza, "PolizaId", "PolizaId");
            ViewBag.Tree = recuperacategoria();
            return View();
        }

        // POST: CuentasXpagars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cpid,CategoriaId,Importe,PolizaId")] CuentasXpagar cuentasXpagar)
        {
            if (cuentasXpagar.Importe >= 1)
            {
                _context.Add(cuentasXpagar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Tree = recuperacategoria();
            ViewData["CategoriaId"] = new SelectList(_context.CategoriaEs, "CategoriaEsid", "Descripcion", cuentasXpagar.CategoriaId);
            ViewData["PolizaId"] = new SelectList(_context.Poliza, "PolizaId", "PolizaId", cuentasXpagar.PolizaId);
            return View(cuentasXpagar);
        }

        // GET: CuentasXpagars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentasXpagar = await _context.CuentasXpagar.FindAsync(id);
            if (cuentasXpagar == null)
            {
                return NotFound();
            }
            ViewBag.Tree = recuperacategoria();
            ViewData["CategoriaId"] = new SelectList(_context.CategoriaEs, "CategoriaEsid", "Descripcion", cuentasXpagar.CategoriaId);
            ViewData["PolizaId"] = new SelectList(_context.Poliza, "PolizaId", "PolizaId", cuentasXpagar.PolizaId);
            return View(cuentasXpagar);
        }

        // POST: CuentasXpagars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cpid,CategoriaId,Importe,PolizaId")] CuentasXpagar cuentasXpagar)
        {
            if (id != cuentasXpagar.Cpid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuentasXpagar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuentasXpagarExists(cuentasXpagar.Cpid))
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
            ViewBag.Tree = recuperacategoria();
            ViewData["CategoriaId"] = new SelectList(_context.CategoriaEs, "CategoriaEsid", "Descripcion", cuentasXpagar.CategoriaId);
            ViewData["PolizaId"] = new SelectList(_context.Poliza, "PolizaId", "PolizaId", cuentasXpagar.PolizaId);
            return View(cuentasXpagar);
        }

        // GET: CuentasXpagars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentasXpagar = await _context.CuentasXpagar
                .Include(c => c.Categoria)
                .Include(c => c.Poliza)
                .FirstOrDefaultAsync(m => m.Cpid == id);
            if (cuentasXpagar == null)
            {
                return NotFound();
            }

            return View(cuentasXpagar);
        }

        // POST: CuentasXpagars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuentasXpagar = await _context.CuentasXpagar.FindAsync(id);
            _context.CuentasXpagar.Remove(cuentasXpagar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuentasXpagarExists(int id)
        {
            return _context.CuentasXpagar.Any(e => e.Cpid == id);
        }
        private string recuperacategoria()
        {
                return JsonConvert.SerializeObject(_context.CategoriaEs
                    .Where(c => c.CategoriaEspadreId == null)
                    .Where(c => c.CuentasXpagar == true)
                    .OrderBy(c => c.Descripcion)
                    .Select(c => new
                    {
                        text = c.Descripcion,
                        Id = c.CategoriaEsid,
                        nodes = c.InverseCategoriaEspadre.Select(i => new
                        {
                            text = i.Descripcion,
                            Id = i.CategoriaEsid,
                        })
                    }).ToList());
        }
    }
}
