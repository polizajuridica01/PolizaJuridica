using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolizaJuridica.Data;

namespace PolizaJuridica.Webservices
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly PolizaJuridicaDbContext _context;

        public UsuariosController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public IEnumerable<Usuarios> GetUsuarios(int id)
        {

            return _context.Usuarios.Include(u => u.Representacion).Where(u => u.RepresentacionId == id).Where(u => u.AreaId == 3).Where(u => u.Activo == true).Select( u => new Usuarios{ 
                UsuariosId = u.UsuariosId , 
                UsuarioNomCompleto = u.UsuarioNomCompleto, 
                UsuarioTelefono = u.UsuarioTelefono,
                UsuarioCelular = u.UsuarioCelular,
                UsurioEmail = u.UsurioEmail,
                Imagen = u.Imagen,
                Titulo = u.Titulo
            });
        }
        [HttpGet("GetUsuario/{nombre}")]
        public IEnumerable<Usuarios> GetUsuario(string nombre)
        {
            List<int> areas = new List<int>();
            areas.Add(3);//representaciones
            areas.Add(7);//soluciones
            areas.Add(12);//Control y Calidad
            areas.Add(1);//Administración
            return _context.Usuarios.Include(u => u.Representacion).Where(u => u.UsurioEmail.Contains(nombre)).Where(u => areas.Contains(u.AreaId)).Where(u => u.Activo == true).Select(u => new Usuarios
            {
                UsuariosId = u.UsuariosId,
                UsuarioNomCompleto = u.UsuarioNomCompleto,
                UsuarioTelefono = u.UsuarioTelefono,
                UsuarioCelular = u.UsuarioCelular,
                UsurioEmail = u.UsurioEmail,
                Representacion = u.Representacion,
                Imagen = u.Imagen,
                Titulo = u.Titulo

            });
        }
    }
}