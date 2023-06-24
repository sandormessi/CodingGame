namespace WpfApplication.Core.Abstraction;

using System.Windows.Input;

public interface IViewModelCommand : ICommand
{
   #region Public Methods and Operators

   void Update();

   #endregion
}