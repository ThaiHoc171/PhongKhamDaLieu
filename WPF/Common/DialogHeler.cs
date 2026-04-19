using System.Windows;

namespace WPF.Common;

public static class DialogHelper
{
	public static async Task OpenDialogAsync(Window dialog, Func<Task> onSuccess)
	{
		var result = dialog.ShowDialog();
		if (result == true)
		{
			await onSuccess();
		}
	}
}