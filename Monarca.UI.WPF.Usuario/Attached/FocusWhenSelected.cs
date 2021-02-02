using System.Windows;
using System.Windows.Controls;

namespace Monarca.UI.WPF.Usuario.Attached
{
    public static class FocusWhenSelected
    {
        public static DependencyProperty FocusWhenSelectedProperty = DependencyProperty.RegisterAttached(
            "FocusWhenSelected",
            typeof(bool),
            typeof(FocusWhenSelected),
            new PropertyMetadata(false, new PropertyChangedCallback(OnFocusWhenSelectedChanged)));

        private static void OnFocusWhenSelectedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var i = (ListBoxItem)obj;
            if ((bool)args.NewValue)
                i.Selected += i_Selected;
            else
                i.Selected -= i_Selected;
        }

        static void i_Selected(object sender, RoutedEventArgs e)
        {
            ((ListBoxItem)sender).Focus();
        }
    }
}
