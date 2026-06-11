using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Calculyator_Trchanova.ViewModels
{
    // Базовый класс для всех ViewModel с реализацией INotifyPropertyChanged
    public class BaseViewModel :INotifyPropertyChanged
    {
        // Событие изменения свойства
        public event PropertyChangedEventHandler PropertyChanged;

        // Метод вызова события изменения свойства
        protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Метод установки значения свойства с уведомлением об изменении
        protected bool SetProperty<T> (ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}