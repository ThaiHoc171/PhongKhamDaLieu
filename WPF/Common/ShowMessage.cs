using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace WPF.Common
{
	public static class MessageHelper
	{
		private const string DIALOG_ID = "RootDialog";

		public static async Task ShowMessage(string message)
		{
			if (DialogHost.IsDialogOpen(DIALOG_ID))
				return;
			var dialog = new StackPanel
			{
				Margin = new Thickness(24),
				Children =
				{
					new TextBlock
					{
						Text = message,
						FontSize = 16,
						Margin = new Thickness(0,0,0,20)
					},
					new Button
					{
						Content = "OK",
						Width = 90,
						HorizontalAlignment = HorizontalAlignment.Right,
						Command = DialogHost.CloseDialogCommand
					}
				}
			};

			await DialogHost.Show(dialog, DIALOG_ID);
		}

		public static async Task<bool> Confirm(string message)
		{
			bool result = false;

			var ok = new Button { Content = "Đồng ý", Width = 90, Margin = new Thickness(5) };
			var cancel = new Button { Content = "Hủy", Width = 90, Margin = new Thickness(5) };

			ok.Click += (s, e) =>
			{
				result = true;
				DialogHost.CloseDialogCommand.Execute(null, null);
			};

			cancel.Click += (s, e) =>
			{
				result = false;
				DialogHost.CloseDialogCommand.Execute(null, null);
			};

			var dialog = new StackPanel
			{
				Margin = new Thickness(24),
				Children =
				{
					new TextBlock
					{
						Text = message,
						FontSize = 16,
						Margin = new Thickness(0,0,0,20)
					},
					new StackPanel
					{
						Orientation = Orientation.Horizontal,
						HorizontalAlignment = HorizontalAlignment.Right,
						Children = { ok, cancel }
					}
				}
			};

			await DialogHost.Show(dialog, DIALOG_ID);

			return result;
		}
	}
}