using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;

namespace Monarca.BIZ
{
    public class VentaManager : GenericManager<Venta>, IVentaManager
    {
        public VentaManager(IGenericRepository<Venta> repositorio) : base (repositorio)
        {
            
        }
    }
}
