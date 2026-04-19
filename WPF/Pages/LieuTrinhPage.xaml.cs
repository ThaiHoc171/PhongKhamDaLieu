using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common;
using WPF.ViewModels;

namespace WPF.Pages;

public partial class LieuTrinhPage : Page
{
	public LieuTrinhPage()
	{
		InitializeComponent();
		var vm = new LieuTrinhViewModel();
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
			Visibility = System.Windows.Visibility.Collapsed,
			Binding = new Binding("LieuTrinhID")
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên liệu trình",
			Binding = new Binding("TenLieuTrinh"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên bệnh nhân",
			Binding = new Binding("BenhNhan"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Số buổi",
			Binding = new Binding("TongSoBuoi"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày bắt đầu",
			Binding = new Binding("NgayBatDau")
			{
				StringFormat = "dd/MM/yyyy"
			},
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày kết thúc",
			Binding = new Binding("NgayKetThuc")
			{
				StringFormat = "dd/MM/yyyy"
			},
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Eye", "ViewCommand", "Xem"));
	}
}
