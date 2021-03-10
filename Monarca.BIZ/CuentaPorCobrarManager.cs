using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;

namespace Monarca.BIZ
{
    public class CuentaPorCobrarManager : GenericManager<CuentaPorCobrar>, ICuentaCobrarManager
    {
        public CuentaPorCobrarManager(IGenericRepository<CuentaPorCobrar> repositorio) : base (repositorio)
        {}


    }
}
