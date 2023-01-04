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
    public class UsuariosSolucionesController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public UsuariosSolucionesController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: UsuariosSoluciones
        public async Task<IActionResult> Index(int Id)
        {
            ViewBag.Id = Id;
            var usuarioArea = User.FindFirst("Area").Value;
            List<int> estatus = new List<int>();
            if (usuarioArea == "Administración" || usuarioArea == "Soluciones")
            {
                estatus.Clear();
                estatus.Add(16);//No se va a ingresar la cancelada
                ViewData["ProcesoSolucionesId"] = new SelectList(_context.ProcesoSoluciones.Where(p => !estatus.Contains(p.ProcesoSolucionesId)), "ProcesoSolucionesId", "Descripcion");
            }
            else
            {
                estatus.Clear();
                estatus.Add(2);//Gestión Teléfonica
                estatus.Add(3);//Requerimiento de Pago 1
                estatus.Add(4);//Requerimiento de Pago 2
                estatus.Add(5);//Aviso de demanda y desocupación
                estatus.Add(15);//Concluido
                ViewData["ProcesoSolucionesId"] = new SelectList(_context.ProcesoSoluciones.Where(p => estatus.Contains(p.ProcesoSolucionesId)), "ProcesoSolucionesId", "Descripcion");
            }
                
            var polizaJuridicaDbContext = _context.UsuariosSoluciones.Include(u => u.ProcesoSoluciones).Include(u => u.Soluciones).Include(u => u.Usuarios.Representacion).Where(u => u.SolucionesId == Id);
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        public async Task<String> CancelarProceso(int id, string Observacion)
        {


            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();
            Boolean isError = false;
            string result = string.Empty;

            var soluciones = await _context.Soluciones.SingleOrDefaultAsync(f => f.SolucionesId == id);
            var usuariosolicitud = _context.UsuariosSoluciones.SingleOrDefault(u => u.ProcesoSolucionesId == 5 && u.SolucionesId == id);
            if (usuariosolicitud != null)
            {
                Error.Add(Mensajes.MensajesError("No se puede cancelar un proceso ya cancelado"));
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

                UsuariosSoluciones usuariosSoluciones = new UsuariosSoluciones()
                {
                    UsuariosId = usuarioId,
                    SolucionesId = id,
                    Fecha = DateTime.Now,
                    Observacion = Observacion,
                    ProcesoSolucionesId = 5 //estatus de cancelado
                };
                _context.Add(usuariosSoluciones);
                int resulta = await _context.SaveChangesAsync();
                if (resulta > 0)
                {
                    soluciones.ProcesoSolucionesId = 5; // se actualiza el encabezado
                    _context.Update(soluciones);
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
            var soluciones = await _context.Soluciones.SingleOrDefaultAsync(f => f.SolucionesId == id);
            
            if(soluciones.ProcesoSolucionesId == 2)
            {
                Error.Add(Mensajes.MensajesError("No se puede el mismo estatus"));
                result = JsonConvert.SerializeObject(Error);
                return result;
            }
            if (soluciones.ProcesoSolucionesId == 3 || soluciones.ProcesoSolucionesId == 4)
            {
                Error.Add(Mensajes.MensajesError("No se puede regresar a un estatus previo"));
                result = JsonConvert.SerializeObject(Error);
                return result;
            }
            if (soluciones.ProcesoSolucionesId == 5)
            {
                Error.Add(Mensajes.MensajesError("Solo un administrador puede re-abrir una solución"));
                result = JsonConvert.SerializeObject(Error);
                return result;
            }
            if (isError == false)
            {
                var usuarioId = Int32.Parse(User.FindFirst("Id").Value);

                UsuariosSoluciones usuariosSoluciones = new UsuariosSoluciones()
                {
                    UsuariosId = usuarioId,
                    SolucionesId = id,
                    Fecha = DateTime.Now,
                    ProcesoSolucionesId = 2 //estatus de cancelado
                };
                _context.Add(usuariosSoluciones);
                int resulta = await _context.SaveChangesAsync();
                if (resulta > 0)
                {
                    soluciones.ProcesoSolucionesId = 2;
                    _context.Update(soluciones);
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
            var soluciones = await _context.Soluciones.Include(s => s.UsuariosSoluciones).SingleOrDefaultAsync(s => s.SolucionesId == id);

            if (soluciones.UsuariosSoluciones.Count >= 1)
            {
                var usuarioSoluciones = _context.UsuariosSoluciones.OrderByDescending(u => u.UsuariosSolulucionesId).LastOrDefault(u => u.SolucionesId == id);
                if (usuarioSoluciones.ProcesoSolucionesId == TipoProcesoId)
                {
                    Error.Add(Mensajes.MensajesError("No se puede generar dos estatus consecutivos"));
                    result = JsonConvert.SerializeObject(Error);
                    return result;
                }
            }
            

            if (isError == false)
            {
                var usuarioId = Int32.Parse(User.FindFirst("Id").Value);

                UsuariosSoluciones usuariosSoluciones = new UsuariosSoluciones()
                {
                    UsuariosId = usuarioId,
                    SolucionesId = id,
                    Fecha = DateTime.Now,
                    ProcesoSolucionesId = TipoProcesoId //estatus de cancelado
                };
                _context.Add(usuariosSoluciones);
                int resulta = await _context.SaveChangesAsync();
                if (resulta > 0)
                {
                    soluciones.ProcesoSolucionesId = TipoProcesoId;
                    _context.Update(soluciones);
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
