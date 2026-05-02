using System.Windows.Controls;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels.LichLamViec;

namespace HoanMyClinic.Pages.LichLamViec;

public partial class Shared : Page
{
	public Shared()
	{
		InitializeComponent();

		var vm = new SharedViewModel();
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();
	}
}