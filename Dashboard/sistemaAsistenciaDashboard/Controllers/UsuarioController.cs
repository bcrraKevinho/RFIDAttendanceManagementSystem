using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using sistemaAsistenciaDashboard.Helpers;
using sistemaAsistenciaDashboard.Models;

namespace sistemaAsistenciaDashboard.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["cuenta"];

            if (cookie != null)
            {
                ViewBag.user = cookie.Values["nombre"];
                ViewBag.profesion = cookie.Values["profesion"];
                ViewBag.noEmpleado = Convert.ToInt32(cookie.Values["noEmpleado"]);
            }
            return View();
        }
    }
}