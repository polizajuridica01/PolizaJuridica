using Microsoft.AspNetCore.Mvc;
using PolizaJuridica.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PolizaJuridica.Webservices
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentacionesController : ControllerBase
    {
        private readonly PolizaJuridicaDbContext _context;

        public RepresentacionesController(PolizaJuridicaDbContext context)
        {
            _context = context;
        }
        // GET: api/<RepresentacionesController>
        [HttpGet]
        public IEnumerable<Representacion> Get()
        {
            return  _context.Representacion.Where(r => r.Activa == true).OrderBy(o => o.RepresentacionNombre).ToList();
        }

        // GET api/<RepresentacionesController>/5
        [HttpGet("{id}")]
        public Representacion Get(int id)
        {
            return  _context.Representacion.Where(r => r.Activa == true).SingleOrDefault(r => r.RepresentacionId == id);
        }

        //// POST api/<RepresentacionesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<RepresentacionesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<RepresentacionesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
