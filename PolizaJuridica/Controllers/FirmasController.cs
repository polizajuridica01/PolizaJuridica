using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PolizaJuridica.Data;
using PolizaJuridica.Models;
using PolizaJuridica.Utilerias;
using PolizaJuridica.ViewModels;

namespace PolizaJuridica.Controllers
{
    public class FirmasController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public FirmasController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Firmas
        public async Task<IActionResult> Index(DateTime FechaInicio, DateTime FechaFin)
        {
            List<Firmas> firmas = new List<Firmas>();
            UserViewModel user = new UserViewModel();
            user = JsonConvert.DeserializeObject<UserViewModel>(User.FindFirst("User").Value);

            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            if (FechaInicio.GetHashCode() == 0)
                FechaInicio = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            if (FechaFin.GetHashCode() == 0)
                FechaFin = FechaInicio.AddMonths(1).AddDays(-1);

             firmas = await _context.Firmas.Include(f => f.CreadaPor).Include(f => f.Poliza)
               .Where(f => f.FechaFirma.Date.CompareTo(FechaInicio.Date) >= 0 &&
                       f.FechaFirma.Date.CompareTo(FechaFin.Date) <= 0)
                .ToListAsync();

            if (user.AreaDescripcion == "Representante")
            {
                firmas = firmas.Where(f => f.CreadaPorId == user.UsuarioId || f.FirmanteId == user.UsuarioId).ToList();
            }

            return View(firmas);
        }

        // GET: Firmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firmas = await _context.Firmas
                .Include(f => f.CreadaPor)
                .Include(f => f.Poliza)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (firmas == null)
            {
                return NotFound();
            }

            return View(firmas);
        }

        // GET: Firmas/Create
        public IActionResult Create()
        {
            ViewData["CreadaPorId"] = new SelectList(_context.Usuarios, "UsuariosId", "UsuariosId");
            ViewData["PolizaId"] = new SelectList(_context.Poliza, "PolizaId", "PolizaId");
            return View();
        }

        // POST: Firmas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Lugar,FechaFirma,FechaCreacion,PolizaId,CreadaPorId")] Firmas firmas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(firmas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreadaPorId"] = new SelectList(_context.Usuarios, "UsuariosId", "UsuariosId", firmas.CreadaPorId);
            ViewData["PolizaId"] = new SelectList(_context.Poliza, "PolizaId", "PolizaId", firmas.PolizaId);
            return View(firmas);
        }

        // GET: Firmas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firmas = await _context.Firmas.FindAsync(id);
            if (firmas == null)
            {
                return NotFound();
            }
            ViewData["CreadaPorId"] = new SelectList(_context.Usuarios, "UsuariosId", "UsuariosId", firmas.CreadaPorId);
            ViewData["PolizaId"] = new SelectList(_context.Poliza, "PolizaId", "PolizaId", firmas.PolizaId);
            return View(firmas);
        }

        // POST: Firmas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Lugar,FechaFirma,FechaCreacion,PolizaId,CreadaPorId")] Firmas firmas)
        {
            if (id != firmas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(firmas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FirmasExists(firmas.Id))
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
            ViewData["CreadaPorId"] = new SelectList(_context.Usuarios, "UsuariosId", "UsuariosId", firmas.CreadaPorId);
            ViewData["PolizaId"] = new SelectList(_context.Poliza, "PolizaId", "PolizaId", firmas.PolizaId);
            return View(firmas);
        }

        // GET: Firmas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firmas = await _context.Firmas
                .Include(f => f.CreadaPor)
                .Include(f => f.Poliza)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (firmas == null)
            {
                return NotFound();
            }

            return View(firmas);
        }

        // POST: Firmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var firmas = await _context.Firmas.FindAsync(id);
            _context.Firmas.Remove(firmas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FirmasExists(int id)
        {
            return _context.Firmas.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert([Bind("Id,Lugar,FechaFirma,FechaCreacion,PolizaId,CreadaPorId,FirmanteId")] Firmas firmas)
        {

            bool isError = false;
            List<ErroresViewModel> mensajes = new List<ErroresViewModel>();
            string result = string.Empty;
            firmas.CreadaPorId = Int32.Parse(User.FindFirst("Id").Value);
            firmas.FechaCreacion = DateTime.Now;

            if (firmas.Lugar == null)
            {
                mensajes.Add(Mensajes.ErroresAtributos("Lugar"));
                isError = true;
            }
               

            if (firmas.FirmanteId <= 0 || firmas.FirmanteId == null)
            {
                mensajes.Add(Mensajes.MensajesError("Favor de seleccionar quien firma"));
                isError = true;
            }
               

            if (isError == false)
            {
                _context.Add(firmas);
                await _context.SaveChangesAsync();
                mensajes.Add(Mensajes.Exitoso("Se agrego correctamente la firma"));
            }
            else
            {
                mensajes.Add(Mensajes.MensajesError("Favor de completar los campos en rojo"));
            }

            result = JsonConvert.SerializeObject(mensajes);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("Id,Lugar,FechaFirma,FechaCreacion,PolizaId,CreadaPorId,FirmanteId")] Firmas firmas)
        {
            bool isError = false;
            List<ErroresViewModel> mensajes = new List<ErroresViewModel>();
            string result = string.Empty;
            firmas.CreadaPorId = Int32.Parse(User.FindFirst("Id").Value);
            firmas.FechaCreacion = DateTime.Now;

            if (firmas.Lugar == null)
            {
                mensajes.Add(Mensajes.ErroresAtributos("Lugar"));
                isError = true;
            }


            if (firmas.FirmanteId <= 0 || firmas.FirmanteId == null)
            {
                mensajes.Add(Mensajes.MensajesError("Favor de seleccionar quien firma"));
                isError = true;
            }

            if (mensajes.Count >= 1)
                isError = true;

            if (isError == false)
            {
                _context.Update(firmas);
                await _context.SaveChangesAsync();
                mensajes.Add(Mensajes.Exitoso("Se agrego correctamente la firma"));
            }
            else
            {
                mensajes.Add(Mensajes.MensajesError("Favor de completar los campos en rojo"));
            }
            result = JsonConvert.SerializeObject(mensajes);
            ViewBag.Errores = result;


            return RedirectToAction("Index");
        }

        public async Task<string> Find(int? id)
        {
            string result = string.Empty;
            var firma = await _context.Firmas.Include(fi => fi.Poliza.FisicaMoral.Solicitud).SingleOrDefaultAsync(fi => fi.PolizaId == id);

            FirmaViewModel f = new FirmaViewModel();

            f.PolizaId = Convert.ToInt32(firma.PolizaId);
            f.direccion = firma.Poliza.FisicaMoral.Solicitud.SolicitudUbicacionArrendado;
            f.SolicitudId = firma.Poliza.FisicaMoral.SolicitudId;
            f.fechafirma = firma.FechaFirma;
            f.FirmanteID = Convert.ToInt32(firma.FirmanteId);
            f.Id = firma.Id;


            result = JsonConvert.SerializeObject(f);
            return result;
        }
    }
}
