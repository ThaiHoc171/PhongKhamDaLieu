using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels.CaKham;

namespace HoanMyClinic.Pages.CaKham;

public partial class Blank : Page
{
	public Blank()
	{
		InitializeComponent();

		var vm = new BlankViewModel();
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();

		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns(vm);
	}

	private void SetupColumns(BlankViewModel vm)
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
			Header = "Ngày khám",
			Binding = new Binding("NgayKham") { StringFormat = "dd/MM/yyyy" },
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Khung giờ",
			Binding = new Binding("TenKhungGio"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("AccountPlus", "RegisterCommand", "Đăng ký"));
	}
}