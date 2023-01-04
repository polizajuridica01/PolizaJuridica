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
    public class RefArrendamientoesController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public RefArrendamientoesController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var RefArrendamiento = _context.RefArrendamiento.SingleOrDefaultAsync(r => r.FisicaMoralId == id);
            //sección del código que se puede mejorar
            var varSolicitudId = (await _context.FisicaMoral.Include(f => f.Solicitud).SingleOrDefaultAsync(f => f.FisicaMoralId == id));
            var TipoRegimen = (await _context.Solicitud.SingleOrDefaultAsync(s => s.SolicitudId == varSolicitudId.SolicitudId));
            ViewBag.TipoRegimen = TipoRegimen.TipoArrendatario;
            ViewBag.Fiador = TipoRegimen.SolicitudFiador;
            //Fin de la sección
            ViewBag.Id = id;
            return View(RefArrendamiento);
        }

        // GET: RefArrens/Details/5
        public async Task<List<RefArrendamiento>> EditAjax(int? id)
        {
            List<RefArrendamiento> referencia = new List<RefArrendamiento>();
            var appReferencia = await _context.RefArrendamiento.SingleOrDefaultAsync(a => a.RefArrendamientoId == id);
            referencia.Add(appReferencia);
            return referencia;
        }

        public async Task<String> EditRefArrenAjax(int RefArrendamientoId, string RefArrenNombres, string RefArrenApePaterno, string RefArrenApeMaterno, string RefArrenTelefono, string RefArrenDomicilio, decimal RefArrenMonto, string RefArrenMotivoCambio, int FisicaMoralId, RefArrendamiento refArrendamiento)
        {
            refArrendamiento = new RefArrendamiento
            {
                RefArrendamientoId = RefArrendamientoId,
                RefArrenNombres = RefArrenNombres,
                RefArrenApePaterno = RefArrenApePaterno,
                RefArrenApeMaterno = RefArrenApeMaterno,
                RefArrenTelefono = RefArrenTelefono,
                RefArrenDomicilio = RefArrenDomicilio,
                RefArrenMonto = RefArrenMonto,
                RefArrenMotivoCambio = RefArrenMotivoCambio,
                FisicaMoralId = FisicaMoralId
            };
            _context.Update(refArrendamiento);
            var result = await _context.SaveChangesAsync();

            return "Save";
            //return RedirectToAction("Index", new { id = RefArren.SFisicaId });
        }

        public async Task<String> Insertar(int RefArrendamientoId, string RefArrenNombres, string RefArrenApePaterno, string RefArrenApeMaterno, string RefArrenTelefono, string RefArrenDomicilio, decimal RefArrenMonto, string RefArrenMotivoCambio, int FisicaMoralId, RefArrendamiento refArrendamiento)
        {
            refArrendamiento = new RefArrendamiento
            {
                RefArrendamientoId = RefArrendamientoId,
                RefArrenNombres = RefArrenNombres,
                RefArrenApePaterno = RefArrenApePaterno,
                RefArrenApeMaterno = RefArrenApeMaterno,
                RefArrenTelefono = RefArrenTelefono,
                RefArrenDomicilio = RefArrenDomicilio,
                RefArrenMonto = RefArrenMonto,
                RefArrenMotivoCambio = RefArrenMotivoCambio,
                FisicaMoralId = FisicaMoralId
            };
            _context.Add(refArrendamiento);
            var result = await _context.SaveChangesAsync();
            return "Save";
            //return RedirectToAction("Index", new { id = RefArren.SFisicaId });
        }

        // GET: RefArrens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refArren = await _context.RefArrendamiento
                                         .Include(r => r.FisicaMoralId)
                                         .SingleOrDefaultAsync(m => m.RefArrendamientoId == id);
            if (refArren == null)
            {
                return NotFound();
            }

            return View(refArren);
        }

        // GET: RefArrens/Create
        public async Task<IActionResult> Create(int id)
        {
            //sección del código que se puede mejorar
            var varSolicitudId = (await _context.FisicaMoral.SingleOrDefaultAsync(f => f.FisicaMoralId == id));
            var TipoRegimen = (await _context.Solicitud.SingleOrDefaultAsync(s => s.SolicitudId == varSolicitudId.SolicitudId));
            ViewBag.TipoRegimen = Int32.Parse(TipoRegimen.TipoArrendatario);
            //Fin de la sección
            ViewBag.FisicaMoralId = id;
            ViewBag.Id = id;
            return View();
        }

        // POST: RefArrens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefArrenId,RefArrenNombres,RefArrenApePaterno,RefArrenApeMaterno,RefArrenTelefono,RefArrenDomicilio,RefArrenMonto,RefArrenMotivoCambio,FisicaMoralId")] RefArrendamiento refArrendamiento)
        {
            if (ModelState.IsValid)
            {                
                _context.Add(refArrendamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", new { Id = refArrendamiento.FisicaMoralId });
            }
            ViewData["FisicaMoralId"] = new SelectList(_context.FisicaMoral, "SFisicaId", "SFisicaId", refArrendamiento.FisicaMoralId);
            return View(refArrendamiento);
        }

        // GET: RefArrens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //sección del código que se puede mejorar
            var Solicitud = (await _context.FisicaMoral.Include(f => f.Solicitud).SingleOrDefaultAsync(f => f.FisicaMoralId == id));
            ViewBag.TipoRegimen = Solicitud.Solicitud.TipoArrendatario;
            ViewBag.Fiador = Solicitud.Solicitud.SolicitudFiador;
            //Fin de la sección
            ViewBag.Id = id;
            if (id == null)
            {
                return NotFound();
            }
            var refArren = await _context.RefArrendamiento.SingleOrDefaultAsync(m => m.FisicaMoralId == id);
            if (refArren == null)
            {
                return RedirectToAction("Create", new { Id = id });
            }
            return View(refArren);
        }

        // POST: RefArrens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("RefArrendamientoId,RefArrenNombres,RefArrenApePaterno,RefArrenApeMaterno,RefArrenTelefono,RefArrenDomicilio,RefArrenMonto,RefArrenMotivoCambio,FisicaMoralId")] RefArrendamiento refArrendamiento)
        {
            int id = 0;
            try
            {
                _context.Update(refArrendamiento);
                await _context.SaveChangesAsync();
                id = refArrendamiento.FisicaMoralId;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefArrenExists(refArrendamiento.RefArrendamientoId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Edit", new { id});
        }
  

        // GET: RefArrens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refArren = await _context.RefArrendamiento
                                         .Include(r => r.FisicaMoralId)
                                         .SingleOrDefaultAsync(m => m.RefArrendamientoId == id);
            if (refArren == null)
            {
                return NotFound();
            }

            return View(refArren);
        }

        // POST: RefArrens/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<String> DeleteConfirmed(int id)
        {
            var refArren = await _context.RefArrendamiento.SingleOrDefaultAsync(m => m.RefArrendamientoId == id);
            _context.RefArrendamiento.Remove(refArren);
            await _context.SaveChangesAsync();
            return "Save";
        }

        private bool RefArrenExists(int id)
        {
            return _context.RefArrendamiento.Any(e => e.RefArrendamientoId == id);
        }

    }
}
