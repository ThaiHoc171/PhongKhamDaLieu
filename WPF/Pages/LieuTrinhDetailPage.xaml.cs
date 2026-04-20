using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common;
using WPF.ViewModels;
using WPF.ViewModels.LieuTrinh;
using WPF.ViewModels.PhienKham;

namespace WPF.Pages;

public partial class LieuTrinhDetailPage : Page
{
	public LieuTrinhDetailPage(int lieuTrinhId)
	{
		InitializeComponent();
		var vm = new LieuTrinhDetailViewModel(lieuTrinhId);
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
			Binding = new Binding("BuoiDieuTriID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Buổi",
			Binding = new Binding("SoBuoi"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày dự kiến",
			Binding = new Binding("NgayDuKien")
			{
				StringFormat = "dd/MM/yyyy"
			},
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Eye", "ViewCommand", "Xem"));

	}
}
