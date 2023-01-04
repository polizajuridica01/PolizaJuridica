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
    public class MenuPsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public MenuPsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: MenuPs
        public async Task<IActionResult> Index()
        {
            var PolizaJuridicaDbContext = _context.MenuP.Include(m => m.Area).Include(m => m.MenuPpadre);
            return View(await PolizaJuridicaDbContext.ToListAsync());
        }
        // GET: MenuPs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuP = await _context.MenuP
                .Include(m => m.Area)
                .Include(m => m.MenuPpadre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuP == null)
            {
                return NotFound();
            }

            return View(menuP);
        }

        // GET: MenuPs/Create
        public IActionResult Create()
        {
            ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "AreaDescripcion");
            ViewData["MenuPpadreId"] = new SelectList(_context.MenuP.Where(m => m.MenuPpadreId == null), "Id", "Nombre");
            return View();
        }

        // POST: MenuPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Controlador,Pantalla,MenuPpadreId,AreaId")] MenuP menuP)
        {
            if (menuP.MenuPpadreId == 0)
            {
                menuP.MenuPpadreId = null;
            }
            if (menuP.Nombre != null)
            {
                _context.Add(menuP);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "AreaDescripcion", menuP.AreaId);
            ViewData["MenuPpadreId"] = new SelectList(_context.MenuP.Where(m => m.MenuPpadreId == null), "Id", "Nombre", menuP.MenuPpadreId);
            return View(menuP);
        }


        // GET: MenuPs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuP = await _context.MenuP.FindAsync(id);
            if (menuP == null)
            {
                return NotFound();
            }
            ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "AreaDescripcion", menuP.AreaId);
            ViewData["MenuPpadreId"] = new SelectList(_context.MenuP, "Id", "Nombre", menuP.MenuPpadreId);
            return View(menuP);
        }

        // POST: MenuPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Controlador,Pantalla,MenuPpadreId,AreaId")] MenuP menuP)
        {
            if (id != menuP.Id)
            {
                return NotFound();
            }

            if (menuP.MenuPpadreId == 0)
            {
                menuP.MenuPpadreId = null;
            }
            if (menuP.Nombre != null)
            {
                try
                {
                    _context.Update(menuP);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuPExists(menuP.Id))
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
            ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "AreaDescripcion", menuP.AreaId);
            ViewData["MenuPpadreId"] = new SelectList(_context.MenuP, "Id", "Nombre", menuP.MenuPpadreId);
            return View(menuP);
        }

        // GET: MenuPs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuP = await _context.MenuP
                .Include(m => m.Area)
                .Include(m => m.MenuPpadre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuP == null)
            {
                return NotFound();
            }

            return View(menuP);
        }

        // POST: MenuPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuP = await _context.MenuP.FindAsync(id);
            _context.MenuP.Remove(menuP);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuPExists(int id)
        {
            return _context.MenuP.Any(e => e.Id == id);
        }
    }
}
