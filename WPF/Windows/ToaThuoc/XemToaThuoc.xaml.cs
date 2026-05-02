using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Client;
using HoanMyClinic.Common;

namespace HoanMyClinic.Windows.ToaThuoc;

public partial class XemToaThuoc : Window
{
	public XemToaThuoc(int phienKhamId)
	{
		InitializeComponent();
		_id = phienKhamId;
		txtId.Text = _id.ToString();
		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns();
		GridContent.ItemsSource = Items;
	}
	private readonly int _id;
	private readonly ToaThuocClient _client = new();
	private ObservableCollection<ToaThuocItem> Items = new();
	private void SetupColumns()
	{
		GridContent.Columns.Clear();

		// ID
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("ThuocID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		// NAME
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên thuốc",
			Binding = new Binding("TenThuoc"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Số lượng",
			Binding = new Binding("SoLuong"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Liều dùng",
			Binding = new Binding("LieuDung"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
	}
	private async void XemToaThuoc_Loaded(object sender, RoutedEventArgs e)
	{
		var result = await _client.GetByPhienKham(_id);
		if (result.Success && result.Data != null)
		{
			var data = result.Data;
			txtDoctor.Text = data.NguoiLap.Name;
			txtNotes.Text = data.GhiChu;
			dtpNgayTao.SelectedDate = data.NgayLap;
			Items = new ObservableCollection<ToaThuocItem>(data.Thuoc.Select(x => new ToaThuocItem
			{
				ThuocID = x.ThuocID,
				TenThuoc = x.TenThuoc ?? "",
				SoLuong = x.SoLuong,
				LieuDung = x.LieuDung
			}));
			GridContent.ItemsSource = Items;
		}
	}

	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}

	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
	public class ToaThuocItem
	{
		public int ThuocID { get; set; }
		public string TenThuoc { get; set; } = "";
		public int SoLuong { get; set; }
		public string? LieuDung { get; set; }
	}
}
