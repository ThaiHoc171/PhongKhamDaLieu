using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common;
using WPF.ViewModels;

namespace WPF.Pages;

public partial class BenhNhanPage : Page
{
	public BenhNhanPage()
	{
		InitializeComponent();

		var vm = new BenhNhanViewModel();
		DataContext = vm;
		Loaded += async (_, __) => await vm.Init();
		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns(vm);
	}

	private void SetupColumns(BenhNhanViewModel vm)
	{
		GridContent.Columns.Clear();
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("BenhNhanID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã Thông Tin",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("ThongTinID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Họ và tên",
			Binding = new Binding("HoTen"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày sinh",
			Binding = new Binding("NgaySinh")
			{
				StringFormat = "dd/MM/yyyy"
			},
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Giới Tính",
			Binding = new Binding("GioiTinh"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Số điện thoại",
			Binding = new Binding("SDT"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Email",
			Binding = new Binding("EmailLienHe"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		// BUTTON	
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("FileText", "HoSoCommand", "Hồ sơ"));
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditCommand", "Sửa"));
	}
}