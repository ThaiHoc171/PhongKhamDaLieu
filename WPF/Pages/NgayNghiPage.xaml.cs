using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels;

namespace HoanMyClinic.Pages;

public partial class NgayNghiPage : Page
{
	public NgayNghiPage()
	{
		InitializeComponent();
		var vm = new NgayNghiViewModel();
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
			Binding = new Binding("NgayNghiID")
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên nhân viên",
			Binding = new Binding("NhanVien.Name"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày nghỉ",
			Binding = new Binding("Ngay") { StringFormat = "dd/MM/yyyy" },
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Lý do",
			Binding = new Binding("LyDo"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditCommand", "Sửa"));
		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Delete", "DeleteCommand", "Xoá"));
	}
}