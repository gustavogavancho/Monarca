using Monarca.COMMON.Entidades;
using System.Collections.Generic;

namespace Monarca.COMMON.Interfaces
{
    public interface ICompraManager : IGenericManager<Compra>
    {
        IEnumerable<Compra> SearchCompra(string text);
    }
}
