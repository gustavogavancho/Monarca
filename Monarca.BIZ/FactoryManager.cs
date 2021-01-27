﻿using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;

namespace Monarca.BIZ
{
    public class FactoryManager
    {
        public FactoryManager(string origen)
        {
            FactoryRepository factoryRepository = new FactoryRepository(origen);
            this.CrearClienteManager = new ClienteManager(factoryRepository.CrearRepositorio<Cliente>());
            this.CrearProveedorManager = new ProveedorManager(factoryRepository.CrearRepositorio<Proveedor>());
            this.CrearProductoManager = new ProductoManager(factoryRepository.CrearRepositorio<Producto>());
            this.CrearVentaManager = new VentaManager(factoryRepository.CrearRepositorio<Venta>());
        }
        public IClienteManager CrearClienteManager { get; }
        public IProveedorManager CrearProveedorManager { get; }
        public IVentaManager CrearVentaManager { get; }
        public IProductoManager CrearProductoManager { get; }
    }
}
