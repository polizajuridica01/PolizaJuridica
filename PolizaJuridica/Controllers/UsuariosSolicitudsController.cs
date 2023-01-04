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
    public class UsuariosSolicitudsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public UsuariosSolicitudsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: UsuariosSolicituds
        public async Task<IActionResult> Index(int id)
        {
            var fisicaMoral = (await _context.FisicaMoral.Include(f => f.Solicitud).SingleOrDefaultAsync(f => f.FisicaMoralId == id));
            ViewBag.TipoPoliza = fisicaMoral.Solicitud.SolicitudTipoPoliza;
            ViewBag.TipoRegimen = fisicaMoral.Solicitud.TipoArrendatario;
            ViewBag.Fiador = fisicaMoral.Solicitud.SolicitudFiador;
            ViewBag.Id = id;
            ViewData["TipoProcesoId"] = new SelectList(_context.TipoProceso.Where(t => t.Orden >= 1), "TipoProcesoId", "Descripcion");
            List<UsuariosSolicitud> us = new List<UsuariosSolicitud>();
            if (fisicaMoral != null)
            {
                us = await _context.UsuariosSolicitud.Include(u => u.TipoProceso).Include(u => u.Usuarios).Where(u => u.SolicitudId == fisicaMoral.SolicitudId).ToListAsync();
                return View(us);
            }
            return NotFound();
        }

        //POST: Cancelar Proceso

        public async Task<String> CancelarProceso(int id, string Observacion)
        {


            bool isError = false;
            List<string> CamposError = new List<string>();
            var CError = "Error";

            //Recuperamos el Id de la solicitud

            var fisicaMoral = await _context.FisicaMoral.Include(s => s.Solicitud).SingleOrDefaultAsync(f => f.FisicaMoralId == id);
            var solicitud = fisicaMoral.Solicitud;
            var usuariosolicitud = _context.UsuariosSolicitud.SingleOrDefault(u => u.TipoProceso.Orden == 0 && u.SolicitudId == fisicaMoral.SolicitudId);
            if (usuariosolicitud != null)
            {
                CamposError.Add("No se puede cancelar un proceso ya cancelado");
                isError = true;
            }
            if (isError == false)
            {
                if (Observacion == null)
                {
                    CamposError.Add("Observacion" + CError);
                    isError = true;
                }
            }

            if (isError == false)
            {
                var Estatus = await _context.TipoProceso.SingleOrDefaultAsync(t => t.Orden == 99);
                var usuarioId = Int32.Parse(User.FindFirst("Id").Value);

                UsuariosSolicitud usuariosSolicitud = new UsuariosSolicitud()
                {
                    SolicitudId = fisicaMoral.SolicitudId,
                    UsuariosId = usuarioId,
                    Fecha = DateTime.Now,
                    Observacion = Observacion,
                    TipoProcesoId = Estatus.TipoProcesoId
                };
                _context.Add(usuariosSolicitud);
                await _context.SaveChangesAsync();

                solicitud.SolicitudEstatus = Estatus.Descripcion;
                _context.Update(solicitud);
                await _context.SaveChangesAsync();
                return "OK";
            }
            else
            {
                string json = JsonConvert.SerializeObject(CamposError);
                return json;
            }
        }
        public async Task<String> TerminarProceso(int id)
        {
            bool isError = false;
            List<string> CamposError = new List<string>();
            var fisicaMoral = await _context.FisicaMoral.Include(s => s.Solicitud).SingleOrDefaultAsync(f => f.FisicaMoralId == id);
            id = fisicaMoral.SolicitudId;
            var solicitud = fisicaMoral.Solicitud;
            var Area = User.FindFirst("Area").Value;
            var EstatusId = 0;
            CamposError.Clear();

            if (solicitud == null)
            {
                CamposError.Add("No se encuentra la solicitud");
                isError = true;
            }
            if (Area == "Asesor" || Area == "Representante")
            {
                EstatusId = 2;
            }
            if (Area == "Captura" || Area == "Atención a clientes")
            {
                EstatusId = 2;
            }
            if (Area == "Documentación")
            {
                EstatusId = 3;
            }
            if (Area == "Investigación y Validación")
            {
                EstatusId = 4;
            }
            if (Area == "Procesos" || Area == "Control y Calidad")
            {
                EstatusId = 5;
            }
            var EstatusSol = _context.UsuariosSolicitud.Include(u => u.TipoProceso).OrderByDescending(u => u.UsuariosSolicitudId).Where(u => u.SolicitudId == fisicaMoral.SolicitudId).FirstOrDefault();
            if (EstatusSol.TipoProceso.Orden == 5)
            {
                if (Area == "Procesos" || Area == "Control y Calidad")
                {
                    EstatusId = 6;
                }
            }


            var Estatus = await _context.TipoProceso.SingleOrDefaultAsync(t => t.Orden == EstatusId);

            if (Estatus == null)
            {
                CamposError.Add("No se encuentra el tipo de proceso");
                isError = true;
            }

            if (isError == false)
            {
                var usuarioId = Int32.Parse(User.FindFirst("Id").Value);

                UsuariosSolicitud usuariosSolicitud = new UsuariosSolicitud()
                {
                    SolicitudId = id,
                    UsuariosId = usuarioId,
                    Fecha = DateTime.Now,
                    TipoProcesoId = Estatus.TipoProcesoId
                };
                _context.Add(usuariosSolicitud);
                await _context.SaveChangesAsync();

                solicitud.SolicitudEstatus = Estatus.Descripcion;
                var error = _context.Update(solicitud);
                await _context.SaveChangesAsync();
                return "OK";
            }
            else
            {
                string json = JsonConvert.SerializeObject(CamposError);
                return json;
            }
        }
        public async Task<String> CambiarProceso(int id, int TipoProcesoId)
        {


            bool isError = false;
            List<string> CamposError = new List<string>();

            //Recuperamos el Id de la solicitud

            var fisicaMoral = await _context.FisicaMoral.Include(s => s.Solicitud).SingleOrDefaultAsync(f => f.FisicaMoralId == id);
            var solicitud = fisicaMoral.Solicitud;
            var usuariosolicitud = _context.UsuariosSolicitud.Where(u => u.SolicitudId == fisicaMoral.SolicitudId).Max(u => u.TipoProcesoId);
            if (usuariosolicitud == TipoProcesoId)
            {
                CamposError.Add("No se puede agregar dos estatus iguales consecutivos");
                isError = true;
            }

            if (isError == false)
            {
                var Estatus = await _context.TipoProceso.SingleOrDefaultAsync(t => t.TipoProcesoId == TipoProcesoId);
                var usuarioId = Int32.Parse(User.FindFirst("Id").Value);

                UsuariosSolicitud usuariosSolicitud = new UsuariosSolicitud()
                {
                    SolicitudId = fisicaMoral.SolicitudId,
                    UsuariosId = usuarioId,
                    Fecha = DateTime.Now,
                    Observacion = null,
                    TipoProcesoId = TipoProcesoId
                };
                _context.Add(usuariosSolicitud);
                await _context.SaveChangesAsync();

                solicitud.SolicitudEstatus = Estatus.Descripcion;
                _context.Update(solicitud);
                await _context.SaveChangesAsync();
                return "OK";
            }
            else
            {
                string json = JsonConvert.SerializeObject(CamposError);
                return json;
            }
        }

        public async Task<String> TerminarProcesoSolicitud(int id)
        {
            bool isError = false;
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            var solicitud = await _context.Solicitud.SingleOrDefaultAsync(s => s.SolicitudId == id);
            var Area = User.FindFirst("Area").Value;
            var EstatusId = 0;
            string result = string.Empty;
            Error.Clear();

            if (solicitud == null)
            {
                Error.Add(Mensajes.MensajesError("No se encuentra la solicitud"));
                isError = true;
            }
            if (Area == "Asesor" || Area == "Representante")
            {
                EstatusId = 2;
            }
            if (Area == "Documentación")
            {
                EstatusId = 3;
            }

            if (Area == "Investigación y Validación" || Area == "Atención a clientes" || Area == "Captura")
            {
                var us = _context.UsuariosSolicitud.Include(ust => ust.TipoProceso).LastOrDefault(s => s.SolicitudId == id);
                if (us != null && us.TipoProceso.Orden == 1)
                    EstatusId = 2;

                if (us != null && EstatusId == 0 && (us.TipoProceso.Orden == 3 || us.TipoProceso.Orden == 2))
                    EstatusId = 4;
            }

            if ((Area == "Procesos" || Area == "Control y Calidad") && EstatusId == 0)
            {
                EstatusId = 5;
            }

            var EstatusSol = _context.UsuariosSolicitud.Include(u => u.TipoProceso).OrderByDescending(u => u.UsuariosSolicitudId).Where(u => u.SolicitudId == solicitud.SolicitudId).FirstOrDefault();
            if (EstatusSol.TipoProceso.Orden == 5)
            {
                if (Area == "Procesos" || Area == "Control y Calidad")
                {
                    EstatusId = 6;
                }
            }
            var Estatus = await _context.TipoProceso.SingleOrDefaultAsync(t => t.Orden == EstatusId);

            if (Estatus == null)
            {
                Error.Add(Mensajes.MensajesError("No se encuentra el tipo de proceso"));
                isError = true;
            }

            if (isError == false)
            {
                var usuarioId = Int32.Parse(User.FindFirst("Id").Value);

                UsuariosSolicitud usuariosSolicitud = new UsuariosSolicitud()
                {
                    SolicitudId = id,
                    UsuariosId = usuarioId,
                    Fecha = DateTime.Now,
                    TipoProcesoId = Estatus.TipoProcesoId
                };
                _context.Add(usuariosSolicitud);
                await _context.SaveChangesAsync();

                solicitud.SolicitudEstatus = Estatus.Descripcion;
                var error = _context.Update(solicitud);
                await _context.SaveChangesAsync();
                Error.Add(Mensajes.Exitoso("Se aplicaron los cambios"));
                result = JsonConvert.SerializeObject(Error);
                return result;
            }
            else
            {
                Error.Add(Mensajes.MensajesError("Favor de completar los campos que estan en rojo"));
                result = JsonConvert.SerializeObject(Error);
                return result;
            }
        }
    }
}
