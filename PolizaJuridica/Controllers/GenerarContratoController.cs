
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

    public class GenerarContratoController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public GenerarContratoController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: GenerarContrato
        public async Task<IActionResult> VistaPrevia(int id)
        {
            //Seccion de contrato
            var tipo = new string[] { "Contrato", "Poliza", "Pagare", "Carta" };
            var contrato = _context.DocumentoPlantilla.Where(d => tipo.Contains(d.DocumentoPlantillaTipo)).Select(d => new { Id = d.DocumentoPlantillaId, Desc = d.DocumentoPlantillaNombre, tipo = d.DocumentoPlantillaTipo }).ToList();
            List<DocumentoPlantilla> plantillaContrato = contrato.ConvertAll(p => new DocumentoPlantilla() { DocumentoPlantillaId = p.Id, DocumentoPlantillaNombre = p.Desc, DocumentoPlantillaTipo = p.tipo });
            ViewData["Contratos"] = plantillaContrato;
            //sección del código que se puede mejorar
            var fisicaMoral = (await _context.FisicaMoral.Include(f => f.Solicitud).SingleOrDefaultAsync(f => f.FisicaMoralId == id));
            ViewBag.TipoPoliza = fisicaMoral.Solicitud.SolicitudTipoPoliza;
            ViewBag.TipoRegimen = fisicaMoral.Solicitud.TipoArrendatario;
            ViewBag.Fiador = fisicaMoral.Solicitud.SolicitudFiador;
            ViewBag.Pagare = fisicaMoral.Solicitud.SolicitudPagare;
            ViewBag.Carta = fisicaMoral.Solicitud.SolicitudCartaEntrega;
            ViewBag.SolicitudId = fisicaMoral.SolicitudId;
            ViewBag.Id = id;
            ViewBag.Jurisdiccion = fisicaMoral.Solicitud.Jurisdiccion;

            return View();
        }

        //Seccion de la jurisdicción
        public async Task<String> Insertar(int id, string jurisdiccion, Solicitud solicitud)
        {
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            string json = string.Empty;
            if (jurisdiccion == null)
            {
                Error.Add(Mensajes.ErroresAtributos("jurisdiccionError"));
                json = JsonConvert.SerializeObject(Error);
                return json;
            }
            else
            {
                var fisicaMoral = _context.FisicaMoral.Include(f => f.Solicitud).SingleOrDefault(f => f.FisicaMoralId == id);
                solicitud = fisicaMoral.Solicitud;
                solicitud.Jurisdiccion = jurisdiccion;
                _context.Update(solicitud);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    Error.Add(Mensajes.Exitoso("Se agrego correctamente"));
                    json = JsonConvert.SerializeObject(Error);
                    return json;
                }
                else
                {
                    Error.Add(Mensajes.MensajesError("No se pudo agregar el registro"));
                    json = JsonConvert.SerializeObject(Error);
                    return json;
                }
            }
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
        [HttpPost]
        public async Task PruebaCalculo(int id)
        {
            CalculoPolizaRegalias calculo = new CalculoPolizaRegalias(_context);
            //calculo.Calcular(id);
        }

        public async Task<IActionResult> Generar(int id, int contratoid)
        {
            var usuarioId = Int32.Parse(User.FindFirst("Id").Value);
            var fisicaMoral = await _context.FisicaMoral.Include(f => f.Solicitud.Estados)
                .Include(f => f.Solicitud.TipoInmobiliario)
                .Include(f => f.Solicitud.CentroCostos)
                .Include(f => f.Solicitud.Representante.Representacion)
                .Include(f => f.FiadorF)
                .Include(f => f.FiadorM)
                .Include(f => f.Poliza)
                .Include(f => f.Solicitud.Asesor)
                .Include(f => f.Solicitud.Representante.Representacion)
                .Include(f => f.Arrendatario)
                .Include(f => f.ReporteInvst)
                .SingleOrDefaultAsync(f => f.FisicaMoralId == id);

            var documento = _context.DocumentoPlantilla.Where(d => d.DocumentoPlantillaId == contratoid).SingleOrDefault();

            if (fisicaMoral.Poliza.Count == 0 && documento.DocumentoPlantillaTipo == "Poliza")
            {
                Poliza polizac = new Poliza()
                {
                    Creacion = DateTime.Now,
                    FisicaMoralId = id,
                };
                _context.Poliza.Add(polizac);
                _context.SaveChanges();

                var tipoProceso = _context.TipoProcesoPo.Where(t => t.Orden == 1).SingleOrDefault();

                UsuarioPoliza up = new UsuarioPoliza()
                {
                    UsuariosId = usuarioId,
                    PolizaId = polizac.PolizaId,
                    Fecha = DateTime.Now,
                    TipoProcesoPoId = tipoProceso.TipoProcesoPoId
                };
                _context.UsuarioPoliza.Add(up);
                _context.SaveChanges();



                fisicaMoral = await _context.FisicaMoral.Include(f => f.Solicitud.Estados)
                .Include(f => f.Solicitud.TipoInmobiliario)
                .Include(f => f.Solicitud.CentroCostos)
                .Include(f => f.Solicitud.Representante.Representacion)
                .Include(f => f.FiadorF)
                .Include(f => f.FiadorM)
                .Include(f => f.Poliza)
                .Include(f => f.Solicitud.Asesor)
                .Include(f => f.Solicitud.Representante.Representacion)
                .Include(f => f.Arrendatario)
                .Include(f => f.ReporteInvst)
                .SingleOrDefaultAsync(f => f.FisicaMoralId == id);

                CalculoPolizaRegalias calculo = new CalculoPolizaRegalias(_context);
                calculo.Calcular(fisicaMoral.FisicaMoralId);
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

                //sección de estructura
                docText = GenerarKWS(docText);
                //fin de sección

                //seccion donde se remplaza las palabras claves

                //seccion arrendador
                docText = Keywords.SolicitudOtorgamiento(docText, fisicaMoral);
                var arrendatarios = _context.Arrendatario.Where(a => a.FisicaMoralId == fisicaMoral.FisicaMoralId).ToList();

                docText = KeywordsFisicaMoral.SolicitudArrendatario(docText, fisicaMoral, arrendatarios);

                if (fisicaMoral.Arrendatario.Count >= 1)
                    docText = KeywordsArrendatarios.SolicitudArrendatarios(docText, fisicaMoral);

                if (fisicaMoral.FiadorF.Count >= 1)
                {
                    var tipoInmueble = _context.TipoInmobiliario.ToList();
                    docText = KeywordsFisicaF.SolicitudFiadorFisico(docText, fisicaMoral, tipoInmueble);
                }


                if (fisicaMoral.FiadorM.Count >= 1)
                    docText = KeywordsFisicaM.SolicitudFiadorMoral(docText, fisicaMoral);

                if (fisicaMoral.Poliza.Count >= 1)
                    docText = KeywordsPoliza.GenerarPoliza(docText, fisicaMoral);

                if (fisicaMoral.ReporteInvst.Count >= 1)
                    docText = KeywordReporteInvest.Generar(docText,fisicaMoral.ReporteInvst.SingleOrDefault());

                //fin de seccion

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
            }
            var arch = DescargaArchivoWord(outputDoc, DocumentName);

            return arch;
        }

        public String GenerarKWS(string docText)
        {
            string pattern = string.Empty;

            foreach (var k in _context.KeywordStructura.OrderBy(o => o.Orden).ToList())
            {
                pattern = string.Empty;
                if (k.Keyword != null)
                {
                    if (k.Estructura != null)
                    {
                        pattern = @"\b" + k.Keyword.Trim() + @"\b";
                        docText = Regex.Replace(docText, pattern, k.Estructura.Trim());
                    }
                    else
                    {
                        pattern = @"\b" + k.Keyword.Trim() + @"\b";
                        docText = Regex.Replace(docText, pattern, "");
                    }
                }
            }

            return docText;
        }



        public async Task<IActionResult> GenerarAuto(int id)
        {
            var usuarioId = Int32.Parse(User.FindFirst("Id").Value);
            var fisicaMoral = await _context.FisicaMoral.Include(f => f.Solicitud.Estados)
                .Include(f => f.Solicitud.TipoInmobiliario)
                .Include(f => f.Solicitud.CentroCostos)
                .Include(f => f.Solicitud.Representante.Representacion)
                .Include(f => f.FiadorF)
                .Include(f => f.FiadorM)
                .Include(f => f.Poliza)
                .Include(f => f.Solicitud.Asesor)
                .Include(f => f.Solicitud.Representante.Representacion)
                .Include(f => f.Arrendatario)
                .SingleOrDefaultAsync(f => f.FisicaMoralId == id);


            var documento = _context.DocumentoPlantilla.Where(d => d.DocumentoPlantillaId == 1).SingleOrDefault();

            if (fisicaMoral.Poliza.Count == 0 && documento.DocumentoPlantillaTipo == "Poliza")
            {
                Poliza polizac = new Poliza()
                {
                    Creacion = DateTime.Now,
                    FisicaMoralId = id,
                };
                _context.Poliza.Add(polizac);
                _context.SaveChanges();

                var tipoProceso = _context.TipoProcesoPo.Where(t => t.Orden == 1).SingleOrDefault();

                UsuarioPoliza up = new UsuarioPoliza()
                {
                    UsuariosId = usuarioId,
                    PolizaId = polizac.PolizaId,
                    Fecha = DateTime.Now,
                    TipoProcesoPoId = tipoProceso.TipoProcesoPoId
                };
                _context.UsuarioPoliza.Add(up);
                _context.SaveChanges();



                fisicaMoral = await _context.FisicaMoral.Include(f => f.Solicitud.Estados)
                .Include(f => f.Solicitud.TipoInmobiliario)
                .Include(f => f.Solicitud.CentroCostos)
                .Include(f => f.Solicitud.Representante.Representacion)
                .Include(f => f.FiadorF)
                .Include(f => f.FiadorM)
                .Include(f => f.Poliza)
                .Include(f => f.Solicitud.Asesor)
                .Include(f => f.Solicitud.Representante.Representacion)
                .Include(f => f.Arrendatario)
                .SingleOrDefaultAsync(f => f.FisicaMoralId == id);

                CalculoPolizaRegalias calculo = new CalculoPolizaRegalias(_context);
                calculo.Calcular(fisicaMoral.FisicaMoralId);
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

                //sección de estructura
                docText = GenerarKWS(docText);
                //fin de sección

                //seccion donde se remplaza las palabras claves

                //seccion arrendador
                docText = Keywords.SolicitudOtorgamiento(docText, fisicaMoral);

                var arrendatarios = _context.Arrendatario.Where(a => a.FisicaMoralId == fisicaMoral.FisicaMoralId).ToList();

                docText = KeywordsFisicaMoral.SolicitudArrendatario(docText, fisicaMoral, arrendatarios);

                if (fisicaMoral.Arrendatario.Count >= 1)
                    docText = KeywordsArrendatarios.SolicitudArrendatarios(docText, fisicaMoral);

                if (fisicaMoral.FiadorF.Count >= 1)
                {
                    var tipoInmueble = _context.TipoInmobiliario.ToList();
                    docText = KeywordsFisicaF.SolicitudFiadorFisico(docText, fisicaMoral, tipoInmueble);
                }


                if (fisicaMoral.FiadorM.Count >= 1)
                    docText = KeywordsFisicaM.SolicitudFiadorMoral(docText, fisicaMoral);

                if (fisicaMoral.Poliza.Count >= 1)
                    docText = KeywordsPoliza.GenerarPoliza(docText, fisicaMoral);

                //fin de seccion

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
            }
            var arch = DescargaArchivoWord(outputDoc, DocumentName);

            return arch;
        }

    }
}
