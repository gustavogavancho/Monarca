using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using System.Collections.Generic;

namespace Monarca.BIZ
{
    public class ProductoManager : GenericManager<Producto>, IProductoManager
    {
        public ProductoManager(IGenericRepository<Producto> repositorio) : base (repositorio)
        {

        }
        public IEnumerable<Producto> SearchProducto(string text)
        {
            return _repositorio.Query(x => x.Nombre != null &&
                                      x.Nombre.ToLowerInvariant().Contains(text.ToLowerInvariant()) ||
                                      x.Marca != null && 
                                      x.Marca.ToLowerInvariant().Contains(text.ToLowerInvariant()));
        }

    }
}
