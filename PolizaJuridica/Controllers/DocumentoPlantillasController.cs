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
    public class DocumentoPlantillasController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public DocumentoPlantillasController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: DocumentoPlantillas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DocumentoPlantilla.Include(d => d.Estados);
            ViewData["EstadoNombre"] = new SelectList(_context.Estados, "EstadosId", "EstadoNombre");
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<String> Insertar(int DocumentoPlantillaId, string DocumentoPlantillaTipo, string DocumentoPlantillaNombre, string DocumentoPlantillaXml, string DocumentoOriginal,int EstadosId,Boolean DocumentoPagare,Boolean DocumentoFiador,Boolean DocumentoInmueble,Boolean DocumentoCarta, DocumentoPlantilla documentoplantilla)
        {

            documentoplantilla = new DocumentoPlantilla
            {
                DocumentoPlantillaId = DocumentoPlantillaId,
                DocumentoPlantillaTipo = DocumentoPlantillaTipo,
                DocumentoPlantillaNombre = DocumentoPlantillaNombre,
                DocumentoPlantillaXml = DocumentoPlantillaXml,
                DocumentoOriginal = DocumentoOriginal,
                EstadosId = EstadosId,
                DocumentoPagare = DocumentoPagare,
                DocumentoFiador = DocumentoFiador,
                DocumentoInmueble = DocumentoInmueble,
                DocumentoCarta = DocumentoCarta
            };
            _context.Add(documentoplantilla);
            var result = await _context.SaveChangesAsync();
            return "Save";
        }

        // GET: RefArrens/Details/5
        public async Task<List<DocumentoPlantilla>> EditAjax(int? id)
        {
            List<DocumentoPlantilla> documento = new List<DocumentoPlantilla>();
            var appDocumento = await _context.DocumentoPlantilla.SingleOrDefaultAsync(a => a.DocumentoPlantillaId == id);
            documento.Add(appDocumento);
            return documento;
        }

        public async Task<String> Editar(int DocumentoPlantillaId, string DocumentoPlantillaTipo, string DocumentoPlantillaNombre, string DocumentoPlantillaXml, string DocumentoOriginal, int EstadosId, Boolean DocumentoPagare, Boolean DocumentoFiador, Boolean DocumentoInmueble, Boolean DocumentoCarta, DocumentoPlantilla documentoplantilla)
        {

            documentoplantilla = new DocumentoPlantilla
            {
                DocumentoPlantillaId = DocumentoPlantillaId,
                DocumentoPlantillaTipo = DocumentoPlantillaTipo,
                DocumentoPlantillaNombre = DocumentoPlantillaNombre,
                DocumentoPlantillaXml = DocumentoPlantillaXml,
                DocumentoOriginal = DocumentoOriginal,
                EstadosId = EstadosId,
                DocumentoPagare = DocumentoPagare,
                DocumentoFiador = DocumentoFiador,
                DocumentoInmueble = DocumentoInmueble,
                DocumentoCarta = DocumentoCarta
            };
            _context.Update(documentoplantilla);
            var result = await _context.SaveChangesAsync();
            return "Save";
        }
        //Eliminamos
        public async Task<String> DeleteConfirmed(int id)
        {
            var documentos = await _context.DocumentoPlantilla.SingleOrDefaultAsync(m => m.DocumentoPlantillaId == id);
            _context.DocumentoPlantilla.Remove(documentos);
            await _context.SaveChangesAsync();
            return "Save";
        }
    }
}
