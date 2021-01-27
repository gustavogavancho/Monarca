using Monarca.COMMON.Entidades;
using System.Collections.Generic;

namespace Monarca.COMMON.Interfaces
{
    public interface IProveedorManager : IGenericManager<Proveedor>
    {
        IEnumerable<Proveedor> SearchProveedor(string text);
    }
}
