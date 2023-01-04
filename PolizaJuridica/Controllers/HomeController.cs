using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PolizaJuridica.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PolizaJuridica.Models;
using PolizaJuridica.ViewModels;
using System;
using System.IO;
using OfficeOpenXml;
using System.Net;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;

namespace PolizaJuridica.Controllers
{
    public class HomeController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public HomeController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            //DateTime FechaInicio;
            //DateTime FechaFin;
            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime FechaInicio = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            DateTime FechaFin = FechaInicio.AddMonths(1).AddDays(-1);

            ViewBag.FiltroFecha = "Filtro de: " + FechaInicio.ToShortDateString() + " Al " + FechaFin.ToShortDateString();

            ViewBag.Recibidas = _context.Solicitud.Where(s => s.SolicitudEstatus == "Recibida").Where(s => s.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.TotalSolicitudes = _context.Solicitud.Where(s => s.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                          s.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.Canceladas = _context.Solicitud.Where(s => s.SolicitudEstatus == "Cancelada").Where(s => s.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.AtencionClientes = _context.Solicitud.Where(s => s.SolicitudEstatus == "Atención a Clientes").Where(s => s.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.Procesos = _context.Solicitud.Where(s => s.SolicitudEstatus == "Procesos").Where(s => s.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.SolicitudConcluida = _context.Solicitud.Where(s => s.SolicitudEstatus == "Concluida").Where(s => s.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.Polizas = _context.Poliza.Where(p => p.Creacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   p.Creacion.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.TotalSoluciones = _context.Soluciones.Where(s => s.FechaCreacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.FechaCreacion.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.NuevasSoluciones = _context.Soluciones.Where(s => s.ProcesoSolucionesId == 1).Where(s => s.FechaCreacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.FechaCreacion.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.ExtraJudicial = _context.Soluciones.Where(s => s.ProcesoSolucionesId == 2).Where(s => s.FechaCreacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.FechaCreacion.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.Judicial = _context.Soluciones.Where(s => s.ProcesoSolucionesId == 3).Where(s => s.FechaCreacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.FechaCreacion.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.SolucionesConcluidas = _context.Soluciones.Where(s => s.ProcesoSolucionesId == 4).Where(s => s.FechaCreacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.FechaCreacion.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.SolucionesCanceladas = _context.Soluciones.Where(s => s.ProcesoSolucionesId == 5).Where(s => s.FechaCreacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.FechaCreacion.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewData["Cuentas"] = _context.CuentasBancarias.Where(c => c.Estatus == 1 ).ToList();//donde las cuentas esten activas
            //Cantidad de Polizas por usuario
            //ViewBag.CPU = _context.UsuariosSolicitud.Include(u => u.Usuarios)
            //    
            //    .GroupBy(u => u.UsuariosId).Join(Usuarios, u => u.Key, us => us.Usa)
            //    .Select(i => new  { Id = i., cantidad = i.Count() }).ToList();

            ViewData["CPU"] = _context.UsuariosSolicitud.Include( u => u.Solicitud)
                          .Where(u => u.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   u.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0)
                          .Join(_context.Usuarios, us => us.UsuariosId, usu => usu.UsuariosId,(us,usu) => new {us ,usu })
                          .Where(w => w.usu.AreaId == 12)
                          .GroupBy(x => new {
                              x.us.UsuariosId,
                              x.usu.UsuarioNomCompleto
                          })
                          .Select( s => new UsuarioCantidadViewModel{ 
                              Id = s.Key.UsuariosId,
                              UsuarioNombre = s.Key.UsuarioNomCompleto,
                              cantidad = s.Count()
                          }).ToList();

            //ViewData["RepreCantCosto"] = _context.Poliza.Include(p => p.FisicaMoral.Solicitud.Representante).Include(p => p.FisicaMoral.Solicitud.CentroCostos)
            //              .Where(p => p.Creacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
            //                                      p.Creacion.Date.Date.CompareTo(FechaFin.Date) <= 0)
            //              .Where(p => p.FisicaMoral.Solicitud.Representante.AreaId == 3)
            //              .GroupBy(x => x.FisicaMoral.Solicitud.Representanteid)
            //              .Select(p => new RepresentantesCPCViewModel
            //              {
            //                  RepresentanteId = p.,
            //                  Nombre = s.Key.UsuarioNomCompleto,
            //                  CantidadPoliza = s.Count(),
            //                  CostoVenta = s.Sum(s.Key.)
            //              }).ToList();


            return View(new FiltrosViewModel());
        }




        [HttpPost]
        public ActionResult Index(DateTime FechaInicio, DateTime FechaFin)
        {
            ViewBag.FiltroFecha = "Filtro de: " + FechaInicio.Date.ToString() + " Al " + FechaFin.Date.ToString();

            ViewBag.TotalSolicitudes = _context.Solicitud.Where(s => s.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.Canceladas = _context.Solicitud.Where(s => s.SolicitudEstatus == "Cancelada").Where(s => s.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.AtencionClientes = _context.Solicitud.Where(s => s.SolicitudEstatus == "Recibida").Where(s => s.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.Procesos = _context.Solicitud.Where(s => s.SolicitudEstatus == "Procesos").Where(s => s.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.SolicitudConcluida = _context.Solicitud.Where(s => s.SolicitudEstatus == "Concluida").Where(s => s.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.Polizas = _context.Poliza.Where(p => p.Creacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   p.Creacion.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.TotalSoluciones = _context.Soluciones.Where(s => s.FechaCreacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.FechaCreacion.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.NuevasSoluciones = _context.Soluciones.Where(s => s.ProcesoSolucionesId == 1).Where(s => s.FechaCreacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.FechaCreacion.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.ExtraJudicial = _context.Soluciones.Where(s => s.ProcesoSolucionesId == 2).Where(s => s.FechaCreacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.FechaCreacion.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.Judicial = _context.Soluciones.Where(s => s.ProcesoSolucionesId == 3).Where(s => s.FechaCreacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.FechaCreacion.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.SolucionesConcluidas = _context.Soluciones.Where(s => s.ProcesoSolucionesId == 4).Where(s => s.FechaCreacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.FechaCreacion.Date.CompareTo(FechaFin.Date) <= 0).Count();
            ViewBag.SolucionesCanceladas = _context.Soluciones.Where(s => s.ProcesoSolucionesId == 5).Where(s => s.FechaCreacion.Date.CompareTo(FechaInicio.Date) >= 0 &&
                                                   s.FechaCreacion.Date.CompareTo(FechaFin.Date) <= 0).Count();
            return View();
        }


        public IActionResult About()//este metodo lo utilizaremos para hacer pruebas
       {
            ViewData["Message"] = "Your application description page.";
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Test");
                worksheet.Cells["A1"].Value = 1;
                worksheet.Cells["A2"].Value = 2;
                worksheet.Cells["A3"].Formula = "SUM(A1:A2)";
                package.Workbook.Calculate();
                var result = worksheet.Cells["A3"].Value; // result = 3
                worksheet = package.Workbook.Worksheets.Add("Test2");
                worksheet.Cells["A1"].Value = 3;
                worksheet.Cells["A2"].Value = 4;
                worksheet.Cells["A3"].Formula = "SUM(A1:A2)";
                package.Workbook.Calculate();
                result = worksheet.Cells["A3"].Value; // result = 7
                var excelData = package.GetAsByteArray();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "MyWorkbook.xlsx";
                return File(excelData, contentType, fileName);
            }

        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ActionResult WS()
        {
            string url = "http://dummy.restapiexample.com/api/v1/employees";
            var json = new WebClient().DownloadString(url);
            dynamic m = JsonConvert.DeserializeObject(json);
            return View(json);
        }

    }
}
