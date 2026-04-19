using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common;
using WPF.ViewModels.CaKham;

namespace WPF.Pages.CaKham;

public partial class WaitPage : Page
{
	public WaitPage()
	{
		InitializeComponent();

		var vm = new WaitPageViewModel();
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
			Binding = new Binding("CaKhamID")
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Họ và tên",
			Binding = new Binding("HoTen")
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Lý do",
			Binding = new Binding("LyDoKham")
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày khám",
			Binding = new Binding("NgayKham")
			{
				StringFormat = "dd/MM/yyyy"
			}
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Khung giờ",
			Binding = new Binding("TenKhungGio")
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai")
		});

		// BUTTON → COMMAND
		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Check", "AcceptCommand", "Xác nhận"));

		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Cancel", "CancelCommand", "Từ chối"));
	}
}