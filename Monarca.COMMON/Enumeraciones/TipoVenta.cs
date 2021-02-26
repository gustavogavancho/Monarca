using System.ComponentModel;

namespace Monarca.COMMON.Enumeraciones
{
    public enum TipoVenta : short
    {
        [Description("Boleta")]
        Boleta = 3,
        [Description("Factura")]
        Factura = 1,
        [Description("Nota de venta")]
        NotaDeVenta = 9,
    }
}
