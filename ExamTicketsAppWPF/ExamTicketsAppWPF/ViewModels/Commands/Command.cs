using System;
using System.Windows.Input;


namespace ExamTicketsAppWPF.ViewModels.Commands
{
	public partial class Command : ICommand
	{
		Action<object?> execute;
		Func<object?, bool>? canExecute;

		public event EventHandler? CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public Command(Action<object?> execute, Func<object?, bool>? canExecute = null)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public bool CanExecute(object? parameter)
		{
			return true;
		}

		public void Execute(object? parameter)
		{
		
			this.execute(parameter);
		}
	}
}
