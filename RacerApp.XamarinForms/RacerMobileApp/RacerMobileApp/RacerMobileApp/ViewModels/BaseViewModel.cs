using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RacerMobileApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        protected void SetProperty<T>(ref T backingField, T newValue, [CallerMemberName]
         string propertyName = null)
        {
            if (Equals(backingField, newValue))
                return;
            backingField = newValue;
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }


        public void OnPropertyChanged([CallerMemberName]string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
