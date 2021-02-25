using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using System.Collections.Generic;

namespace Monarca.BIZ
{
    public class GastoOperativoManager : GenericManager<GastoOperativo>, IGastoOperativoManager
    {
        public GastoOperativoManager(IGenericRepository<GastoOperativo> repositorio) : base (repositorio)
        {}

        public IEnumerable<GastoOperativo> SearchGastoOperativo(string text)
        {
            return _repositorio.Query(x => x.Nombre != null &&
            x.Nombre.ToLowerInvariant().Contains(text.ToLowerInvariant()) ||
            x.Descripcion.ToLowerInvariant().Contains(text.ToLowerInvariant()));
        }
    }
}
