using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using sistemaAsistenciasAPI.Models;

namespace sistemaAsistenciasAPI.Controllers
{
    [RoutePrefix("api/maestro")]
    public class MaestroController : ApiController
    {
        [Route("recuperarDatos")]
        [HttpGet]
        public IHttpActionResult recuperaMaestro(int noEmpleado)
        {
            sistemaRFIDEntities1 _db = new sistemaRFIDEntities1();

            try
            {
                var response = _db.usp_RecuperaMaestro(noEmpleado).FirstOrDefault();
                return Ok(response);
            }
            catch (Exception)
            {
                return InternalServerError(new Exception("Error interno en el servidor")); //retorna error 500 se refiere a un "Error interno del servidor"
            }
            
        }

    }
}