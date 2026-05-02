using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels;

namespace HoanMyClinic.Pages;

public partial class ThietBiPage : Page
{
	public ThietBiPage()
	{
		InitializeComponent();

		var vm = new ThietBiViewModel();
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();

		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns(vm);
	}

	private void SetupColumns(ThietBiViewModel vm)
	{
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Binding = new Binding("ThietBiID")
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên thiết bị",
			Binding = new Binding("TenTB"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Loại",
			Binding = new Binding("LoaiTB"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		// BUTTON (MVVM)
		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditCommand", "Sửa"));
	}
}