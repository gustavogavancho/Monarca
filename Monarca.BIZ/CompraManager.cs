using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;

namespace Monarca.BIZ
{
    public class CompraManager : GenericManager<Compra>, ICompraManager
    {
        public CompraManager(IGenericRepository<Compra> repositorio) : base (repositorio)
        {

        }
    }
}
