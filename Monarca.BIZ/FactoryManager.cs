using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;

namespace Monarca.BIZ
{
    public class FactoryManager
    {
        public FactoryManager(string origen)
        {
            FactoryRepository factoryRepository = new FactoryRepository(origen);
            this.CrearClienteManager = new ClienteManager(factoryRepository.CrearRepositorio<Cliente>());
            this.CrearProductoManager = new ProductoManager(factoryRepository.CrearRepositorio<Producto>());
            this.CrearVentaManager = new VentaManager(factoryRepository.CrearRepositorio<Venta>());
        }
        public IClienteManager CrearClienteManager { get; set; }
        public IVentaManager CrearVentaManager { get; set; }
        public IProductoManager CrearProductoManager { get; set; }
    }
}
