using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using PolizaJuridica.Data;
using PolizaJuridica.ViewModels;

namespace PolizaJuridica.Controllers
{
    public class ReportesController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public ReportesController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ventas(DateTime FechaInicio, DateTime FechaFin, DateTime FechaInicioPoliza, DateTime FechaFinPoliza, int IdSolicitudInicio, int IdSolicitudFin, int IdPolizaInicio, int IdPolizafin)
        {
            //var usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            //var area = User.FindFirst("Area").Value;
            List<ReporteVentasViewModel> exceles = new List<ReporteVentasViewModel>();
            List<Poliza> excel = new List<Poliza>();


            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Polizas");

                if (FechaInicio.GetHashCode() != 0 && FechaFin.GetHashCode() != 0)
                {
                    excel = _context.Poliza.Include(p => p.FisicaMoral.Solicitud.Representante.Representacion).Where(s => s.FisicaMoral.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                       s.FisicaMoral.Solicitud.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0).ToList();
                }

                if (FechaInicioPoliza.GetHashCode() != 0 && FechaFinPoliza.GetHashCode() != 0)
                {
                    excel = _context.Poliza.Include(p => p.FisicaMoral.Solicitud.Representante.Representacion).Where(s => s.Creacion.Date.CompareTo(FechaInicioPoliza.Date) >= 0 &&
                       s.Creacion.Date.CompareTo(FechaFinPoliza.Date) <= 0).ToList();
                }

                if (IdSolicitudInicio > 0 && IdSolicitudFin > 0)
                {
                    excel = _context.Poliza.Include(p => p.FisicaMoral.Solicitud.Representante.Representacion).Where(s => s.FisicaMoral.SolicitudId >= IdSolicitudInicio && s.FisicaMoral.SolicitudId <= IdSolicitudFin).ToList();
                }

                if (IdPolizaInicio > 0 && IdPolizafin > 0)
                {
                    excel = _context.Poliza.Include(p => p.FisicaMoral.Solicitud.Representante.Representacion).Where(s => s.PolizaId >= IdPolizaInicio && s.PolizaId <= IdPolizafin).ToList();
                }

                exceles = ReporteVentas(excel);

                worksheet.Cells.LoadFromCollection(exceles, true);

                package.Workbook.Calculate();
                var excelData = package.GetAsByteArray();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "Polizas.xlsx";
                return File(excelData, contentType, fileName);
            }
        }

        private List<ReporteVentasViewModel> ReporteVentas(List<Poliza> poliza)
        {
            List<ReporteVentasViewModel> rvv = new List<ReporteVentasViewModel>();
            foreach (var p in poliza)
            {

                try
                {

                    ReporteVentasViewModel rv = new ReporteVentasViewModel();
                    StructMovimiento sm = MovimientoMaestro(p.PolizaId);
                    rv.SolicitudId = p.FisicaMoral.SolicitudId;
                    var usuarioSolicitud = _context.UsuariosSolicitud.Include(us => us.Usuarios).Where(us => us.SolicitudId == rv.SolicitudId).ToList();
                    rv.PolizaId = p.PolizaId;
                    rv.Representacion = p.FisicaMoral.Solicitud.Representante == null ? "" : p.FisicaMoral.Solicitud.Representante.Representacion.RepresentacionNombre;
                    rv.Ejecutivo = p.FisicaMoral.Solicitud.Representante == null ? "" : p.FisicaMoral.Solicitud.Representante.UsuarioNomCompleto;
                    rv.Ubicacion = p.FisicaMoral.Solicitud.SolicitudUbicacionArrendado;
                    rv.Renovacion = p.FisicaMoral.Solicitud.IsRenovacion.ToString();
                    rv.PrecioPoliza = _context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 2).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault() == null ? 0 : (Decimal)_context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 2).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault().Importe;//p.FisicaMoral.Solicitud.CentroCostosId == null ? (Decimal)p.FisicaMoral.Solicitud.CostoPoliza : _context.CentroCostos.SingleOrDefault(c => c.CentroCostosId == p.FisicaMoral.Solicitud.CentroCostosId).CentroCostosMonto;
                    rv.PrecioSinIVA = rv.PrecioPoliza / Convert.ToDecimal(1.16);
                    rv.IVA = rv.PrecioPoliza - rv.PrecioSinIVA;
                    rv.IVAConcepto = _context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 13).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault() == null ? 0 : (Decimal)_context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 13).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault().Importe;
                    rv.Descuento = _context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 18).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault() == null ? 0 : (Decimal)_context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 18).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault().Importe;
                    rv.ImporteCobrado = TotalImporteCobrado(p.PolizaId, rv.PrecioPoliza, rv.IVAConcepto, rv.Descuento);
                    rv.ComisionAsesor = _context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 4).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault() == null ? 0 : (Decimal)_context.DetallePoliza.Where(po => p.PolizaId == po.PolizaId && po.CategoriaEsid == 4).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault().Importe;
                    rv.Anticipo = _context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 17).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault() == null ? 0 : (Decimal)_context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 17).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault().Importe;
                    rv.IngresoRepresentacion = _context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 3).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault() == null ? 0 : (Decimal)_context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 3).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault().Importe; ;
                    rv.Regalias = sm.Entrada > 0 ? ImporteCobrado(p.PolizaId) : 0;
                    rv.ComisionEjecutivo = _context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 7).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault() == null ? 0 : (Decimal)_context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 7).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault().Importe;
                    rv.Firma = _context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 9).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault() == null ? 0 : (Decimal)_context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 9).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault().Importe;
                    rv.Translados = _context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 6).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault() == null ? 0 : (Decimal)_context.DetallePoliza.Where(po => po.PolizaId == p.PolizaId && po.CategoriaEsid == 6).OrderByDescending(or => or.DetallePolizaId).FirstOrDefault().Importe;
                    rv.OtrosOperativos = OtrosMovimientos(p.PolizaId);
                    rv.ReciboId = sm.ReciboId;
                    rv.FechaCobro = sm.FechaConcluido.ToString() == "1/1/0001 12:00:00 AM" ? "" : sm.FechaConcluido.ToString();
                    rv.Asesor = Asesor(p.FisicaMoral.Solicitud);
                    rv.Capturada =  usuarioSolicitud.FirstOrDefault(us => us.Usuarios.AreaId == 10) == null ? "": usuarioSolicitud.FirstOrDefault(us => us.Usuarios.AreaId == 10).Usuarios.UsuarioNomCompleto;
                    rv.Investigada = usuarioSolicitud.FirstOrDefault(us => us.Usuarios.AreaId == 11) == null ? "": usuarioSolicitud.FirstOrDefault(us => us.Usuarios.AreaId == 11).Usuarios.UsuarioNomCompleto;
                    rv.Abogada = usuarioSolicitud.FirstOrDefault(us => us.Usuarios.AreaId == 12) == null ? "" : usuarioSolicitud.FirstOrDefault(us => us.Usuarios.AreaId == 12).Usuarios.UsuarioNomCompleto;
                    rv.EstatusPoliza = _context.UsuarioPoliza.Include(po => po.TipoProcesoPo).Where(s => s.PolizaId == p.PolizaId).OrderByDescending(o => o.UsuarioPolizaId).FirstOrDefault().TipoProcesoPo.Descripcion;
                    rv.EstatusSolicitud = _context.UsuariosSolicitud.Include(us => us.TipoProceso).Where(us => us.SolicitudId == p.FisicaMoral.SolicitudId).OrderByDescending(us => us.UsuariosSolicitudId).FirstOrDefault().TipoProceso.Descripcion;
                    Console.WriteLine(rv.PolizaId.ToString());
                    rvv.Add(rv);

                }
                catch (Exception e)
                {
                    Log log = new Log()
                    {
                        LogObjetoIn = "",
                        LogFecha = DateTime.Now,
                        LogPantalla = "venta",
                        LogProceso = "Reportes",
                        LogObjetoOut = e.ToString() + " - PolizaId: "+ p.PolizaId.ToString()

                    };
                    _context.Add(log);
                    _context.SaveChanges();
                    ViewBag.Error = "Ups... algo salió mal";
                }
            }

            return rvv;
        }



        private string Asesor(Solicitud s)
        {
            string NombreAsesor = String.Empty;
            if (s.SolicitudAdmiInmueble == true)
                NombreAsesor = "Propietario";

            if (s.SolicitudAdmiInmueble == false && s.Asesorid > 0)
                NombreAsesor = _context.Usuarios.SingleOrDefault(u => u.UsuariosId == s.Asesorid).UsuarioNomCompleto;

            if (s.SolicitudAdmiInmueble == false && s.Asesorid == null)
                NombreAsesor = "Sin Asesor";

            return NombreAsesor;
        }

        private StructMovimiento MovimientoMaestro(int polizaId)
        {
            StructMovimiento sm = new StructMovimiento();

            var mom = _context.MovimientoMaestro
                        .Join(_context.Movimientos, mm => mm.Id, mov => mov.Mmid, (mm, mov) => new { mm, mov })
                        .Join(_context.DetallePoliza, mmov => mmov.mov.DetallePolizaId, dp => dp.DetallePolizaId, (mmov, dp) => new { mmov, dp }).Where(maestro => maestro.mmov.mov.DetallePoliza.PolizaId == polizaId).FirstOrDefault();
            if (mom != null)
            {
                sm.ReciboId = mom.mmov.mm.Id;
                sm.FechaConcluido = (DateTime)mom.mmov.mm.Fecha;
                sm.Entrada = (Decimal)mom.mmov.mm.Entrada;
            }

            return sm;
        }

        private decimal OtrosMovimientos(int polizaId)
        {
            decimal Monto = 0;
            List<int> categoriasId = new List<int>();
            categoriasId.Add(13);//IVA Concepto
            categoriasId.Add(18);//DEscuento
            categoriasId.Add(4);//Comision del asesor
            categoriasId.Add(3);//Representacion 7,9 6
            categoriasId.Add(7);//Ejecutivo
            categoriasId.Add(9);//Firma
            categoriasId.Add(6);//Traslados
            categoriasId.Add(17);//Anticipo
            var conceptos = _context.DetallePoliza.Include(c => c.CategoriaEs).Where(dp => dp.PolizaId == polizaId
            && !categoriasId.Contains(dp.CategoriaEsid)
            && dp.CategoriaEs.TipoEs == 2).Select(se => new MontoClase { Monto = (decimal)se.Importe }).ToList();

            foreach (var i in conceptos)
            {
                Monto += i.Monto;
            }

            return Monto;
        }
        private decimal ImporteCobrado(int polizaId)
        {
            decimal Monto = 0;

            var conceptos = _context.DetallePoliza.Include(c => c.CategoriaEs).Where(dp => dp.PolizaId == polizaId).ToList();

            foreach (var i in conceptos)
            {
                if (i.CategoriaEs.TipoEs == 1)
                {
                    Monto += (Decimal)i.Importe;
                }
                if (i.CategoriaEs.TipoEs == 2)
                {
                    Monto -= (Decimal)i.Importe;
                }
            }

            return Monto;
        }

        private decimal TotalImporteCobrado(int polizaId, decimal PrecioPoliza, decimal IVAConcepto, decimal Descuento)
        {
            decimal Monto = 0;
            List<int> estatus = new List<int>();
            estatus.Add(4);
            estatus.Add(7);
            estatus.Add(2);
            var cantidad = _context.UsuarioPoliza.Where(up => up.PolizaId == polizaId && estatus.Contains(up.TipoProcesoPoId)).Count();

            if (cantidad > 0)
                Monto = PrecioPoliza - (IVAConcepto + Descuento);

            return Monto;
        }

        public class StructMovimiento
        {
            public decimal ImporteNetoCobrado { get; set; }
            public decimal Entrada { get; set; }
            public DateTime FechaConcluido { get; set; }
            public int ReciboId { get; set; }
        }

        public class MontoClase
        {
            public decimal Monto { get; set; }
        }
    }
}

