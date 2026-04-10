using System.Windows;
using WPF.Windows;

namespace WPF
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			ShowLogin();
		}
		private void ShowLogin()
		{
			var login = new loginWindow();
			var result = login.ShowDialog();

			if (result == true)
			{
				ShowMain();
			}
			else
			{
				Shutdown();
			}
		}
		private void ShowMain()
		{
			var main = new appClinic();
			Application.Current.MainWindow = main;

			main.Show(); 

			main.Closed += (s, e) =>
			{	
				ShowLogin();
			};
		}
	}
}