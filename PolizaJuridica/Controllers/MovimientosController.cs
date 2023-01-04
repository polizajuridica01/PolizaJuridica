using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PolizaJuridica.Data;
using PolizaJuridica.ViewModels;

namespace PolizaJuridica.Controllers
{
    public class MovimientosController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public MovimientosController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Movimientos
        public async Task<IActionResult> Index()
        {
            ViewData["Cuentas"] = _context.CuentasBancarias.Where(c => c.Estatus == 1).ToList();//donde las cuentas esten activas
            ViewData["MovimientoMaestro"] = _context.MovimientoMaestro.Include(mm => mm.Usuario).ToList();
            var polizaJuridicaDbContext = _context.Movimientos.Include(m => m.Cc).Include(m => m.Cp).Include(m => m.DetallePoliza).Include(m => m.Dm).Include(m => m.IdPadreNavigation).Include(m => m.Mm);
            ViewData["Cuenta"] = new SelectList(_context.CuentasBancarias, "CuentaId", "Nombre");
            return View(await polizaJuridicaDbContext.ToListAsync());
        }

        // GET: Movimientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimientos = await _context.Movimientos
                .Include(m => m.Cc)
                .Include(m => m.Cp)
                .Include(m => m.DetallePoliza)
                .Include(m => m.Dm)
                .Include(m => m.IdPadreNavigation)
                .Include(m => m.Mm)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimientos == null)
            {
                return NotFound();
            }

            return View(movimientos);
        }

        // GET: Movimientos/Create
        public IActionResult Create()
        {
            ViewData["Ccid"] = new SelectList(_context.CuentasXcobrar, "Ccid", "Ccid");
            ViewData["Cpid"] = new SelectList(_context.CuentasXpagar, "Cpid", "Cpid");
            ViewData["DetallePolizaId"] = new SelectList(_context.DetallePoliza, "DetallePolizaId", "DetallePolizaId");
            ViewData["Dmid"] = new SelectList(_context.DetalleMovimiento, "Id", "Id");
            ViewData["IdPadre"] = new SelectList(_context.Movimientos, "Id", "Id");
            ViewData["Mmid"] = new SelectList(_context.MovimientoMaestro, "Id", "Id");
            return View();
        }

        // POST: Movimientos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DetallePolizaId,Mmid,IdPadre,Ccid,Cpid,Dmid")] Movimientos movimientos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimientos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ccid"] = new SelectList(_context.CuentasXcobrar, "Ccid", "Ccid", movimientos.Ccid);
            ViewData["Cpid"] = new SelectList(_context.CuentasXpagar, "Cpid", "Cpid", movimientos.Cpid);
            ViewData["DetallePolizaId"] = new SelectList(_context.DetallePoliza, "DetallePolizaId", "DetallePolizaId", movimientos.DetallePolizaId);
            ViewData["Dmid"] = new SelectList(_context.DetalleMovimiento, "Id", "Id", movimientos.Dmid);
            ViewData["IdPadre"] = new SelectList(_context.Movimientos, "Id", "Id", movimientos.IdPadre);
            ViewData["Mmid"] = new SelectList(_context.MovimientoMaestro, "Id", "Id", movimientos.Mmid);
            return View(movimientos);
        }

        // GET: Movimientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimientos = await _context.Movimientos.FindAsync(id);
            if (movimientos == null)
            {
                return NotFound();
            }
            ViewData["Ccid"] = new SelectList(_context.CuentasXcobrar, "Ccid", "Ccid", movimientos.Ccid);
            ViewData["Cpid"] = new SelectList(_context.CuentasXpagar, "Cpid", "Cpid", movimientos.Cpid);
            ViewData["DetallePolizaId"] = new SelectList(_context.DetallePoliza, "DetallePolizaId", "DetallePolizaId", movimientos.DetallePolizaId);
            ViewData["Dmid"] = new SelectList(_context.DetalleMovimiento, "Id", "Id", movimientos.Dmid);
            ViewData["IdPadre"] = new SelectList(_context.Movimientos, "Id", "Id", movimientos.IdPadre);
            ViewData["Mmid"] = new SelectList(_context.MovimientoMaestro, "Id", "Id", movimientos.Mmid);
            return View(movimientos);
        }

        // POST: Movimientos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DetallePolizaId,Mmid,IdPadre,Ccid,Cpid,Dmid")] Movimientos movimientos)
        {
            if (id != movimientos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimientos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimientosExists(movimientos.Id))
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
            ViewData["Ccid"] = new SelectList(_context.CuentasXcobrar, "Ccid", "Ccid", movimientos.Ccid);
            ViewData["Cpid"] = new SelectList(_context.CuentasXpagar, "Cpid", "Cpid", movimientos.Cpid);
            ViewData["DetallePolizaId"] = new SelectList(_context.DetallePoliza, "DetallePolizaId", "DetallePolizaId", movimientos.DetallePolizaId);
            ViewData["Dmid"] = new SelectList(_context.DetalleMovimiento, "Id", "Id", movimientos.Dmid);
            ViewData["IdPadre"] = new SelectList(_context.Movimientos, "Id", "Id", movimientos.IdPadre);
            ViewData["Mmid"] = new SelectList(_context.MovimientoMaestro, "Id", "Id", movimientos.Mmid);
            return View(movimientos);
        }

        // GET: Movimientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimientos = await _context.Movimientos
                .Include(m => m.Cc)
                .Include(m => m.Cp)
                .Include(m => m.DetallePoliza)
                .Include(m => m.Dm)
                .Include(m => m.IdPadreNavigation)
                .Include(m => m.Mm)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimientos == null)
            {
                return NotFound();
            }

            return View(movimientos);
        }

        // POST: Movimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimientos = await _context.Movimientos.FindAsync(id);
            _context.Movimientos.Remove(movimientos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimientosExists(int id)
        {
            return _context.Movimientos.Any(e => e.Id == id);
        }

        //GET de Movimientos
        public IActionResult GenerarMovimiento(int? id, int? CuentaId)
        {
            List<Movimientos> mov = new List<Movimientos>();
            List<int> dpid = new List<int>();
            List<int> ccid = new List<int>();
            List<int> cpid = new List<int>();
            List<int> dmid = new List<int>();

            if(id == 0 || id == null )
            {
                mov = _context.Movimientos.ToList();
                foreach (var m in mov)
                {
                    if (m.DetallePolizaId > 0)
                        dpid.Add(m.DetallePolizaId.Value);

                    if (m.Ccid > 0)
                        ccid.Add(m.Ccid.Value);

                    if (m.Cpid > 0)
                        cpid.Add(m.Cpid.Value);

                    if (m.Dmid > 0)
                        dmid.Add(m.Dmid.Value);
                }
                var Cuentas = _context.CuentasBancarias.SingleOrDefault(c => c.CuentaId == CuentaId);
                ViewBag.CuentaId = CuentaId;
                ViewBag.Cuenta = Cuentas.Nombre;
                ViewBag.Saldo = Cuentas.Saldo;
                ViewData["DetallePoliza"] = _context.DetallePoliza.Where(d => !dpid.Contains(d.DetallePolizaId)).Include(dp => dp.CategoriaEs).ToList();
                ViewData["CuentasXCobrar"] = _context.CuentasXcobrar.Where(cc => !ccid.Contains(cc.Ccid)).ToList();
                ViewData["CuentasXPagar"] = _context.CuentasXpagar.Where(cp => !cpid.Contains(cp.Cpid)).Include(cp => cp.Categoria).ToList();
                ViewData["DetalleMovimiento"] = _context.DetalleMovimiento.Where(dm => !dmid.Contains(dm.Id)).ToList();
            }

            if (id > 0)
            {
                mov = _context.Movimientos.Where(m => m.Mmid == id).ToList();
                foreach (var m in mov)
                {
                    if (m.DetallePolizaId > 0)
                        dpid.Add(m.DetallePolizaId.Value);

                    if (m.Ccid > 0)
                        ccid.Add(m.Ccid.Value);

                    if (m.Cpid > 0)
                        cpid.Add(m.Cpid.Value);

                    if (m.Dmid > 0)
                        dmid.Add(m.Dmid.Value);
                }
                ViewBag.CuentaId = CuentaId;
                ViewData["Cuenta"] = new SelectList(_context.CuentasBancarias, "CuentaId", "Nombre");
                ViewData["DetallePoliza"] = _context.DetallePoliza.Where(d => dpid.Contains(d.DetallePolizaId)).Include(dp => dp.CategoriaEs).ToList();
                ViewData["CuentasXCobrar"] = _context.CuentasXcobrar.Where(cc => ccid.Contains(cc.Ccid)).ToList();
                ViewData["CuentasXPagar"] = _context.CuentasXpagar.Where(cp => cpid.Contains(cp.Cpid)).Include(cp => cp.Categoria).ToList();
                ViewData["DetalleMovimiento"] = _context.DetalleMovimiento.Where(dm => dmid.Contains(dm.Id)).ToList();
            }

            return View();
        }

        [HttpPost]
        public ActionResult GenerarMovimiento(string datos)
        {

            if (datos == null)
                return BadRequest();

            ListMovimientoViewModel mv = new ListMovimientoViewModel();            
            mv = JsonConvert.DeserializeObject<ListMovimientoViewModel>(datos);
            decimal Entrada = 0;
            decimal Salida = 0;
            decimal Total = 0;
            decimal residuo = 0;
            decimal faltante = 0;
            decimal auxEntrada = 0;
            bool isBandera = false;
            int mmId = 0;
            int cantidad = 0;


            if (mv.trans.Count > 0)
            {
                cantidad = mv.trans.Count > cantidad ? mv.trans.Count : cantidad;
               isBandera = true;
                foreach (var t in mv.trans)
                {
                    Entrada = Entrada + Convert.ToDecimal(t.importe);
                }
            }

            if (mv.dp.Count > 0)
            {
                cantidad = mv.dp.Count > cantidad ? mv.dp.Count : cantidad;
                foreach (var dp in mv.dp)
                {
                    if (isBandera == true) {
                        auxEntrada = auxEntrada + Convert.ToDecimal(dp.importe);
                    } else
                    {
                        Entrada = Entrada + Convert.ToDecimal(dp.importe);
                    }

                }
            }


            if (mv.cxp.Count > 0)
            {
                cantidad = mv.cxp.Count > cantidad ? mv.cxp.Count : cantidad;
                foreach (var cxp in mv.cxp)
                {
                    Salida = Salida + Convert.ToDecimal(cxp.importe);
                }
            }

            if (mv.cxc.Count > 0)
            {
                cantidad = mv.cxc.Count > cantidad ? mv.cxc.Count : cantidad;
                foreach (var cxc in mv.cxc)
                {
                    if (isBandera == true)
                    {
                        auxEntrada = auxEntrada + Convert.ToDecimal(cxc.importe);
                    }
                    else
                    {
                        Entrada = Entrada + Convert.ToDecimal(cxc.importe);
                    }

                }
            }
       
            
            if (cantidad > 0 && isBandera == true)
            {

                    if (auxEntrada == Entrada)
                    {
                        MovimientoMaestro mm = new MovimientoMaestro
                        {
                            CuentaId = mv.Cuenta,
                            Entrada = Entrada,
                            Salida = Salida,
                            Fecha = DateTime.Now
                        };

                        _context.MovimientoMaestro.Add(mm);
                        _context.SaveChanges();
                        mmId = mm.Id;

                    }

                //actualizamos el saldo de la cuenta
 
                if(mmId > 0)
                {
                    cantidad = cantidad == 1 ? 0 : cantidad;
                    List<Movimientos> lmv = new List<Movimientos>();

                    for (int i = 0; i < cantidad; i++)
                    {
                        Movimientos movi = new Movimientos();
                        if (mv.dp.Count > 0 && i < mv.dp.Count)
                            movi.DetallePolizaId = Convert.ToInt32(mv.dp[i].id);
                        if (mv.cxp.Count > 0 && i < mv.cxp.Count)
                            movi.Cpid = Convert.ToInt32(mv.cxp[i].id);
                        if (mv.trans.Count > 0 && i < mv.trans.Count)
                            movi.Dmid = Convert.ToInt32(mv.trans[i].id);
                        if (mv.cxc.Count > 0 && i < mv.cxc.Count)
                            movi.Ccid = Convert.ToInt32(mv.cxc[i].id);
                        lmv.Add(movi);
                    }
                    System.Diagnostics.Debug.WriteLine("entro al proceso");
                    System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(lmv));
                }

            }           
            return Ok();
        }

    }
}
