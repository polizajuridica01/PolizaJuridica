using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PolizaJuridica.Data;
using PolizaJuridica.ViewModels;

namespace PolizaJuridica.Controllers
{
    public class DashboardController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public DashboardController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Administración
        public async Task<IActionResult> admin(DateTime FechaInicio, DateTime FechaFin)
        {


            DateTime date = DateTime.Now;
            bool isBandera = false;

            //Asi obtenemos el primer dia del mes actual
            if (FechaInicio.GetHashCode() == 0)
            {
                FechaInicio = date;
                isBandera = false;
            }
            else
            {
                isBandera = true;
            }
                
            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            if (FechaFin.GetHashCode() == 0)
            {
                FechaFin = date;
                isBandera = false;
            }
            else
            {
                isBandera = true;
            }


            //total de solicitudes
            ViewBag.Solicitudes = await _context.Solicitud.Where(s => s.SolicitudEstatus != "Cancelada").Where(s => s.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0).CountAsync();

            //total de Polizas
            ViewBag.Poliza = await _context.Poliza.Where(p => p.Creacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   p.Creacion.Date.CompareTo(FechaFin.Date) <= 0).CountAsync();

            //Venta total de polizas
            ViewBag.MontoGlobalVenta = await _context.Poliza
               .Include(p => p.FisicaMoral.Solicitud.CentroCostos)
               .Where(p =>
               p.Creacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                p.Creacion.Date.CompareTo(FechaFin.Date) <= 0)
               .SumAsync(p => p.FisicaMoral.Solicitud.CentroCostosId > 0 ? p.FisicaMoral.Solicitud.CentroCostos.CentroCostosMonto : p.FisicaMoral.Solicitud.CostoPoliza);

            //total de Soluciones
            ViewBag.Soluciones = await _context.Soluciones.Where(s => s.ProcesoSolucionesId != 5).CountAsync();

            if(isBandera)
            {
                List<RepresentantesCPCViewModel> rep = new List<RepresentantesCPCViewModel>();

                ViewData["RepreCantCosto"] = await _context.Poliza
                    .Include(p => p.FisicaMoral.Solicitud.CentroCostos)
                    .Include(p => p.FisicaMoral.Solicitud.Representante)
                    .Where(p => p.Creacion.Date.CompareTo(FechaInicio.Date) >= 0
                            && p.Creacion.Date.CompareTo(FechaFin.Date) <= 0
                            && p.FisicaMoral.Solicitud.Representante.AreaId == 3
                            && p.FisicaMoral.Solicitud.SolicitudEstatus != "Cancelado")
                    .Join(_context.Usuarios, us => us.FisicaMoral.Solicitud.Representanteid, usu => usu.UsuariosId, (us, usu) => new { us, usu })
                    .GroupBy(x => new
                    {
                        x.us.FisicaMoral.Solicitud.Representanteid,
                        x.usu.UsuarioNomCompleto
                    })
                    .Select(
                    s => new RepresentantesCPCViewModel
                    {
                        RepresentanteId = s.Key.Representanteid.Value,
                        Nombre = s.Key.UsuarioNomCompleto,
                        CantidadPoliza = s.Count(),
                        CostoVenta = s.Sum(x => x.us.FisicaMoral.Solicitud.CentroCostosId > 0 ? x.us.FisicaMoral.Solicitud.CentroCostos.CentroCostosMonto : Convert.ToDecimal(x.us.FisicaMoral.Solicitud.CostoPoliza))//(Decimal)_context.CentroCostos.SingleOrDefault(c => c.CentroCostosId == x.us.FisicaMoral.Solicitud.CentroCostosId).CentroCostosMonto : Convert.ToDecimal(x.us.FisicaMoral.Solicitud.CostoPoliza))
                }
                    ).OrderByDescending(o => o.CantidadPoliza)
                    .ToListAsync();

                //Cantidad de polizas por representacion
                ViewData["RepresentacionCantidad"] = await _context.Poliza
                   .Include(p => p.FisicaMoral.Solicitud.CentroCostos)
                   .Include(p => p.FisicaMoral.Solicitud.Representante)
                   .Where(p => p.Creacion.Date.CompareTo(FechaInicio.Date) >= 0
                           && p.Creacion.Date.CompareTo(FechaFin.Date) <= 0
                          )
                   .Join(_context.Usuarios, us => us.FisicaMoral.Solicitud.Representanteid, usu => usu.UsuariosId, (us, usu) => new { us, usu })
                   .GroupBy(x => new
                   {
                       x.us.FisicaMoral.Solicitud.Representante.RepresentacionId,
                       x.usu.Representacion.RepresentacionNombre
                   })
                   .Select(
                   s => new RepresentantesCPCViewModel
                   {
                       RepresentanteId = s.Key.RepresentacionId,
                       Nombre = s.Key.RepresentacionNombre,
                       CantidadPoliza = s.Count(),
                       CostoVenta = s.Sum(x => x.us.FisicaMoral.Solicitud.CentroCostosId > 0 ? x.us.FisicaMoral.Solicitud.CentroCostos.CentroCostosMonto : Convert.ToDecimal(x.us.FisicaMoral.Solicitud.CostoPoliza))//(Decimal)_context.CentroCostos.SingleOrDefault(c => c.CentroCostosId == x.us.FisicaMoral.Solicitud.CentroCostosId).CentroCostosMonto : Convert.ToDecimal(x.us.FisicaMoral.Solicitud.CostoPoliza))
               }
                   ).OrderByDescending(o => o.CantidadPoliza)
                   .ToListAsync();



                ViewData["CantidadFlujo"] = await _context.FlujoSolicitud.Include(u => u.Solicitud)
                              .Where(u => u.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                       u.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0)
                              .Join(_context.Usuarios, us => us.PersonaId, usu => usu.UsuariosId, (us, usu) => new { us, usu })
                              .GroupBy(x => new
                              {
                                  x.us.PersonaId,
                                  x.usu.UsuarioNomCompleto
                              })
                              .Select(s => new UsuarioCantidadViewModel
                              {
                                  Id = s.Key.PersonaId,
                                  UsuarioNombre = s.Key.UsuarioNomCompleto,
                                  cantidad = s.Count()
                              }).ToListAsync();

                ViewData["CantidadSolicutdes"] = await _context.UsuariosSolicitud.Include(u => u.Solicitud)
                              .Where(u => u.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                       u.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0)
                              .Join(_context.Usuarios, us => us.UsuariosId, usu => usu.UsuariosId, (us, usu) => new { us, usu })
                              .GroupBy(x => new
                              {
                                  x.us.UsuariosId,
                                  x.usu.UsuarioNomCompleto
                              })
                              .Select(s => new UsuarioCantidadViewModel
                              {
                                  Id = s.Key.UsuariosId,
                                  UsuarioNombre = s.Key.UsuarioNomCompleto,
                                  cantidad = s.Count()
                              }).ToListAsync();


                ViewBag.FiltroFecha = "Fecha de: " + FechaInicio.ToShortDateString() + " Al " + FechaFin.ToShortDateString();
            }
            else
            {
                ViewData["RepreCantCosto"] = new List<RepresentantesCPCViewModel>();
                ViewData["RepresentacionCantidad"] = new List<RepresentantesCPCViewModel>();
                ViewData["CantidadFlujo"] = new List<UsuarioCantidadViewModel>();
                ViewData["CantidadSolicutdes"] = new List<UsuarioCantidadViewModel>();
            }
            
            return View();

        }

        // GET: generico
        public async Task<IActionResult> generico()
        {

            return View();

        }

        // GET: generico
        public async Task<IActionResult> representate(DateTime FechaInicio, DateTime FechaFin)
        {
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            if (FechaInicio.GetHashCode() == 0)
                FechaInicio = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            if (FechaFin.GetHashCode() == 0)
                FechaFin = FechaInicio.AddMonths(1).AddDays(-1);

            //total de solicitudes
            ViewBag.Solicitudes = await _context.Solicitud.Where(s => s.SolicitudEstatus != "Cancelada").Where(s => s.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0 && s.Representanteid == usuarioid).CountAsync();

            //total de Polizas
            ViewBag.Poliza = await _context.Poliza
                .Include(p => p.FisicaMoral.Solicitud)
                .Where(p => p.Creacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   p.Creacion.Date.CompareTo(FechaFin.Date) <= 0 && p.FisicaMoral.Solicitud.Representanteid == usuarioid).CountAsync();

            //Venta total de polizas
            ViewBag.MontoTotalVentas = await _context.Poliza
                .Where(p => p.FisicaMoral.Solicitud.Representanteid == usuarioid &&
                p.Creacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                 p.Creacion.Date.CompareTo(FechaFin.Date) <= 0)
                .SumAsync(p => p.FisicaMoral.Solicitud.CentroCostos != null ? _context.CentroCostos.SingleOrDefault(c => c.CentroCostosId == p.FisicaMoral.Solicitud.CentroCostosId).CentroCostosMonto : p.FisicaMoral.Solicitud.CostoPoliza);

            //Cantidad de solicitudes por representación y suma de montos
            ViewData["RepreCantCosto"] = await _context.Poliza
                //.Include(p => p.FisicaMoral.Solicitud.CentroCostos)
                .Include(p => p.FisicaMoral.Solicitud.Representante)
                .Where(p => p.Creacion.Date.CompareTo(FechaInicio.Date) >= 0
                        && p.Creacion.Date.CompareTo(FechaFin.Date) <= 0
                        && p.FisicaMoral.Solicitud.Representante.AreaId == 3
                        && p.FisicaMoral.Solicitud.SolicitudEstatus != "Cancelado")
                .Join(_context.Usuarios, us => us.FisicaMoral.Solicitud.Representanteid, usu => usu.UsuariosId, (us, usu) => new { us, usu })
                .GroupBy(x => new
                {
                    x.us.FisicaMoral.Solicitud.Representanteid,
                    x.usu.UsuarioNomCompleto
                })
                .Select(
                s => new RepresentantesCPCViewModel
                {
                    RepresentanteId = s.Key.Representanteid.Value,
                    Nombre = s.Key.UsuarioNomCompleto,
                    CantidadPoliza = s.Count()
                }
                ).OrderByDescending(o => o.CantidadPoliza)
                .ToListAsync();
            return View();

        }
        // GET: generico
        public async Task<IActionResult> documentacion(DateTime FechaInicio, DateTime FechaFin)
        {
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            if (FechaInicio.GetHashCode() == 0)
                FechaInicio = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            if (FechaFin.GetHashCode() == 0)
                FechaFin = FechaInicio.AddMonths(1).AddDays(-1);

            ViewData["CantidadFlujo"] = _context.FlujoSolicitud.Include(u => u.Solicitud)
                          .Where(u => u.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   u.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0)
                          .Join(_context.Usuarios, us => us.PersonaId, usu => usu.UsuariosId, (us, usu) => new { us, usu })
                          .GroupBy(x => new
                          {
                              x.us.PersonaId,
                              x.usu.UsuarioNomCompleto
                          })
                          .Select(s => new UsuarioCantidadViewModel
                          {
                              Id = s.Key.PersonaId,
                              UsuarioNombre = s.Key.UsuarioNomCompleto,
                              cantidad = s.Count()
                          }).ToList();

            ViewData["CantidadSolicutdes"] = _context.UsuariosSolicitud.Include(u => u.Solicitud)
                          .Where(u => u.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   u.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0)
                          .Join(_context.Usuarios, us => us.UsuariosId, usu => usu.UsuariosId, (us, usu) => new { us, usu })
                          .GroupBy(x => new
                          {
                              x.us.UsuariosId,
                              x.usu.UsuarioNomCompleto
                          })
                          .Select(s => new UsuarioCantidadViewModel
                          {
                              Id = s.Key.UsuariosId,
                              UsuarioNombre = s.Key.UsuarioNomCompleto,
                              cantidad = s.Count()
                          }).ToList();

            ViewData["Solicitudes"] = _context.FlujoSolicitud
                .Include(f => f.Solicitud)
                .Include(f => f.Persona)
                .Include(f => f.Solicitud.Representante)
                .Where(f =>
                (f.Solicitud.SolicitudEstatus != "Concluida" || f.Solicitud.SolicitudEstatus != "Cancelada")
                && (f.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                   f.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0)
                )
                .Select(s => new FlujoSolicitudesViewModel
                {
                    Id = s.PersonaId,
                    NombreCompleto = s.Persona.UsuarioNomCompleto,
                    SolicitudId = s.SolicitudId,
                    TipoPoliza = s.Solicitud.SolicitudTipoPoliza,
                    Representante = s.Solicitud.Representante.UsuarioNomCompleto,
                    Direccion = s.Solicitud.SolicitudUbicacionArrendado + " Colonia: " + s.Solicitud.ColoniaArrendar + " Alcaldía o Municipio: " + s.Solicitud.AlcaldiaMunicipioArrendar,
                    Estatus = s.Solicitud.SolicitudEstatus

                }).ToList();

            return View();

        }

        public async Task<IActionResult> controlcalidad(DateTime FechaInicio, DateTime FechaFin)
        {
            DateTime date = DateTime.Now;
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);

            //Asi obtenemos el primer dia del mes actual
            if (FechaInicio.GetHashCode() == 0)
                FechaInicio = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            if (FechaFin.GetHashCode() == 0)
                FechaFin = FechaInicio.AddMonths(1).AddDays(-1);


            ViewData["Solicitudes"] = _context.FlujoSolicitud
                .Include(f => f.Solicitud)
                .Include(f => f.Persona)
                .Include(f => f.Solicitud.Representante)
                .Where(f => f.PersonaId == usuarioid &&
                (f.Solicitud.SolicitudEstatus != "Concluida" || f.Solicitud.SolicitudEstatus != "Cancelada")
                && (f.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                   f.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0)
                )
                .Select(s => new FlujoSolicitudesViewModel
                {
                    Id = s.PersonaId,
                    NombreCompleto = s.Persona.UsuarioNomCompleto,
                    SolicitudId = s.SolicitudId,
                    TipoPoliza = s.Solicitud.SolicitudTipoPoliza,
                    Representante = s.Solicitud.Representante.UsuarioNomCompleto,
                    Direccion = s.Solicitud.SolicitudUbicacionArrendado + " Colonia: " + s.Solicitud.ColoniaArrendar + " Alcaldía o Municipio: " + s.Solicitud.AlcaldiaMunicipioArrendar,
                    Estatus = s.Solicitud.SolicitudEstatus

                }).ToList();

            return View();

        }

    }
}
