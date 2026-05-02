using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels;

namespace HoanMyClinic.Pages;

public partial class ThuocPage : Page
{
	public ThuocPage()
	{
		InitializeComponent();

		var vm = new ThuocViewModel();
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();

		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns(vm);
	}

	private void SetupColumns(ThuocViewModel vm)
	{
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("ThuocID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên thuốc",
			Binding = new Binding("TenThuoc"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Hoạt chất",
			Binding = new Binding("HoatChat"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		// BUTTON
		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditCommand", "Sửa"));

		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Delete", "DeleteCommand", "Xóa"));
	}
}