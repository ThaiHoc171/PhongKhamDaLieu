using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HoanMyClinic.Common;

public class BaseViewModel : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string name = "")
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

	#region LOADING

	private bool _isLoading;
	public bool IsLoading
	{
		get => _isLoading;
		set
		{
			if (_isLoading == value) return;
			_isLoading = value;
			OnPropertyChanged();
		}
	}

	#endregion

	#region HELPER

	protected async Task RunAsync(Func<Task> action)
	{
		try
		{
			IsLoading = true;
			await action();
		}
		finally
		{
			IsLoading = false;
		}
	}

	#endregion
}