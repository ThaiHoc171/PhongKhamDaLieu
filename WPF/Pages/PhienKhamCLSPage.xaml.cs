using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common;
using WPF.ViewModels;

namespace WPF.Pages;

public partial class PhienKhamCLSPage : Page
{
	public PhienKhamCLSPage()
	{
		InitializeComponent();

		var vm = new PhienKhamCLSViewModel();
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();

		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns(vm);
	}

	private void SetupColumns(PhienKhamCLSViewModel vm)
	{
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = System.Windows.Visibility.Collapsed,
			Binding = new Binding("PhienKhamCLSID")
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên CLS",
			Binding = new Binding("TenCLS"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Kết quả",
			Binding = new Binding("KetQua"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày thực hiện",
			Binding = new Binding("NgayThucHien")
			{
				StringFormat = "dd/MM/yyyy"
			},
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		// BUTTON (chuẩn MVVM)
		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Play", "AcceptCommand", "Nhận / Thực hiện", "Đang chờ,Đang thực hiện"));

		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Eye", "ViewCommand", "Xem"));

		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Cancel", "CancelCommand", "Hủy", "Đang chờ,Đang thực hiện"));
	}
}