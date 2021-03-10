using Monarca.COMMON.Enumeraciones;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Monarca.COMMON.Entidades
{
    public class Venta : BaseEntity
    {
        public string IdCliente { get; set; }
        public TipoCliente TipoCliente { get; set; }
        public string NombreProveedor { get; set; }
        public string RazonSocialProveedor { get; set; }
        public string Dni { get; set; }
        public string Ruc { get; set; }
        public string SerieDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public ObservableCollection<Producto> Productos { get; set; }
        public TipoVenta TipoVenta { get; set; }
        public string ExternalId { get; set; }
        public string Hash { get; set; }
        public string Qr { get; set; }
        public string linkPdf { get; set; }
        public string linkXml { get; set; }
        public string linkCdr { get; set; }
        public bool Baja { get; set; }
        public bool Credito { get; set; }
        public string ExternalIdBaja { get; set; }
        public string Ticket { get; set; }

        public override string ToString()
        {
            var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };

            return Productos.Sum(x => x.Total).ToString("#,##0.00", nfi);
        }
    }
}
