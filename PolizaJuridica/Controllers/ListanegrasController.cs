using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PolizaJuridica.Data;
using PolizaJuridica.Utilerias;
using PolizaJuridica.ViewModels;

namespace PolizaJuridica.Controllers
{
    public class ListanegrasController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public ListanegrasController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Listanegras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Listanegra.ToListAsync());
        }

        // GET: Listanegras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listanegra = await _context.Listanegra
                .FirstOrDefaultAsync(m => m.ListaNegraId == id);
            if (listanegra == null)
            {
                return NotFound();
            }

            return View(listanegra);
        }

        // GET: Listanegras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Listanegras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListaNegraId,Nombres,ApellidoPaterno,ApellidoMaterno,RazonSocial,Rfc,Observaciones,Estatus")] Listanegra listanegra)
        {
            bool isError = false;
            List<ErroresViewModel> notificaciones = new List<ErroresViewModel>();
            var CError = "Error";
            List<Listanegra> ls = new List<Listanegra>();

            if (listanegra.Nombres == null)
            {
                notificaciones.Add(Mensajes.ErroresAtributos("Nombres"));
                isError = true;
            }
            else
            {
                ls = _context.Listanegra.Where(l => l.Nombres.Contains( listanegra.Nombres)).ToList();
            }

            if (ls.Count >= 1 && listanegra.ApellidoPaterno != null)
                ls = ls.Where(l => l.Nombres.Contains( listanegra.ApellidoPaterno)).ToList();


            if (ls.Count >= 1 && listanegra.ApellidoMaterno != null)
                ls = ls.Where(l => l.Nombres.Contains(listanegra.ApellidoMaterno)).ToList();

            if (ls.Count >= 1 && listanegra.RazonSocial != null)
                ls = ls.Where(l => l.Nombres.Contains(listanegra.RazonSocial)).ToList();

            if (ls.Count >= 1 && listanegra.Rfc != null)
                ls = ls.Where(l => l.Nombres.Contains(listanegra.Rfc)).ToList();

            if (ls.Count >= 1)
            {
                notificaciones.Add(Mensajes.MensajesError("No se puede agregar ya que coincide con lo siguiente: "));
                foreach (var i in ls)
                {
                    string datos = string.Empty;

                    if (i.Nombres != null)
                        datos += i.Nombres + " ";

                    if (i.ApellidoMaterno != null)
                        datos += i.ApellidoMaterno + " ";

                    if (i.ApellidoPaterno != null)
                        datos += i.ApellidoPaterno + " ";

                    if (i.Rfc != null)
                        datos += i.Rfc + " ";

                    if (i.RazonSocial != null)
                        datos += i.RazonSocial;

                    notificaciones.Add(Mensajes.MensajesError(datos));
                }
                isError = true;
            }

            if (isError == false)
            {
                _context.Add(listanegra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Errores = notificaciones;
            return View(listanegra);
        }

        // GET: Listanegras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listanegra = await _context.Listanegra.FindAsync(id);
            if (listanegra == null)
            {
                return NotFound();
            }
            return View(listanegra);
        }

        // POST: Listanegras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListaNegraId,Nombres,ApellidoPaterno,ApellidoMaterno,RazonSocial,Rfc,Observaciones,Estatus")] Listanegra listanegra)
        {
            if (id != listanegra.ListaNegraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listanegra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListanegraExists(listanegra.ListaNegraId))
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
            return View(listanegra);
        }

        // GET: Listanegras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listanegra = await _context.Listanegra
                .FirstOrDefaultAsync(m => m.ListaNegraId == id);
            if (listanegra == null)
            {
                return NotFound();
            }

            return View(listanegra);
        }

        // POST: Listanegras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listanegra = await _context.Listanegra.FindAsync(id);
            _context.Listanegra.Remove(listanegra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListanegraExists(int id)
        {
            return _context.Listanegra.Any(e => e.ListaNegraId == id);
        }
    }
}
