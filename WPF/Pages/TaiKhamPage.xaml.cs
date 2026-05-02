using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels;

namespace HoanMyClinic.Pages;

public partial class TaiKhamPage : Page
{
	public TaiKhamPage()
	{
		InitializeComponent();
		var vm = new TaiKhamViewModel();
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
			Binding = new Binding("TaiKhamID")
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên bệnh nhân",
			Binding = new Binding("BenhNhan.Name"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày dự kiến",
			Binding = new Binding("NgayDuKien")
			{
				StringFormat = "dd/MM/yyyy"
			},
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Lý do",
			Binding = new Binding("LyDo"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
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
