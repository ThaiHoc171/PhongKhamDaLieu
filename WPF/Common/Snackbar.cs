using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF.Common
{
	public static class SnackbarHelper
	{
		private static ISnackbarMessageQueue? _messageQueue;

		private static Snackbar? _snackbar;

		public static void Init(Snackbar snackbar)
		{
			_snackbar = snackbar;
			_snackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
			_messageQueue = snackbar.MessageQueue;
		}

		public static void ShowSuccess(string message)
		{
			Show(message, Brushes.White, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50")), PackIconKind.CheckCircle);
		}

		public static void ShowWarning(string message)
		{
			Show(message, Brushes.Black, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC107")), PackIconKind.InfoCircleOutline);
		}

		public static void ShowError(string message)
		{
			Show(message, Brushes.White, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F44336")), PackIconKind.CloseCircle);
		}

		private static void Show(string message, Brush foreground, Brush background, PackIconKind iconKind)
		{
			if (_messageQueue == null || _snackbar == null) return;

			_snackbar.Background = background;
			_snackbar.Foreground = foreground;

			var panel = new StackPanel
			{
				Orientation = Orientation.Horizontal
			};

			var icon = new PackIcon
			{
				Kind = iconKind,
				Margin = new Thickness(0, 0, 8, 0),
				Foreground = foreground
			};

			var text = new TextBlock
			{
				Text = message,
				Foreground = foreground,
				VerticalAlignment = VerticalAlignment.Center,
				TextWrapping = TextWrapping.Wrap
			};

			panel.Children.Add(icon);
			panel.Children.Add(text);

			_messageQueue.Enqueue(panel);

		}
	}
}