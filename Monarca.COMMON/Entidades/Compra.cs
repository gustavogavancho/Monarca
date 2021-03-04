using Monarca.COMMON.Enumeraciones;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Monarca.COMMON.Entidades
{
    public class Compra : BaseEntity
    {
        public string IdProveedor { get; set; }
        public TipoCliente TipoCliente { get; set; }
        public string NombreProveedor { get; set; }
        public string RazonSocialProveedor { get; set; }
        public string Dni { get; set; }
        public string Ruc { get; set; }
        public string NumeroDocumento { get; set; }
        public ObservableCollection<Producto> Productos { get; set; }

        public override string ToString()
        {
            var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };

            return Productos.Sum(x => x.Total).ToString("#,##0.00", nfi);
        }
    }
}
