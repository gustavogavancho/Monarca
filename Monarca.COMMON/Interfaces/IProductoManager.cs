using Monarca.COMMON.Entidades;
using System.Collections.Generic;

namespace Monarca.COMMON.Interfaces
{
    public interface IProductoManager : IGenericManager<Producto>
    {
        IEnumerable<Producto> SearchProducto(string text);
    }
}
