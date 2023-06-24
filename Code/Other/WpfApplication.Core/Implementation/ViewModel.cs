namespace WpfApplication.Core.Implementation;

using System.ComponentModel;
using System.Runtime.CompilerServices;

using WpfApplication.Core.Abstraction;

public class ViewModel : IViewModel
{
   #region Public Events

   public event PropertyChangedEventHandler? PropertyChanged;

   #endregion

   #region Methods

   protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
   {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }

   #endregion
}