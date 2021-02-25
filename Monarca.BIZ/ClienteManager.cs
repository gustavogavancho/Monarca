using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using System;
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
            return _repositorio.Query(x => x.Nombres != null && 
                                      x.Nombres.ToLowerInvariant().Contains(text.ToLowerInvariant()) ||
                                      x.Apellidos != null && 
                                      x.Apellidos.ToLowerInvariant().Contains(text.ToLowerInvariant()) ||
                                      x.RazonSocial != null &&
                                      x.RazonSocial.ToLowerInvariant().Contains(text.ToLowerInvariant()) ||
                                      x.Dni != null && 
                                      x.Dni.ToString().Contains(text) ||
                                      x.Ruc != null &&
                                      x.Ruc.ToString().Contains(text));
        }
    }
}
