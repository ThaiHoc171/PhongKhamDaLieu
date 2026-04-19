using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common;
using WPF.ViewModels.PhienKham;

namespace WPF.Pages.PhienKham;

public partial class PersonalPage : Page
{
	public PersonalPage()
	{
		InitializeComponent();

		var vm = new ViewModels.PhienKham.PersonalViewModel();
		DataContext = vm;

		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns();

		Loaded += async (_, __) => await vm.Init();
	}

	private void SetupColumns()
	{
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Bệnh nhân",
			Binding = new Binding("BenhNhan"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày khám",
			Binding = new Binding("NgayKham") { StringFormat = "dd/MM/yyyy" },
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Chẩn đoán cuối",
			Binding = new Binding("ChanDoanCuoi"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Play", "StartCommand", "Khám", "Đang chờ,Đang khám"));
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Cancel", "CancelCommand", "Hủy", "Đang chờ,Đang khám"));
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Eye", "ViewCommand", "Xem"));
	}
}