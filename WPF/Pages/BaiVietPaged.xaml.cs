using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels;

namespace HoanMyClinic.Pages
{
	/// <summary>
	/// Interaction logic for BaiVietPaged.xaml
	/// </summary>
	public partial class BaiVietPaged : Page
	{
		public BaiVietPaged()
		{
			InitializeComponent();
			var vm = new BaiVietViewModel();
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
				Binding = new Binding("BaiVietID"),
				Width = new DataGridLength(1, DataGridLengthUnitType.Star)
			});

			GridContent.Columns.Add(new DataGridTextColumn
			{
				Header = "Tiêu đề",
				Binding = new Binding("TieuDe"),
				Width = new DataGridLength(3, DataGridLengthUnitType.Star)
			});

			GridContent.Columns.Add(new DataGridTextColumn
			{
				Header = "Lượt xem",
				Binding = new Binding("LuotXem"),
				Width = new DataGridLength(2, DataGridLengthUnitType.Star)
			});

			GridContent.Columns.Add(new DataGridTextColumn
			{
				Header = "Ngày đăng",
				Binding = new Binding("NgayDang")
				{
					StringFormat = "dd/MM/yyyy"
				},
				Width = new DataGridLength(2, DataGridLengthUnitType.Star)
			});

			GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumnVer2("Pencil", "EditCommand", "Sửa"));
		}
	}
}
