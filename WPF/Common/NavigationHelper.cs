using System;
using System.Windows;
using System.Windows.Navigation;
using WPF.Pages;
using WPF.Windows;

namespace WPF.Common;

public class NavigationHelper
{
	public void Navigate(string key)
	{
		if (!NavigationRoutes.Routes.TryGetValue(key, out var factory))
			return;

		if (!HasAccess(key))
		{
			SnackbarHelper.ShowWarning("Bạn không có quyền truy cập!");
			return;
		}

		var page = factory.Invoke();

		Application.Current.Dispatcher.BeginInvoke(new Action(() =>
		{
			if (Application.Current.MainWindow is appClinic main)
			{
				main.txtHeader.Text = NavigationRoutes.Titles.GetValueOrDefault(key, key);
				main.MainFrame.Navigate(page);
			}
		}));
	}
	private bool HasAccess(string key)
	{
		if (Session.VaiTro == "Admin")
			return true;

		if (Session.Permissions == null || !Session.Permissions.Any())
			return false;

		if (!NavigationRoutes.Permissions.TryGetValue(key, out var requiredPermission)
			|| string.IsNullOrWhiteSpace(requiredPermission))
			return true;

		// Check permission
		return Session.Permissions.Contains(requiredPermission);
	}
	public void GoBack()
	{
		Application.Current.Dispatcher.BeginInvoke(new Action(() =>
		{
			if (Application.Current.MainWindow is appClinic main)
			{
				if (main.MainFrame.CanGoBack)
					main.MainFrame.GoBack();
				else
					main.MainFrame.Navigate(new DashboardPage());
			}
		}));
	}
}