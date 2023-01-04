using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PolizaJuridica.Data;
using PolizaJuridica.Utilerias;

namespace PolizaJuridica.Controllers
{
    public class EventoUsuariosController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public EventoUsuariosController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: EventoUsuarios
        public async Task<IActionResult> Index()
        {
            ViewData["Evento"] = new SelectList(_context.Eventos.Where(e => e.Activo == true), "EventosId", "Descripcion");
            var polizaJuridicaDbContext = _context.EventoUsuarios.Include(e => e.Eventos).Include(e => e.Representante).Include(e => e.Estado);
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        // GET: EventoUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventoUsuarios = await _context.EventoUsuarios
                .Include(e => e.Eventos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventoUsuarios == null)
            {
                return NotFound();
            }

            return View(eventoUsuarios);
        }

        // GET: EventoUsuarios/Create
        public IActionResult Create()
        {
            ViewData["EventosId"] = new SelectList(_context.Eventos, "EventosId", "EventosId");
            return View();
        }

        // POST: EventoUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Correo,Celular,ApellidoPaterno,RepresentanteId,AsesorId,EstadoId,EventosId")] EventoUsuarios eventoUsuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventoUsuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventosId"] = new SelectList(_context.Eventos, "EventosId", "EventosId", eventoUsuarios.EventosId);
            return View(eventoUsuarios);
        }

        // GET: EventoUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventoUsuarios = await _context.EventoUsuarios.FindAsync(id);
            if (eventoUsuarios == null)
            {
                return NotFound();
            }
            ViewData["EventosId"] = new SelectList(_context.Eventos, "EventosId", "EventosId", eventoUsuarios.EventosId);
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios.Where(u => u.AreaId == 3), "RepresentanteId", "UsuarioNombreCompleto", eventoUsuarios.RepresentanteId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadosId", "EstadoNombre", eventoUsuarios.EstadoId);
            return View(eventoUsuarios);
        }

        // POST: EventoUsuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Correo,Celular,ApellidoPaterno,RepresentanteId,AsesorId,EstadoId,EventosId")] EventoUsuarios eventoUsuarios)
        {
            if (id != eventoUsuarios.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventoUsuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoUsuariosExists(eventoUsuarios.Id))
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
            ViewData["EventosId"] = new SelectList(_context.Eventos, "EventosId", "EventosId", eventoUsuarios.EventosId);
            return View(eventoUsuarios);
        }

        // GET: EventoUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventoUsuarios = await _context.EventoUsuarios
                .Include(e => e.Eventos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventoUsuarios == null)
            {
                return NotFound();
            }

            return View(eventoUsuarios);
        }

        // POST: EventoUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventoUsuarios = await _context.EventoUsuarios.FindAsync(id);
            _context.EventoUsuarios.Remove(eventoUsuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoUsuariosExists(int id)
        {
            return _context.EventoUsuarios.Any(e => e.Id == id);
        }

        [HttpPost]
        public ActionResult EnvioMasivo(int EventosId)
        {
            if(EventosId == 0)
            {
                return NotFound();
            }
            var mensaje = _context.Parametros.FirstOrDefault(p => p.ParametroValor == "MensajeMasivo").ParametroValor1.Trim();
            if(mensaje != null)
            {
                EnviarCorreo correo = new EnviarCorreo(_context);
                var usu = _context.EventoUsuarios.Where(e => e.EventosId == EventosId).ToList();
                foreach (var u in usu)
                {
                    if(u.Correo != null)
                    correo.EnviarCorreoSendGridMasivo(u,mensaje);
                }
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
