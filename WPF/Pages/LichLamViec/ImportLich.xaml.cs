using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common;
using WPF.ViewModels;

namespace WPF.Pages.LichLamViec;

public partial class ImportLich : Page
{
	public ImportLich()
	{
		InitializeComponent();

		var vm = new ImportLichViewModel();
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();

		SetupDataGrid.ApplyStyle(PreviewDataGrid);
		SetupColumns(vm);
	}
	private void SetupColumns(ImportLichViewModel vm)
	{
		PreviewDataGrid.Columns.Clear();
		PreviewDataGrid.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã Nhân Viên",
			Binding = new Binding("NhanVienID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		PreviewDataGrid.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày",
			Binding = new Binding("Ngay")
			{
				StringFormat = "dd/MM/yyyy"
			},
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		PreviewDataGrid.Columns.Add(new DataGridTextColumn
		{
			Header = "Ca làm việc",
			Binding = new Binding("CaLamViec"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		PreviewDataGrid.Columns.Add(new DataGridTextColumn
		{
			Header = "Ghi chú",
			Binding = new Binding("GhiChu"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
	}
}