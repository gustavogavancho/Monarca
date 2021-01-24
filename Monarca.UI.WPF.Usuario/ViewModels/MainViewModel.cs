using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.Helpers;
using Monarca.UI.WPF.Usuario.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Monarca.UI.WPF.Usuario.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        InicioViewModel _inicioViewModel = new InicioViewModel();
        ClientesViewModel _clientesViewModel = new ClientesViewModel();
        ProductosViewModel _productosViewModel = new ProductosViewModel();
        VentasViewModel _ventasViewModel = new VentasViewModel();

        FactoryManager _factoryManager = new FactoryManager("LiteDB");
        IClienteManager _clienteManager;
        List<Cliente> _listaClientes;

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
                    Icon = "/Images/people.png"
                },
                new CurrentUserControl
                {
                    Ventana = "Productos",
                    Icon = "/Images/product.png"
                },
                new CurrentUserControl
                {
                    Ventana = "Ventas",
                    Icon = "/Images/ventas.png"
                }
            };
            _clienteManager = _factoryManager.CrearClienteManager;
            CrearCliente();
            _listaClientes = ClienteObtenerTodo();
            CloseCommand = new RelayCommand(OnClose);
            NavCommand = new RelayCommand(OnNav);
        }

        private void OnNav()
        {
            switch (UserControl.Ventana)
            {
                case "Inicio":
                    CurrentViewModel = _inicioViewModel;
                    break;
                case "Clientes":
                    CurrentViewModel = _clientesViewModel;
                    break;
                case "Productos":
                    CurrentViewModel = _productosViewModel;
                    break;
                case "Ventas":
                    CurrentViewModel = _ventasViewModel;
                    break;
            }
        }

        private void OnClose()
        {
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().Close();
        }

        public void CrearCliente()
        {
            _clienteManager.Insertar(new Cliente
            {
                Nombres = "Gustavo",
            });
        }

        public List<Cliente> ClienteObtenerTodo()
        {
            return _clienteManager.ObtenerTodo.ToList();
        }
    }
}
