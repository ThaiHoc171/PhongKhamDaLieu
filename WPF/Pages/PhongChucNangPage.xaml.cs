using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels;

namespace HoanMyClinic.Pages;

public partial class PhongChucNangPage : Page
{
	public PhongChucNangPage()
	{
		InitializeComponent();

		var vm = new PhongChucNangViewModel();
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();

		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns(vm);
	}

	private void SetupColumns(PhongChucNangViewModel vm)
	{
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Binding = new Binding("PhongChucNangID")
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên phòng",
			Binding = new Binding("TenPhong"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		// BUTTON (MVVM)
		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Eye", "ViewCommand", "Xem"));

		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditCommand", "Sửa"));

		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Sync", "ToggleStatusCommand", "Đổi trạng thái"));
	}
}