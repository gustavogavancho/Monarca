using Monarca.COMMON.Entidades;
using System.Collections.Generic;

namespace Monarca.COMMON.Interfaces
{
    public interface IGenericManager<T> where T : BaseEntity
    {
        string Error { get; }
        T Insertar(T entidad);
        IEnumerable<T> ObtenerTodo { get; }
        T Actualizar(T entidad);
        bool Eliminar(string id);
        T SearchById(string id);
    }
}
