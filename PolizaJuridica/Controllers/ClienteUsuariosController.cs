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
    public class ClienteUsuariosController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public ClienteUsuariosController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: ClienteUsuarios
        public async Task<IActionResult> Index(int? id)
        {
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            var area = User.FindFirst("Area").Value;
            ViewData["RepresentacionId"] = new SelectList(_context.Usuarios, "RepresentacionId", "RepresentacionNombre");
            if (area == "Administración")
            {
                var polizaJuridicaDbContext = _context.ClienteUsuario.Include(c => c.Usuarios).Include(c => c.Usuarios.Representacion).Where( c => c.Usuarios.RepresentacionId == id);
                return View(await polizaJuridicaDbContext.ToListAsync());
            }
            else
            {
                var polizaJuridicaDbContext = _context.ClienteUsuario.Include(c => c.Usuarios).Where(c => c.UsuariosId == usuarioid);
                return View(await polizaJuridicaDbContext.ToListAsync());
            }
            
        }

        // GET: ClienteUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteUsuario = await _context.ClienteUsuario
                .Include(c => c.Usuarios)
                .FirstOrDefaultAsync(m => m.ClienteUsuario1 == id);
            if (clienteUsuario == null)
            {
                return NotFound();
            }

            return View(clienteUsuario);
        }

        // GET: ClienteUsuarios/Create
        public IActionResult Create()
        {
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "UsuariosId", "UsuarioNomCompleto");
            return View();
        }

        // POST: ClienteUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteUsuario1,Nombre,ApellidoPaterno,ApellidoMaterno,Celular,Correo,UsuariosId,FechaCreacion")] ClienteUsuario clienteUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "UsuariosId", "UsuarioNomCompleto", clienteUsuario.UsuariosId);
            return View(clienteUsuario);
        }

        // GET: ClienteUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteUsuario = await _context.ClienteUsuario.FindAsync(id);
            if (clienteUsuario == null)
            {
                return NotFound();
            }
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "UsuariosId", "UsuarioNomCompleto", clienteUsuario.UsuariosId);
            return View(clienteUsuario);
        }

        // POST: ClienteUsuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteUsuario1,Nombre,ApellidoPaterno,ApellidoMaterno,Celular,Correo,UsuariosId,FechaCreacion")] ClienteUsuario clienteUsuario)
        {
            if (id != clienteUsuario.ClienteUsuario1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteUsuarioExists(clienteUsuario.ClienteUsuario1))
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
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "UsuariosId", "UsuarioNomCompleto", clienteUsuario.UsuariosId);
            return View(clienteUsuario);
        }

        // GET: ClienteUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteUsuario = await _context.ClienteUsuario
                .Include(c => c.Usuarios)
                .FirstOrDefaultAsync(m => m.ClienteUsuario1 == id);
            if (clienteUsuario == null)
            {
                return NotFound();
            }

            return View(clienteUsuario);
        }

        // POST: ClienteUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteUsuario = await _context.ClienteUsuario.FindAsync(id);
            _context.ClienteUsuario.Remove(clienteUsuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteUsuarioExists(int id)
        {
            return _context.ClienteUsuario.Any(e => e.ClienteUsuario1 == id);
        }
    }
}
