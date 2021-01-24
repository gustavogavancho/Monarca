using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Monarca.UI.WPF.Usuario.CustomControls
{
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox()
        {
            InitializeComponent();
        }

        static CustomMessageBox cMessageBox;
        static DialogResult result = System.Windows.Forms.DialogResult.No;

        public enum CMessageBoxButton
        {
            Aceptar,
            No,
            Si,
            Cancelar,
            Confirmar
        }

        public enum CMessageBoxTitle
        {
            Error,
            Información,
            Advertencia,
            Confirmación
        }

        public static DialogResult Show(string message, CMessageBoxTitle title, CMessageBoxButton btnOk, CMessageBoxButton btnNo)
        {
            cMessageBox = new CustomMessageBox();
            cMessageBox.txtMessage.Text = message;
            cMessageBox.btnOk.Content = cMessageBox.GetMessageButton(btnOk);
            cMessageBox.btnCancel.Content = cMessageBox.GetMessageButton(btnNo);
            cMessageBox.txtTitle.Text = cMessageBox.GetTile(title);

            //Icon
            switch (title)
            {
                case CMessageBoxTitle.Error:
                    cMessageBox.iconMsgError.Visibility = Visibility.Visible;
                    break;
                case CMessageBoxTitle.Información:
                    cMessageBox.iconMsgInformacion.Visibility = Visibility.Visible;
                    cMessageBox.btnCancel.Visibility = Visibility.Collapsed;
                    cMessageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CMessageBoxTitle.Advertencia:
                    cMessageBox.iconMsgAdvertencia.Visibility = Visibility.Visible;
                    cMessageBox.btnCancel.Visibility = Visibility.Collapsed;
                    cMessageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CMessageBoxTitle.Confirmación:
                    cMessageBox.iconMsgConfirmacion.Visibility = Visibility.Visible;
                    break;
            }
            cMessageBox.ShowDialog();
            return result;
        }

        public string GetTile(CMessageBoxTitle value)
        {
            return Enum.GetName(typeof(CMessageBoxTitle), value);
        }

        //For buttons
        public string GetMessageButton(CMessageBoxButton value)
        {
            return Enum.GetName(typeof(CMessageBoxButton), value);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.Yes;
            cMessageBox.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.No;
            cMessageBox.Close();
        }
    }
}
