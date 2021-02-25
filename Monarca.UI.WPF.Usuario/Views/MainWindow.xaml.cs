using Monarca.UI.WPF.Usuario.CustomControls;
using Monarca.UI.WPF.Usuario.Helpers;
using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace Monarca.UI.WPF.Usuario
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (CheckOneTimeInternetConnection.CheckInternetConnection())
                SetDisponible();
            else
                SetNoDisponible();

            NetworkChange.NetworkAvailabilityChanged += AvailabilityChanged;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            DialogResult result = CustomMessageBox.Show("¿Está seguro que desea cerrar la aplicación?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
            if (result == System.Windows.Forms.DialogResult.Yes)
                base.OnClosing(e);
            else
                e.Cancel = true;
        }

        private void AvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            SolidColorBrush blueBrush = new SolidColorBrush();
            blueBrush.Color = Colors.Blue;
            App.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                if (e.IsAvailable)
                    SetDisponible();
                else
                    SetNoDisponible();
            }));
        }

        private void SetNoDisponible()
        {
            elp.Fill = new SolidColorBrush(System.Windows.Media.Colors.Red);
            ElipseToolTip.Content = "Consulta RUC y DNI no disponible";
            txtInternet.Text = "Internet no disponible";
        }

        private void SetDisponible()
        {
            elp.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
            ElipseToolTip.Content = "Consulta RUC y DNI disponible";
            txtInternet.Text = "Internet disponible";
        }
    }
}
