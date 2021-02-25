using Monarca.COMMON.Entidades;
using System.Collections.Generic;

namespace Monarca.COMMON.Interfaces
{
    public interface IGastoOperativoManager : IGenericManager<GastoOperativo>
    {
        IEnumerable<GastoOperativo> SearchGastoOperativo(string text);
    }
}
