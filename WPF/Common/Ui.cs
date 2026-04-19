using System.Windows;

namespace WPF.Common;

public static class Ui
{
	public static Task RunAsync(Func<Task> action)
	{
		return Application.Current.Dispatcher.InvokeAsync(action).Task;
	}

	public static Task Run(Action action)
	{
		return Application.Current.Dispatcher.InvokeAsync(() =>
		{
			action();
		}).Task;
	}
}