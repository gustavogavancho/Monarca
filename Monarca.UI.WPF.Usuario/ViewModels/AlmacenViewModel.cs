using Monarca.BIZ;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.Extensions;
using Monarca.UI.WPF.Usuario.Helpers;
using Monarca.UI.WPF.Usuario.Models;
using Monarca.UI.WPF.Usuario.Views.Modals;
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
                ReadCommand.RaiseCanExecuteChanged();
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

        public RelayCommand ReadCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand ExportCommand { get; private set; }

        public AlmacenViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            _compraManager = factoryManager.CrearCompraManager;
            _proveedorManger = factoryManager.CrearProveedorManager;
            _productoManager = factoryManager.CrearProductoManager;
            ReadCommand = new RelayCommand(OnRead, CanRead);
            SearchCommand = new RelayCommand(OnSearch);
            ExportCommand = new RelayCommand(OnExport);
            UpdateData();
        }

        private void OnExport()
        {
        }

        private void OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                ProcessAlmacenData();
                Almacenes = Almacenes.Where(x => x.NombreProducto.ToLowerInvariant().Contains(SearchText.ToLowerInvariant())).ToObservableCollection();
            }
            else
            {
                UpdateData();
            }

        }

        private void OnRead()
        {
            if (new AlmacenModal(_factoryManager, Almacen).ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private bool CanRead()
        {
            if (!string.IsNullOrWhiteSpace(Almacen?.IdProducto))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateData()
        {
            SearchText = "";
            ProcessAlmacenData();
        }

        private void ProcessAlmacenData()
        {
            Almacenes = new ObservableCollection<AlmacenModel>();
            var compras = _compraManager.ObtenerTodo;
            if (compras.Count() >= 1)
            {
                VisibilityListBox = true;
                VisibilityBorder = false;
                var compraData = _compraManager.ObtenerTodo;
                foreach (var item in _compraManager.ObtenerTodo.DistinctBy(x => x.IdProducto))
                {
                    var almacen = new AlmacenModel
                    {
                        IdProducto = item.IdProducto,
                        IdProveedor = item.IdProveedor,
                        NombreProducto = item.NombreProducto,
                        MarcaProducto = item.MarcaProducto,
                        CantidadComprada = compraData.Where(x => x.IdProducto == item.IdProducto).Sum(x => x.Cantidad),
                        Stock = compraData.Where(x => x.IdProducto == item.IdProducto).Sum(x => x.Cantidad),
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
