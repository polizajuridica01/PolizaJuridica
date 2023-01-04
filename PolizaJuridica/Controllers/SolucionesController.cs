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
    public class SolucionesController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public SolucionesController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Soluciones
        public async Task<IActionResult> Index()
        {
            var UsuarioId = Int32.Parse(User.FindFirst("Id").Value);
            var RepresentacionId = Int32.Parse(User.FindFirst("RepresentacionId").Value);
            var area = User.FindFirst("Area").Value;
            List<Soluciones> soluciones = new List<Soluciones>();

            if (area == "Administración" || area == "Soluciones")
            {
                soluciones = await _context.Soluciones.Include(s => s.Estado).Include(s => s.Usuario.Representacion).Include(s => s.Poliza.FisicaMoral).Include(s => s.ProcesoSoluciones).ToListAsync();
            }
            else
            {
                soluciones = await _context.Soluciones.Include(s => s.Estado).Include(s => s.Usuario.Representacion).Include(s => s.Poliza.FisicaMoral).Include(s => s.ProcesoSoluciones).Where(s => s.Usuario.RepresentacionId == RepresentacionId).ToListAsync();
            }
                
            return View(soluciones);
        }

        // GET: Soluciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soluciones = await _context.Soluciones
                .Include(s => s.Estado)
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.SolucionesId == id);
            if (soluciones == null)
            {
                return NotFound();
            }

            return View(soluciones);
        }

        // GET: Soluciones/Create
        public IActionResult Create(int? Id)
        {
            var Poliza = _context.Poliza.Include(p => p.FisicaMoral).Include(p => p.FisicaMoral.Solicitud.Representante).Include(p => p.FisicaMoral.Solicitud.Asesor).Where(p => p.PolizaId == Id).SingleOrDefault();

            var UsuarioId = Int32.Parse(User.FindFirst("Id").Value);
            ViewBag.UsuarioId = UsuarioId;

            if (Poliza != null)
            {
                ViewData["EstadoId"] = new SelectList(_context.Estados.Where(e => e.EstadosId == Poliza.FisicaMoral.Solicitud.EstadosId), "EstadosId", "EstadoNombre");
                Soluciones soluciones = new Soluciones()
                {
                    FechaCreacion = DateTime.Now,
                    PolizaId = Id,
                    NombrePro = Poliza.FisicaMoral.Solicitud.SolicitudNombreProp,
                    ApellidoPatPro = Poliza.FisicaMoral.Solicitud.SolicitudApePaternoProp,
                    ApellidoMatPro = Poliza.FisicaMoral.Solicitud.SolicitudApeMaternoProp,
                    DireccionInmueble = Poliza.FisicaMoral.Solicitud.SolicitudUbicacionArrendado,
                    ColoniaInmueble = Poliza.FisicaMoral.Solicitud.ColoniaArrendar,
                    AlcaldiaMunicipioInmueble = Poliza.FisicaMoral.Solicitud.AlcaldiaMunicipioArrendar,
                    EstadoId = Poliza.FisicaMoral.Solicitud.EstadosId,
                    Cpinmueble = Poliza.FisicaMoral.Solicitud.CodigoPostalArrendar,
                    FechaContratoI = Poliza.FisicaMoral.Solicitud.SolicitudVigenciaContratoI,
                    UsuarioId = UsuarioId,
                    CelularProp = Poliza.FisicaMoral.Solicitud.SolicitudCelularProp,
                    EmailProp = Poliza.FisicaMoral.Solicitud.SolicitudEmailProp,
                    NombreArren = Poliza.FisicaMoral.SfisicaNombre,
                    ApellidoPatArren = Poliza.FisicaMoral.SfisicaApePat,
                    ApellidoMatArren = Poliza.FisicaMoral.SfisicaApeMat,
                    CelularArrem = Poliza.FisicaMoral.SfisicaCelular,
                    EmailArren = Poliza.FisicaMoral.SfisicaEmail,
                    FechaContratoF = Poliza.FisicaMoral.Solicitud.SolicitudVigenciaContratoF,
                    TipoPoliza = Poliza.FisicaMoral.Solicitud.SolicitudTipoPoliza,

                };
                return View(soluciones);
            }
            else
            {
                ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadosId", "EstadoNombre");
                Soluciones soluciones = new Soluciones()
                {
                    FechaCreacion = DateTime.Now,
                    UsuarioId = UsuarioId,
                };
                return View(soluciones);
            }
            
        }

        // POST: Soluciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SolucionesId,FechaCreacion,PolizaAnterior,PolizaId,NombrePro,ApellidoPatPro,ApellidoMatPro,DireccionInmueble,ColoniaInmueble,AlcaldiaMunicipioInmueble,EstadoId,Cpinmueble,DescripcionProblema,FechaArrendatario,Argumenta,FechaContratoI,UsuarioId,CelularProp,EmailProp,NombreArren,ApellidoPatArren,ApellidoMatArren,CelularArrem,EmailArren,NombreFiador,ApellidoPatFiador,ApellidoMatFiador,CelularFiador,EmailFiador,FechaContratoF,TipoPoliza")] Soluciones soluciones)
        {

            bool isError = false;
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();

            //metemos todas las validaciones sobre campos mandatorios
             if (soluciones.FechaContratoI == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FechaContratoI"));
                isError = true;
            }
            if (soluciones.FechaContratoF == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FechaContratoF"));
            }

            //arrendador
            if (soluciones.NombrePro == null)
            {
                Error.Add(Mensajes.ErroresAtributos("NombrePro"));
                isError = true;
            }
            if (soluciones.ApellidoPatPro == null)
            {
                Error.Add(Mensajes.ErroresAtributos("ApellidoPatPro"));
                isError = true;
            }
            if (soluciones.CelularProp == null)
            {
                Error.Add(Mensajes.ErroresAtributos("CelularProp"));
                isError = true;
            }
            if (soluciones.EmailProp == null)
            {
                Error.Add(Mensajes.ErroresAtributos("EmailProp"));
                isError = true;
            }

            //arendatarios
            if (soluciones.NombreArren == null)
            {
                Error.Add(Mensajes.ErroresAtributos("NombreArren"));
                isError = true;
            }
            if (soluciones.ApellidoPatArren == null)
            {
                Error.Add(Mensajes.ErroresAtributos("ApellidoPatArren"));
                isError = true;
            }
            if (soluciones.CelularArrem == null)
            {
                Error.Add(Mensajes.ErroresAtributos("CelularArrem"));
                isError = true;
            }
            if (soluciones.EmailArren == null)
            {
                Error.Add(Mensajes.ErroresAtributos("EmailArren"));
                isError = true;
            }

            //dirección del inmueble
            if (soluciones.DireccionInmueble == null)
            {
                Error.Add(Mensajes.ErroresAtributos("DireccionInmueble"));
                isError = true;
            }
            if (soluciones.ColoniaInmueble == null)
            {
                Error.Add(Mensajes.ErroresAtributos("ColoniaInmueble"));
                isError = true;
            }
            if (soluciones.AlcaldiaMunicipioInmueble == null)
            {
                Error.Add(Mensajes.ErroresAtributos("AlcaldiaMunicipioInmueble"));
                isError = true;
            }
            if (soluciones.Cpinmueble == null)
            {
                Error.Add(Mensajes.ErroresAtributos("Cpinmueble"));
                isError = true;
            }
            if (soluciones.EstadoId == null)
            {
                Error.Add(Mensajes.ErroresAtributos("EstadoId"));
                isError = true;
            }
            //problema
            if (soluciones.DescripcionProblema == null)
            {
                Error.Add(Mensajes.ErroresAtributos("DescripcionProblema"));
                isError = true;
            }
            if (soluciones.FechaArrendatario == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FechaArrendatario"));
                isError = true;
            }
            if (soluciones.Argumenta == null)
            {
                Error.Add(Mensajes.ErroresAtributos("Argumenta"));
                isError = true;
            }

            
            if (soluciones.TipoPoliza == null)
            {
                Error.Add(Mensajes.MensajesError("Tiene que seleccionar el tipo de Póliza"));
                isError = true;
            }

            if (isError == false)
            {
                soluciones.ProcesoSolucionesId = 1;
                _context.Add(soluciones);
                await _context.SaveChangesAsync();

                //agregamos la seccion de la estatus
                int id = soluciones.SolucionesId;

                UsuariosSoluciones usuariosSoluciones = new UsuariosSoluciones()
                {
                    UsuariosId = soluciones.UsuarioId,
                    SolucionesId = id,
                    Fecha = DateTime.Now,
                    ProcesoSolucionesId = 1
                };
                _context.Add(usuariosSoluciones);
                await _context.SaveChangesAsync();

                //agregamos a una lista negra al arrendatario y fiador

                    //Arrendatario
                string ApellidoMaternoArrendatario = string.Empty;

                if (soluciones.ApellidoMatArren != null)
                    ApellidoMaternoArrendatario = soluciones.ApellidoMatArren;

                Listanegra listanegra = new Listanegra()
                {
                    Nombres = soluciones.NombreArren,
                    ApellidoPaterno = soluciones.ApellidoPatArren,
                    ApellidoMaterno = ApellidoMaternoArrendatario,
                    RazonSocial = "",
                    Rfc = "",
                    Observaciones = soluciones.DescripcionProblema,
                    Estatus = 1,
                };
                _context.Add(listanegra);
                await _context.SaveChangesAsync();

                //Fiador
                string ApellidoMaternoFiador = string.Empty;

                if (soluciones.ApellidoMatFiador != null)
                    ApellidoMaternoFiador = soluciones.ApellidoMatArren;

                Listanegra listanegraFiador = new Listanegra()
                {
                    Nombres = soluciones.NombreFiador,
                    ApellidoPaterno = soluciones.ApellidoPatFiador,
                    ApellidoMaterno = ApellidoMaternoFiador,
                    RazonSocial = "",
                    Rfc = "",
                    Observaciones = soluciones.DescripcionProblema,
                    Estatus = 1,
                };

                _context.Add(listanegraFiador);
                await _context.SaveChangesAsync();
                //fin
                return RedirectToAction(nameof(Index));
            }
            Error.Add(Mensajes.MensajesError("Favor de completar los campos que estan en rojo"));
            ViewBag.Error = JsonConvert.SerializeObject(Error);
            ViewBag.Poliza = _context.Poliza.Include(p => p.FisicaMoral).Where(p => p.PolizaId == soluciones.PolizaId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadosId", "EstadoNombre", soluciones.EstadoId);
            return View(soluciones);
        }

        // GET: Soluciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var soluciones = await _context.Soluciones.FindAsync(id);
            if (soluciones == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadosId", "EstadosId", soluciones.EstadoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuariosId", "UsuariosId", soluciones.UsuarioId);
            return View(soluciones);
        }

        // POST: Soluciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SolucionesId,FechaCreacion,PolizaAnterior,PolizaId,NombrePro,ApellidoPatPro,ApellidoMatPro,DireccionInmueble,ColoniaInmueble,AlcaldiaMunicipioInmueble,EstadoId,Cpinmueble,DescripcionProblema,FechaArrendatario,Argumenta,FechaContratoI,UsuarioId,CelularProp,EmailProp,NombreArren,ApellidoPatArren,ApellidoMatArren,CelularArrem,EmailArren,NombreFiador,ApellidoPatFiador,ApellidoMatFiador,CelularFiador,EmailFiador,FechaContratoF,TipoPoliza  ")] Soluciones soluciones)
        {
            if (id != soluciones.SolucionesId)
            {
                return NotFound();
            }
            bool isError = false;
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();

            //metemos todas las validaciones sobre campos mandatorios
            if (soluciones.FechaContratoI == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FechaContratoI"));
                isError = true;
            }
            if (soluciones.FechaContratoF == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FechaContratoF"));
            }

            //arrendador
            if (soluciones.NombrePro == null)
            {
                Error.Add(Mensajes.ErroresAtributos("NombrePro"));
                isError = true;
            }
            if (soluciones.ApellidoPatPro == null)
            {
                Error.Add(Mensajes.ErroresAtributos("ApellidoPatPro"));
                isError = true;
            }
            if (soluciones.CelularProp == null)
            {
                Error.Add(Mensajes.ErroresAtributos("CelularProp"));
                isError = true;
            }
            if (soluciones.EmailProp == null)
            {
                Error.Add(Mensajes.ErroresAtributos("EmailProp"));
                isError = true;
            }

            //arendatarios
            if (soluciones.NombreArren == null)
            {
                Error.Add(Mensajes.ErroresAtributos("NombreArren"));
                isError = true;
            }
            if (soluciones.ApellidoPatArren == null)
            {
                Error.Add(Mensajes.ErroresAtributos("ApellidoPatArren"));
                isError = true;
            }
            if (soluciones.CelularArrem == null)
            {
                Error.Add(Mensajes.ErroresAtributos("CelularArrem"));
                isError = true;
            }
            if (soluciones.EmailArren == null)
            {
                Error.Add(Mensajes.ErroresAtributos("EmailArren"));
                isError = true;
            }

            //dirección del inmueble
            if (soluciones.DireccionInmueble == null)
            {
                Error.Add(Mensajes.ErroresAtributos("DireccionInmueble"));
                isError = true;
            }
            if (soluciones.ColoniaInmueble == null)
            {
                Error.Add(Mensajes.ErroresAtributos("ColoniaInmueble"));
                isError = true;
            }
            if (soluciones.AlcaldiaMunicipioInmueble == null)
            {
                Error.Add(Mensajes.ErroresAtributos("AlcaldiaMunicipioInmueble"));
                isError = true;
            }
            if (soluciones.Cpinmueble == null)
            {
                Error.Add(Mensajes.ErroresAtributos("Cpinmueble"));
                isError = true;
            }
            if (soluciones.EstadoId == null)
            {
                Error.Add(Mensajes.ErroresAtributos("EstadoId"));
                isError = true;
            }
            //problema
            if (soluciones.DescripcionProblema == null)
            {
                Error.Add(Mensajes.ErroresAtributos("DescripcionProblema"));
                isError = true;
            }
            if (soluciones.FechaArrendatario == null)
            {
                Error.Add(Mensajes.ErroresAtributos("FechaArrendatario"));
                isError = true;
            }
            if (soluciones.Argumenta == null)
            {
                Error.Add(Mensajes.ErroresAtributos("Argumenta"));
                isError = true;
            }
            if (soluciones.TipoPoliza == null)
            {
                Error.Add(Mensajes.ErroresAtributos("TipoPoliza"));
                isError = true;
            }
            if (isError == false)
            {
                try
                {
                    _context.Update(soluciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolucionesExists(soluciones.SolucionesId))
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
            Error.Add(Mensajes.MensajesError("Favor de completar los campos que estan en rojo"));
            ViewBag.Error = JsonConvert.SerializeObject(Error);
            ViewBag.Poliza = _context.Poliza.Include(p => p.FisicaMoral).Where(p => p.PolizaId == soluciones.PolizaId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadosId", "EstadoNombre", soluciones.EstadoId);
            return View(soluciones);
        }

        // GET: Soluciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soluciones = await _context.Soluciones
                .Include(s => s.Estado)
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.SolucionesId == id);
            if (soluciones == null)
            {
                return NotFound();
            }

            return View(soluciones);
        }

        // POST: Soluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soluciones = await _context.Soluciones.FindAsync(id);
            _context.Soluciones.Remove(soluciones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolucionesExists(int id)
        {
            return _context.Soluciones.Any(e => e.SolucionesId == id);
        }
    }
}
