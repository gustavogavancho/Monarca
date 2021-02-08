using System;

namespace Monarca.UI.WPF.Usuario.Models
{
    public class AlmacenModel
    {
        public string IdProducto { get; set; }
        public string IdProveedor { get; set; }
        public string NombreProducto { get; set; }
        public string MarcaProducto { get; set; }
        public string NombreProveedor { get; set; }
        public decimal CantidadComprada { get; set; }
        public decimal CantidadVendida { get; set; }
        public decimal Stock { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public int Item { get; set; }
    }
}
