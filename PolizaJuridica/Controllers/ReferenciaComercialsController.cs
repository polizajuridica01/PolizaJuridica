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
    public class ReferenciaComercialsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public ReferenciaComercialsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: ReferenciaComercials
        public async Task<IActionResult> Index(int? id)
        {
            var referenciaComercials = _context.ReferenciaComercial.Include(r => r.FisicaMoral).Include(r => r.TipoRefComercial).Where(r => r.FisicaMoralId == id);
            //sección del código que se puede mejorar
            var varSolicitudId = (await _context.FisicaMoral.SingleOrDefaultAsync(f => f.FisicaMoralId == id));
            var TipoRegimen = (await _context.Solicitud.SingleOrDefaultAsync(s => s.SolicitudId == varSolicitudId.SolicitudId));
            ViewBag.TipoRegimen = Int32.Parse(TipoRegimen.TipoArrendatario);
            ViewBag.Fiador = TipoRegimen.SolicitudFiador;
            //Fin de la sección
            ViewData["TipoRepresentaRC"] = new SelectList(_context.TipoRefComercial, "TipoRefComercialId", "TipoRepresentaRC");
            ViewBag.Id = id;
            return View(await referenciaComercials.ToListAsync());
        }
        // GET: RefArrens/Details/5
        public async Task<List<ReferenciaComercial>> EditAjax(int? id)
        {
            List<ReferenciaComercial> ListReferenciaComercial = new List<ReferenciaComercial>();
            var appReferencia = await _context.ReferenciaComercial.SingleOrDefaultAsync(a => a.ReferenciaComercialId == id);
            ListReferenciaComercial.Add(appReferencia);
            return ListReferenciaComercial;
        }

        public async Task<String> EditRefArrenAjax(int ReferenciaComercialId, string RCDetalle, string RCREpresenta, int TipoRefComercialId, int FisicaMoralId, ReferenciaComercial comercial)
        {
            comercial = new ReferenciaComercial
            {
                ReferenciaComercialId = ReferenciaComercialId,
                Rcdetalle = RCDetalle,
                Rcrepresenta = RCREpresenta,
                TipoRefComercialId = TipoRefComercialId,
                FisicaMoralId = FisicaMoralId
            };
            _context.Update(comercial);
            var result = await _context.SaveChangesAsync();
            return "Save";
        }

        public async Task<String> Insertar(int ReferenciaComercialId,string RCDetalle, string RCREpresenta, int TipoRefComercialId, int FisicaMoralId, ReferenciaComercial comercial)
        {

            comercial = new ReferenciaComercial
            {
                ReferenciaComercialId =  ReferenciaComercialId,
                Rcdetalle = RCDetalle,
                Rcrepresenta = RCREpresenta,
                TipoRefComercialId = TipoRefComercialId,
                FisicaMoralId = FisicaMoralId
            };
            _context.Add(comercial);
            var result = await _context.SaveChangesAsync();
            return "Save";
            //return RedirectToAction("Index", new { id = RefArren.SFisicaId });
        }
        public async Task<String> DeleteConfirmed(int id)
        {
            var document = await _context.ReferenciaComercial.SingleOrDefaultAsync(m => m.ReferenciaComercialId == id);
            _context.ReferenciaComercial.Remove(document);
            await _context.SaveChangesAsync();
            return "Save";
        }
    }
}
