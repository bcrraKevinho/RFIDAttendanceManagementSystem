using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistemaAsistenciaDashboard.Models
{
    public class detalleAsistenciaModel
    {
        public int idDetalle { get; set; }
        public string nombreCompleto { get; set; }
        public int id_Estatus { get; set; }
        public string estatus { get; set; }
        public Nullable<System.DateTime> horaLlegada { get; set; }
        public string materia { get; set; }
        public System.DateTime horarioClase { get; set; }
        public string edificio { get; set; }
        public string mac_Persona { get; set; }
    }
}