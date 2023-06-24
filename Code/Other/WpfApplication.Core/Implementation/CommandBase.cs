namespace WpfApplication.Core.Implementation;

using System;

using WpfApplication.Core.Abstraction;

public abstract class CommandBase : IViewModelCommand
{
   #region Public Events

   public event EventHandler? CanExecuteChanged;

   #endregion

   #region IViewModelCommand Members

   public bool CanExecute(object? parameter)
   {
      return DoCanExecute(parameter);
   }

   public void Execute(object? parameter)
   {
      DoExecute(parameter);
   }

   public void Update()
   {
      throw new NotImplementedException();
   }

   #endregion

   #region Methods

   protected virtual bool DoCanExecute(object? parameter)
   {
      return true;
   }

   protected abstract void DoExecute(object? parameter);

   protected virtual void OnCanExecuteChanged()
   {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
   }

   #endregion
}