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
        static FactoryManager _factoryManager = new FactoryManager("LiteDB");

        InicioViewModel _inicioViewModel = new InicioViewModel();
        ClientesViewModel _clientesViewModel = new ClientesViewModel(_factoryManager);
        ProductosViewModel _productosViewModel = new ProductosViewModel();
        VentasViewModel _ventasViewModel = new VentasViewModel();

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
    }
}
