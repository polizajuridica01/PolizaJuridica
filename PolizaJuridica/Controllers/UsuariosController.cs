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
using PolizaJuridica.Utilerias;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PolizaJuridica.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public UsuariosController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            Usuarios usuario = _context.Usuarios.Include(u => u.Area).SingleOrDefault(u => u.UsuariosId == usuarioid);
            if (usuario.Area.AreaDescripcion == "Representante")
            {
                var PolizaJuridicaDbContext = _context.Usuarios.Include(u => u.Area).Include(u => u.Representacion).Where(u => u.Area.AreaDescripcion == "Asesor" && u.RepresentacionId == usuario.RepresentacionId).Where(u => u.UsuarioPadreId == usuarioid);
                return View(await PolizaJuridicaDbContext.ToListAsync());
            }
            else
            {
                var PolizaJuridicaDbContext = _context.Usuarios.Include(u => u.Area).Include(u => u.Representacion);
                return View(await PolizaJuridicaDbContext.ToListAsync());
            }
        }

        // GET: Usuarios
        public async Task<IActionResult> DirectorioPoliza()
        {
            var PolizaJuridicaDbContext = _context.Usuarios.Include(u => u.Area).Include(u => u.Representacion).Where(u => u.RepresentacionId == 3);
            return View(await PolizaJuridicaDbContext.ToListAsync());

        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .Include(u => u.Area)
                .Include(u => u.Representacion)
                .FirstOrDefaultAsync(m => m.UsuariosId == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "AreaDescripcion");
            ViewData["RepresentacionId"] = new SelectList(_context.Representacion, "RepresentacionId", "RepresentacionNombre");
            ViewData["UsuariosPadre"] = new SelectList(_context.Usuarios.Where(u => u.UsuarioPadreId == null), "UsuariosId", "UsuarioNomCompleto");
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            Usuarios usuario = _context.Usuarios.Include(u => u.Area).Include(u => u.Representacion).SingleOrDefault(u => u.UsuariosId == usuarioid);
            ViewBag.UsuarioRepresentacionId = usuario.RepresentacionId;
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuariosId,UsurioEmail,UsuarioNombre,UsuarioApellidoPaterno,UsuarioApellidoMaterno,UsuarioTelefono,UsuarioContrasenia,UsuarioNomCompleto,UsuarioInmobiliaria,UsuarioCelular,AreaId,RepresentacionId,IsResponsanle,UsuarioPadreId")] Usuarios usuarios)
        {
            var consultaUsuario = _context.Usuarios.SingleOrDefault(u => u.UsurioEmail == usuarios.UsurioEmail);

            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();

            if (usuarios.UsurioEmail == null || usuarios.UsurioEmail == "")
            {
                Error.Add(Mensajes.ErroresAtributos("UsurioEmail"));
            }
            if (consultaUsuario != null)
            {
                Error.Add(Mensajes.MensajesError("El correo ya existe"));

            }

            if (usuarios.UsuarioNombre == null || usuarios.UsuarioNombre == "")
            {
                Error.Add(Mensajes.ErroresAtributos("UsuarioNombre"));
                Error.Add(Mensajes.MensajesError("El nombre no puede estar vacío"));
            }
            if (usuarios.UsuarioApellidoPaterno == null || usuarios.UsuarioApellidoPaterno == "")
            {
                Error.Add(Mensajes.ErroresAtributos("UsuarioApellidoPaterno"));
                Error.Add(Mensajes.MensajesError("El Apellido no puede estar vacío"));
            }
            if (usuarios.UsuarioTelefono == null || usuarios.UsuarioTelefono == "")
            {
                Error.Add(Mensajes.ErroresAtributos("UsuarioTelefono"));
                Error.Add(Mensajes.MensajesError("El teléfono no puede estar vacío"));
            }
            if (usuarios.RepresentacionId == 0)
            {
                Error.Add(Mensajes.MensajesError("Favor de Selecionar Representación"));
            }
            if (usuarios.AreaId == 0)
            {
                Error.Add(Mensajes.MensajesError("Favor de Selecionar Área"));
            }

            if ("Administración" == User.FindFirst("Area").Value)
                {
                if (usuarios.UsuarioContrasenia == null || usuarios.UsuarioContrasenia == "")
                {
                    Error.Add(Mensajes.ErroresAtributos("UsuarioContrasenia"));
                    Error.Add(Mensajes.MensajesError("La Contraseña no puede estar vacío"));
                }
            }
            if (usuarios.IsResponsanle == true && usuarios.AreaId == 3)
            {
                var existeRepre = _context.Usuarios.Where(u => u.RepresentacionId == usuarios.RepresentacionId && u.IsResponsanle == true).Count();
                if (existeRepre >= 1)
                Error.Add(Mensajes.MensajesError("No Puede existir mas de un Responsable por Representación"));
            }
            else
            {
                usuarios.IsResponsanle = false;
            }
            if (Error.Count() == 0)
            {
                usuarios.UsuarioNomCompleto = usuarios.UsuarioNombre + " " + usuarios.UsuarioApellidoPaterno + " " + usuarios.UsuarioApellidoMaterno;
                _context.Add(usuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "AreaDescripcion");
                ViewData["RepresentacionId"] = new SelectList(_context.Representacion, "RepresentacionId", "RepresentacionNombre");
                ViewData["UsuariosPadre"] = new SelectList(_context.Usuarios.Where(u => u.UsuarioPadreId == null), "UsuariosId", "UsuarioNomCompleto");
                ViewBag.Error = JsonConvert.SerializeObject(Error);

                return View(usuarios);
            }
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "AreaDescripcion", usuarios.AreaId);
            ViewData["RepresentacionId"] = new SelectList(_context.Representacion, "RepresentacionId", "RepresentacionNombre", usuarios.RepresentacionId);
            ViewData["UsuariosPadre"] = new SelectList(_context.Usuarios.Where(u => u.UsuarioPadreId == null), "UsuariosId", "UsuarioNomCompleto");
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuariosId,UsurioEmail,UsuarioNombre,UsuarioApellidoPaterno,UsuarioApellidoMaterno,UsuarioTelefono,UsuarioContrasenia,UsuarioNomCompleto,UsuarioInmobiliaria,UsuarioCelular,AreaId,RepresentacionId,IsResponsanle,UsuarioPadreId")] Usuarios usuarios)
        {
            if (id != usuarios.UsuariosId)
            {
                return NotFound();
            }
            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();

            if (usuarios.UsurioEmail == null || usuarios.UsurioEmail == "")
            {
                Error.Add(Mensajes.ErroresAtributos("UsurioEmail"));
            }

            if (usuarios.UsuarioNombre == null || usuarios.UsuarioNombre == "")
            {
                Error.Add(Mensajes.ErroresAtributos("UsuarioNombre"));
                Error.Add(Mensajes.MensajesError("El nombre no puede estar vacío"));
            }
            if (usuarios.UsuarioApellidoPaterno == null || usuarios.UsuarioApellidoPaterno == "")
            {
                Error.Add(Mensajes.ErroresAtributos("UsuarioApellidoPaterno"));
                Error.Add(Mensajes.MensajesError("El Apellido no puede estar vacío"));
            }
            if (usuarios.UsuarioTelefono == null || usuarios.UsuarioTelefono == "")
            {
                Error.Add(Mensajes.ErroresAtributos("UsuarioTelefono"));
                Error.Add(Mensajes.MensajesError("El teléfono no puede estar vacío"));
            }
            if (usuarios.RepresentacionId == 0)
            {
                Error.Add(Mensajes.MensajesError("Favor de Selecionar Representación"));
            }
            if (usuarios.AreaId == 0)
            {
                Error.Add(Mensajes.MensajesError("Favor de Selecionar Área"));
            }

            if ("Administración" == User.FindFirst("Area").Value)
            {
                if (usuarios.UsuarioContrasenia == null || usuarios.UsuarioContrasenia == "")
                {
                    Error.Add(Mensajes.ErroresAtributos("UsuarioContrasenia"));
                    Error.Add(Mensajes.MensajesError("La Contraseña no puede estar vacío"));
                }
            }
            if (usuarios.IsResponsanle == true && usuarios.AreaId == 3)
            {
                var existeRepre = _context.Usuarios.Where(u => u.RepresentacionId == usuarios.RepresentacionId && u.IsResponsanle == true).Count();
                if (existeRepre >= 1)
                    Error.Add(Mensajes.MensajesError("No Puede existir mas de un Responsable por Representación"));
            }
            else
            {
                usuarios.IsResponsanle = false;
            }            
            if (Error.Count == 0)
            {
                try
                {
                    usuarios.UsuarioNomCompleto = usuarios.UsuarioNombre + " " + usuarios.UsuarioApellidoPaterno + " " + usuarios.UsuarioApellidoMaterno;
                    _context.Update(usuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.UsuariosId))
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
            else
            {
                ViewBag.Error = JsonConvert.SerializeObject(Error);
                ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "AreaDescripcion", usuarios.AreaId);
                ViewData["RepresentacionId"] = new SelectList(_context.Representacion, "RepresentacionId", "RepresentacionNombre", usuarios.RepresentacionId);
                ViewData["UsuariosPadre"] = new SelectList(_context.Usuarios.Where(u => u.UsuarioPadreId == null), "UsuariosId", "UsuarioNomCompleto");
                return View(usuarios);
            }
        }


        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .Include(u => u.Area)
                .Include(u => u.Representacion)
                .FirstOrDefaultAsync(m => m.UsuariosId == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuariosId == id);
        }

        
        public async Task<String> Insertar(string UsuarioNombre, string UsuarioApellidoPaterno, string UsuarioApellidoMaterno, string UsuarioTelefono, string UsurioEmail, string UsuarioInmobiliaria, string UsuarioCelular, int UsuarioPadreId, Usuarios usuarios)
        {


            List<ErroresViewModel> Error = new List<ErroresViewModel>();
            Error.Clear();
            Boolean isError = false;
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);
            var area = User.FindFirst("Area").Value;
            var representacionId = Int32.Parse(User.FindFirst("RepresentacionId").Value);
            string result = string.Empty;
            if (area == "Representante")
                UsuarioPadreId = usuarioid;

            if (UsurioEmail == null)
            {
                Error.Add(Mensajes.ErroresAtributos("UsurioEmail"));
                result = JsonConvert.SerializeObject(Error);
                return result;
            }
            else
            {
                var validaEmail = _context.Usuarios.SingleOrDefault(u => u.UsurioEmail == UsurioEmail);
                if (validaEmail != null)
                {
                    Error.Add(Mensajes.MensajesError("El correo que ingreso ya existe"));
                    result = JsonConvert.SerializeObject(Error);
                    return result;
                }
                else
                {
                    if (UsuarioNombre == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("UsuarioNombre"));
                        isError = true;
                    }
                    if (UsuarioApellidoPaterno == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("UsuarioApellidoPaterno" ));
                        isError = true;
                    }
                    if (UsuarioCelular == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("UsuarioCelular"));
                        isError = true;
                    }
                    if (UsuarioInmobiliaria == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("UsuarioInmobiliaria"));
                        isError = true;
                    }
                    if (UsuarioInmobiliaria == null)
                    {
                        Error.Add(Mensajes.ErroresAtributos("No se puede agregar un asesor sin representante"));
                        isError = true;
                    }

                    if (UsuarioPadreId == 0)
                        UsuarioPadreId = 8;

                    if (isError == false)
                    {
                        usuarios = new Usuarios
                        {
                            UsuarioNombre = UsuarioNombre,
                            UsuarioApellidoPaterno = UsuarioApellidoPaterno,
                            UsuarioApellidoMaterno = UsuarioApellidoMaterno,
                            UsuarioTelefono = UsuarioTelefono,
                            UsurioEmail = UsurioEmail,
                            UsuarioInmobiliaria = UsuarioInmobiliaria,
                            UsuarioCelular = UsuarioCelular,
                            UsuarioPadreId = UsuarioPadreId
                        };


                        if (area == "Administración")
                        {
                            usuarios.RepresentacionId = 1;
                            usuarios.AreaId = 2;
                        }
                        else
                        {
                            usuarios.RepresentacionId = representacionId;
                            usuarios.AreaId = 2;

                        }

                        usuarios.UsuarioNomCompleto = UsuarioNombre + " " + UsuarioApellidoPaterno + " " + UsuarioApellidoMaterno;
                        _context.Add(usuarios);
                        var resulta = await _context.SaveChangesAsync();


                        if (resulta > 0)
                        {
                            var Usuario = usuarios.UsuariosId.ToString();
                            ViewData["Asesor"] = _context.Usuarios.Include(u => u.Representacion).Include(u => u.Area).Where(u => u.Area.AreaDescripcion == "Asesor").ToList();
                            Error.Add(Mensajes.Exitoso("Se agrego correctamente el asesor"));
                            result = JsonConvert.SerializeObject(Error);
                            return result;
                        }
                        else
                        {
                            Error.Add(Mensajes.MensajesError("Error, favor de copiar el error y mandarlo al da" + result.ToString()));
                            result = JsonConvert.SerializeObject(Error);
                            return result;
                        }
                    }
                    else
                    {
                        result = JsonConvert.SerializeObject(Error);
                        return result;
                    }
                }

            }
        }


        // GET: Upload Asesores
        public async Task<IActionResult> Upload()
        {
            var usuarioid = Int32.Parse(User.FindFirst("Id").Value);            
            var PolizaJuridicaDbContext = _context.Usuarios.Include(u => u.Area).Include(u => u.Representacion).Where(u => u.Area.AreaDescripcion == "Asesor");
            return View(await PolizaJuridicaDbContext.ToListAsync());
            
        }

    }
}
