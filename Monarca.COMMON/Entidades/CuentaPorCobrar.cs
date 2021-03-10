using Monarca.COMMON.Enumeraciones;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Monarca.COMMON.Entidades
{
    public class CuentaPorCobrar : BaseEntity
    {
        public TipoCliente TipoCliente { get; set; }
        public Venta Venta { get; set; }
        public bool Estado { get; set; }
        public ObservableCollection<MontoPagos> Pagos { get; set; } = new ObservableCollection<MontoPagos>();
        public decimal TotalCobrar { get; set; }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };

            return Pagos.Sum(x=> x.Monto).ToString("#,##0.00", nfi);
        }
    }

    public class MontoPagos
    {
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
