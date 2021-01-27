using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using System.Collections.Generic;

namespace Monarca.BIZ
{
    public class ProveedorManager : GenericManager<Proveedor>, IProveedorManager
    {
        public ProveedorManager(IGenericRepository<Proveedor> repositorio) : base (repositorio)
        {

        }

        public IEnumerable<Proveedor> SearchProveedor(string text)
        {
            return _repositorio.Query(x => x.Nombres != null &&
                                      x.Nombres.ToLowerInvariant().Contains(text.ToLowerInvariant()) ||
                                      x.Apellidos != null &&
                                      x.Apellidos.ToLowerInvariant().Contains(text.ToLowerInvariant()) ||
                                      x.RazonSocial != null &&
                                      x.RazonSocial.ToLowerInvariant().Contains(text.ToLowerInvariant()) ||
                                      x.Dni > 0 &&
                                      x.Dni.ToString().Contains(text) ||
                                      x.Ruc > 0 &&
                                      x.Ruc.ToString().Contains(text));
        }
    }
}
