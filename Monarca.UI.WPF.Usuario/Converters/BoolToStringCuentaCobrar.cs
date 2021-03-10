using System;
using System.Globalization;
using System.Windows.Data;

namespace Monarca.UI.WPF.Usuario.Converters
{
    public class BoolToStringCuentaCobrar : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string boolToString = "";
            if ((bool)value == true)
            {
                boolToString = "Liquidado";
            }
            else if ((bool)value == false)
            {
                boolToString = "Pendiente";
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
