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
        ComprasViewModel _comprasViewModel = new ComprasViewModel();

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
            };
            CloseCommand = new RelayCommand(OnClose);
            NavCommand = new RelayCommand(OnNav);
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
            }
        }

        private void OnClose()
        {
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().Close();
        }
    }
}
