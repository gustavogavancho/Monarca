using Monarca.COMMON.Enumeraciones;

namespace Monarca.COMMON.Entidades
{
    public class Producto : BaseEntity
    {
        public string CodigoInterno { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public Unidad Unidad { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }

        public override string ToString()
        {
            return $"{Nombre} {Marca}";
        }
    }
}
