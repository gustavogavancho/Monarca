using System.Collections.Generic;

namespace Monarca.Tools.API.Models
{
    public class ActividadEconomica
    {
        public string ciiu { get; set; }
        public string descripcion { get; set; }
    }

    public class Result
    {
        public string ruc { get; set; }
        public string razon_social { get; set; }
        public string nombre_comercial { get; set; }
        public string tipo { get; set; }
        public string fecha_inscripcion { get; set; }
        public string estado { get; set; }
        public string direccion { get; set; }
        public string sistema_emision { get; set; }
        public string actividad_exterior { get; set; }
        public string sistema_contabilidad { get; set; }
        public string oficio { get; set; }
        public string ple { get; set; }
        public string inicio_actividades { get; set; }
        public List<ActividadEconomica> actividad_economica { get; set; }
        public List<object> establecimientos { get; set; }
        public List<object> representantes_legales { get; set; }
        public string cantidad_trabajadores { get; set; }
    }

    public class Datos
    {
        public bool success { get; set; }
        public Result result { get; set; }
    }

    public class RUC
    {
        public Datos datos { get; set; }
        public bool ok { get; set; }
    }
}
