using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels;

namespace HoanMyClinic.Pages;

public partial class ViewDetailPhongPage : Page
{
	public ViewDetailPhongPage(int id, string name)
	{
		InitializeComponent();

		var vm = new ViewDetailPhongViewModel(id, name);
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();

		SetupDataGrid.ApplyStyle(GridContent);
		SetupDataGrid.ApplyStyle(GridDetail);

		SetupColumns(vm);
	}

	private void SetupColumns(ViewDetailPhongViewModel vm)
	{
		// ===== LEFT GRID =====
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("PCN_TB_ID")
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Thiết bị",
			Binding = new Binding("ThietBi"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Số lượng",
			Binding = new Binding("TongSoLuong"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Eye", "ViewCommand", "Xem")
		);

		// ===== RIGHT GRID =====
		GridDetail.Columns.Clear();

		GridDetail.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã tài sản",
			Binding = new Binding("MaTaiSan"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridDetail.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày nhập",
			Binding = new Binding("NgayNhap")
			{
				StringFormat = "dd/MM/yyyy"
			}
		});

		GridDetail.Columns.Add(new DataGridTextColumn
		{
			Header = "Tình trạng",
			Binding = new Binding("TinhTrang")
		});

		GridDetail.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditCommand", "Sửa")
		);

		GridDetail.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Minus", "DeleteCommand", "Xóa")
		);
	}
}