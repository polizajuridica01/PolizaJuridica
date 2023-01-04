using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PolizaJuridica.Data;
using PolizaJuridica.Utilerias;
using PolizaJuridica.ViewModels;

namespace PolizaJuridica.Controllers
{
    public class SolucionDetallesController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public SolucionDetallesController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: SolucionDetalles
        public async Task<IActionResult> Index(int Id)
        {
            ViewBag.Id = Id;
            var polizaJuridicaDbContext = _context.SolucionDetalle.Include(s => s.Usuario.Representacion).Where(s => s.SolucionesId == Id);
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        public async Task<String> Insertar( string Observaciones,string DocumentosImagen,string DocumentoDesc, int SolucionesId, SolucionDetalle solucionDetalle)
        {
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();
            Boolean isError = false;
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            string result = string.Empty;

            if (Observaciones == null)
            {
                Error.Add(Mensajes.ErroresAtributos("Observaciones"));
                isError = true;
            }
            if (isError == false)
            {
                solucionDetalle = new SolucionDetalle
                {
                    DocumentosImagen = DocumentosImagen,
                    DocumentoDesc = DocumentoDesc,
                    SolucionesId = SolucionesId,
                    FechaCreacion = DateTime.Now,
                    Observaciones = Observaciones,
                    UsuarioId = usuarioid,
                };
                _context.Add(solucionDetalle);
                int resulta = await _context.SaveChangesAsync();
                if (resulta > 0)
                {
                    Error.Add(Mensajes.Exitoso("Se agrego correctamente"));
                    result = JsonConvert.SerializeObject(Error);
                    return result;
                }
                else
                {
                    Error.Add(Mensajes.MensajesError("Error, favor de copiar el error y mandarlo al da" + resulta.ToString()));
                    result = JsonConvert.SerializeObject(Error);
                    return result;
                }                
            }
            result = JsonConvert.SerializeObject(Error);
            return result;

        }

        // GET: RefArrens/Details/5
        public async Task<List<SolucionDetalle>> EditAjax(int? id)
        {
            List<SolucionDetalle> documento = new List<SolucionDetalle>();
            var appDocumento = await _context.SolucionDetalle.SingleOrDefaultAsync(s => s.SolucionDetalleId == id);
            documento.Add(appDocumento);
            return documento;
        }

        public async Task<String> Editar(int SolucionDetalleId,string Observaciones, string DocumentosImagen, string DocumentoDesc, int SolucionesId, SolucionDetalle solucionDetalle)
        {
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();
            string result = string.Empty;
            Boolean isError = false;
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            var solucionDet = _context.SolucionDetalle.Where(s => s.SolucionDetalleId == SolucionDetalleId).FirstOrDefault();
            if (Observaciones == null)
            {
                Error.Add(Mensajes.ErroresAtributos("Observaciones"));
                isError = true;
            }
            if (isError == false)
            {
                solucionDetalle = new SolucionDetalle
                {
                    SolucionDetalleId = SolucionDetalleId,
                    DocumentosImagen = DocumentosImagen,
                    DocumentoDesc = DocumentoDesc,
                    SolucionesId = SolucionesId,
                    UsuarioId = usuarioid,
                    Observaciones = Observaciones,
                    FechaCreacion = solucionDet.FechaCreacion,
                };
                _context.Update(solucionDetalle);
                int resulta = await _context.SaveChangesAsync();
                if (resulta > 0)
                {
                    Error.Add(Mensajes.Exitoso("Se actualizaron los datos"));
                    result = JsonConvert.SerializeObject(Error);
                    return result;
                }
                else
                {
                    Error.Add(Mensajes.MensajesError("Error, favor de copiar el error y mandarlo al " + resulta.ToString()));
                    result = JsonConvert.SerializeObject(Error);
                    return result;
                }
            }
            result = JsonConvert.SerializeObject(Error);
            return result;
        }
        //Eliminamos
        public async Task<String> DeleteConfirmed(int id)
        {
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();
            string result = string.Empty;
            var solucionDetalle = await _context.SolucionDetalle.SingleOrDefaultAsync(m => m.SolucionDetalleId == id);
            _context.SolucionDetalle.Remove(solucionDetalle);
            int resulta = await _context.SaveChangesAsync();
            if (resulta > 0)
            {
                Error.Add(Mensajes.Exitoso(""));
                result = JsonConvert.SerializeObject(Error);
                return result;
            }
            else
            {
                Error.Add(Mensajes.MensajesError("Error, favor de copiar el error y mandarlo al " + resulta.ToString()));
                result = JsonConvert.SerializeObject(Error);
                return result;
            }
        }
    }
}
