using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels;

namespace HoanMyClinic.Pages;

public partial class CanLamSangPage : Page
{
	public CanLamSangPage()
	{
		InitializeComponent();

		var vm = new CanLamSangViewModel();
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();

		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns(vm);
	}

	private void SetupColumns(CanLamSangViewModel vm)
	{
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("CanLamSangID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên cận lâm sàng",
			Binding = new Binding("TenCLS"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Loại",
			Binding = new Binding("LoaiXetNghiem"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditCommand", "Sửa"));
	}
}