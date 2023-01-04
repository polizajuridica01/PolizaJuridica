using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PolizaJuridica.Data;

namespace PolizaJuridica.Controllers
{
    public class PolizasAnterioresController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public PolizasAnterioresController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: PolizasAnteriores
        public async Task<IActionResult> Index()
        {
            ViewBag.Cantidad = _context.PolizasAnteriores.Count();
            var Cantidad = 10;
            return View(await _context.PolizasAnteriores.Take(Cantidad).ToListAsync());
        }

        // GET: PolizasAnteriores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polizasAnteriores = await _context.PolizasAnteriores
                .FirstOrDefaultAsync(m => m.PolizasAnterioresId == id);
            if (polizasAnteriores == null)
            {
                return NotFound();
            }

            return View(polizasAnteriores);
        }

        // GET: PolizasAnteriores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PolizasAnteriores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolizasAnterioresId,NombreInq,DirecciónInq,TeléfonoInq,DirecciónProp,TeléfonoProp,NombreInmob,NombreDelAgente,UsoInmueb,NombreProp,DirecciónInmue,NombreAval,DirecciónAval,TeléfonoAval,ImporteRenta,FechaFirmaContrato,CostoPóliza,VigenciaContrato,VigenciaPóliza,Municipio,PrimerDiaVencimiento,DiaDePagoRenta,DiaDeEntrega,NúmeroPóliza,IdentificadoComo,EscrituraNo,Volumen,Libro,Seccion,DeFecha,NomNotario,NumNotario,MunDel,Partida,FechaRpp,IfePro,IfeInq,IfeAval,EmailProp,EmailInquilino,EmailAgenteInmob,NombreFiador,EmailFiador,OficinaPj,EjecutivoPj,AgenteInmobiliario,DireccionDelFiador,TelefonoFiador,TelInmobiliaria")] PolizasAnteriores polizasAnteriores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(polizasAnteriores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(polizasAnteriores);
        }

        // GET: PolizasAnteriores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polizasAnteriores = await _context.PolizasAnteriores.FindAsync(id);
            if (polizasAnteriores == null)
            {
                return NotFound();
            }
            return View(polizasAnteriores);
        }

        // POST: PolizasAnteriores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PolizasAnterioresId,NombreInq,DirecciónInq,TeléfonoInq,DirecciónProp,TeléfonoProp,NombreInmob,NombreDelAgente,UsoInmueb,NombreProp,DirecciónInmue,NombreAval,DirecciónAval,TeléfonoAval,ImporteRenta,FechaFirmaContrato,CostoPóliza,VigenciaContrato,VigenciaPóliza,Municipio,PrimerDiaVencimiento,DiaDePagoRenta,DiaDeEntrega,NúmeroPóliza,IdentificadoComo,EscrituraNo,Volumen,Libro,Seccion,DeFecha,NomNotario,NumNotario,MunDel,Partida,FechaRpp,IfePro,IfeInq,IfeAval,EmailProp,EmailInquilino,EmailAgenteInmob,NombreFiador,EmailFiador,OficinaPj,EjecutivoPj,AgenteInmobiliario,DireccionDelFiador,TelefonoFiador,TelInmobiliaria")] PolizasAnteriores polizasAnteriores)
        {
            if (id != polizasAnteriores.PolizasAnterioresId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(polizasAnteriores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolizasAnterioresExists(polizasAnteriores.PolizasAnterioresId))
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
            return View(polizasAnteriores);
        }

        // GET: PolizasAnteriores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polizasAnteriores = await _context.PolizasAnteriores
                .FirstOrDefaultAsync(m => m.PolizasAnterioresId == id);
            if (polizasAnteriores == null)
            {
                return NotFound();
            }

            return View(polizasAnteriores);
        }

        // POST: PolizasAnteriores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var polizasAnteriores = await _context.PolizasAnteriores.FindAsync(id);
            _context.PolizasAnteriores.Remove(polizasAnteriores);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolizasAnterioresExists(int id)
        {
            return _context.PolizasAnteriores.Any(e => e.PolizasAnterioresId == id);
        }
    }
}
