using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PolizaJuridica.Data;
using PolizaJuridica.ViewModels;

namespace PolizaJuridica.Controllers
{
    public class CalendariosController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public CalendariosController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Calendarios
        public async Task<IActionResult> Index(int id)
        {
            var PolizaJuridicaDbContext = _context.Calendario.Include(c => c.Usuarios).Where(c => c.FisicaMoralId == id);
            //sección del código que se puede mejorar
            var varSolicitudId = (await _context.FisicaMoral.SingleOrDefaultAsync(f => f.FisicaMoralId == id));
            var TipoRegimen = (await _context.Solicitud.SingleOrDefaultAsync(s => s.SolicitudId == varSolicitudId.SolicitudId));
            ViewBag.TipoRegimen = TipoRegimen.SolicitudTipoRegimen;
            ViewBag.Fiador = TipoRegimen.SolicitudFiador;
            string[] Areas = { "Representante" };
            ViewData["UsuarioNombre"] = new SelectList(_context.Usuarios.Include(u => u.Area).Where(u => Areas.Contains(u.Area.AreaDescripcion)), "UsuariosId", "UsuarioNombre");
            //Fin de la sección
            ViewBag.Id = id;
            

            return View(await PolizaJuridicaDbContext.ToListAsync());
        }

        public async Task<IActionResult> CalendarioTablero()
        {
            //ViewData["Solicitud"] = calendario;
            //var calendario = _context.Calendario.Include(c => c.Usuarios).Where(c => c.UsuariosId == usuarioid);
            int[] Area = { 3,7,12};
            ViewBag.Usuarios = new SelectList(_context.Usuarios.Where(p => Area.Contains(p.AreaId) && p.Activo == true), "UsuariosId", "UsuarioNomCompleto");
            return View();
        }


        // Get carga de elementos
        public async Task<JsonResult> LoadCalendario(DateTime start, DateTime end)
        {

            var solicitudes = _context.Solicitud.Include(h => h.Representante).Include(h => h.Asesor)
                                       .Where(h => h.SolicitudHoraFirma.Date.CompareTo(start.Date) >= 0 &&
                                                   h.SolicitudHoraFirma.Date.CompareTo(end.Date) <= 0)
                                       .AsQueryable();


            List<CalendarioViewModel> eventos = new List<CalendarioViewModel>();

            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);

            var usuario = await _context.Usuarios
                                          .Include(u => u.Area)
                                          .SingleOrDefaultAsync(u => u.UsuariosId == usuarioid);

            switch (usuario.Area.AreaDescripcion)
            {
                case "Administración":
                    solicitudes = solicitudes.Include(s => s.Representante).Include(s => s.Asesor);

                    break;
                case "Procesos":
                    solicitudes = solicitudes.Where(s => s.Creadorid == usuarioid || s.Representanteid == usuarioid).Include(s => s.Representante).Include(s => s.Asesor);

                    break;
                case "Asesor":
                    solicitudes = solicitudes.Where(s => s.Creadorid == usuarioid || s.Asesorid == usuarioid).Include(s => s.Representante).Include(s => s.Asesor);

                    break;
                case "Representante":
                    solicitudes = solicitudes.Where(s => s.Creadorid == usuarioid || s.Representanteid == usuarioid).Include(s => s.Representante).Include(s => s.Asesor);
                    break;
                default:
                    solicitudes = solicitudes.Where(s => s.SolicitudEstatus == "Concluida");
                    break;
            }

            var firmas = _context.Firmas.Include(f => f.Poliza.FisicaMoral.Solicitud)
                .Include(f => f.CreadaPor)
                .Where(f => f.FechaFirma.Date.CompareTo(start.Date) >= 0 &&
                                                   f.FechaFirma.Date.CompareTo(end.Date) <= 0).AsQueryable();
            if (firmas.Count() > 1)
            {
                var sol = new HashSet<int>(firmas.Select(f => f.Poliza.FisicaMoral.SolicitudId));
                solicitudes = solicitudes.Where(f => !sol.Contains((int)f.SolicitudId));
            }

            eventos = MapeoSolicitudCalendarioViewModel(solicitudes.ToList(), firmas.ToList());

            return Json(eventos);
        }

        private List<CalendarioViewModel> MapeoSolicitudCalendarioViewModel(List<Solicitud> solicitudes, List<Firmas> firmas)
        {
            List<CalendarioViewModel> eventos = new List<CalendarioViewModel>();
            string color = string.Empty;

            foreach (var sol in solicitudes)
            {
                string AsesorNombre = string.Empty;
                string RepresentanteNombre = string.Empty;
                

                if (sol.Asesorid != null)
                    AsesorNombre = sol.Asesor.UsuarioNomCompleto;

                if (sol.Representanteid != null)
                    RepresentanteNombre = sol.Representante.UsuarioNomCompleto;

                color = "#2ECC71";//verde
                //color = "#F4D03F"; amariillo

                CalendarioViewModel evento = new CalendarioViewModel
                {
                    // PROPIEDADES EXTENDIDAS
                    SolicitudId = (int)sol.SolicitudId,
                    SolicitudTipoPoliza = sol.SolicitudTipoPoliza,
                    SolicitudTipoRegimen = sol.SolicitudTipoRegimen,
                    SolicitudNombreProp = sol.SolicitudNombreProp,
                    SolicitudApePaternoProp = sol.SolicitudApePaternoProp,
                    SolicitudApeMaternoProp = sol.SolicitudApeMaternoProp,
                    SolicitudRepresentanteLegal = sol.SolicitudRepresentanteLegal,
                    SolicitudApePaternoLegal = sol.SolicitudApePaternoLegal,
                    SolicitudApeMaternoLegal = sol.SolicitudApeMaternoLegal,
                    ArrendatarioNombre = sol.ArrendatarioNombre,
                    ArrendatarioApePat = sol.ArrendatarioApePat,
                    ArrendatarioApeMat = sol.ArrendatarioApeMat,
                    SolicitudLugarFirma = sol.SolicitudLugarFirma,
                    AsesorNombre = AsesorNombre,
                    RepresentanteNombre = RepresentanteNombre,
                    BackgroundColor = color,

                    // PROPIEDADES DE FULL CALENDAR
                    Title = sol.SolicitudTipoPoliza,
                    Start = sol.SolicitudHoraFirma,
                    End = sol.SolicitudHoraFirma.AddHours(3) // TEMPORAL

                    /*
                     Para propiedades adicionales 
                        Url = sol.Url,
                        Color = sol.Color,
                        BorderColor = sol.BorderColor,
                        TextColor = sol.TextColor,
                        BackgroundColor = sol.BackgroundColor,
                        ClassName = sol.ClassName
                    */
                };                
                eventos.Add(evento);

                //agregamos los eventos
                
            }

            foreach (var f in firmas)
            {
                color = "#5DADE2";

                CalendarioViewModel e = new CalendarioViewModel
                {
                    // PROPIEDADES EXTENDIDAS
                    SolicitudLugarFirma = f.Lugar,
                    BackgroundColor = color,

                    // PROPIEDADES DE FULL CALENDAR
                    Title = f.Lugar,
                    Start = f.FechaFirma,
                    End = f.FechaFirma.AddHours(3) // TEMPORAL
                    /*
                     Para propiedades adicionales 
                        Url = sol.Url,
                        Color = sol.Color,
                        BorderColor = sol.BorderColor,
                        TextColor = sol.TextColor,
                        BackgroundColor = sol.BackgroundColor,
                        ClassName = sol.ClassName
                    */
                };

                eventos.Add(e);
            }

            return eventos;
        }


        // GET: Calendarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendario = await _context.Calendario
                .Include(c => c.Usuarios)
                .Include(c => c.FisicaMoral)
                .FirstOrDefaultAsync(m => m.CalendarioId == id);
            if (calendario == null)
            {
                return NotFound();
            }

            return View(calendario);
        }

        // GET: Calendarios/Create
        public IActionResult Create()
        {
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "UsuariosId", "UsuariosId");
            ViewData["FisicaMoralId"] = new SelectList(_context.FisicaMoral, "FisicaMoralId", "FisicaMoralId");
            return View();
        }

        // POST: Calendarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CalendarioId,CalendarioFechaFirma,CalendarioUbicación,CalendarioEstatus,CalendarioDescripcion,UsuariosId,FisicaMoralId")] Calendario calendario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calendario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "UsuariosId", "UsuariosId", calendario.UsuariosId);
            ViewData["FisicaMoralId"] = new SelectList(_context.FisicaMoral, "FisicaMoralId", "FisicaMoralId", calendario.FisicaMoralId);
            return View(calendario);
        }

        // GET: Calendarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendario = await _context.Calendario.FindAsync(id);
            if (calendario == null)
            {
                return NotFound();
            }
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "UsuariosId", "UsuariosId", calendario.UsuariosId);
            ViewData["FisicaMoralId"] = new SelectList(_context.FisicaMoral, "FisicaMoralId", "FisicaMoralId", calendario.FisicaMoralId);
            return View(calendario);
        }

        // POST: Calendarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CalendarioId,CalendarioFechaFirma,CalendarioUbicación,CalendarioEstatus,CalendarioDescripcion,UsuariosId,FisicaMoralId")] Calendario calendario)
        {
            if (id != calendario.CalendarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calendario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalendarioExists(calendario.CalendarioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "UsuariosId", "UsuariosId", calendario.UsuariosId);
            ViewData["FisicaMoralId"] = new SelectList(_context.FisicaMoral, "FisicaMoralId", "FisicaMoralId", calendario.FisicaMoralId);
            return View(calendario);
        }

        // GET: Calendarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendario = await _context.Calendario
                .Include(c => c.Usuarios)
                .Include(c => c.FisicaMoral)
                .FirstOrDefaultAsync(m => m.CalendarioId == id);
            if (calendario == null)
            {
                return NotFound();
            }

            return View(calendario);
        }

        //// POST: Calendarios/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var calendario = await _context.Calendario.FindAsync(id);
        //    _context.Calendario.Remove(calendario);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool CalendarioExists(int id)
        {
            return _context.Calendario.Any(e => e.CalendarioId == id);
        }


        // GET: RefArrens/Details/5
        public async Task<List<Calendario>> EditAjax(int? id)
        {
            List<Calendario> ListaCalendario = new List<Calendario>();
            var appCalendario = await _context.Calendario.SingleOrDefaultAsync(c => c.CalendarioId == id);
            ListaCalendario.Add(appCalendario);
            return ListaCalendario;
        }

        public async Task<String> EditRefArrenAjax(int CalendarioId, DateTime CalendarioFechaFirma, string CalendarioUbicación, string CalendarioEstatus, string CalendarioDescripcion, int FisicaMoralId, int UsuariosId, Calendario calendario)
        {
            calendario = new Calendario
            {
                CalendarioId = CalendarioId,
                CalendarioFechaFirma = CalendarioFechaFirma,
                CalendarioUbicacion = CalendarioUbicación,
                CalendarioEstatus = CalendarioEstatus,
                CalendarioDescripcion = CalendarioDescripcion,
                FisicaMoralId = FisicaMoralId,
                UsuariosId = UsuariosId
            };
            _context.Update(calendario);
            var result = await _context.SaveChangesAsync();
            return "Save";
        }

        public async Task<String> Insertar(int CalendarioId, DateTime CalendarioFechaFirma, string CalendarioUbicación, string CalendarioEstatus, string CalendarioDescripcion, int FisicaMoralId, int UsuariosId, Calendario calendario)
        {
            calendario = new Calendario
            {
                CalendarioId = CalendarioId,
                CalendarioFechaFirma = CalendarioFechaFirma,
                CalendarioUbicacion = CalendarioUbicación,
                CalendarioEstatus = CalendarioEstatus,
                CalendarioDescripcion = CalendarioDescripcion,
                FisicaMoralId = FisicaMoralId,
                UsuariosId = UsuariosId
            };
            _context.Add(calendario);
            var result = await _context.SaveChangesAsync();
            return "Save";
        }
        public async Task<String> DeleteConfirmed(int id)
        {
            var calendario = await _context.Calendario.SingleOrDefaultAsync(c => c.CalendarioId == id);
            _context.Calendario.Remove(calendario);
            await _context.SaveChangesAsync();
            return "Save";
        }
    }
}
