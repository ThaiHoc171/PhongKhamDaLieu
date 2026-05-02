using System.Windows.Input;
using System.Windows;
namespace HoanMyClinic.Common;

public class RelayCommand : ICommand
{
	private readonly Func<Task>? _executeAsync;
	private readonly Action? _executeSync;
	private readonly Func<bool>? _canExecute;
	private bool _isExecuting;

	public RelayCommand(Func<Task> execute, Func<bool>? canExecute = null)
	{
		_executeAsync = execute;
		_canExecute = canExecute;
	}

	public RelayCommand(Action execute, Func<bool>? canExecute = null)
	{
		_executeSync = execute;
		_canExecute = canExecute;
	}

	public bool CanExecute(object? parameter)
		=> !_isExecuting && (_canExecute?.Invoke() ?? true);

	public async void Execute(object? parameter)
	{
		if (!CanExecute(parameter)) return;

		try
		{
			_isExecuting = true;
			RaiseCanExecuteChanged();

			if (_executeSync != null)
			{
				_executeSync();
			}
			else if (_executeAsync != null)
			{
				await _executeAsync();
			}
		}
		finally
		{
			_isExecuting = false;
			RaiseCanExecuteChanged();
		}
	}

	public event EventHandler? CanExecuteChanged;
	public void RaiseCanExecuteChanged()
	{
		if (Application.Current?.Dispatcher?.CheckAccess() == true)
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
		else
		{
			Application.Current?.Dispatcher.BeginInvoke(() =>
			{
				CanExecuteChanged?.Invoke(this, EventArgs.Empty);
			});
		}
	}
}
public class RelayCommandWithParam<T> : ICommand
{
	private readonly Func<T?, Task> _execute;
	private readonly Func<T?, bool>? _canExecute;
	private bool _isExecuting;

	public RelayCommandWithParam(Func<T?, Task> execute, Func<T?, bool>? canExecute = null)
	{
		_execute = execute;
		_canExecute = canExecute;
	}

	public bool CanExecute(object? parameter)
	{
		if (_isExecuting) return false;

		if (_canExecute == null) return true;

		if (parameter is T t)
			return _canExecute(t);

		if (parameter == null && default(T) == null)
			return _canExecute(default);

		return false;
	}

	public async void Execute(object? parameter)
	{
		if (!CanExecute(parameter)) return;

		try
		{
			_isExecuting = true;
			RaiseCanExecuteChanged();

			T? value = parameter is T t ? t : default;
			await _execute(value);
		}
		finally
		{
			_isExecuting = false;
			RaiseCanExecuteChanged();
		}
	}

	public event EventHandler? CanExecuteChanged;

	public void RaiseCanExecuteChanged()
	{
		if (Application.Current?.Dispatcher?.CheckAccess() == true)
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
		else
		{
			Application.Current?.Dispatcher.BeginInvoke(() =>
			{
				CanExecuteChanged?.Invoke(this, EventArgs.Empty);
			});
		}
	}
}