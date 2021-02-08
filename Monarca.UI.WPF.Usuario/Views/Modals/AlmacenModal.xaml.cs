using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.Models;
using System.Windows;
using System.Windows.Input;

namespace Monarca.UI.WPF.Usuario.Views.Modals
{
    public partial class AlmacenModal : Window
    {
        FactoryManager _factoryManager;
        IProductoManager _productoManager;
        IProveedorManager _proveedorManager;
        AlmacenModel _almacen;

        public AlmacenModal(FactoryManager factoryManager, AlmacenModel almacen)
        {
            _factoryManager = factoryManager;
            _productoManager = factoryManager.CrearProductoManager;
            _proveedorManager = factoryManager.CrearProveedorManager;
            Producto producto = _productoManager.SearchById(almacen.IdProducto);
            Proveedor proveedor = _proveedorManager.SearchById(almacen.IdProveedor);
            InitializeComponent();
            txtNombreProducto.Text = producto.Nombre;
            txtDescripcionProducto.Text = producto.Descripción;
            txtMarcaProducto.Text = producto.Marca;
            txtUnidadProducto.Text = producto.Unidad.ToString();
            txtNombreApellidoProveedor.Text = $"{proveedor.Nombres} {proveedor.Apellidos}";
            txtRazonSocialProveedor.Text = proveedor.RazonSocial;
            txtDniProveedor.Text = proveedor.Dni.ToString();
            txtRucProveedor.Text = proveedor.Ruc.ToString();
            txtCantidadComprada.Text = almacen.CantidadComprada.ToString("n");
            txtCantidadDisponible.Text = almacen.CantidadVendida.ToString("n");
            txtCantidadDisponible.Text = almacen.Stock.ToString("n");
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
