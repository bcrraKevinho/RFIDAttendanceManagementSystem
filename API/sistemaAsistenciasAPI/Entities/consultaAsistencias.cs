using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaAsistenciasAPI.Entities
{
    public class consultaAsistencias
    {
        public string idSalon { get; set; }
        public DateTime horarioSeleccionado { get; set; }
    }
}