using Monarca.COMMON.Enumeraciones;

namespace Monarca.COMMON.Entidades
{
    public class Compra : BaseEntity
    {
        public string IdProducto { get; set; }
        public string IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public TipoCliente TipoCliente { get; set; }
        public string RazonSocialProveedor { get; set; }
        public long Dni { get; set; }
        public long Ruc { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public string MarcaProducto { get; set; }
        public string UnidadProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal Total { get; set; }
    }
}
