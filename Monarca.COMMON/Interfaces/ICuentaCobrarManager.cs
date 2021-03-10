using Monarca.COMMON.Entidades;
using System.Collections.Generic;

namespace Monarca.COMMON.Interfaces
{
    public interface ICuentaCobrarManager : IGenericManager<CuentaPorCobrar>
    {
        IEnumerable<CuentaPorCobrar> SearchCuentaCobrar(string text);
    }
}
