using Monarca.COMMON.Enumeraciones;

namespace Monarca.COMMON.Entidades
{
    public class Producto : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripción { get; set; }
        public string Marca { get; set; }
        public Unidad Unidad { get; set; }
        public string Cantidad { get; set; }
        public string PrecioCompra { get; set; }
        public string PrecioVenta { get; set; }
        public bool StockDisponible { get; set; }
    }
}
