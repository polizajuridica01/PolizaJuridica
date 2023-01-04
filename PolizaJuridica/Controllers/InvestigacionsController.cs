using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PolizaJuridica.Data;
using static PolizaJuridica.ViewModels.BuholegalViewModel;

namespace PolizaJuridica.Controllers
{
    public class InvestigacionsController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public InvestigacionsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Investigacions
        public async Task<IActionResult> Index(int Id)
        {
            var fisicaMoral = (await _context.FisicaMoral.Include(f => f.Solicitud).SingleOrDefaultAsync(f => f.FisicaMoralId == Id));
            ViewBag.Id = Id;
            ViewBag.TipoRegimen = fisicaMoral.Solicitud.TipoArrendatario;
            var polizaJuridicaDbContext = _context.Investigacion.Include(d => d.DetalleInvestigacion).Where(d => d.FisicaMoralId == Id);
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        // GET: Investigacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigacion = await _context.Investigacion
                .Include(i => i.FisicaMoral)
                .FirstOrDefaultAsync(m => m.InvestigacionId == id);
            if (investigacion == null)
            {
                return NotFound();
            }

            return View(investigacion);
        }

        // GET: Investigacions/Create
        public IActionResult Create()
        {
            ViewData["FisicaMoralId"] = new SelectList(_context.FisicaMoral, "FisicaMoralId", "FisicaMoralId");
            return View();
        }

        // POST: Investigacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvestigacionId,Criterio,FisicaMoralId,FechaInicial,Fec,FechaFinal,Nombre,Entidad,Detalle")] Investigacion investigacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(investigacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FisicaMoralId"] = new SelectList(_context.FisicaMoral, "FisicaMoralId", "FisicaMoralId", investigacion.FisicaMoralId);
            return View(investigacion);
        }

        // GET: Investigacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigacion = await _context.Investigacion.FindAsync(id);
            if (investigacion == null)
            {
                return NotFound();
            }
            ViewData["FisicaMoralId"] = new SelectList(_context.FisicaMoral, "FisicaMoralId", "FisicaMoralId", investigacion.FisicaMoralId);
            return View(investigacion);
        }

        // POST: Investigacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvestigacionId,Criterio,FisicaMoralId,FechaInicial,Fec,FechaFinal,Nombre,Entidad,Detalle")] Investigacion investigacion)
        {
            if (id != investigacion.InvestigacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(investigacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestigacionExists(investigacion.InvestigacionId))
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
            ViewData["FisicaMoralId"] = new SelectList(_context.FisicaMoral, "FisicaMoralId", "FisicaMoralId", investigacion.FisicaMoralId);
            return View(investigacion);
        }

        // GET: Investigacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigacion = await _context.Investigacion
                .Include(i => i.FisicaMoral)
                .FirstOrDefaultAsync(m => m.InvestigacionId == id);
            if (investigacion == null)
            {
                return NotFound();
            }

            return View(investigacion);
        }

        // POST: Investigacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var investigacion = await _context.Investigacion.FindAsync(id);
            _context.Investigacion.Remove(investigacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvestigacionExists(int id)
        {
            return _context.Investigacion.Any(e => e.InvestigacionId == id);
        }

        public async Task<IActionResult> Investigar(int id)
        {
            var investigacion = await _context.Investigacion.FindAsync(id);
            var APIKEY = _context.Parametros.FirstOrDefault(p => p.ParametroValor == "APIKEYBUHOLEGAL");
            if (investigacion != null && APIKEY != null)
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(investigacion.FechaFinal);
                    DateTime dti = Convert.ToDateTime(investigacion.FechaInicial);
                    string uriString = APIKEY.ParametroValorNumerico.Trim();
                    WebClient client = new WebClient();
                    client.QueryString.Add("apikey", APIKEY.ParametroValor1.Trim());
                    client.QueryString.Add("criterio", investigacion.Criterio.Trim());
                    client.QueryString.Add("fecha_final", dt.ToString("yyyy-MM-dd"));
                    client.QueryString.Add("fecha_inicial", dti.ToString("yyyy-MM-dd"));
                    client.QueryString.Add("nombre", investigacion.Nombre.Trim());
                    client.QueryString.Add("entidad", investigacion.Entidad.Trim());//Entidad : todas
                    client.QueryString.Add("detalle", investigacion.Detalle.ToString().Trim());//detalle : 1
                    string reply = client.DownloadString(uriString);//URL del Endpoint
                                                                                               //string reply = _context.Parametros.FirstOrDefault(s => s.ParametroValor == "pruebabuho").ParametroValorNumerico;
                    var resp = JsonConvert.DeserializeObject<BuhoLegalResponse>(reply);
                    if(resp.query.numero_resultados == 0)
                    {
                        investigacion.Cantidad = 0;
                        _context.Investigacion.Update(investigacion);
                        _context.SaveChanges();
                    }
                    else
                    {
                        investigacion.Cantidad = resp.query.numero_resultados;
                        _context.Investigacion.Update(investigacion);
                        _context.SaveChanges();
                    }

                    foreach (var i in resp.results)
                    {
                        foreach (var o in i.Value)
                        {
                            DetalleInvestigacion di = new DetalleInvestigacion
                            {
                                Entidad = o.entidad,
                                Tribunal = o.tribunal,
                                Tipo = o.tipo,
                                Fuente = o.fuente,
                                Fuero = o.fuero,
                                Fecha = DateTime.ParseExact(o.fecha, "dd-MM-yyyy", null),
                                Actor = o.actor,
                                Circuitoid = o.circuito_id,
                                Demandado = o.demandado,
                                Expediente = o.expediente,
                                Juzgadoid = o.juzgado_id,
                                Juzgado = o.juzgado,
                                InvestigacionId = investigacion.InvestigacionId
                            };
                            _context.DetalleInvestigacion.Add(di);
                            _context.SaveChanges();

                            if (o.acuerdos.Count() > 0)
                                foreach (var a in o.acuerdos)
                                {
                                    Data.Acuerdos acu = new Data.Acuerdos
                                    {
                                        Acuerdo = a.acuerdo,
                                        Fecha = DateTime.ParseExact(a.fecha, "dd-MM-yyyy", null),
                                        Juicio = a.juicio,
                                        DetalleInvestigacionId = di.Id
                                    };
                                    _context.Acuerdos.Add(acu);
                                    _context.SaveChanges();
                                }
                        }
                    }

                    Log l = new Log
                    {
                        LogFecha = DateTime.Now,
                        LogObjetoIn = "FisicaMoralId:" + investigacion.FisicaMoralId.ToString().Trim() + " Id: " + investigacion.InvestigacionId.ToString().Trim(),
                        LogObjetoOut = reply.Trim(),
                        LogProceso = "BuhoLegal"
                    };

                    _context.Log.Add(l);
                    _context.SaveChanges();
                }
                catch(Exception e)
                {
                    Log l = new Log
                    {
                        LogFecha = DateTime.Now,
                        LogObjetoIn = "FisicaMoralId:" + investigacion.FisicaMoralId.ToString().Trim() + " Id: " + investigacion.InvestigacionId.ToString().Trim(),
                        LogObjetoOut = e.ToString(),
                        LogProceso = "BuhoLegal"
                    };

                    _context.Log.Add(l);
                    _context.SaveChanges();
                }
            
                //ahora lo insertamos en el costo de la póliza en caso de no existir generamos la póliza
                var poliza = _context.Poliza.SingleOrDefault(p => p.FisicaMoralId == investigacion.FisicaMoralId);
                var usuarioId = Int32.Parse(User.FindFirst("Id").Value);
                decimal CostoInvestigacion = Convert.ToDecimal(_context.Parametros.SingleOrDefault(p => p.ParametroValor == "BuhoLegal").ParametroValorNumerico);
                if (poliza == null)
                {
                    Poliza polizac = new Poliza()
                    {
                        Creacion = DateTime.Now,
                        FisicaMoralId = investigacion.FisicaMoralId,
                    };
                    _context.Poliza.Add(polizac);
                    _context.SaveChanges();

                    var tipoProceso = _context.TipoProcesoPo.Where(t => t.Orden == 1).SingleOrDefault();

                    UsuarioPoliza up = new UsuarioPoliza()
                    {
                        UsuariosId = usuarioId,
                        PolizaId = polizac.PolizaId,
                        Fecha = DateTime.Now,
                        TipoProcesoPoId = tipoProceso.TipoProcesoPoId
                    };
                    _context.UsuarioPoliza.Add(up);
                    _context.SaveChanges();

                    //agregamos el concepto y su costo a la póliza
                    DetallePoliza dp = new DetallePoliza()
                    {
                        CategoriaEsid = 14, // BuhoLEgal
                        Importe = CostoInvestigacion,
                        PolizaId = polizac.PolizaId,

                    };
                    _context.DetallePoliza.Add(dp);
                    _context.SaveChanges();
                }
                else
                {
                    DetallePoliza dp = new DetallePoliza()
                    {
                        CategoriaEsid = 14, // BuhoLEgal
                        Importe = CostoInvestigacion,
                        PolizaId = poliza.PolizaId,

                    };
                    _context.DetallePoliza.Add(dp);
                    _context.SaveChanges();

                }
            }
            return RedirectToAction(nameof(Index),new {id = investigacion.FisicaMoralId });
        }

        public void InsertaCandidatos(int id,string nombre)
        {
            var parametros = _context.Parametros.FirstOrDefault(p => p.ParametroValor == "APIKEYBUHOLEGAL");
            if (parametros != null)
            {
                DateTime dt = Convert.ToDateTime(DateTime.Now.AddYears(-10));
                Investigacion i = new Investigacion
                {
                    Criterio = parametros.ParametroValor4.Trim(),
                    FisicaMoralId = id,
                    FechaInicial = dt,
                    FechaFinal = DateTime.Now,
                    Nombre = nombre.Trim(),
                    Entidad = parametros.ParametroValor2.Trim(),
                    Detalle = Int32.Parse(parametros.ParametroValor3.Trim()),
                    Cantidad = null
                };

                _context.Investigacion.Add(i);
                _context.SaveChanges();
            }
            
        }

    }
}
