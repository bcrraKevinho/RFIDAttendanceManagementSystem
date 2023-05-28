using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using sistemaAsistenciasAPI.Models;

namespace sistemaAsistenciasAPI.Controllers
{
    [RoutePrefix("api/Horario")]
    public class HorarioController : ApiController
    {
        sistemaRFIDEntities1 _db = new sistemaRFIDEntities1();

        [Route("obtenerHorario")]
        [HttpGet]
        public IHttpActionResult ObtenerHorario(string idsalon)
        {
            try
            {
                var response = _db.usp_RecuperaHorarios(idsalon).ToList();

                return Ok(response);
            }
            catch (Exception)
            {
                return InternalServerError(new Exception("Error interno en el servidor")); //retorna error 500 se refiere a un "Error interno del servidor"
            }
        }
    }
}