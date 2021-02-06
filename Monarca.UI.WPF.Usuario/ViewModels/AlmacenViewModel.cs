using Monarca.BIZ;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.Helpers;
using Monarca.UI.WPF.Usuario.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Monarca.UI.WPF.Usuario.ViewModels
{
    public class AlmacenViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;
        ICompraManager _compraManager;
        IProductoManager _productoManager;
        IProveedorManager _proveedorManger;
        IVentaManager ventaManager;

        private ObservableCollection<AlmacenModel> _almacenes = new ObservableCollection<AlmacenModel>();
        public ObservableCollection<AlmacenModel> Almacenes
        {
            get => _almacenes;
            set => SetProperty(ref _almacenes, value);
        }

        private AlmacenModel _almacen = new AlmacenModel();
        public AlmacenModel Almacen
        {
            get => _almacen;
            set
            {
                SetProperty(ref _almacen, value);
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private bool _visibilityListBox;
        public bool VisibilityListBox
        {
            get => _visibilityListBox;
            set => SetProperty(ref _visibilityListBox, value);
        }

        private bool _visibilityBorder;
        public bool VisibilityBorder
        {
            get => _visibilityBorder;
            set => SetProperty(ref _visibilityBorder, value);
        }



        public AlmacenViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            _compraManager = factoryManager.CrearCompraManager;
            _proveedorManger = factoryManager.CrearProveedorManager;
            _productoManager = factoryManager.CrearProductoManager;
            UpdateData();
        }

        private void UpdateData()
        {
            Almacenes = new ObservableCollection<AlmacenModel>();
            SearchText = "";
            var compras = _compraManager.ObtenerTodo;
            if (compras.Count() >= 1)
            {
                VisibilityListBox = true;
                VisibilityBorder = false;
                foreach (var item in _compraManager.ObtenerTodo)
                {
                    var almacen = new AlmacenModel
                    {
                        IdProducto = item.IdProducto,
                        IdProveedor = item.IdProveedor,
                        NombreProducto = item.NombreProducto,
                        MarcaProducto = item.MarcaProducto,
                        CantidadComprada = item.Cantidad,

                    };
                    if (string.IsNullOrWhiteSpace(item.NombreProveedor))
                    {
                        almacen.NombreProveedor = item.RazonSocialProveedor;
                    }
                    else
                    {
                        almacen.NombreProveedor = item.NombreProveedor;
                    }
                    Almacenes.Add(almacen);
                }
            }
            else
            {
                VisibilityBorder = true;
                VisibilityListBox = false;
            }
        }
    }
}
