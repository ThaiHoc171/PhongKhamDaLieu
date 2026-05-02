using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels.PhienKham;
namespace HoanMyClinic.Pages.PhienKham;
public partial class ViewPage : Page
{
	public ViewPage(int id)
	{
		InitializeComponent();
		if (Session.VaiTro == "Admin")
			btnBack.Visibility = System.Windows.Visibility.Collapsed;
		var vm = new LogViewModel(id);
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();

		SetupDataGrid.ApplyStyle(DataGridBenh);
		SetupDataGrid.ApplyStyle(DataGridThietBi);
		SetupDataGrid.ApplyStyle(DataGridCLS);

		SetupColumns();

	}

	private void SetupColumns()
	{

		// ===== BỆNH =====
		DataGridBenh.Columns.Clear();

		DataGridBenh.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên bệnh",
			Binding = new Binding("LoaiBenh.Name"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		DataGridBenh.Columns.Add(new DataGridTextColumn
		{
			Header = "Loại chẩn đoán",
			Binding = new Binding("LoaiChanDoan"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		// ===== THIẾT BỊ =====
		DataGridThietBi.Columns.Clear();

		DataGridThietBi.Columns.Add(new DataGridTextColumn
		{
			Header = "Thiết bị",
			Binding = new Binding("TenThietBi"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		DataGridThietBi.Columns.Add(new DataGridTextColumn
		{
			Header = "Phòng",
			Binding = new Binding("TenPhong"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		DataGridThietBi.Columns.Add(new DataGridTextColumn
		{
			Header = "Ghi chú",
			Binding = new Binding("GhiChu"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		// ===== CLS =====
		DataGridCLS.Columns.Clear();

		DataGridCLS.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên CLS",
			Binding = new Binding("TenCLS"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		DataGridCLS.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		DataGridCLS.Columns.Add(new DataGridTextColumn
		{
			Header = "Kết quả",
			Binding = new Binding("KetQua"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		DataGridCLS.Columns.Add(new DataGridTextColumn
		{
			Header = "Ghi chú",
			Binding = new Binding("GhiChu"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
	}
}