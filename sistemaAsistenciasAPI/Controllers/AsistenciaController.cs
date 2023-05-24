using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using sistemaAsistenciasAPI.Models;
using sistemaAsistenciasAPI.Entities;
using Newtonsoft.Json;

namespace sistemaAsistenciasAPI.Controllers
{
    [RoutePrefix("api/Asistencia")]
    public class AsistenciaController : ApiController
    {
        sistemaRFIDEntities1 _db = new sistemaRFIDEntities1();

        [Route("registro")]
        [HttpPost]
        public IHttpActionResult RegistrarAsistencia(asistenciaValida asistencia)
        {
            try
            {
                var response = _db.usp_RegistraAsistencia(asistencia.mac, asistencia.idsalon).FirstOrDefault();
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return Ok("Alumno no encontrado");
                }
            }
            catch (Exception)
            {
                return InternalServerError(new Exception("Error interno en el servidor")); //retorna error 500 se refiere a un "Error interno del servidor"
            }
        }
        //[Route("consulta")]
        //[HttpPost]
        //public IHttpActionResult ConsultaRegistroAsistencias(consultaAsistencias parametros)
        //{
        //    try
        //    {
        //        var response = _db.usp_ConsultaRegistrosAsistencias2(parametros.idSalon, parametros.horarioSeleccionado);

        //        return Ok(response);
        //    }
        //    catch (Exception)
        //    {
        //        return InternalServerError(new Exception("Error interno en el servidor"));
        //    }
        //}
        [Route("consulta")]
        [HttpGet]
        public IHttpActionResult ConsultaRegistroAsistencias(string idSalon, string horarioSeleccionado)
        {
            try
            {
                DateTime horario = Convert.ToDateTime(horarioSeleccionado);
                var response = _db.usp_ConsultaRegistrosAsistencias2(idSalon, horario);

                return Ok(response);
            }
            catch (Exception)
            {
                return InternalServerError(new Exception("Error interno en el servidor"));
            }
        }
    }
}