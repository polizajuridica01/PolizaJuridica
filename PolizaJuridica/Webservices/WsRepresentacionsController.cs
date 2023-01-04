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
    public class WsRepresentacionsController : ControllerBase
    {
        private readonly PolizaJuridicaDbContext _context;

        public WsRepresentacionsController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: api/WsRepresentacions
        [HttpGet]
        public IEnumerable<representacionjson> GetRepresentacion()
        {
            return _context.Representacion.OrderBy(id => id.RepresentacionNombre).Select(r => new representacionjson{ id = r.RepresentacionId,text = r.RepresentacionNombre});
        }      

        public class representacionjson
        {
            public int id { get; set; }
            public string text { get; set; }

        }
    }
}