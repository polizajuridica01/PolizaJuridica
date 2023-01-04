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
    public class UsuarioPolizasController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public UsuarioPolizasController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: UsuarioPolizas
        public async Task<IActionResult> Index(int id)
        {
            var polizaJuridicaDbContext = _context.UsuarioPoliza.Include(u => u.Poliza).Include(u => u.TipoProcesoPo).Include(u => u.Usuarios).Where(u => u.PolizaId == id);
            ViewData["TipoProcesoId"] = new SelectList(_context.TipoProcesoPo.OrderBy(t => t.Orden), "TipoProcesoPoId", "Descripcion");
            ViewBag.Id = id;
            return View(await polizaJuridicaDbContext.ToListAsync());
        }
        public async Task<String> CancelarProceso(int id, string Observacion)
        {


            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();
            Boolean isError = false;
            string result = string.Empty;

            var poliza = await _context.Poliza.FirstOrDefaultAsync(f => f.PolizaId == id);
            var up = _context.UsuarioPoliza.OrderByDescending(p => p.UsuarioPolizaId).FirstOrDefault(u => u.TipoProcesoPoId == 99 && u.PolizaId == id);
            if (up != null)
            {
                Error.Add(Mensajes.MensajesError("No se puede cancelar, esta solicitud de forma consecutiva"));
                result = JsonConvert.SerializeObject(Error);
                return result;
            }
            if (isError == false)
            {
                if (Observacion == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("Observacion"));
                    isError = true;
                }
            }

            if (isError == false)
            {
                var usuarioId = Int32.Parse(User.FindFirst("Id").Value);
                var tpp = _context.TipoProcesoPo.FirstOrDefault(t => t.Orden == 99).TipoProcesoPoId;

                UsuarioPoliza usuariopoliza = new UsuarioPoliza()
                {
                    UsuariosId = usuarioId,
                    PolizaId = id,
                    Fecha = DateTime.Now,
                    Observacion = Observacion,
                    TipoProcesoPoId = tpp //estatus de cancelado
                };
                _context.Add(usuariopoliza);
                int resulta = await _context.SaveChangesAsync();
                if (resulta > 0)
                {
                    _context.Update(poliza);
                    await _context.SaveChangesAsync();

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
        public async Task<String> TerminarProceso(int id)
        {
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();
            Boolean isError = false;
            string result = string.Empty;
            var poliza = await _context.Poliza.SingleOrDefaultAsync(f => f.PolizaId == id);
            int tipoProceso = 0;
            var up = _context.UsuarioPoliza.Include(u => u.TipoProcesoPo).OrderByDescending(u => u.UsuarioPolizaId).FirstOrDefault(u => u.PolizaId == id);
            var tp = _context.TipoProceso.OrderByDescending(t => t.Orden).ToList();
            var usuarioId = Int32.Parse(User.FindFirst("Id").Value);
            if (usuarioId == up.UsuariosId)
            {
                isError = true;
                Error.Add(Mensajes.MensajesError("No puedes tener dos estatus consecutivos"));
            }

            if (isError == false)
            {
                if (up != null)
                {
                    int tmp = 0;
                    tmp = up.TipoProcesoPo.Orden + 1;
                    foreach (var i in tp)
                    {
                        if (i.Orden == tmp)
                        {
                            tipoProceso = i.TipoProcesoId;
                            break;
                        }
                        tmp++;
                    }
                }
                else
                {
                    tipoProceso = _context.TipoProcesoPo.OrderBy(t => t.Orden).FirstOrDefault().TipoProcesoPoId;
                }
                if (tipoProceso == 0)
                {
                    isError = true;
                    Error.Add(Mensajes.MensajesError("No se encontro el siguiente proceso"));
                }
            }
                        
            if (isError == false)
            {
                UsuarioPoliza usuarioPoliza = new UsuarioPoliza()
                {
                    UsuariosId = usuarioId,
                    PolizaId = id,
                    Fecha = DateTime.Now,
                    TipoProcesoPoId = tipoProceso //estatus de cancelado
                };
                _context.Add(usuarioPoliza);
                int resulta = await _context.SaveChangesAsync();
                if (resulta > 0)
                {
                    //poliza.PolizaEstatusId = 2;
                    _context.Update(poliza);
                    await _context.SaveChangesAsync();

                    Error.Add(Mensajes.Exitoso("Se termino el proceso de forma correctamente"));
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
        public async Task<String> CambiarProceso(int id, int TipoProcesoId)
        {
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();
            Boolean isError = false;
            string result = string.Empty;
            var poliza = await _context.Poliza.Include(s => s.UsuarioPoliza).SingleOrDefaultAsync(s => s.PolizaId == id);

            if (poliza.UsuarioPoliza.Count >= 1)
            {
                var usuarioPoliza = _context.UsuarioPoliza.OrderByDescending(u => u.UsuarioPolizaId).FirstOrDefault(u => u.PolizaId == id);
                if (usuarioPoliza.TipoProcesoPoId == TipoProcesoId)
                {
                    Error.Add(Mensajes.MensajesError("No se puede generar dos estatus consecutivos"));
                    result = JsonConvert.SerializeObject(Error);
                    return result;
                }
            }

            if (isError == false)
            {
                var usuarioId = Int32.Parse(User.FindFirst("Id").Value);

                UsuarioPoliza usuarioPoliza = new UsuarioPoliza()
                {
                    UsuariosId = usuarioId,
                    PolizaId = id,
                    Fecha = DateTime.Now,
                    TipoProcesoPoId = TipoProcesoId //estatus de cancelado
                };
                _context.Add(usuarioPoliza);
                int resulta = await _context.SaveChangesAsync();
                if (resulta > 0)
                {
                    //poliza.PolizaEstatusId = TipoProcesoId;
                    _context.Update(poliza);
                    await _context.SaveChangesAsync();

                    Error.Add(Mensajes.Exitoso("Se termino el proceso de forma correctamente"));
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

    }
}
