using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels;

namespace HoanMyClinic.Pages;

public partial class ChucVuPage : Page
{
	public ChucVuPage()
	{
		InitializeComponent();

		var vm = new ChucVuViewModel();
		DataContext = vm;
		Loaded += async (_, __) => await vm.Init();
		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns(vm);
	}

	private void SetupColumns(ChucVuViewModel vm)
	{
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("ChucVuID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên chức vụ",
			Binding = new Binding("TenChucVu"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add( SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditCommand", "Sửa"));

		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("AccountKey", "AuthorizeCommand", "Phân quyền"));
	}
}