using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PolizaJuridica.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PolizaJuridica.Webservices
{
    [Route("api/[controller]")]
    public class Wscentrocostos : Controller
    {
        private readonly PolizaJuridicaDbContext _context;

        public Wscentrocostos(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        [EnableCors("AllowAll")]
        public async Task<ActionResult<IEnumerable<CentroCostos>>> AllCentroCostos()
        {
            return await _context.CentroCostos.ToListAsync();
        }

        //// GET api/<controller>/5
        //[HttpGet("Calcular")]
        //public string Calcular()
        //{
        //    var respuesta = _context.CentroCostos.ToList();
        //    var res = respuesta.ToString();
        //    return res;
        //}

        //// POST api/<controller>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
