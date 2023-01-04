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
    public class KeywordStructurasController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public KeywordStructurasController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: KeywordStructuras
        public async Task<IActionResult> Index()
        {
            return View(await _context.KeywordStructura.Include(k => k.TipoInmobiliario).OrderBy(k => k.Orden).ToListAsync());
        }

        // GET: KeywordStructuras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordStructura = await _context.KeywordStructura
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keywordStructura == null)
            {
                return NotFound();
            }

            return View(keywordStructura);
        }

        // GET: KeywordStructuras/Create
        public IActionResult Create()
        {
            ViewData["TipoInmobiliarioId"] = new SelectList(_context.TipoInmobiliario, "TipoInmobiliarioId", "TipoInmobiliarioDesc");
            return View();
        }

        // POST: KeywordStructuras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Estructura,Comentarios,Keyword,TipoInmobiliarioId,Orden")] KeywordStructura keywordStructura)
        {
            if (keywordStructura.Keyword != null)
            {
                _context.Add(keywordStructura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(keywordStructura);
        }

        // GET: KeywordStructuras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordStructura = await _context.KeywordStructura.FindAsync(id);
            if (keywordStructura == null)
            {
                return NotFound();
            }
            ViewData["TipoInmobiliarioId"] = new SelectList(_context.TipoInmobiliario, "TipoInmobiliarioId", "TipoInmobiliarioDesc", keywordStructura.TipoInmobiliarioId);
            return View(keywordStructura);
        }

        // POST: KeywordStructuras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Estructura,Comentarios,Keyword,TipoInmobiliarioId,Orden")] KeywordStructura keywordStructura)
        {
            if (id != keywordStructura.Id)
            {
                return NotFound();
            }

            if (keywordStructura.Keyword != null)
            { 
                try
                {
                    _context.Update(keywordStructura);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeywordStructuraExists(keywordStructura.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
        }

            return View(keywordStructura);
        }

        // GET: KeywordStructuras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordStructura = await _context.KeywordStructura
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keywordStructura == null)
            {
                return NotFound();
            }

            return View(keywordStructura);
        }

        // POST: KeywordStructuras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keywordStructura = await _context.KeywordStructura.FindAsync(id);
            _context.KeywordStructura.Remove(keywordStructura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeywordStructuraExists(int id)
        {
            return _context.KeywordStructura.Any(e => e.Id == id);
        }

        public string KWS(string doctext)
        {
            var estructura = _context.KeywordStructura.SingleOrDefault();



            return doctext;
        }
    }
}
