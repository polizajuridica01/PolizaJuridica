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
    public class PersonasOcupanInmsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public PersonasOcupanInmsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }
        // GET: ReferenciaPersonals
        public async Task<IActionResult> Index(int? id)
        {
            var Persona =  _context.PersonasOcupanInm.Include(p => p.TipoParentesco).Where(p => p.FisicaMoralId == id);
            //sección del código que se puede mejorar
            var Solicitud = (await _context.FisicaMoral.Include(f => f.Solicitud).SingleOrDefaultAsync(f => f.FisicaMoralId == id));
            ViewBag.TipoRegimen = Solicitud.Solicitud.TipoArrendatario;
            ViewBag.Fiador = Solicitud.Solicitud.SolicitudFiador;
            //Fin de la sección
            ViewBag.Id = id;
            ViewData["TipoParentescoDesc"] = new SelectList(_context.TipoParentesco, "TipoParentescoId", "TipoParentescoDesc");
            return View(Persona);
        }

        // GET: RefArrens/Details/5
        public async Task<List<PersonasOcupanInm>> EditAjax(int? id)
        {
            List<PersonasOcupanInm> Persona = new List<PersonasOcupanInm>();
            var appReferencia = await _context.PersonasOcupanInm.SingleOrDefaultAsync(a => a.PersonasOcupanInmId == id);
            Persona.Add(appReferencia);
            return Persona;
        }

        public async Task<String> EditRefArrenAjax(int PersonasOcupanInmId, string PersonasOcupanInmNombre, int TipoParentescoId, int FisicaMoralId, PersonasOcupanInm persona)
        {
            persona = new PersonasOcupanInm
            {
                PersonasOcupanInmId = PersonasOcupanInmId,
                PersonasOcupanInmNombre = PersonasOcupanInmNombre,
                TipoParentescoId = TipoParentescoId,
                FisicaMoralId = FisicaMoralId
            };
            _context.Update(persona);
            var result = await _context.SaveChangesAsync();
            return "Save";
        }

        public async Task<String> Insertar(int PersonasOcupanInmId, string PersonasOcupanInmNombre, int TipoParentescoId, int FisicaMoralId, PersonasOcupanInm persona)
        {

            persona = new PersonasOcupanInm
            {
                PersonasOcupanInmId = PersonasOcupanInmId,
                PersonasOcupanInmNombre = PersonasOcupanInmNombre,
                TipoParentescoId = TipoParentescoId,
                FisicaMoralId = FisicaMoralId
            };
            _context.Add(persona);
            var result = await _context.SaveChangesAsync();
            return "Save";
            //return RedirectToAction("Index", new { id = RefArren.SFisicaId });
        }
        public async Task<String> DeleteConfirmed(int id)
        {
            var document = await _context.PersonasOcupanInm.SingleOrDefaultAsync(m => m.PersonasOcupanInmId == id);
            _context.PersonasOcupanInm.Remove(document);
            await _context.SaveChangesAsync();
            return "Save";
        }

    }
}
