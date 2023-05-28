using Newtonsoft.Json;
using sistemaAsistenciasAPI.Entities;
using sistemaAsistenciasAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sistemaAsistenciasAPI.Controllers
{
    [RoutePrefix("api/gestion")]
    public class ListasController : ApiController
    {
        [Route("generaLista")]
        //[HttpGet]
        //[Authorize]
        public IHttpActionResult GenerarListas()
        {
            sistemaRFIDEntities1 _db = new sistemaRFIDEntities1();

            _db.usp_GeneraHeaderDetails();

            return Ok("Se han generado las listas");
        }
    }
}