using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using sistemaAsistenciaDashboard.Helpers;
using sistemaAsistenciaDashboard.Models;
namespace sistemaAsistenciaDashboard.Controllers
{
    public class AsistenciaController : Controller
    {
        ConsultaApi apiRest = new ConsultaApi();
        // GET: Asistencia
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["cuenta"];

            if (cookie != null)
            {
                ViewBag.user = cookie.Values["nombre"];
            }
            string idsalon = "E0:E2:E6:0D:59:18";
            var DatosHorarios = apiRest.Get(ConfigurationManager.AppSettings["baseApiURL"] + "api/Horario/obtenerHorario?idsalon=" + idsalon);
            List<horarioSalonModel> horarios = JsonConvert.DeserializeObject<List<horarioSalonModel>>(DatosHorarios);

            ViewBag.Horarios = horarios;
            ViewBag.Salon = horarios[0];
            ViewBag.PrimerCarga = true;
            ConsultaApi n = new ConsultaApi();

            //DateTime fecha = new DateTime(DateTime.Now.Year, 5, 24, 7, 0, 0);
            DateTime fecha = DateTime.Now;
            string date = fecha.ToString(/*"dd/MM/yyyy HH:mm:ss"*/);
            //string date = "24/05/2023 07:00:00";
            var Datos = apiRest.Get(ConfigurationManager.AppSettings["baseApiURL"] + "api/Asistencia/consulta?idsalon=E0:E2:E6:0D:59:18&horarioSeleccionado=" + date);
            //ConfigurationManager.AppSettings["baseApiURL"]
            //var data = JsonConvert.SerializeObject(a);
            //var response = n.Post("http://localhost:12710/api/Asistencia/registro", data);
            List<detalleAsistenciaModel> model = JsonConvert.DeserializeObject<List<detalleAsistenciaModel>>(Datos);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string salon, string hora)
        {
            HttpCookie cookie = Request.Cookies["cuenta"];

            if (cookie != null)
            {
                ViewBag.user = cookie.Values["nombre"];
            }

            var DatosHorarios = apiRest.Get(ConfigurationManager.AppSettings["baseApiURL"] + "api/Horario/obtenerHorario?idsalon=" + salon);
            List<horarioSalonModel> horarios = JsonConvert.DeserializeObject<List<horarioSalonModel>>(DatosHorarios);

            ViewBag.Horarios = horarios;
            ViewBag.Salon = horarios[0];
            ViewBag.PrimerCarga = false;

            //string date = "24/05/2023 07:00:00";
            var Datos = apiRest.Get(ConfigurationManager.AppSettings["baseApiURL"] + "api/Asistencia/consulta?idsalon=" + salon + "&horarioSeleccionado=" + hora);
            //ConfigurationManager.AppSettings["baseApiURL"]
            //var data = JsonConvert.SerializeObject(a);
            //var response = n.Post("http://localhost:12710/api/Asistencia/registro", data);
            List<detalleAsistenciaModel> model = JsonConvert.DeserializeObject<List<detalleAsistenciaModel>>(Datos);
            return View(model);
        }

        public ActionResult RegistrarAsistencia(string mac, string idsalon, string horaClase, string horallegada)
        {
            HttpCookie cookie = Request.Cookies["cuenta"];

            if (cookie != null)
            {
                ViewBag.user = cookie.Values["nombre"];
            }
            if(horallegada == null)
            {
                asistenciaModel asistencia = new asistenciaModel
                {
                    mac = mac,
                    idsalon = idsalon
                };
                //ConfigurationManager.AppSettings["baseApiURL"]
                var data = JsonConvert.SerializeObject(asistencia);
                var response = apiRest.Post(ConfigurationManager.AppSettings["baseApiURL"] + "api/Asistencia/registro", data);
            }
            


            var DatosHorarios = apiRest.Get(ConfigurationManager.AppSettings["baseApiURL"] + "api/Horario/obtenerHorario?idsalon=" + idsalon);
            List<horarioSalonModel> horarios = JsonConvert.DeserializeObject<List<horarioSalonModel>>(DatosHorarios);

            ViewBag.Horarios = horarios;
            ViewBag.Salon = horarios[0];
            ViewBag.PrimerCarga = false;

            var Datos = apiRest.Get(ConfigurationManager.AppSettings["baseApiURL"] + "api/Asistencia/consulta?idsalon=" + idsalon + "&horarioSeleccionado=" + horaClase);
            
            List<detalleAsistenciaModel> model = JsonConvert.DeserializeObject<List<detalleAsistenciaModel>>(Datos);

            return View("Index", model);
        }

        public ActionResult ActualizarAsistencia(int idDetalle, int idEstatus, string idsalon, string horaClase)
        {
            HttpCookie cookie = Request.Cookies["cuenta"];

            if (cookie != null)
            {
                ViewBag.user = cookie.Values["nombre"];
            }
            
            var Data = apiRest.Get(ConfigurationManager.AppSettings["baseApiURL"] + "api/Asistencia/actualizar?idDetalle=" + idDetalle + "&idEstatus=" + idEstatus);


            var DatosHorarios = apiRest.Get(ConfigurationManager.AppSettings["baseApiURL"] + "api/Horario/obtenerHorario?idsalon=" + idsalon);
            List<horarioSalonModel> horarios = JsonConvert.DeserializeObject<List<horarioSalonModel>>(DatosHorarios);

            ViewBag.Horarios = horarios;
            ViewBag.Salon = horarios[0];
            ViewBag.PrimerCarga = false;

            var Datos = apiRest.Get(ConfigurationManager.AppSettings["baseApiURL"] + "api/Asistencia/consulta?idsalon=" + idsalon + "&horarioSeleccionado=" + horaClase);

            List<detalleAsistenciaModel> model = JsonConvert.DeserializeObject<List<detalleAsistenciaModel>>(Datos);

            return View("Index", model);
        }

    }
}