using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.CustomControls;
using Monarca.UI.WPF.Usuario.Extensions;
using Monarca.UI.WPF.Usuario.Helpers;
using Monarca.UI.WPF.Usuario.Views.Modals;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace Monarca.UI.WPF.Usuario.ViewModels
{
    public class ProductosViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;
        IProductoManager _productoManager;

        private ObservableCollection<Producto> _productos = new ObservableCollection<Producto>();
        public ObservableCollection<Producto> Productos
        {
            get => _productos;
            set => SetProperty(ref _productos, value);
        }

        private Producto _producto = new Producto();
        public Producto Producto
        {
            get => _producto;
            set
            {
                SetProperty(ref _producto, value);
                ReadCommand.RaiseCanExecuteChanged();
                EditCommnad.RaiseCanExecuteChanged();
                DeleteCommnad.RaiseCanExecuteChanged();
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
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand EditCommnad { get; private set; }
        public RelayCommand DeleteCommnad { get; set; }
        public RelayCommand SearchCommand { get; set; }

        public ProductosViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            _productoManager = _factoryManager.CrearProductoManager;
            ReadCommand = new RelayCommand(OnRead, CanReadEditDelete);
            AddCommand = new RelayCommand(OnAdd);
            EditCommnad = new RelayCommand(OnEdit, CanReadEditDelete);
            DeleteCommnad = new RelayCommand(OnDelete, CanReadEditDelete);
            SearchCommand = new RelayCommand(OnSearch);
            UpdateData();
        }

        private void OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                Productos = _productoManager.SearchProducto(SearchText).ToObservableCollection();
            }
            else
            {
                Productos = _productoManager.ObtenerTodo.ToObservableCollection();
            }
        }

        private void OnRead()
        {
            if (new ProductosModal(_factoryManager, "Read", Producto).ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnAdd()
        {
            if (new ProductosModal(_factoryManager, "Add").ShowDialog().Value) 
            {
                UpdateData();
            }
        }

        private void OnEdit()
        {
            if (new ProductosModal(_factoryManager, "Edit", Producto).ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnDelete()
        {
            DialogResult result = CustomMessageBox.Show("¿Está seguro que desea borrar la información del producto?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                _productoManager.Eliminar(Producto.Id);
                UpdateData();
            }
        }

        private bool CanReadEditDelete()
        {
            if (!string.IsNullOrWhiteSpace(Producto?.Id))
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
            Productos = _productoManager.ObtenerTodo.ToObservableCollection();
            SearchText = "";
            if (Productos.Count >= 1)
            {
                VisibilityListBox = true;
                VisibilityBorder = false;
            }
            else
            {
                VisibilityBorder = true;
                VisibilityListBox = false;
            }
        }
    }
}
