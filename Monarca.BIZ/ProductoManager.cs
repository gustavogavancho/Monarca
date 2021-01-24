using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;

namespace Monarca.BIZ
{
    public class ProductoManager : GenericManager<Producto>, IProductoManager
    {
        public ProductoManager(IGenericRepository<Producto> repositorio) : base (repositorio)
        {

        }
    }
}
