using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using System.Collections.Generic;

namespace Monarca.BIZ
{
    public class GenericManager<T> : IGenericManager<T> where T : BaseEntity
    {
        internal readonly IGenericRepository<T> _repositorio;

        public GenericManager(IGenericRepository<T> repositorio)
        {
            _repositorio = repositorio;
        }

        public string Error => _repositorio.Error;
        public IEnumerable<T> ObtenerTodo => _repositorio.Read;
        public T Actualizar(T entidad) => _repositorio.Update(entidad);
        public bool Eliminar(string id) => _repositorio.Delete(id);
        public T Insertar(T entidad) => _repositorio.Create(entidad);
        public T SearchById(string id) => _repositorio.SearchById(id);
    }
}
