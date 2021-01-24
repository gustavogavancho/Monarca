using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.DAL.Local.LiteDB;

namespace Monarca.BIZ
{
    public class FactoryRepository
    {
        string _origen;

        public FactoryRepository(string origen)
        {
            _origen = origen;
        }

        public IGenericRepository<T> CrearRepositorio<T>() where T : BaseEntity
        {
            switch (_origen)
            {
                case "LiteDB":
                    return new GenericRepository<T>();
                default:
                    return null;
            }
        }
    }
}
