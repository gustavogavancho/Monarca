using Monarca.COMMON.Enumeraciones;

namespace Monarca.COMMON.Entidades
{
    public class Venta : BaseEntity
    {
        public string IdProducto { get; set; }
        public string IdCliente { get; set; }
        public TipoVenta TipoVenta { get; set; }
    }
}
