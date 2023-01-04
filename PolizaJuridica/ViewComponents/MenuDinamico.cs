using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolizaJuridica.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace PolizaJuridica.ViewComponents
{
    public class MenuDinamico : ViewComponent
    {
        private readonly PolizaJuridicaDbContext _context;

        public MenuDinamico(PolizaJuridicaDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var UserId = this.UserClaimsPrincipal.FindFirst("Id").Value;
            var id = int.Parse(UserId);
            var usuario = _context.Usuarios.SingleOrDefault(u => u.UsuariosId == id);
            var menup =  _context.MenuP.Include(m => m.MenuPpadre).Where(m => m.AreaId == usuario.AreaId);
            return View(await menup.ToListAsync());
        }
    }
}
