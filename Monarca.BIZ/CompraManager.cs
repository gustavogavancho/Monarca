using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using System.Collections.Generic;

namespace Monarca.BIZ
{
    public class CompraManager : GenericManager<Compra>, ICompraManager
    {
        public CompraManager(IGenericRepository<Compra> repositorio) : base (repositorio)
        {}

        public IEnumerable<Compra> SearchCompra(string text)
        {
            return _repositorio.Query(x => x.NombreProveedor != null &&
                          x.NombreProveedor.ToLowerInvariant().Contains(text.ToLowerInvariant()) ||
                          x.RazonSocialProveedor != null &&
                          x.RazonSocialProveedor.ToLowerInvariant().Contains(text.ToLowerInvariant()) ||
                          x.Dni != null &&
                          x.Dni.Contains(text) ||
                          x.Ruc != null &&
                          x.Ruc.Contains(text) ||
                          x.FechaHoraCreacion != null &&
                          x.FechaHoraCreacion.ToString().Contains(text));
        }
    }
}
