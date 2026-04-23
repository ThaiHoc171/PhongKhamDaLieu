using System.Windows.Controls;
using WPF.ViewModels;

namespace WPF.Pages;

public partial class DashboardPage : Page
{
	public DashboardPage()
	{
		InitializeComponent();

		var vm = new DashboardViewModel();
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();
	}
}