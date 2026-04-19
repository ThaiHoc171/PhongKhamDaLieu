using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WPF.Client;
using WPF.Common;
using WPF.Models;
using WPF.ViewModels;
using WPF.Windows.LoaiBenh;
using WPF.Windows.Thuoc;

namespace WPF.Pages;

public partial class LoaiBenhPage : Page
{
	public LoaiBenhPage()
	{
		InitializeComponent();

		var vm = new LoaiBenhViewModel();
		DataContext = vm;
		Loaded += async (_, __) => await vm.Init();
		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns(vm);
	}
	private void SetupColumns(LoaiBenhViewModel vm)
	{
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("LoaiBenhID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên Bệnh",
			Binding = new Binding("TenBenh"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Nhóm bệnh",
			Binding = new Binding("NhomBenh"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Độ nghiêm trọng",
			Binding = new Binding("MucDoNghiemTrong"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditCommand", "Sửa"));
		//GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Delete", "DeleteCommand", "Xóa"));
	}

}

