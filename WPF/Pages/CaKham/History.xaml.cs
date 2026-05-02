using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels;

namespace HoanMyClinic.Pages.CaKham;

public partial class History : Page
{
	public History()
	{
		InitializeComponent();

		var vm = new HistoryViewModel();
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();

		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns(vm);
	}

	private void SetupColumns(HistoryViewModel vm)
	{
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("CaKhamID")
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Họ và tên",
			Binding = new Binding("HoTen"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Lý do",
			Binding = new Binding("LyDoKham"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày khám",
			Binding = new Binding("NgayKham")
			{
				StringFormat = "dd/MM/yyyy"
			},
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

		// BUTTON (IMPORTANT: dùng Command, không event)
		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Eye", "ViewCommand", "Xem"));
	}
}