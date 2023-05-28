using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sistemaAsistenciasAPI.Controllers
{
    [RoutePrefix("api/tiempo")]
    public class TiempoController : ApiController
    {
        [Route("hora")]
        [HttpGet]
        public IHttpActionResult currentTime()
        {
            string datetime = DateTime.Now.ToString("hh:mm:ss tt");
            return Ok(datetime); 
        }
    }
}