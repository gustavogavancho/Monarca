using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Monarca.UI.WPF.Usuario.Helpers
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Propiedades_Full_Region
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        #endregion

        #region Metodos_Protected_Region
        /// <summary>
        /// Realiza la acción de comparación, asignación y notificación de cambio de la propiedad a la que se hace referencia.
        /// </summary>
        /// <typeparam name="T">Tipo de dato del cual es la propiedad</typeparam>
        /// <param name="backingStore">Variable con valor actual a cambiar de la propiedad (Se utiliza ref para hacer referencia al tipo de dato de la misma)</param>
        /// <param name="value">Valor nuevo a comparar para la propiedad</param>
        /// <param name="propertyName">Nombre de la propiedad (Se toma automáticamente por el compiler services)</param>
        /// <param name="onChanged">Acción de cambio de la propiedad (Se invoca de manera automática cuando se realiza el cambio de valor)</param>
        /// <returns>Devuelve verdadero si el valor ha sido cambiado, devuelve falso en caso contrario</returns>
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value)) //Comparación de valores (Actual y nuevo)
            {
                return false;
            }
            //Asignación, realización y notificación de cambio
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Método que realiza la notificación de cambio de propiedad 
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad a cambiar</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Eventos_Region
        /// <summary>
        /// Evento que se invoca para notificar el cambio de una propiedad
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
