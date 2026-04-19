using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common;
using WPF.ViewModels;

namespace WPF.Pages;

public partial class KhachPage : Page
{
	public KhachPage()
	{
		InitializeComponent();

		var vm = new KhachViewModel();
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();

		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns(vm);
	}

	private void SetupColumns(KhachViewModel vm)
	{
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("ThongTinID")
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Họ tên",
			Binding = new Binding("HoTen"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Giới tính",
			Binding = new Binding("GioiTinh"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày sinh",
			Binding = new Binding("NgaySinh") { StringFormat = "dd/MM/yyyy" }
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "SĐT",
			Binding = new Binding("SDT")
		});

		// BUTTONS (COMMAND PATTERN)
		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditCommand", "Sửa"));

		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("AccountPlus", "CreateBenhNhanCommand", "Tạo BN"));
	}
}