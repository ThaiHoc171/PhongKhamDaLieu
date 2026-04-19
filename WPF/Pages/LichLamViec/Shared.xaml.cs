using System.Windows.Controls;
using WPF.Common;
using WPF.ViewModels.LichLamViec;

namespace WPF.Pages.LichLamViec;

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