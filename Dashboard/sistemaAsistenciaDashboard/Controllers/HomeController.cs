using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistemaAsistenciaDashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["cuenta"];

            if (cookie != null)
            {
                ViewBag.user = cookie.Values["nombre"];
            }
            return View();
        }
    }
}