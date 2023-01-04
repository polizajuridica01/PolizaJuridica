using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PolizaJuridica.Data;

namespace PolizaJuridica.Controllers
{
    public class DocumentosController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        private IHostingEnvironment _environment;
        public DocumentosController(PolizaJuridicaDbContext context,IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Documentos
        public async Task<IActionResult> Index(int? id)
        {
            //sección del código que se puede mejorar
            var Solicitud = (await _context.FisicaMoral.Include(f => f.Solicitud).SingleOrDefaultAsync(f => f.FisicaMoralId == id));
            ViewBag.TipoRegimen = Solicitud.Solicitud.TipoArrendatario;
            ViewBag.Fiador = Solicitud.Solicitud.SolicitudFiador;
            ViewBag.Estatus = Solicitud.Solicitud.SolicitudEstatus;
            //Fin de la sección
            var Document = _context.Documentos.Include(d => d.TipoDocumento).Where(d => d.FisicaMoralId == id);
;
            ViewBag.Id = id;
            ViewData["TipoDocumentoDesc"] = new SelectList(_context.TipoDocumento, "TipoDocumentoId", "TipoDocumentoDesc");
            return View(Document);
        }

        public async Task<String> Insertar(int DocumentosId, string DocumentosImagen, string DocumentoDesc, int TipoDocumentoId, int FisicaMoralId, Documentos documentos)
        {

            documentos = new Documentos
            {
                DocumentosId = DocumentosId,
                DocumentosImagen = DocumentosImagen,
                DocumentoDesc = DocumentoDesc,
                TipoDocumentoId = TipoDocumentoId,
                FisicaMoralId = FisicaMoralId,
            };
            _context.Add(documentos);
            var result = await _context.SaveChangesAsync();
            return "Save";
        }

        // GET: RefArrens/Details/5
        public async Task<List<Documentos>> EditAjax(int? id)
        {
            List<Documentos> documento = new List<Documentos>();
            var appDocumento = await _context.Documentos.SingleOrDefaultAsync(a => a.DocumentosId == id);
            documento.Add(appDocumento);
            return documento;
        }

        public async Task<String> Editar(int DocumentosId, string DocumentosImagen, string DocumentoDesc, int TipoDocumentoId, int FisicaMoralId, Documentos documentos)
        {

            documentos = new Documentos
            {
                DocumentosId = DocumentosId,
                DocumentosImagen = DocumentosImagen,
                DocumentoDesc = DocumentoDesc,
                TipoDocumentoId = TipoDocumentoId,
                FisicaMoralId = FisicaMoralId,
            };
            _context.Update(documentos);
            var result = await _context.SaveChangesAsync();
            return "Save";
        }
        //Eliminamos
        public async Task<String> DeleteConfirmed(int id)
        {
            var documentos = await _context.Documentos.SingleOrDefaultAsync(m => m.DocumentosId == id);
            _context.Documentos.Remove(documentos);
            await _context.SaveChangesAsync();
            return "Save";
        }



        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file,int? id)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
