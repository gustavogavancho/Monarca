using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;

namespace Monarca.BIZ
{
    public class ClienteManager : GenericManager<Cliente>, IClienteManager
    {
        public ClienteManager(IGenericRepository<Cliente> repositorio) : base (repositorio)
        {

        }
    }
}
