using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PolizaJuridica.Data;
using PolizaJuridica.Utilerias;
using PolizaJuridica.ViewModels;

namespace PolizaJuridica.Controllers
{
    public class DetallePolizasController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public DetallePolizasController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: DetallePolizas
        public async Task<IActionResult> Index(int id)
        {
            var polizaJuridicaDbContext = _context.DetallePoliza.Include(d => d.Poliza).Include(d => d.CategoriaEs).Where(d => d.PolizaId == id);
            decimal Entrada = 0;
            decimal Salida = 0;
            foreach (var i in polizaJuridicaDbContext)
            {
                if (i.CategoriaEs.TipoEs == 1)
                {
                    Entrada += Convert.ToDecimal(i.Importe);
                }
                if (i.CategoriaEs.TipoEs == 2)
                {
                    Salida += Convert.ToDecimal(i.Importe);
                }

            }

            var treetmp = _context.CategoriaEs
                .Where(c => c.CategoriaEspadreId == null)
                .Where(c => c.Poliza == true)
                .OrderBy(c => c.Descripcion)
                .Select(c => new
                {
                    text = c.Descripcion,
                    Id = c.CategoriaEsid,
                    nodes = c.InverseCategoriaEspadre.Select(i => new
                    {
                        text = i.Descripcion,
                        Id = i.CategoriaEsid,
                    })
                }).ToList();

            ViewData["Cuenta"] = new SelectList(_context.CuentasBancarias, "CuentaId", "Nombre");
            ViewBag.Entrada = Entrada;
            ViewBag.Salida = Salida;
            ViewBag.Total = Entrada - Salida;
            ViewBag.Id = id;
            ViewBag.Tree = JsonConvert.SerializeObject(treetmp);
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        public async Task<String> Insertar(int categoriaESId, decimal Importe, int PolizaId, DetallePoliza detallePoliza)
        {
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();
            Boolean isError = false;
            string result = string.Empty;

            if (categoriaESId == 0)
            {
                isError = true;
                Error.Add(Mensajes.MensajesError("La categoría no puede estar vacía"));
            }

            if (Importe == 0)
            {
                isError = true;
                Error.Add(Mensajes.MensajesError("El importe no puede ser cero "));

            }
            if (isError == false)
            {
                _context.Add(detallePoliza);
                var resulta = await _context.SaveChangesAsync();
                if (resulta > 0)
                {
                    Error.Add(Mensajes.Exitoso("Se agrego correctamente"));
                    result = JsonConvert.SerializeObject(Error);
                    return result;
                }
                else
                {
                    Error.Add(Mensajes.MensajesError("Error, favor de copiar el error y mandarlo al admin" + resulta.ToString()));
                    result = JsonConvert.SerializeObject(Error);
                    return result;
                }
            }
            else
            {
                result = JsonConvert.SerializeObject(Error);
                return result;
            }

        }

        public async Task<List<DetallePoliza>> Consulta(int? id)
        {
            List<DetallePoliza> detallePoliza = new List<DetallePoliza>();
            var dt = await _context.DetallePoliza.SingleOrDefaultAsync(d => d.DetallePolizaId == id);
            detallePoliza.Add(dt);
            return detallePoliza;
        }

        public async Task<List<DetallePoliza>> ActualizarC(int? id)
        {
            List<DetallePoliza> detallePoliza = new List<DetallePoliza>();
            var dt = await _context.DetallePoliza.SingleOrDefaultAsync(d => d.DetallePolizaId == id);
            detallePoliza.Add(dt);
            return detallePoliza;
        }

        public async Task<String> Actualizar(int categoriaESId, decimal Importe, int PolizaId, int detallePolizaId, DetallePoliza detallePoliza)
        {

            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();
            Boolean isError = false;
            string result = string.Empty;

            if (categoriaESId == 0)
            {
                isError = true;
                Error.Add(Mensajes.MensajesError("La categoría no puede estar vacía"));
            }

            if (Importe == 0)
            {
                isError = true;
                Error.Add(Mensajes.MensajesError("El importe no puede ser cero "));

            }
            if (isError == false)
            {
                _context.Update(detallePoliza);
                var resulta = await _context.SaveChangesAsync();
                if (resulta > 0)
                {
                    Error.Add(Mensajes.Exitoso("Se agrego correctamente"));
                    result = JsonConvert.SerializeObject(Error);
                    return result;
                }
                else
                {
                    Error.Add(Mensajes.MensajesError("Error, favor de copiar el error y mandarlo al admin" + resulta.ToString()));
                    result = JsonConvert.SerializeObject(Error);
                    return result;
                }
            }
            else
            {
                result = JsonConvert.SerializeObject(Error);
                return result;
            }

        }

        public async Task<String> DeleteConfirmed(int id)
        {
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            string resulta = string.Empty;

            var dp = await _context.DetallePoliza.SingleOrDefaultAsync(d => d.DetallePolizaId == id);
            _context.DetallePoliza.Remove(dp);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                Error.Add(Mensajes.Exitoso("Se elimino correctamente el registro"));
            }
            else
            {
                Error.Add(Mensajes.MensajesError("Error, favor de copiar el error y mandarlo al da" + result.ToString()));
            }
            resulta = JsonConvert.SerializeObject(Error);
            return resulta;
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
        public ActionResult GenerarMovimiento(int CuentaId, int PolizaId)
        {
            Boolean IsError = false;

            var polizamovimientos = _context.DetallePoliza.Include(pm => pm.CategoriaEs).Where(pm => pm.PolizaId == PolizaId);
            var usuarioId = Int32.Parse(User.FindFirst("Id").Value);
            int MovimientoMaestroId = 0;
            if (polizamovimientos == null)
                IsError = true;

            if (IsError == false)
            {
                //Revisamos en movimientos si existen, de lo contrario los damos de alta en movimiento se genera el movimiento maestro con la suma de los movimientos.
                var movimientos = _context.Movimientos.Include(m => m.DetallePoliza).Where(m => m.DetallePoliza.PolizaId == PolizaId);
                var moviColect = new List<int>();
                foreach (var movi in movimientos)
                {
                    if (movi.DetallePolizaId != null)
                    {
                        moviColect.Add(movi.DetallePolizaId.GetValueOrDefault());
                    }

                }
                decimal totalEntrada = 0;
                decimal totalSalida = 0;
                if (movimientos != null)
                {
                    if (movimientos.Count() == polizamovimientos.Count())
                    {
                        MovimientoMaestroId = (int)movimientos.FirstOrDefault().Mmid;
                        IsError = false;
                    }
                    else
                    {
                        polizamovimientos = polizamovimientos.Where(dif => !moviColect.Contains(dif.DetallePolizaId));

                        foreach (var dp in polizamovimientos)
                        {
                            if (dp.CategoriaEs.TipoEs == 1)
                                totalEntrada += Convert.ToDecimal(dp.Importe);
                            if (dp.CategoriaEs.TipoEs == 2)
                                totalSalida += Convert.ToDecimal(dp.Importe);

                        }

                        MovimientoMaestro mm = new MovimientoMaestro
                        {
                            CuentaId = CuentaId,
                            Entrada = totalEntrada,
                            Salida = totalSalida,
                            Fecha = DateTime.Now,
                            UsuarioId = usuarioId
                        };

                        _context.MovimientoMaestro.Add(mm);
                        _context.SaveChanges();
                        MovimientoMaestroId = mm.Id;

                        foreach (var m in polizamovimientos)
                        {
                            Movimientos mo = new Movimientos
                            {
                                DetallePolizaId = m.DetallePolizaId,
                                Mmid = mm.Id,
                                IdPadre = null,
                                Ccid = null,
                                Cpid = null,
                                Dmid = null
                            };

                            _context.Movimientos.Add(mo);

                        }
                        _context.SaveChanges();

                        //actualizamos el saldo de la cuenta
                        var cuenta = _context.CuentasBancarias.FirstOrDefault(cuentas => cuentas.CuentaId == CuentaId);
                        cuenta.Saldo = cuenta.Saldo + (mm.Entrada - mm.Salida);
                        _context.CuentasBancarias.Update(cuenta);
                        _context.SaveChanges();
                    }
                }
                else//cuando hay movimientos buscam
                {

                    foreach (var dp in polizamovimientos)
                    {
                        if (dp.CategoriaEs.TipoEs == 1)
                            totalEntrada += Convert.ToDecimal(dp.Importe);
                        if (dp.CategoriaEs.TipoEs == 2)
                            totalSalida += Convert.ToDecimal(dp.Importe);

                    }

                    MovimientoMaestro mm = new MovimientoMaestro
                    {
                        CuentaId = CuentaId,
                        Entrada = totalEntrada,
                        Salida = totalSalida,
                        Fecha = DateTime.Now
                    };

                    _context.MovimientoMaestro.Add(mm);
                    _context.SaveChanges();
                    MovimientoMaestroId = mm.Id;

                    foreach (var m in movimientos)
                    {
                        Movimientos mo = new Movimientos
                        {
                            DetallePolizaId = m.DetallePolizaId,
                            Mmid = mm.Id,
                            IdPadre = null,
                            Ccid = null,
                            Cpid = null,
                            Dmid = null
                        };

                        _context.Movimientos.Add(mo);

                    }
                    _context.SaveChanges();

                    //actualizamos el saldo de la cuenta
                    var cuenta = _context.CuentasBancarias.FirstOrDefault(cuentas => cuentas.CuentaId == CuentaId);
                    cuenta.Saldo = cuenta.Saldo + (mm.Entrada - mm.Salida);
                    _context.CuentasBancarias.Update(cuenta);
                    _context.SaveChanges();

                }

            }



            if (IsError == false)
            {
                var polizasolicitud = _context.Poliza.Include(p => p.FisicaMoral).FirstOrDefault(p => p.PolizaId == PolizaId);
                var solicitud = _context.Solicitud.Include(s => s.Asesor).Include(s => s.Representante).FirstOrDefault(s => s.SolicitudId == polizasolicitud.FisicaMoral.SolicitudId);
                var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuariosId == usuarioId);
                var documento = _context.DocumentoPlantilla.Where(d => d.DocumentoPlantillaTipo == "MovimientoPoliza").SingleOrDefault();
                var DocumentoOriginal = documento.DocumentoOriginal?.Split(';')[1].Split(',')[1];
                var DocumentName = documento.DocumentoPlantillaNombre;
                var ArchivoOriginal = Convert.FromBase64String(DocumentoOriginal);
                var templateOriginal = new MemoryStream(ArchivoOriginal);
                templateOriginal.Seek(0, SeekOrigin.Begin);
                var outputDoc = new MemoryStream();
                decimal TotalCuenta = 0;
                templateOriginal.CopyTo(outputDoc);
                outputDoc.Seek(0, SeekOrigin.Begin);

                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(outputDoc, true))
                {
                    string docText = null;
                    using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                    {
                        docText = sr.ReadToEnd();
                    }

                    docText = docText.Replace("PolizaId", PolizaId.ToString());
                    docText = docText.Replace("MovimientoMaestroId", MovimientoMaestroId.ToString());

                    if (solicitud.Asesor != null)
                    {
                        docText = docText.Replace("AsesorNombre", solicitud.Asesor.UsuarioNomCompleto.Trim());
                    }
                    else
                    {
                        docText = docText.Replace("AsesorNombre", "");
                    }
                    if (solicitud.Representante != null)
                    {
                        docText = docText.Replace("RepresentanteNombre", solicitud.Representante.UsuarioNomCompleto.Trim());
                    }
                    else
                    {
                        docText = docText.Replace("RepresentanteNombre", "");
                    }
                    if (usuario != null)
                    {
                        docText = docText.Replace("UsuarioMovimiento", usuario.UsuarioNomCompleto.Trim());
                    }

                    int contador = 1;
                    foreach (var i in polizamovimientos)
                    {
                        docText = docText.Replace("Concepto" + contador.ToString().Trim(), i.CategoriaEs.Descripcion.Trim());
                        if (i.CategoriaEs.TipoEs == 1)
                        {
                            docText = docText.Replace("Flujo" + contador.ToString().Trim(), "Entrada");
                            TotalCuenta += Convert.ToDecimal(i.Importe);
                        }


                        if (i.CategoriaEs.TipoEs == 2)
                        {
                            docText = docText.Replace("Flujo" + contador.ToString().Trim(), "Salida");
                            TotalCuenta -= Convert.ToDecimal(i.Importe);
                        }

                        docText = docText.Replace("Importe" + contador.ToString().Trim(), "$ " + i.Importe.ToString().Trim());
                        contador++;
                    }
                    if (contador <= 5)
                    {
                        contador--;
                        for (var j = contador; j <= 5; j++)
                        {
                            docText = docText.Replace("Concepto" + j.ToString().Trim(), "");
                            docText = docText.Replace("Flujo" + j.ToString().Trim(), "");
                            docText = docText.Replace("Importe" + j.ToString().Trim(), "$ " + "");
                        }
                    }

                    docText = docText.Replace("FechaSistema", DateTime.Now.ToString());

                    docText = docText.Replace("TotalCuenta", TotalCuenta.ToString());
                    string DomicilioArrendar = solicitud.SolicitudUbicacionArrendado != null ? solicitud.SolicitudUbicacionArrendado.ToUpper() : "" +
                        ", " + solicitud.ColoniaArrendar != null ? solicitud.ColoniaArrendar.ToUpper() : "" + " "
                        + solicitud.AlcaldiaMunicipioArrendar != null ? solicitud.AlcaldiaMunicipioArrendar.ToUpper() : ""
                        + " CÓDIGO POSTAL : " + solicitud.CodigoPostalArrendar != null ? solicitud.AlcaldiaMunicipioArrendar.ToUpper() : ""
                        + ", " + solicitud.Estados != null ? solicitud.Estados.EstadoNombre.ToUpper() : "";
                    docText = docText.Replace("DIRECCIONARRENDAR", DomicilioArrendar.ToUpper().Trim());


                    using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(docText);
                    }
                }
                var arch = DescargaArchivoWord(outputDoc, DocumentName);

                return arch;
            }
            else
            {
                return View();
            }

        }

    }
}
