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
    public class ReferenciaPersonalsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public ReferenciaPersonalsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var Referencia = _context.ReferenciaPersonal.Include(r => r.TipoRefPersonal).Where(r => r.FisicaMoralId == id);
            //sección del código que se puede mejorar
            var fisicaMoral = await  _context.FisicaMoral.Include(f => f.Solicitud).SingleOrDefaultAsync(f => f.FisicaMoralId == id);
            ViewBag.Fiador = fisicaMoral.Solicitud.SolicitudFiador;
            //Fin de la sección
            ViewBag.Id = id;
            ViewData["TipoRefPersonalDesc"] = new SelectList(_context.TipoRefPersonal, "TipoRefPersonalId", "TipoRefPersonalDesc");
            return View(Referencia);
        }

        // GET: RefArrens/Details/5
        public async Task<List<ReferenciaPersonal>> EditAjax(int? id)
        {
            List<ReferenciaPersonal> referenciaper = new List<ReferenciaPersonal>();
            var appReferencia = await _context.ReferenciaPersonal.SingleOrDefaultAsync(a => a.ReferenciaPersonalId == id);
            referenciaper.Add(appReferencia);
            return referenciaper;
        }

        public async Task<String> EditRefArrenAjax(int ReferenciaPersonalId, string RPNombres, string RPApePaterno, string RPApeMaterno, string RPTelefono, int FisicaMoralId, int TipoRefPersonalId, ReferenciaPersonal ReferenciaPersonal)
        {
            ReferenciaPersonal = new ReferenciaPersonal
            {
                ReferenciaPersonalId = ReferenciaPersonalId,
                Rpnombres = RPNombres,
                RpapePaterno = RPApePaterno,
                RpApeMaterno = RPApeMaterno,
                Rptelefono = RPTelefono,
                FisicaMoralId = FisicaMoralId,
                TipoRefPersonalId = TipoRefPersonalId
            };
            _context.Update(ReferenciaPersonal);
            var result = await _context.SaveChangesAsync();
            return "Save";
        }

        public async Task<String> Insertar(int ReferenciaPersonalId, string RPNombres, string RPApePaterno, string RPApeMaterno, string RPTelefono, int FisicaMoralId, int TipoRefPersonalId, ReferenciaPersonal ReferenciaPersonal)
        {
            ReferenciaPersonal = new ReferenciaPersonal
            {
                ReferenciaPersonalId = ReferenciaPersonalId,
                Rpnombres = RPNombres,
                RpapePaterno = RPApePaterno,
                RpApeMaterno = RPApeMaterno,
                Rptelefono = RPTelefono,
                FisicaMoralId = FisicaMoralId,
                TipoRefPersonalId = TipoRefPersonalId

            };
            _context.Add(ReferenciaPersonal);
            var result = await _context.SaveChangesAsync();
            return "Save";
            //return RedirectToAction("Index", new { id = RefArren.SFisicaId });
        }
        public async Task<String> DeleteConfirmed(int id)
        {
            var referencia = await _context.ReferenciaPersonal.SingleOrDefaultAsync(m => m.ReferenciaPersonalId == id);
            _context.ReferenciaPersonal.Remove(referencia);
            await _context.SaveChangesAsync();
            return "Save";
        }

    }
}
