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
    public class CuentasBancariasController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public CuentasBancariasController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: CuentasBancarias
        public async Task<IActionResult> Index()
        {
            return View(await _context.CuentasBancarias.ToListAsync());
        }

        // GET: CuentasBancarias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentasBancarias = await _context.CuentasBancarias
                .FirstOrDefaultAsync(m => m.CuentaId == id);
            if (cuentasBancarias == null)
            {
                return NotFound();
            }

            return View(cuentasBancarias);
        }

        // GET: CuentasBancarias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CuentasBancarias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CuentasBancarias1,Nombre,Clabe,Banco,Estatus")] CuentasBancarias cuentasBancarias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuentasBancarias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cuentasBancarias);
        }

        // GET: CuentasBancarias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentasBancarias = await _context.CuentasBancarias.FindAsync(id);
            if (cuentasBancarias == null)
            {
                return NotFound();
            }
            return View(cuentasBancarias);
        }

        // POST: CuentasBancarias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CuentasBancarias1,Nombre,Clabe,Banco,Estatus")] CuentasBancarias cuentasBancarias)
        {
            if (id != cuentasBancarias.CuentaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuentasBancarias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuentasBancariasExists(cuentasBancarias.CuentaId))
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
            return View(cuentasBancarias);
        }

        // GET: CuentasBancarias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuentasBancarias = await _context.CuentasBancarias
                .FirstOrDefaultAsync(m => m.CuentaId == id);
            if (cuentasBancarias == null)
            {
                return NotFound();
            }

            return View(cuentasBancarias);
        }

        // POST: CuentasBancarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuentasBancarias = await _context.CuentasBancarias.FindAsync(id);
            _context.CuentasBancarias.Remove(cuentasBancarias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuentasBancariasExists(int id)
        {
            return _context.CuentasBancarias.Any(e => e.CuentaId == id);
        }
    }
}
