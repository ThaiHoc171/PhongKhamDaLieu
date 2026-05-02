using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels;

namespace HoanMyClinic.Pages;

public partial class NhanVienPage : Page
{
	public NhanVienPage()
	{
		InitializeComponent();

		var vm = new NhanVienViewModel();
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();

		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns();
	}

	private void SetupColumns()
	{
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("NhanVienID")
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Họ và tên",
			Binding = new Binding("HoTen"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Email",
			Binding = new Binding("Email"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Chức vụ",
			Binding = new Binding("TenChucVu"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		// BUTTONS (COMMAND PATTERN)
		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditCommand", "Sửa"));
		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Sync", "ToogleCommand", "Sa thải / Vào làm lại"));

		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Public", "PublicCommand", "Tạo hồ sơ công khai"));
	}
}