using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Pages.LichLamViec;

public partial class Personal : Page
{
	public Personal()
	{
		InitializeComponent();

		var vm = new PersonalViewModel();
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();
	}
}