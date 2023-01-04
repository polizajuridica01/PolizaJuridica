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
    public class EstadosController : ControllerBase
    {
        private readonly PolizaJuridicaDbContext _context;

        public EstadosController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: api/Estados
        [HttpGet]
        public IEnumerable<estadojson> GetEstados()
        {
            return _context.Estados.Select(e => new estadojson{ id = e.EstadosId, text = e.EstadoNombre});
        }

        public class estadojson
        {
            public int id { get; set; }
            public string text { get; set; }

        }
    
    }
}