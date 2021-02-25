using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using System.Collections.Generic;

namespace Monarca.BIZ
{
    public class VentaManager : GenericManager<Venta>, IVentaManager
    {
        public VentaManager(IGenericRepository<Venta> repositorio) : base (repositorio)
        {}

        public IEnumerable<Venta> SearchVenta(string text)
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
