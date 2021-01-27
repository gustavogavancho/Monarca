using Monarca.COMMON.Entidades;
using System.Collections.Generic;

namespace Monarca.COMMON.Interfaces
{
    public interface IClienteManager : IGenericManager<Cliente>
    {
        IEnumerable<Cliente> SearchCliente(string text);
    }
}
