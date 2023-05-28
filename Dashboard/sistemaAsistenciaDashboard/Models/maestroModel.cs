using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaAsistenciaDashboard.Models
{
    public class maestroModel
    {
        public string macMaestro { get; set; }
        public string nombre { get; set; }
        public string aPaterno { get; set; }
        public string aMaterno { get; set; }
        public int noEmpleado { get; set; }
        public string profesion { get; set; }
    }
}