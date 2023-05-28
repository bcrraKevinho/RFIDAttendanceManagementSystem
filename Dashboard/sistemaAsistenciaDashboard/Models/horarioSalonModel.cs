using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaAsistenciaDashboard.Models
{
    public class horarioSalonModel
    {
        public int idHorario { get; set; }
        public System.DateTime fechaHoraClase { get; set; }
        public string mac_Salon { get; set; }
        public string salon { get; set; }
        public string materia { get; set; }
        public string tipo { get; set; }
        public string nombreMaestro { get; set; }
    }
}