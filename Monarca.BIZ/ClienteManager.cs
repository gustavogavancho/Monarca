using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using System.Collections.Generic;

namespace Monarca.BIZ
{
    public class ClienteManager : GenericManager<Cliente>, IClienteManager
    {
        public ClienteManager(IGenericRepository<Cliente> repositorio) : base (repositorio)
        {

        }

        public IEnumerable<Cliente> SearchCliente(string text)
        {
            return _repositorio.Query(x => x.Nombres != null && x.Nombres.Contains(text));
        }
    }
}
