using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common;
using WPF.ViewModels.PhienKham;

namespace WPF.Pages.PhienKham;

public partial class ConsultationPage : Page
{
	public ConsultationPage(int id)
	{
		InitializeComponent();

		var vm = new ConsultationViewModel(id);
		DataContext = vm;

		Loaded += async (_, __) => await vm.Init();
		SetupDataGrid.ApplyStyle(DataGridBenh);
		SetupDataGrid.ApplyStyle(DataGridCLS);
		SetupDataGrid.ApplyStyle(DataGridThietBi);
		SetupColumns(vm);

	}
	private void SetupColumns(ConsultationViewModel vm)
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
		DataGridBenh.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditBenhCommand", "Sửa"));
		DataGridBenh.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Cancel", "DeleteBenhCommand", "Xóa"));
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
		DataGridThietBi.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditThietBiCommand", "Sửa"));
		DataGridThietBi.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Cancel", "DeleteThietBiCommand", "Xóa"));

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
		DataGridCLS.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Eye", "ViewClsCommand", "Xem"));
		DataGridCLS.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Cancel", "CancelClsCommand", "Hủy yêu cầu"));
	}
}