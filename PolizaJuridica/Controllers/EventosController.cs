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
    public class EventosController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public EventosController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eventos.ToListAsync());
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventos = await _context.Eventos
                .FirstOrDefaultAsync(m => m.EventosId == id);
            if (eventos == null)
            {
                return NotFound();
            }

            return View(eventos);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventosId,Titulo,Descripcion,Lugar,FechaHoraInicio,FechaHoraFin,Activo,ReunionIde,ReunionUr,ReunionPass")] Eventos eventos)
        {
            bool isError = false;
            List<string> Errores = new List<string>();
            List<string> CamposError = new List<string>();
            var CError = "Error";

            if (eventos.Titulo == null)
            {
                CamposError.Add("Titulo" + CError);
                isError = true;
            }
            //if (eventos.Descripcion == null)
            //{
            //    CamposError.Add("Descripcion" + CError);
            //    isError = true;
            //}
            if (eventos.Lugar == null)
            {
                CamposError.Add("Lugar" + CError);
                isError = true;
            }
            if (eventos.FechaHoraInicio.Day == 1 && eventos.FechaHoraInicio.Month == 1 && eventos.FechaHoraInicio.Year == 1)
            {
                CamposError.Add("FechaHoraInicio" + CError);
                isError = true;
            }
            //if (eventos.FechaHoraFin.Day == 1 && eventos.FechaHoraFin.Month == 1 && eventos.FechaHoraFin.Year == 1)
            //{
            //    CamposError.Add("FechaHoraFin" + CError);
            //    isError = true;
            //}

            if (isError == false)
            {
                _context.Add(eventos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["CamposError"] = CamposError;
                if (CamposError.Count >= 1)
                {
                    Errores.Add("Favor de completar los campos que estan en rojo");
                }
                ViewData["Error"] = Errores;
                return View(eventos);
            }
            
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventos = await _context.Eventos.FindAsync(id);
            if (eventos == null)
            {
                return NotFound();
            }
            return View(eventos);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventosId,Titulo,Descripcion,Lugar,FechaHoraInicio,FechaHoraFin,Activo,ReunionIde,ReunionUr,ReunionPass")] Eventos eventos)
        {
            bool isError = false;
            List<string> Errores = new List<string>();
            List<string> CamposError = new List<string>();
            var CError = "Error";

            if (id != eventos.EventosId)
            {
                return RedirectToAction(nameof(Index));
            }
            if (eventos.Titulo == null)
            {
                CamposError.Add("Titulo" + CError);
                isError = true;
            }
            //if (eventos.Descripcion == null)
            //{
            //    CamposError.Add("Descripcion" + CError);
            //    isError = true;
            //}
            if (eventos.Lugar == null)
            {
                CamposError.Add("Lugar" + CError);
                isError = true;
            }
            if (eventos.FechaHoraInicio.Day == 1 && eventos.FechaHoraInicio.Month == 1 && eventos.FechaHoraInicio.Year == 1)
            {
                CamposError.Add("FechaHoraInicio" + CError);
                isError = true;
            }
            //if (eventos.FechaHoraFin.Day == 1 && eventos.FechaHoraFin.Month == 1 && eventos.FechaHoraFin.Year == 1)
            //{
            //    CamposError.Add("FechaHoraFin" + CError);
            //    isError = true;
            //}

            if (isError == false)
            {
                try
                {
                    _context.Update(eventos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventosExists(eventos.EventosId))
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
            else
            {
                ViewData["CamposError"] = CamposError;
                if (CamposError.Count >= 1)
                {
                    Errores.Add("Favor de completar los campos que estan en rojo");
                }
                ViewData["Error"] = Errores;
                return View(eventos);
            }
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventos = await _context.Eventos
                .FirstOrDefaultAsync(m => m.EventosId == id);
            if (eventos == null)
            {
                return NotFound();
            }

            return View(eventos);
        }

        // POST: Eventos/Delete/5
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventos = await _context.Eventos.FindAsync(id);
            _context.Eventos.Remove(eventos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventosExists(int id)
        {
            return _context.Eventos.Any(e => e.EventosId == id);
        }
    }
}
