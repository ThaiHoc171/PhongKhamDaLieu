using System.Windows.Controls;
using HoanMyClinic.ViewModels;

namespace HoanMyClinic.Pages;

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