using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PolizaJuridica.Data;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Utilities;
using Newtonsoft.Json;
using PolizaJuridica.ViewModels;

namespace PolizaJuridica.Controllers
{
    public class LoginController : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public LoginController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }
        public IActionResult Logear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logear(Usuarios usuario)
        {
            var result = await _context.Usuarios
                .Include(s => s.Representacion)
                .Include(s => s.Area)
                .SingleOrDefaultAsync(s => s.UsurioEmail.ToLower() == usuario.UsurioEmail.Trim().ToLower() && s.UsuarioContrasenia == usuario.UsuarioContrasenia.Trim());            
            if (result != null)
            {
                UserViewModel user = new UserViewModel();

                user.AreaDescripcion = result.Area.AreaDescripcion;
                user.AreaId = result.AreaId;
                user.Email = result.UsurioEmail;
                user.NombreCompleto = result.UsuarioNomCompleto;
                user.RepresentacionId = result.RepresentacionId;
                user.Representacón = result.Representacion.RepresentacionNombre;
                user.Dashboard = result.Area.Dashboard;
                user.UsuarioId = result.UsuariosId;


                var jsonresult = JsonConvert.SerializeObject(user);

                var dashboard = result.Area.Dashboard;
                
                if (result.Activo == true)
                {                    
                    int convertid = result.UsuariosId;
                    var claims = new List<Claim>
                    {
                        new Claim("Email", result.UsurioEmail),
                        new Claim("FullName", result.UsuarioNombre+' '+result.UsuarioApellidoPaterno),
                        new Claim("Id",result.UsuariosId.ToString()),
                        new Claim("Representacion", result.Representacion.RepresentacionNombre),
                        new Claim("RepresentacionId", result.RepresentacionId.ToString()),
                        new Claim("NombreCompleto", result.UsuarioNomCompleto),
                        new Claim("Area", result.Area.AreaDescripcion),
                        new Claim("RepresentacionId",result.RepresentacionId.ToString()),
                        new Claim("Dashboard",result.Area.Dashboard),
                        new Claim("User", jsonresult),
                    new Claim("SistemaId","1227")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);

                    return RedirectToAction(dashboard, "Dashboard");
                }
                else
                {
                    ViewBag.Error = "Usuario inhabilitado";
                }
                
            }
            else
            {
                ViewBag.Error = "El e-mail o la contrasenia son incorrectos";
                
            }
            return View();
        }

        
        public async Task<ActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("Menu");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Logear");
        }

    }
}