using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Rewrite.Internal.UrlMatches;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PolizaJuridica.Data;
using PolizaJuridica.Models;
using PolizaJuridica.Utilerias;
using PolizaJuridica.ViewModels;


namespace PolizaJuridica.Controllers
{
    public class SolicitudsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;
        private UsuariosSolicitud usuariosSolicitud;

        public SolicitudsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Solicituds
        public async Task<IActionResult> Index(int? TipoprocesoId, DateTime FechaInicio, DateTime FechaFin, string IsRenovaciones)
        {
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            ViewBag.TipoProceso = new SelectList(_context.TipoProceso, "TipoProcesoId", "Descripcion");
            Usuarios usuario = _context.Usuarios.Include(u => u.Area).SingleOrDefault(u => u.UsuariosId == usuarioid);
            String tipoproceso = String.Empty;
            List<SolicitudIndexViewModel> sol = new List<SolicitudIndexViewModel>();

            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            if (FechaInicio.GetHashCode() == 0)
                FechaInicio = new DateTime(date.Year, date.Month, 1);
            FechaInicio.AddMonths(-1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            if (FechaFin.GetHashCode() == 0)
                FechaFin = FechaInicio.AddMonths(1).AddDays(-1);

            if (TipoprocesoId > 0 || TipoprocesoId != null)
            {
                tipoproceso = _context.TipoProceso.SingleOrDefault(t => t.TipoProcesoId == TipoprocesoId).Descripcion;
                ViewBag.Tipoproceso = tipoproceso;
            }

            sol = await _context.Solicitud
                .Include(s => s.Creador)
                .Include(s => s.Representante)
                .Include(s => s.Asesor)
                .Include(s => s.FisicaMoral)
                .Where(s => s.SolicitudFechaSolicitud.Date.CompareTo(FechaInicio.Date) >= 0 &&
                   s.SolicitudFechaSolicitud.Date.CompareTo(FechaFin.Date) <= 0
                   ).Select(x => new SolicitudIndexViewModel
                   {
                       SolicitudId = (int)x.SolicitudId,
                       SolicitudTipoPoliza = x.SolicitudTipoPoliza,
                       SolicitudNombreProp = x.SolicitudNombreProp,
                       SolicitudApePaternoProp = x.SolicitudApePaternoProp,
                       SolicitudApeMaternoProp = x.SolicitudApeMaternoProp,
                       SolicitudRazonSocial = x.SolicitudRazonSocial,
                       SolicitudUbicacionArrendado = x.SolicitudUbicacionArrendado,
                       SolicitudFechaSolicitud = x.SolicitudFechaSolicitud,
                       CreadorNombreCompleto = x.Creador.UsuarioNomCompleto,//x.Creadorid > 0 ? _context.Usuarios.SingleOrDefault(u => u.UsuariosId == x.Creadorid).UsuarioNomCompleto : "",
                       RepresentanteNombreCompleto = x.Representante.UsuarioNomCompleto,//x.Representanteid > 0 ? _context.Usuarios.SingleOrDefault(u => u.UsuariosId == x.Representanteid).UsuarioNomCompleto : "",
                       AsesorNombreCompleto = x.Asesor.UsuarioNomCompleto,//x.Asesorid > 0 ? _context.Usuarios.SingleOrDefault(u => u.UsuariosId == x.Asesorid).UsuarioNomCompleto : "",
                       SolicitudEstatus = x.SolicitudEstatus,
                       IsRenovacion = x.IsRenovacion,
                       Representanteid = x.Representanteid,
                       Asesorid = x.Asesorid,
                       Creadorid = x.Creadorid,
                       SolicitudTipoRegimen = x.SolicitudTipoRegimen,
                       FisicaMoralId = x.FisicaMoral.Count <= 0 ? 0 : x.FisicaMoral.FirstOrDefault().FisicaMoralId,
                       RepresentacionId = x.Representante == null ? 0: x.Representante.RepresentacionId
                   }
                   ).ToListAsync();

            if (TipoprocesoId > 0 || TipoprocesoId != null)
                sol = sol.Where(s => s.SolicitudEstatus == tipoproceso).ToList();

            if (usuario.Area.AreaDescripcion == "Asesor")
                sol = sol.Where(s => s.Creadorid == usuarioid).ToList();

            if (usuario.Area.AreaDescripcion == "Representante")
                sol = sol.Where(s => s.RepresentacionId == usuario.RepresentacionId).ToList();

            if (IsRenovaciones == "on")
                sol = sol.Where(s => s.IsRenovacion == true).ToList();

            ViewBag.FiltroFecha = "Fecha de: " + FechaInicio.ToShortDateString() + " Al " + FechaFin.ToShortDateString();
            return View(sol);

        }

        // GET: Solicituds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud
                .Include(s => s.CentroCostos)
                .Include(s => s.Estados)
                .Include(s => s.TipoInmobiliario)
                .FirstOrDefaultAsync(m => m.SolicitudId == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // GET: Solicituds/Create
        public IActionResult Create(int? id)
        {
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            string area = User.FindFirst("Area").Value;
            ViewBag.RepresentanteId = usuarioid;
            ViewData["EstadosId"] = new SelectList(_context.Estados, "EstadosId", "EstadoNombre");
            ViewData["TipoInmobiliarioId"] = new SelectList(_context.TipoInmobiliario, "TipoInmobiliarioId", "TipoInmobiliarioDesc");
            ViewBag.Area = User.FindFirst("Area").Value;
            ViewData["Representante"] = _context.Usuarios.Include(u => u.Representacion).Include(u => u.Area).Where(u => u.Area.AreaDescripcion == "Representante" && u.Activo == true).ToList();
            if (area == "Representante")
            {
                ViewData["Asesor"] = _context.Usuarios.Include(u => u.Representacion).Include(u => u.Area).Where(u => u.Area.AreaDescripcion == "Asesor").Where(u => u.UsuarioPadreId == usuarioid).ToList();
            }
            else
            {
                ViewData["Asesor"] = _context.Usuarios.Include(u => u.Representacion).Include(u => u.Area).Where(u => u.Area.AreaDescripcion == "Asesor").ToList();
            }

            if (id >= 1)
            {
                var solicitud = _context.Solicitud.FirstOrDefault(s => s.SolicitudId == id);
                return View(solicitud);
            }
            return View();
        }

        // POST: Solicituds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SolicitudId,SolicitudTipoPoliza,Inmobiliaria,SolicitudPersonaSolicita,SolicitudTelefono,SolicitudCelular,SolicitudEmail,SolicitudFechaSolicitud,SolicitudFechaFirma,SolicitudHoraFirma,SolicitudLugarFirma,SolicitudAdmiInmueble,SolicitudEsAdminInmueble,SolicitudRecibodePago,SolicitudNombreProp,SolicitudApePaternoProp,SolicitudApeMaternoProp,SolicitudNacionalidad,SolicitudRazonSocial,SolicitudRfc,SolicitudRepresentanteLegal,SolicitudApePaternoLegal,SolicitudApeMaternoLegal,SolicitudDomicilioProp,SolicitudTelefonoProp,SolicitudCelularProp,SolicitudEmailProp,SolicitudTipoDeposito,SolicitudNombreCuenta,SolicitudBanco,SolicitudCuenta,SolicitudClabe,SolicitudUbicacionArrendado,SolicitudTelefonoInmueble,SolicitudNumero,SolicitudImporteMensual,SolicitudCuotaMant,SolicitudIncluidaRenta,SolicitudDepositoGarantia,SolicitudVigenciaContratoI,SolicitudVigenciaContratoF,SolicitudPagare,SolicitudDestinoArrendamien,SolicitudObservaciones,SolicitudEstatus,SolicitudTipoRegimen,CentroCostosId,EstadosId,SolicitudInmuebleGaran,SolicitudFiador,SolicitudCartaEntrega,TipoArrendatario,ArrendatarioNombre,ArrendatarioApePat,ArrendatarioApeMat,ArrendatarioTelefono,ArrendatarioCorreo,TipoInmobiliarioId,Representanteid,RepresentanteNombre,Asesorid,AsesorNombre,Creadorid,CreadorNombre,ColoniaActual,AlcaldiaMunicipioActual,CodigoPostalActual,EstadoActual,ColoniaArrendar,AlcaldiaMunicipioArrendar,CodigoPostalArrendar,EstadoArrendar,TipoIdentificacion,NumeroIdentificacion,Nombre1 ,ApePat1 ,ApeMat1 ,TipoIdent1 ,NumIdent1 ,Nombre2 ,ApePat2 ,ApeMat2 ,TipoIdent2 ,NumIdent2 ,Nombre3 ,ApePat3 ,ApeMat3 ,TipoIdent3 ,NumIdent3,NumRppcons,EscrituraNumero,Licenciado,NumeroNotaria,FechaRppcons,NumEscPoder,TitularNotaPoder,NumNotaria,FechaEmitePoder,FechaConstitutiva,IsRenovacion,EsAmueblado")] Solicitud solicitud)
        {
            bool isError = false;
            bool isMatch = false;
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            var SistemaId = Int32.Parse(User.FindFirst("SistemaId").Value);
            string area = User.FindFirst("Area").Value;
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();

            //if (solicitud.SolicitudTipoRegimen == 0)
            //{
            //    Error.Add(Mensajes.MensajesError("Debe seleccionar el tipo de Régimen para el Arrendador"));
            //    isError = true;
            //}
            //if (solicitud.SolicitudTipoRegimen == 1)
            //{
            //    if (solicitud.SolicitudNombreProp == null)
            //    {
            //        Error.Add(Mensajes.ErroresAtributos("SolicitudNombreProp"));
            //        isError = true;
            //    }
            //    if (solicitud.SolicitudApePaternoProp == null)
            //    {
            //        Error.Add(Mensajes.ErroresAtributos("SolicitudApePaternoProp"));
            //        isError = true;
            //    }
            //}
            //if (solicitud.SolicitudTipoRegimen == 2)
            //{
            //    if (solicitud.SolicitudRazonSocial == null)
            //    {
            //        Error.Add(Mensajes.ErroresAtributos("SolicitudRazonSocial"));
            //        isError = true;
            //    }
            //    if (solicitud.SolicitudRfc == null)
            //    {
            //        Error.Add(Mensajes.ErroresAtributos("SolicitudRfc"));
            //        isError = true;
            //    }
            //    if (solicitud.SolicitudRepresentanteLegal == null)
            //    {
            //        Error.Add(Mensajes.ErroresAtributos("SolicitudRepresentanteLegal"));
            //        isError = true;
            //    }
            //    if (solicitud.SolicitudApePaternoLegal == null)
            //    {
            //        Error.Add(Mensajes.ErroresAtributos("SolicitudApePaternoLegal"));
            //        isError = true;
            //    }
            //}
            //if (solicitud.SolicitudCelularProp == null)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("SolicitudCelularProp"));
            //    isError = true;
            //}
            if (solicitud.EstadosId == 0)
            {
                Error.Add(Mensajes.MensajesError("El Estado del inmueble a rentar no puede estar vacío"));
                isError = true;
            }
            if (solicitud.SolicitudTipoPoliza == null)
            {
                Error.Add(Mensajes.MensajesError("Favor de indicar el tipo de póliza"));
                isError = true;
            }
            //if (solicitud.TipoInmobiliarioId == 0)
            //{
            //    Error.Add(Mensajes.MensajesError("Favor de seleccionar el inmueble"));
            //    isError = true;
            //}

            if (solicitud.SolicitudFechaSolicitud.Year == 1)
            {
                solicitud.SolicitudFechaSolicitud = DateTime.Now;
            }
            //if (solicitud.TipoArrendatario == null || solicitud.TipoArrendatario == "0")
            //{
            //    Error.Add(Mensajes.MensajesError("El Tipo de Arrendatario no puede estar vacío"));
            //    isError = true;
            //}
            //if (solicitud.ArrendatarioNombre == null)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("ArrendatarioNombre"));
            //    isError = true;
            //}
            //if (solicitud.ArrendatarioApePat == null)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("ArrendatarioApePat"));
            //    isError = true;
            //}
            //if (solicitud.ArrendatarioTelefono == null)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("ArrendatarioTelefono"));
            //    isError = true;
            //}
            //if (solicitud.SolicitudDomicilioProp == null)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("SolicitudDomicilioProp"));
            //    isError = true;
            //}
            //if (solicitud.SolicitudUbicacionArrendado == null)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("SolicitudUbicacionArrendado"));
            //    isError = true;
            //}
            //if (solicitud.SolicitudDomicilioProp == solicitud.SolicitudUbicacionArrendado)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("SolicitudDomicilioProp"));
            //    Error.Add(Mensajes.ErroresAtributos("SolicitudUbicacionArrendado"));
            //    Error.Add(Mensajes.MensajesError("Domicilio Actual no puede ser el mismo que el que va Arrendar"));
            //    isError = true;
            //}
            //if (solicitud.SolicitudNacionalidad == null)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("SolicitudNacionalidad"));
            //    isError = true;
            //}
            //if (solicitud.SolicitudDepositoGarantia == 0)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("SolicitudDepositoGarantia"));
            //    isError = true;
            //}
            ////valida el tipo de deposito si es SolicitudTipoDeposito = 1 deben de estar todos los siguientes datos:SolicitudNombreCuenta,SolicitudBanco,SolicitudCuenta,SolicitudCLABE
            //if (solicitud.SolicitudTipoDeposito == "1")
            //{
            //    if (solicitud.SolicitudNombreCuenta == null)
            //    {
            //        Error.Add(Mensajes.ErroresAtributos("SolicitudNombreCuenta"));
            //        isError = true;
            //    }
            //    if (solicitud.SolicitudBanco == null)
            //    {
            //        Error.Add(Mensajes.ErroresAtributos("SolicitudBanco"));
            //        isError = true;
            //    }
            //    if (solicitud.SolicitudCuenta == null)
            //    {
            //        Error.Add(Mensajes.ErroresAtributos("SolicitudCuenta"));
            //        isError = true;
            //    }
            //    if (solicitud.SolicitudClabe == null)
            //    {
            //        Error.Add(Mensajes.ErroresAtributos("SolicitudCLABE"));
            //        isError = true;
            //    }
            //}
            ////Bloque de validacion de direcciones
            //if (solicitud.ColoniaActual == null)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("ColoniaActual"));
            //    isError = true;
            //}
            //if (solicitud.AlcaldiaMunicipioActual == null)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("AlcaldiaMunicipioActual"));
            //    isError = true;
            //}
            //if (solicitud.CodigoPostalActual == null)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("CodigoPostalActual"));
            //    isError = true;
            //}
            //if (solicitud.EstadoActual == null)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("EstadoActual"));
            //    isError = true;
            //}
            ////Inmueble arrendar
            //if (solicitud.ColoniaArrendar == null)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("ColoniaArrendar"));
            //    isError = true;
            //}
            //if (solicitud.AlcaldiaMunicipioArrendar == null)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("AlcaldiaMunicipioArrendar"));
            //    isError = true;
            //}
            //if (solicitud.CodigoPostalArrendar == null)
            //{
            //    Error.Add(Mensajes.ErroresAtributos("CodigoPostalArrendar"));
            //    isError = true;
            //}
            /////Fin de Bloque

            //if (solicitud.SolicitudTipoPoliza == "Plus")
            //{
            //    if (solicitud.SolicitudFiador != true && solicitud.SolicitudInmuebleGaran != true)
            //    {
            //        solicitud.SolicitudFiador = true;
            //        solicitud.SolicitudInmuebleGaran = true;
            //    }
            //    else
            //    {
            //        solicitud.SolicitudFiador = true;
            //        solicitud.SolicitudInmuebleGaran = true;
            //    }
            //}

            if (solicitud.SolicitudImporteMensual <= 0)
            {
                Error.Add(Mensajes.ErroresAtributos("SolicitudImporteMensual"));
                Error.Add(Mensajes.MensajesError("Favor indicar el importe de renta"));
                isError = true;
            }
            else
            {

                decimal importe = 0;
                importe = solicitud.SolicitudIncluidaRenta == true ? solicitud.SolicitudImporteMensual + solicitud.SolicitudCuotaMant : solicitud.SolicitudImporteMensual;
                var centroDeCosto = _context.CentroCostos.FirstOrDefault(c => string.Equals(c.CentroCostosTipo.Trim(), solicitud.SolicitudTipoPoliza.Trim(), StringComparison.InvariantCultureIgnoreCase) &&
                                       importe >= c.CentroCostosRentaInicial &&
                                       importe <= c.CentroCostosRentaFinal);
                if (centroDeCosto == null)
                {
                    solicitud.CentroCostosId = null;

                    if (solicitud.SolicitudTipoPoliza == "Tradicional")
                        solicitud.CostoPoliza = importe * Convert.ToDecimal(0.20);

                    if (solicitud.SolicitudTipoPoliza == "Plus")
                        solicitud.CostoPoliza = importe * Convert.ToDecimal(0.35);

                    solicitud.CostoPoliza = solicitud.CostoPoliza * Convert.ToDecimal(1.16);
                }
                else
                {
                    solicitud.CentroCostosId = centroDeCosto.CentroCostosId;
                }
            }
            if (!String.IsNullOrEmpty(solicitud.ArrendatarioNombre) && !String.IsNullOrEmpty(solicitud.ArrendatarioApePat) && isError)
            {
                InvestigacionListasNegras inv = new InvestigacionListasNegras(_context);
                isMatch = inv.ListasNegras(solicitud.ArrendatarioNombre, solicitud.ArrendatarioApePat, solicitud.ArrendatarioApeMat, null, null);

            }
            if (isError == true)
            {
                Error.Add(Mensajes.MensajesError("Favor de completar los campos que estan en rojo"));
                ViewBag.Error = JsonConvert.SerializeObject(Error);
                ViewData["EstadosId"] = new SelectList(_context.Estados, "EstadosId", "EstadoNombre");
                ViewData["TipoInmobiliarioId"] = new SelectList(_context.TipoInmobiliario, "TipoInmobiliarioId", "TipoInmobiliarioDesc");
                ViewData["Representante"] = _context.Usuarios.Include(u => u.Representacion)
                    .Include(u => u.Area).Where(u => u.Area.AreaDescripcion == "Representante" && u.Activo == true).ToList();
                if (area == "Representante")
                {
                    ViewData["Asesor"] = _context.Usuarios.Include(u => u.Representacion).Include(u => u.Area).Where(u => u.Area.AreaDescripcion == "Asesor").Where(u => u.UsuarioPadreId == usuarioid).ToList();
                }
                else
                {
                    ViewData["Asesor"] = _context.Usuarios.Include(u => u.Representacion).Include(u => u.Area).Where(u => u.Area.AreaDescripcion == "Asesor").ToList();
                }
                return View(solicitud);
            }
            else
            {
                solicitud.Creadorid = usuarioid;
                if (area == "Representante")
                {
                    solicitud.Representanteid = usuarioid;
                }
                if (area == "Asesor")
                {
                    solicitud.Asesorid = usuarioid;
                }
                if (solicitud.Asesorid > 0)
                {
                    solicitud.Inmobiliaria = _context.Usuarios.SingleOrDefault(u => u.UsuariosId == solicitud.Asesorid).UsuarioInmobiliaria;
                }
                try
                {
                    _context.Add(solicitud);
                    await _context.SaveChangesAsync();
                    int id = (int)solicitud.SolicitudId;
                    int CreadorId = solicitud.Creadorid;
                    if (isMatch == true)
                    {
                        var proceso = _context.TipoProceso.SingleOrDefault(t => t.Orden == 99);
                        solicitud.SolicitudEstatus = proceso.Descripcion;
                        usuariosSolicitud = new UsuariosSolicitud
                        {
                            SolicitudId = id,
                            TipoProcesoId = proceso.TipoProcesoId,
                            UsuariosId = SistemaId,
                            Fecha = DateTime.Now,
                            Observacion = "Se encontro en la lista negra a la siguiente persona: " + solicitud.ArrendatarioNombre + " " + solicitud.ArrendatarioApePat + " " + solicitud.ArrendatarioApeMat
                        };
                        _context.Add(usuariosSolicitud);
                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        var proceso = _context.TipoProceso.SingleOrDefault(t => t.Orden == 1);
                        usuariosSolicitud = new UsuariosSolicitud
                        {
                            SolicitudId = id,
                            TipoProcesoId = proceso.TipoProcesoId,
                            UsuariosId = CreadorId,
                            Fecha = DateTime.Now
                        };
                        _context.Add(usuariosSolicitud);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    Log log = new Log()
                    {
                        LogObjetoIn = solicitud.ToString(),
                        LogFecha = DateTime.Now,
                        LogPantalla = "Create",
                        LogProceso = "Solicitud",
                        LogObjetoOut = e.ToString()

                    };
                    _context.Add(log);
                    _context.SaveChanges();
                    Error.Add(Mensajes.MensajesError("Revisar Id: " + log.LogId.ToString()));
                }
                return View(solicitud);
            }
        }

        // GET: Solicituds/Edit/5
        public async Task<IActionResult> Edit(int? id, int origen)
        {
            Solicitud solicitud = new Solicitud();
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            string area = User.FindFirst("Area").Value;
            ViewBag.Area = User.FindFirst("Area").Value;
            UserViewModel user = new UserViewModel();
            user = JsonConvert.DeserializeObject<UserViewModel>(User.FindFirst("User").Value);
            int areaId = user.AreaId;
            if (id == 0)
                return Redirect("Index");

            solicitud = origen == 1 ? _context.Solicitud
                .Include(s => s.Representante.Representacion)
                .Include(s => s.FisicaMoral)
                .FirstOrDefault(m => m.SolicitudId == id) :
                 _context.FisicaMoral
                .Include(s => s.Solicitud.Representante.Representacion)
                .FirstOrDefault(m => m.FisicaMoralId == id).Solicitud;
          
            ViewBag.Id = solicitud.FisicaMoral.Count == 0 ? 0 : solicitud.FisicaMoral.FirstOrDefault().FisicaMoralId;

            ViewBag.TipoRegimen = Int32.Parse(solicitud.TipoArrendatario);
            ViewBag.Fiador = solicitud.SolicitudFiador;
            ViewData["EstadosId"] = new SelectList(_context.Estados, "EstadosId", "EstadoNombre");
            ViewData["TipoInmobiliarioId"] = new SelectList(_context.TipoInmobiliario, "TipoInmobiliarioId", "TipoInmobiliarioDesc");
            ViewData["Representante"] = await _context.Usuarios
                .Include(u => u.Representacion)
                .Include(u => u.Area)
                .Where(u => u.Area.AreaDescripcion == "Representante" && u.Activo == true).ToListAsync();

            ViewData["Asesor"] = area == "Representante" ?
                await _context.Usuarios
                .Include(u => u.Representacion)
                .Include(u => u.Area).Where(u => u.AreaId == 2)
                .Where(u => u.UsuarioPadreId == usuarioid).ToListAsync() :
                await _context.Usuarios
                .Include(u => u.Representacion)
                .Include(u => u.Area)
                .Where(u => u.AreaId == 2).ToListAsync();

            if (solicitud.Representanteid > 0)
            {
                ViewBag.NombreRepresentante = solicitud.Representante.UsuarioNomCompleto;
                ViewBag.NombreRepresentacion = solicitud.Representante.Representacion.RepresentacionNombre;
            }

            ViewBag.NombreAsesor = solicitud.Asesorid > 0 ? _context.Usuarios.FirstOrDefault(u => u.UsuariosId == solicitud.Asesorid).UsuarioNomCompleto : null;

            return View(solicitud);
        }

        // POST: Solicituds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SolicitudId,SolicitudTipoPoliza,Inmobiliaria,SolicitudPersonaSolicita,SolicitudTelefono,SolicitudCelular,SolicitudEmail,SolicitudFechaSolicitud,SolicitudFechaFirma,SolicitudHoraFirma,SolicitudLugarFirma,SolicitudAdmiInmueble,SolicitudEsAdminInmueble,SolicitudRecibodePago,SolicitudNombreProp,SolicitudApePaternoProp,SolicitudApeMaternoProp,SolicitudNacionalidad,SolicitudRazonSocial,SolicitudRfc,SolicitudRepresentanteLegal,SolicitudApePaternoLegal,SolicitudApeMaternoLegal,SolicitudDomicilioProp,SolicitudTelefonoProp,SolicitudCelularProp,SolicitudEmailProp,SolicitudTipoDeposito,SolicitudNombreCuenta,SolicitudBanco,SolicitudCuenta,SolicitudClabe,SolicitudUbicacionArrendado,SolicitudTelefonoInmueble,SolicitudNumero,SolicitudImporteMensual,SolicitudCuotaMant,SolicitudIncluidaRenta,SolicitudDepositoGarantia,SolicitudVigenciaContratoI,SolicitudVigenciaContratoF,SolicitudPagare,SolicitudDestinoArrendamien,SolicitudObservaciones,SolicitudEstatus,SolicitudTipoRegimen,CentroCostosId,EstadosId,SolicitudInmuebleGaran,SolicitudFiador,SolicitudCartaEntrega,TipoArrendatario,ArrendatarioNombre,ArrendatarioApePat,ArrendatarioApeMat,ArrendatarioTelefono,ArrendatarioCorreo,TipoInmobiliarioId,Representanteid,RepresentanteNombre,Asesorid,AsesorNombre,Creadorid,CreadorNombre,ColoniaActual,AlcaldiaMunicipioActual,CodigoPostalActual,EstadoActual,ColoniaArrendar,AlcaldiaMunicipioArrendar,CodigoPostalArrendar,EstadoArrendar,TipoIdentificacion,NumeroIdentificacion,Nombre1 ,ApePat1 ,ApeMat1 ,TipoIdent1 ,NumIdent1 ,Nombre2 ,ApePat2 ,ApeMat2 ,TipoIdent2 ,NumIdent2 ,Nombre3 ,ApePat3 ,ApeMat3 ,TipoIdent3 ,NumIdent3,NumRppcons,EscrituraNumero,Licenciado,NumeroNotaria,FechaRppcons,NumEscPoder,TitularNotaPoder,NumNotaria,FechaEmitePoder,FechaConstitutiva,IsRenovacion,EsAmueblado")] Solicitud solicitud)
        {
            bool isError = false;
            bool isMatch = false;
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            string area = User.FindFirst("Area").Value;
            Error.Clear();
            string RepresentanteUsuarioNomCompleto = string.Empty;
            string AsesorUsuarioNomCompleto = string.Empty;
            string RepresentacionNombre = string.Empty;

            if (solicitud.SolicitudFechaSolicitud.GetHashCode() == 0)
            {
                solicitud.SolicitudFechaSolicitud = DateTime.Now.AddDays(-1);
            }

            if (solicitud.Asesorid > 0)
            {
                var asesor = _context.Usuarios.FirstOrDefault(u => u.UsuariosId == solicitud.Asesorid);
                AsesorUsuarioNomCompleto = asesor.UsuarioNomCompleto;
            }

            if (solicitud.Representanteid > 0)
            {
                var representante = _context.Usuarios.Include(u => u.Representacion).FirstOrDefault(u => u.UsuariosId == solicitud.Representanteid);
                RepresentanteUsuarioNomCompleto = representante.UsuarioNomCompleto;
                RepresentacionNombre = representante.Representacion.RepresentacionNombre;
            }

            var fisicaMoral = await _context.FisicaMoral.AsNoTracking().SingleOrDefaultAsync(f => f.SolicitudId == id);
            if (fisicaMoral == null)
            {
                ViewBag.Id = 0;
            }
            else
            {
                ViewBag.Id = fisicaMoral.FisicaMoralId;
            }

            if (solicitud.SolicitudTipoRegimen == 0)
            {
                Error.Add(Mensajes.MensajesError("Debe seleccionar el tipo de Régimen para el Arrendador"));
                isError = true;
            }
            if (solicitud.SolicitudTipoRegimen == 1)
            {
                if (solicitud.SolicitudNombreProp == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("SolicitudNombreProp"));
                    isError = true;
                }
                if (solicitud.SolicitudApePaternoProp == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("SolicitudApePaternoProp"));
                    isError = true;
                }
            }
            if (solicitud.SolicitudTipoRegimen == 2)
            {
                if (solicitud.SolicitudRazonSocial == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("SolicitudRazonSocial"));
                    isError = true;
                }
                if (solicitud.SolicitudRfc == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("SolicitudRfc"));
                    isError = true;
                }
                if (solicitud.SolicitudRepresentanteLegal == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("SolicitudRepresentanteLegal"));
                    isError = true;
                }
                if (solicitud.SolicitudApePaternoLegal == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("SolicitudApePaternoLegal"));
                    isError = true;
                }
            }
            if (solicitud.EstadosId == 0)
            {
                Error.Add(Mensajes.MensajesError("El Estado del Inmueble a rentar no puede estar vacío"));
                isError = true;
            }
            if (solicitud.SolicitudTipoPoliza == null)
            {
                Error.Add(Mensajes.ErroresAtributos("SolicitudTipoPoliza"));
                isError = true;
            }
            if (solicitud.TipoInmobiliarioId == 0)
            {
                Error.Add(Mensajes.MensajesError("Favor de seleccionar el inmueble"));
                isError = true;
            }
            if (solicitud.TipoArrendatario == null || solicitud.TipoArrendatario == "0")
            {
                Error.Add(Mensajes.MensajesError("El Tipo de Arrendatario no puede estar vacío"));
                isError = true;
            }
            if (solicitud.ArrendatarioNombre == null)
            {
                Error.Add(Mensajes.ErroresAtributos("ArrendatarioNombre"));
                isError = true;
            }
            if (solicitud.ArrendatarioApePat == null)
            {
                Error.Add(Mensajes.ErroresAtributos("ArrendatarioApePat"));
                isError = true;
            }
            if (solicitud.ArrendatarioTelefono == null)
            {
                Error.Add(Mensajes.ErroresAtributos("ArrendatarioTelefono"));
                isError = true;
            }
            if (solicitud.SolicitudDomicilioProp == null)
            {
                Error.Add(Mensajes.ErroresAtributos("SolicitudDomicilioProp"));
                isError = true;
            }
            if (solicitud.SolicitudUbicacionArrendado == null)
            {
                Error.Add(Mensajes.ErroresAtributos("SolicitudUbicacionArrendado"));
                isError = true;
            }
            if (solicitud.SolicitudDomicilioProp == solicitud.SolicitudUbicacionArrendado)
            {
                Error.Add(Mensajes.ErroresAtributos("SolicitudDomicilioProp"));
                Error.Add(Mensajes.ErroresAtributos("SolicitudUbicacionArrendado"));
                Error.Add(Mensajes.MensajesError("Domicilio Actual no puede ser el mismo que el que va Arrendar"));
                isError = true;
            }
            if (solicitud.SolicitudNacionalidad == null)
            {
                Error.Add(Mensajes.ErroresAtributos("SolicitudNombreCuenta"));
                isError = true;
            }
            //valida el tipo de deposito si es SolicitudTipoDeposito = 1 deben de estar todos los siguientes datos:SolicitudNombreCuenta,SolicitudBanco,SolicitudCuenta,SolicitudCLABE
            if (solicitud.SolicitudTipoDeposito == "1")
            {
                if (solicitud.SolicitudNombreCuenta == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("SolicitudNombreCuenta"));
                    isError = true;
                }
                if (solicitud.SolicitudBanco == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("SolicitudBanco"));
                    isError = true;
                }
                if (solicitud.SolicitudCuenta == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("SolicitudCuenta"));
                    isError = true;
                }
                if (solicitud.SolicitudClabe == null)
                {
                    Error.Add(Mensajes.ErroresAtributos("SolicitudCLABE"));
                    isError = true;
                }
            }
            if (solicitud.SolicitudTipoPoliza == "Plus")
            {
                if (solicitud.SolicitudFiador != true && solicitud.SolicitudInmuebleGaran != true)
                {
                    solicitud.SolicitudFiador = true;
                    solicitud.SolicitudInmuebleGaran = true;
                }
                else
                {
                    solicitud.SolicitudFiador = true;
                    solicitud.SolicitudInmuebleGaran = true;
                }
            }
            if (solicitud.SolicitudDepositoGarantia == 0)
            {
                Error.Add(Mensajes.ErroresAtributos("SolicitudDepositoGarantia"));
                isError = true;
            }
            //Bloque de validacion de direcciones

            if (solicitud.ColoniaActual == null)
            {
                Error.Add(Mensajes.ErroresAtributos("ColoniaActual"));
                isError = true;
            }
            if (solicitud.AlcaldiaMunicipioActual == null)
            {
                Error.Add(Mensajes.ErroresAtributos("AlcaldiaMunicipioActual"));
                isError = true;
            }
            if (solicitud.CodigoPostalActual == null)
            {
                Error.Add(Mensajes.ErroresAtributos("CodigoPostalActual"));
                isError = true;
            }
            if (solicitud.EstadoActual == null)
            {
                Error.Add(Mensajes.ErroresAtributos("EstadoActual"));
                isError = true;
            }
            //Inmueble arrendar
            if (solicitud.ColoniaArrendar == null)
            {
                Error.Add(Mensajes.ErroresAtributos("AlcaldiaMunicipioArrendar"));
                isError = true;
            }
            if (solicitud.AlcaldiaMunicipioArrendar == null)
            {
                Error.Add(Mensajes.ErroresAtributos("AlcaldiaMunicipioArrendar"));
                isError = true;
            }
            if (solicitud.CodigoPostalArrendar == null)
            {
                Error.Add(Mensajes.MensajesError("CodigoPostalArrendar"));
                isError = true;
            }

            if (solicitud.SolicitudImporteMensual <= 0)
            {
                Error.Add(Mensajes.ErroresAtributos("SolicitudImporteMensual"));
                Error.Add(Mensajes.MensajesError("Favor de ingresar el importe de la renta"));
                isError = true;
            }
            else
            {
                decimal importe = 0;
                importe = solicitud.SolicitudIncluidaRenta == true ? solicitud.SolicitudImporteMensual + solicitud.SolicitudCuotaMant : solicitud.SolicitudImporteMensual;
                var centroDeCosto = _context.CentroCostos.FirstOrDefault(c => string.Equals(c.CentroCostosTipo.Trim(), solicitud.SolicitudTipoPoliza.Trim(), StringComparison.InvariantCultureIgnoreCase) &&
                                       importe >= c.CentroCostosRentaInicial &&
                                       importe <= c.CentroCostosRentaFinal);
                if (centroDeCosto == null)
                {
                    solicitud.CentroCostosId = null;

                    if (solicitud.SolicitudTipoPoliza == "Tradicional")
                        solicitud.CostoPoliza = importe * Convert.ToDecimal(0.20);

                    if (solicitud.SolicitudTipoPoliza == "Plus")
                        solicitud.CostoPoliza = importe * Convert.ToDecimal(0.35);

                    solicitud.CostoPoliza = solicitud.CostoPoliza * Convert.ToDecimal(1.16);

                }
                else
                {
                    solicitud.CentroCostosId = centroDeCosto.CentroCostosId;
                }                
            }

            if (solicitud.ArrendatarioNombre != null && solicitud.ArrendatarioApePat != null && isError == false)
            {

                //  isMatch = InvestigacionListasNegras.ListasNegras(solicitud.ArrendatarioNombre, solicitud.ArrendatarioApePat, solicitud.ArrendatarioApeMat,null,null);

            }

            if (isError == true)
            {
                ViewBag.NombreRepresentante = RepresentanteUsuarioNomCompleto;
                ViewBag.NombreRepresentacion = RepresentacionNombre;
                ViewBag.NombreAsesor = AsesorUsuarioNomCompleto;
                Error.Add(Mensajes.MensajesError("Favor de completar los campos que estan en rojo"));
                ViewBag.Error = JsonConvert.SerializeObject(Error);
                ViewData["EstadosId"] = new SelectList(_context.Estados, "EstadosId", "EstadoNombre");
                ViewData["TipoInmobiliarioId"] = new SelectList(_context.TipoInmobiliario, "TipoInmobiliarioId", "TipoInmobiliarioDesc");
                ViewData["Representante"] = await _context.Usuarios.Include(u => u.Representacion).Include(u => u.Area).Where(u => u.Area.AreaDescripcion == "Representante" && u.Activo == true).ToListAsync();
                if (area == "Representante")
                {
                    ViewData["Asesor"] = _context.Usuarios.Include(u => u.Representacion).Include(u => u.Area).Where(u => u.Area.AreaDescripcion == "Asesor").Where(u => u.UsuarioPadreId == usuarioid).ToList();
                }
                else
                {
                    ViewData["Asesor"] = _context.Usuarios.Include(u => u.Representacion).Include(u => u.Area).Where(u => u.Area.AreaDescripcion == "Asesor").ToList();
                }
                ViewBag.TipoRegimen = Int32.Parse(solicitud.TipoArrendatario);
                return View(solicitud);
            }
            else
            {
                solicitud.Creadorid = Int32.Parse(User.FindFirst("Id").Value);//_context.Solicitud.Where(s => s.SolicitudId == id).SingleOrDefault().Creadorid;//
                if (User.FindFirst("Area").Value == "Representante")
                {
                    solicitud.Representanteid = Int32.Parse(User.FindFirst("Id").Value);
                }
                if (User.FindFirst("Area").Value == "Asesor")
                {
                    solicitud.Asesorid = Int32.Parse(User.FindFirst("Id").Value);
                }
                if (solicitud.Asesorid > 0)
                {
                    solicitud.Inmobiliaria = _context.Usuarios.SingleOrDefault(u => u.UsuariosId == solicitud.Asesorid).UsuarioInmobiliaria;
                }
                ViewBag.NombreRepresentante = RepresentanteUsuarioNomCompleto;
                ViewBag.NombreRepresentacion = RepresentacionNombre;
                ViewBag.NombreAsesor = AsesorUsuarioNomCompleto;
                ViewData["EstadosId"] = new SelectList(_context.Estados, "EstadosId", "EstadoNombre");
                ViewData["TipoInmobiliarioId"] = new SelectList(_context.TipoInmobiliario, "TipoInmobiliarioId", "TipoInmobiliarioDesc");
                ViewData["Representante"] = await _context.Usuarios.Include(u => u.Representacion).Include(u => u.Area).Where(u => u.Area.AreaDescripcion == "Representante" && u.Activo == true).ToListAsync();
                if (area == "Representante")
                {
                    ViewData["Asesor"] = _context.Usuarios.Include(u => u.Representacion).Include(u => u.Area).Where(u => u.Area.AreaDescripcion == "Asesor").Where(u => u.UsuarioPadreId == usuarioid).ToList();
                }
                else
                {
                    ViewData["Asesor"] = _context.Usuarios.Include(u => u.Representacion).Include(u => u.Area).Where(u => u.Area.AreaDescripcion == "Asesor").ToList();
                }
                ViewBag.TipoRegimen = Int32.Parse(solicitud.TipoArrendatario);

                if (isMatch == true)
                {
                    var proceso = _context.TipoProceso.SingleOrDefault(t => t.Orden == 99);
                    solicitud.SolicitudEstatus = proceso.Descripcion;
                    usuariosSolicitud = new UsuariosSolicitud
                    {
                        SolicitudId = id,
                        TipoProcesoId = proceso.TipoProcesoId,
                        UsuariosId = usuarioid,
                        Fecha = DateTime.Now,
                        Observacion = "Se encontro en la lista negra a la siguiente persona: " + solicitud.ArrendatarioNombre + " " + solicitud.ArrendatarioApePat + " " + solicitud.ArrendatarioApeMat
                    };
                    _context.Add(usuariosSolicitud);
                    await _context.SaveChangesAsync();

                }

                try
                {
                    //Validamos si el costo de la póliza tiene algun cambio.
                    Solicitud sol = new Solicitud();
                    sol = await _context.Solicitud.AsNoTracking().SingleOrDefaultAsync(s => s.SolicitudId == solicitud.SolicitudId);
                    Boolean isBandera = false;

                    if (solicitud.CentroCostosId != sol.CentroCostosId)
                        isBandera = true;

                    if (solicitud.CostoPoliza != sol.CostoPoliza)
                        isBandera = true;

                    _context.Update(solicitud);
                    await _context.SaveChangesAsync();                    

                    if (isBandera == true)
                    {
                        //Categoria Póliza = 2
                        DetallePoliza dp = new DetallePoliza();
                        Poliza p = new Poliza();

                        p = await _context.Poliza.Include(po => po.FisicaMoral).AsNoTracking().SingleOrDefaultAsync(po => po.FisicaMoral.SolicitudId == solicitud.SolicitudId);

                        if (p != null)
                        {
                            CalculoPolizaRegalias realizamos = new CalculoPolizaRegalias(_context);
                            realizamos.Calcular((int)p.FisicaMoralId);
                        }

                    }

                }
                catch (DbUpdateConcurrencyException e)
                {
                    Error.Add(Mensajes.MensajesError(e.ToString()));
                }
                fisicaMoral = await _context.FisicaMoral.Include(f => f.Solicitud).SingleOrDefaultAsync(f => f.SolicitudId == solicitud.SolicitudId);
                if (fisicaMoral == null)
                {
                    ViewBag.Id = 0;
                }
                else
                {
                    ViewBag.Id = fisicaMoral.FisicaMoralId;
                }

                return View(solicitud);
            }
        }
        // GET: Solicituds/Delete/5
        // GET: Solicituds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var solicitud = await _context.Solicitud
                .Include(s => s.CentroCostos)
                //     .Include(s => s.Usuario)
                .SingleOrDefaultAsync(m => m.SolicitudId == id);
            if (solicitud == null)
            {
                return NotFound();
            }
            ViewData["EstadosId"] = new SelectList(_context.Estados, "EstadosId", "EstadoNombre");
            ViewData["TipoInmobiliarioId"] = new SelectList(_context.TipoInmobiliario, "TipoInmobiliarioId", "TipoInmobiliarioDesc");
            ViewBag.TipoRegimen = solicitud.SolicitudTipoRegimen;
            return View(solicitud);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitud = await _context.Solicitud.SingleOrDefaultAsync(s => s.SolicitudId == id);
            var usuariosolicitud = _context.UsuariosSolicitud.Where(u => u.SolicitudId == solicitud.SolicitudId).ToList<UsuariosSolicitud>();
            //var fisicamoral = await _context.FisicaMoral.SingleOrDefaultAsync(f => f.SolicitudId == solicitud.SolicitudId);
            //var poliza = _context.Poliza.SingleOrDefaultAsync(p => p.FisicaMoralId == fisicamoral.FisicaMoralId);

            foreach (var usu in usuariosolicitud)
            {
                _context.UsuariosSolicitud.Remove(usu);
                await _context.SaveChangesAsync();
            }

            if (solicitud != null)
            {
                _context.Solicitud.Remove(solicitud);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Solicituds");
        }

        public async Task<IActionResult> ProcesoRealizar(int id)
        {
            var area = User.FindFirst("Area").Value;
            if (id == 0)
            {
                ViewBag.Error = "Favor de intentarlo mas tarde.";
            }

            var solicitud = await _context.Solicitud.FirstOrDefaultAsync(m => m.SolicitudId == id);
            if (solicitud.TipoArrendatario == "1")
            {
                var FisicaMoral = await _context.FisicaMoral.FirstOrDefaultAsync(m => m.SolicitudId == id);
                if (FisicaMoral == null)
                {
                    return RedirectToAction("Create", "FisicaMorals", new { @id = solicitud.SolicitudId });
                }
                else
                {
                    var refarrens = await _context.RefArrendamiento.FirstOrDefaultAsync(m => m.FisicaMoralId == FisicaMoral.FisicaMoralId);
                    if (refarrens == null)
                    {
                        return RedirectToAction("Create", "RefArrendamientoes", new { @id = FisicaMoral.FisicaMoralId });
                    }
                    var referenciaPersonals = await _context.ReferenciaPersonal.FirstOrDefaultAsync(m => m.FisicaMoralId == FisicaMoral.FisicaMoralId);
                    if (referenciaPersonals == null)
                    {
                        return RedirectToAction("Index", "ReferenciaPersonals", new { @id = FisicaMoral.FisicaMoralId });
                    }
                    var personasOcupanInms = await _context.PersonasOcupanInm.FirstOrDefaultAsync(m => m.FisicaMoralId == FisicaMoral.FisicaMoralId);
                    if (personasOcupanInms == null)
                    {
                        return RedirectToAction("Index", "PersonasOcupanInms", new { @id = FisicaMoral.FisicaMoralId });
                    }
                    var documentos = await _context.Documentos.FirstOrDefaultAsync(m => m.FisicaMoralId == FisicaMoral.FisicaMoralId);
                    if (documentos == null)
                    {
                        return RedirectToAction("Index", "Documentos", new { @id = FisicaMoral.FisicaMoralId });
                    }
                    if (solicitud.SolicitudFiador == true)
                    {
                        var fiadorFs = await _context.FiadorF.FirstOrDefaultAsync(m => m.FisicaMoralId == FisicaMoral.FisicaMoralId);
                        if (fiadorFs == null)
                        {
                            return RedirectToAction("Index", "FiadorFs", new { @id = FisicaMoral.FisicaMoralId });
                        }
                        var fiadorM = await _context.FiadorM.FirstOrDefaultAsync(m => m.FisicaMoralId == FisicaMoral.FisicaMoralId);
                        if (fiadorM == null)
                        {
                            return RedirectToAction("Index", "FiadorMs", new { @id = FisicaMoral.FisicaMoralId });
                        }
                    }
                    if (area == "Administración" || area == "Procesos")
                        return RedirectToAction("VistaPrevia", "GenerarContrato", new { @id = FisicaMoral.FisicaMoralId });
                }

            }
            if (solicitud.TipoArrendatario == "2")
            {
                var FisicaMoral = await _context.FisicaMoral.FirstOrDefaultAsync(m => m.SolicitudId == id);
                if (FisicaMoral == null)
                {
                    return RedirectToAction("Create", "FisicaMorals", new { @id = solicitud.SolicitudId });
                }
                else
                {
                    var refarrens = await _context.RefArrendamiento.FirstOrDefaultAsync(m => m.FisicaMoralId == FisicaMoral.FisicaMoralId);
                    if (refarrens == null)
                    {
                        return RedirectToAction("Create", "RefArrendamientoes", new { @id = FisicaMoral.FisicaMoralId });
                    }
                    var referenciaPersonals = await _context.ReferenciaPersonal.FirstOrDefaultAsync(m => m.FisicaMoralId == FisicaMoral.FisicaMoralId);
                    if (referenciaPersonals == null)
                    {
                        return RedirectToAction("Index", "ReferenciaComercials", new { @id = FisicaMoral.FisicaMoralId });
                    }
                    var documentos = await _context.Documentos.FirstOrDefaultAsync(m => m.FisicaMoralId == FisicaMoral.FisicaMoralId);
                    if (documentos == null)
                    {
                        return RedirectToAction("Index", "Documentos", new { @id = FisicaMoral.FisicaMoralId });
                    }
                    if (solicitud.SolicitudFiador == true)
                    {
                        var fiadorFs = await _context.FiadorF.FirstOrDefaultAsync(m => m.FisicaMoralId == FisicaMoral.FisicaMoralId);
                        if (fiadorFs == null)
                        {
                            return RedirectToAction("Index", "FiadorFs", new { @id = FisicaMoral.FisicaMoralId });
                        }
                        var fiadorM = await _context.FiadorM.FirstOrDefaultAsync(m => m.FisicaMoralId == FisicaMoral.FisicaMoralId);
                        if (fiadorM == null)
                        {
                            return RedirectToAction("Index", "FiadorMs", new { @id = FisicaMoral.FisicaMoralId });
                        }
                    }
                    if (area == "Administración" || area == "Procesos")
                        return RedirectToAction("VistaPrevia", "GenerarContrato", new { @id = FisicaMoral.FisicaMoralId });
                }

            }
            return View(solicitud);

        }

        public async Task<String> GeneraRenovacion(int id)
        {
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            string result = string.Empty;
            Solicitud s = new Solicitud();
            s = await _context.Solicitud.AsNoTracking().FirstOrDefaultAsync(m => m.SolicitudId == id);
            if (s != null)
            {
                s.SolicitudId = 0;
                s.SolicitudFechaSolicitud = DateTime.Now;
                s.SolicitudEstatus = "Recibida";
                _context.Solicitud.Add(s);
                await _context.SaveChangesAsync();

                if (s.SolicitudId > 0)
                {
                    var proceso = _context.TipoProceso.SingleOrDefault(t => t.Orden == 1);
                    usuariosSolicitud = new UsuariosSolicitud
                    {
                        SolicitudId = s.SolicitudId,
                        TipoProcesoId = proceso.TipoProcesoId,
                        UsuariosId = s.Creadorid,
                        Fecha = DateTime.Now
                    };
                    _context.Add(usuariosSolicitud);
                    await _context.SaveChangesAsync();
                }

            }

            FisicaMoral fm = new FisicaMoral();
            fm = await _context.FisicaMoral.AsNoTracking().FirstOrDefaultAsync(m => m.SolicitudId == id);
            int anteriorFisicaMoral = fm.FisicaMoralId;
            if (fm != null && s.SolicitudId > 0)
            {
                fm.FisicaMoralId = 0;
                fm.SolicitudId = s.SolicitudId;
                _context.FisicaMoral.Add(fm);
                await _context.SaveChangesAsync();

                if (fm.FisicaMoralId > 0)
                {
                    RefArrendamiento refarrens = await _context.RefArrendamiento.AsNoTracking().FirstOrDefaultAsync(m => m.FisicaMoralId == anteriorFisicaMoral);
                    if (refarrens != null)
                    {
                        refarrens.FisicaMoralId = fm.FisicaMoralId;
                        _context.Add(refarrens);
                        await _context.SaveChangesAsync();
                    }
                    List<ReferenciaPersonal> rp = await _context.ReferenciaPersonal.AsNoTracking().Where(m => m.FisicaMoralId == anteriorFisicaMoral).ToListAsync();

                    if (rp.Count > 0)
                    {
                        foreach (var i in rp)
                        {
                            i.ReferenciaPersonalId = 0;
                            i.FisicaMoralId = fm.FisicaMoralId;
                            _context.Add(i);
                            await _context.SaveChangesAsync();
                        }
                    }
                    List<PersonasOcupanInm> personasOcupanInms = await _context.PersonasOcupanInm.AsNoTracking().Where(m => m.FisicaMoralId == anteriorFisicaMoral).ToListAsync();
                    if (personasOcupanInms.Count > 0)
                    {
                        foreach (var i in personasOcupanInms)
                        {
                            i.PersonasOcupanInmId = 0;
                            i.FisicaMoralId = fm.FisicaMoralId;
                            _context.Add(i);
                            await _context.SaveChangesAsync();
                        }
                    }

                    List<Documentos> documentos = await _context.Documentos.AsNoTracking().Where(d => d.FisicaMoralId == anteriorFisicaMoral).ToListAsync();
                    if (documentos.Count > 0)
                    {
                        foreach (var i in documentos)
                        {
                            i.DocumentosId = 0;
                            i.FisicaMoralId = fm.FisicaMoralId;
                            _context.Add(i);
                            await _context.SaveChangesAsync();
                        }
                    }
                    List<FiadorF> ff = await _context.FiadorF.AsNoTracking().Where(d => d.FisicaMoralId == anteriorFisicaMoral).ToListAsync();
                    if (ff.Count > 0)
                    {
                        foreach (var i in ff)
                        {
                            i.FiadorFid = 0;
                            i.FisicaMoralId = fm.FisicaMoralId;
                            _context.Add(i);
                            await _context.SaveChangesAsync();
                        }
                    }

                    List<FiadorM> fiadorm = await _context.FiadorM.AsNoTracking().Where(d => d.FisicaMoralId == anteriorFisicaMoral).ToListAsync();
                    if (ff.Count > 0)
                    {
                        foreach (var i in fiadorm)
                        {
                            i.FiadorMid = 0;
                            i.FisicaMoralId = fm.FisicaMoralId;
                            _context.Add(i);
                            await _context.SaveChangesAsync();
                        }
                    }

                    List<Arrendatario> arrendatarios = await _context.Arrendatario.AsNoTracking().Where(a => a.FisicaMoralId == anteriorFisicaMoral).ToListAsync();
                    if (arrendatarios.Count > 0)
                    {
                        foreach (var a in arrendatarios)
                        {
                            a.ArrendatarioId = 0;
                            a.FisicaMoralId= fm.FisicaMoralId;
                            _context.Add(a);
                            await _context.SaveChangesAsync();
                        }
                    }

                }
            }
            Error.Add(Mensajes.Exitoso("Se ha generado la solicitud: " + s.SolicitudId.ToString()));
            result = JsonConvert.SerializeObject(Error);
            return result;
        }
    }
}
