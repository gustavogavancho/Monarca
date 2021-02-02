namespace Monarca.COMMON.Entidades
{
    public class Compra : BaseEntity
    {
        public string IdProducto { get; set; }
        public string IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string NombreProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal Total { get; set; }
    }
}
