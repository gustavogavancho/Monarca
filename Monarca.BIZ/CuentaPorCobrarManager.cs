using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using System.Collections.Generic;

namespace Monarca.BIZ
{
    public class CuentaPorCobrarManager : GenericManager<CuentaPorCobrar>, ICuentaCobrarManager
    {
        public CuentaPorCobrarManager(IGenericRepository<CuentaPorCobrar> repositorio) : base (repositorio)
        {}

        public IEnumerable<CuentaPorCobrar> SearchCuentaCobrar(string text)
        {
            return _repositorio.Query(x => x.Venta.NombreProveedor != null &&
                          x.Venta.NombreProveedor.ToLowerInvariant().Contains(text.ToLowerInvariant()) ||
                          x.Venta.RazonSocialProveedor != null &&
                          x.Venta.RazonSocialProveedor.ToLowerInvariant().Contains(text.ToLowerInvariant()) ||
                          x.Venta.Dni != null &&
                          x.Venta.Dni.Contains(text) ||
                          x.Venta.Ruc != null &&
                          x.Venta.Ruc.Contains(text) ||
                          x.FechaHoraCreacion != null &&
                          x.FechaHoraCreacion.ToString().Contains(text));
        }
    }
}
