using Monarca.BIZ;
using Monarca.UI.WPF.Usuario.Helpers;
using Monarca.UI.WPF.Usuario.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Monarca.UI.WPF.Usuario.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        static FactoryManager _factoryManager = new FactoryManager("LiteDB");

        InicioViewModel _inicioViewModel = new InicioViewModel();
        ClientesViewModel _clientesViewModel = new ClientesViewModel(_factoryManager);
        ProveedoresViewModel _proveedoresViewModel = new ProveedoresViewModel(_factoryManager);
        ProductosViewModel _productosViewModel = new ProductosViewModel(_factoryManager);
        VentasViewModel _ventasViewModel = new VentasViewModel();
        ComprasViewModel _comprasViewModel = new ComprasViewModel(_factoryManager);
        GastosOperativosViewModel _gastosOperativosViewModel = new GastosOperativosViewModel(_factoryManager);
        CuentasPorCobrarViewModel _cuentasPorCobrarViewModel = new CuentasPorCobrarViewModel();
        AlmacenViewModel _almacenViewModel = new AlmacenViewModel(_factoryManager);

        private ObservableCollection<CurrentUserControl> _userControlList;

        public ObservableCollection<CurrentUserControl> UserControlList
        {
            get => _userControlList;
            set => SetProperty(ref _userControlList, value);
        }

        private CurrentUserControl _userControl;
        public CurrentUserControl UserControl
        {
            get => _userControl;
            set => SetProperty(ref _userControl, value);
        }

        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public RelayCommand CloseCommand { get; set; }
        public RelayCommand NavCommand { get; set; }

        public MainViewModel()
        {
            UserControlList = new ObservableCollection<CurrentUserControl>
            {
                new CurrentUserControl
                {
                    Ventana = "Inicio",
                    Icon = "/Images/home.png"
                },
                new CurrentUserControl
                {
                    Ventana = "Clientes",
                    Icon = "/Images/persona.png"
                },
                new CurrentUserControl
                {
                    Ventana = "Proveedores",
                    Icon = "/Images/people.png"
                },
                new CurrentUserControl
                {
                    Ventana = "Productos",
                    Icon = "/Images/product.png"
                },
                new CurrentUserControl
                {
                    Ventana = "Compras",
                    Icon = "/Images/compras.png"
                },
                new CurrentUserControl
                {
                    Ventana = "Ventas",
                    Icon = "/Images/ventas.png"
                },
                new CurrentUserControl
                {
                    Ventana = "Gastos Operativos",
                    Icon = "/Images/gastospersonales.png"
                },
                new CurrentUserControl
                {
                    Ventana = "Cuentas por cobrar",
                    Icon = "/Images/cuentacobrar.png"
                },
                new CurrentUserControl
                {
                    Ventana = "Almacén",
                    Icon = "/Images/almacen.png"
                },
            };
            CloseCommand = new RelayCommand(OnClose);
            NavCommand = new RelayCommand(OnNav);
            _comprasViewModel.AlmacenUpdate += _comprasViewModel_AlmacenUpdate;
        }

        private void _comprasViewModel_AlmacenUpdate()
        {
            _almacenViewModel = new AlmacenViewModel(_factoryManager);
        }

        public void OnNav()
        {
            switch (UserControl.Ventana)
            {
                case "Inicio":
                    CurrentViewModel = _inicioViewModel;
                    break;
                case "Clientes":
                    CurrentViewModel = _clientesViewModel;
                    break;
                case "Proveedores":
                    CurrentViewModel = _proveedoresViewModel;
                    break;
                case "Productos":
                    CurrentViewModel = _productosViewModel;
                    break;
                case "Ventas":
                    CurrentViewModel = _ventasViewModel;
                    break;
                case "Compras":
                    CurrentViewModel = _comprasViewModel;
                    break;
                case "Gastos Operativos":
                    CurrentViewModel = _gastosOperativosViewModel;
                    break;
                case "Cuentas por cobrar":
                    CurrentViewModel = _cuentasPorCobrarViewModel;
                    break;
                case "Almacén":
                    CurrentViewModel = _almacenViewModel;
                    break;
            }
        }

        public void LoadMainView()
        {
            UserControl = UserControlList[0];
            CurrentViewModel = _inicioViewModel;
        }

        private void OnClose()
        {
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().Close();
        }
    }
}
