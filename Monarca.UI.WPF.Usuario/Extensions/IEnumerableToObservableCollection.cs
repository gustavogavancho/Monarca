using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Monarca.UI.WPF.Usuario.Extensions
{
    public static class IEnumerableToObservableCollection
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> _LinqResult)
        {
            return new ObservableCollection<T>(_LinqResult);
        }
    }
}
