using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;

namespace Monarca.BIZ
{
    public class GastoOperativoManager : GenericManager<GastoOperativo>, IGastoOperativoManager
    {
        public GastoOperativoManager(IGenericRepository<GastoOperativo> repositorio) : base (repositorio)
        {}

        //TODO:
    }
}
