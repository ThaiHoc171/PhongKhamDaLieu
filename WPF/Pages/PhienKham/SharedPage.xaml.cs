using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels.PhienKham;

namespace HoanMyClinic.Pages.PhienKham;
public partial class SharedPage : Page
{
	public SharedPage()
	{
		InitializeComponent();

		var vm = new SharedViewModel();
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
			Header = "Bệnh nhân",
			Binding = new Binding("BenhNhan"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Bác sĩ",
			Binding = new Binding("NhanVien"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày khám",
			Binding = new Binding("NgayKham")
			{
				StringFormat = "dd/MM/yyyy"
			}
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai")
		});

		GridContent.Columns.Add(
			SetupDataGrid.CreateIconButtonColumnVer2("Eye", "ViewCommand", "Xem"));
	}
}