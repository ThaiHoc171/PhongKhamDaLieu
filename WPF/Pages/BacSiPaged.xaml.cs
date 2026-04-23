using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common;
using WPF.ViewModels;
using System.Windows;

namespace WPF.Pages;

public partial class BacSiPaged : Page
{
	public BacSiPaged()
	{
		InitializeComponent();

		var vm = new BacSiPublicViewModel();
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
			Header = "Họ tên",
			Binding = new Binding("HoTen"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Chuyên môn",
			Binding = new Binding("ChuyenMon"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày cập nhật",
			Binding = new Binding("NgayCapNhat")
			{
				StringFormat = "dd/MM/yyyy"
			},
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditCommand", "Sửa"));
	}
}
