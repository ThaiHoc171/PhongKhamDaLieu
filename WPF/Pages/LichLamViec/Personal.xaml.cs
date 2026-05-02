using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Pages.LichLamViec;

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