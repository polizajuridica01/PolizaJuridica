using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Xsl;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PolizaJuridica.Data;
using PolizaJuridica.Utilerias;
using PolizaJuridica.ViewModels;

namespace PJWEB.Controllers
{
    public class GenerarSolicitudesController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;
        //private XmlSerializer serializer;
        public GenerarSolicitudesController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int id)
        {
            ViewData["Fisicas"] = _context.FiadorF.Where(f => f.FisicaMoralId == id).ToList();
            ViewData["Morales"] = _context.FiadorM.Where(f => f.FisicaMoralId == id).ToList();
            ViewData["Arrendadores"] = _context.Arrendatario.Where(a => a.FisicaMoralId == id).ToList();
            
            //sección del código que se puede mejorar
            var fisicaMoral = (await _context.FisicaMoral.Include(f => f.Solicitud).SingleOrDefaultAsync(f => f.FisicaMoralId == id));
            ViewBag.TipoPoliza = fisicaMoral.Solicitud.SolicitudTipoPoliza;
            ViewBag.TipoRegimen = fisicaMoral.Solicitud.TipoArrendatario;
            ViewBag.Fiador = fisicaMoral.Solicitud.SolicitudFiador;
            //Arrendador
            ViewBag.Arrendador = fisicaMoral.Solicitud.SolicitudNombreProp + " " + fisicaMoral.Solicitud.SolicitudApePaternoProp;

            //Arrendatario
            ViewBag.Arrendatario = fisicaMoral.SfisicaNombre + " " + fisicaMoral.SfisicaApePat;

            //Motramos Fiadores
            bool fisica = false;
            bool Moral = false;

            if (fisicaMoral.FiadorF.Count >= 1)
                fisica = true;

            if (fisicaMoral.FiadorM.Count >= 1)
                Moral = true;

            ViewBag.Fisica = fisica;
            ViewBag.Moral = Moral;
            ViewBag.SolicitudId = fisicaMoral.SolicitudId;
            ViewBag.Id = id;


            return View();
        }


        public async Task<IActionResult> GenerarSolicitudes(int id, int contratoid)
        {

            var fisicaMoral = await _context.FisicaMoral.Include(f => f.Solicitud.Estados)
                .Include(f => f.Solicitud.TipoInmobiliario)
                .Include(f => f.Solicitud.CentroCostos)
                .Include(f => f.Solicitud.Representante.Representacion)
                .Include(f => f.FiadorF)
                .Include(f => f.FiadorM)
                .Include(f => f.Poliza)
                .Include(f => f.Solicitud.Asesor)
                .Include(f => f.Solicitud.Representante)
                .SingleOrDefaultAsync(f => f.FisicaMoralId == id);

            var documento = _context.DocumentoPlantilla.Where(d => d.DocumentoPlantillaId == contratoid).SingleOrDefault();

            if (fisicaMoral.Poliza.Count == 0 && documento.DocumentoPlantillaTipo == "Poliza")
            {
                //var PolizaEstatus = _context.PolizaEstatus.SingleOrDefault(p => p.PolizaEstatusId == 1);
                //Poliza polizac = new Poliza()
                //{
                //    Creacion = DateTime.Now,
                //    PolizaEstatusId = PolizaEstatus.PolizaEstatusId,
                //    FisicaMoralId = id,
                //};
                //_context.Poliza.Add(polizac);
                //_context.SaveChanges();

                fisicaMoral = await _context.FisicaMoral.Include(f => f.Solicitud.Estados)
                .Include(f => f.Solicitud.TipoInmobiliario)
                .Include(f => f.Solicitud.CentroCostos)
                .Include(f => f.Solicitud.Representante.Representacion)
                .Include(f => f.FiadorF)
                .Include(f => f.FiadorM)
                .Include(f => f.Poliza)
                .Include(f => f.Solicitud.Asesor)
                .Include(f => f.Solicitud.Representante)
                .SingleOrDefaultAsync(f => f.FisicaMoralId == id);
            }

            var DocumentoOriginal = documento.DocumentoOriginal?.Split(';')[1].Split(',')[1];
            var DocumentName = documento.DocumentoPlantillaNombre;
            var ArchivoOriginal = Convert.FromBase64String(DocumentoOriginal);
            var templateOriginal = new MemoryStream(ArchivoOriginal);
            templateOriginal.Seek(0, SeekOrigin.Begin);
            var outputDoc = new MemoryStream();
            templateOriginal.CopyTo(outputDoc);
            outputDoc.Seek(0, SeekOrigin.Begin);

            ///
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(outputDoc, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }
                //seccion donde se remplaza las palabras claves

                //seccion arrendador
                docText = Keywords.SolicitudOtorgamiento(docText, fisicaMoral);

                //docText = Keywords.SolicitudArrendatario(docText, fisicaMoral);

                //if (fisicaMoral.Arrendatario.Count >= 1)
                //    docText = Keywords.SolicitudArrendatarios(docText, fisicaMoral);

                //if (fisicaMoral.FiadorF.Count >= 1)
                //    docText = Keywords.SolicitudFiadorFisico(docText, fisicaMoral);


                //if (fisicaMoral.FiadorM.Count >= 1)
                //    docText = Keywords.SolicitudFiadorMoral(docText, fisicaMoral);

                //if (fisicaMoral.Poliza.Count >= 1)
                //    docText = Keywords.GenerarPoliza(docText, fisicaMoral);

                //fin de seccion

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
            }
            var arch = DescargaArchivoWord(outputDoc, DocumentName.ToUpper());

            return arch;
        }
        public FileResult DescargaArchivoWord(Stream file, string DocumentName)
        {
            var mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            file.Seek(0, SeekOrigin.Begin);
            return File(file, mimeType, DocumentName + ".docx");
        }

        public FileResult DescargaArchivoWord(string filePath, string DocumentName)
        {
            var mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            return File(fileStream, mimeType, DocumentName + ".docx");
        }
    }
}
