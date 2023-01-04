using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PolizaJuridica.Controllers
{
    public class InvestigacionOnlineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string nomb, string apepat, string apemat)
        {
            persona data = new persona()
            {
                nombres = nomb,
                apellidoPaterno = apepat,
                apellidoMaterno = apemat
            };
                string method = "POST";
            WebClient client = new WebClient();
            client.Headers.Add("x-api-key", "FFvs4n0fNKWGwvhdnEqjUqemGurWgaMi");
            string reply = client.UploadString("https://services.circulodecredito.com.mx/sandbox/v1/pld/", method, JsonConvert.SerializeObject(data));
            ViewBag.resultado = reply;
            return View();
        }

        public class persona
        {
            public string nombres { get; set; }
            public string apellidoPaterno { get; set; }
            public string apellidoMaterno { get; set; }
        }
    }
}