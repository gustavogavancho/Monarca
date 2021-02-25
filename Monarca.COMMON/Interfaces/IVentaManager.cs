using Monarca.COMMON.Entidades;
using System.Collections.Generic;

namespace Monarca.COMMON.Interfaces
{
    public interface IVentaManager : IGenericManager<Venta>
    {
        IEnumerable<Venta> SearchVenta(string text);
    }
}
