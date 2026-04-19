using System.Windows;
using System.Windows.Controls;

namespace WPF.Common;
public static class OverlayHelper
{
	public static Border? GetOverlay(FrameworkElement element)
	{
		var window = Window.GetWindow(element);
		return window?.FindName("Overlay") as Border;
	}

	public static void Show(Border? overlay)
	{
		if (overlay != null)
			overlay.Visibility = Visibility.Visible;
	}

	public static void Hide(Border? overlay)
	{
		if (overlay != null)
			overlay.Visibility = Visibility.Collapsed;
	}
}