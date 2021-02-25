using System;
using System.Globalization;
using System.Windows.Data;

namespace Monarca.UI.WPF.Usuario.Converters
{
    public class BoolToStringVenta : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string boolToString = "";
            if ((bool)value == true)
            {
                boolToString = "Eliminada";
            }
            else if ((bool)value == false)
            {
                boolToString = "Vigente";
            }
            else
            {
                boolToString = "";
            }
            return boolToString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
