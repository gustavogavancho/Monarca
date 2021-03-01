using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;

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
            [Description("Aceptar")]
            Aceptar,
            [Description("No")]
            No,
            [Description("Si")]
            Si,
            [Description("Cancelar")]
            Cancelar,
            [Description("Confirmar")]
            Confirmar,
            [Description("Nota de venta")]
            NotaDeVenta,
            [Description("Boleta/Factura")]
            BoletaFactura,
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

        public string GetEnumDescription(Enum enumObj)
        {
            if (enumObj == null)
            {
                return string.Empty;
            }
            FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

            object[] attribArray = fieldInfo?.GetCustomAttributes(false);

            if (attribArray?.Length == 0)
            {
                return enumObj.ToString();
            }
            else
            {
                DescriptionAttribute attrib = attribArray?[0] as DescriptionAttribute;
                return attrib?.Description;
            }
        }

        //For buttons
        public string GetMessageButton(CMessageBoxButton value)
        {
            return GetEnumDescription(value);
            //return Enum.GetName(typeof(CMessageBoxButton), value);
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
