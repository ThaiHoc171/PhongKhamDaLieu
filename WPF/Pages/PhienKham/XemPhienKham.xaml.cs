using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using WPF.Client;
using WPF.Common;
using WPF.Models;
using WPF.Windows;
using WPF.Windows.HSBenhAn;
using WPF.Windows.ToaThuoc;

namespace WPF.Pages.PhienKham;


public partial class XemPhienKham : Page
{
	public XemPhienKham(int id)
	{
		InitializeComponent();
		DataContext = this;

		_id = id;

		SetupDataGrid.ApplyStyle(gridBenh);
		SetupDataGrid.ApplyStyle(gridThietBi);
		SetupDataGrid.ApplyStyle(gridCLS);

		Loaded += async (_, __) =>
		{
			SetupColumns();
			await LoadData();
		};
	}
	private readonly int _id;
	private NameHelper _benhNhan = new();
	#region api clients
	private readonly PhienKhamClient _client = new();
	private readonly UploadClient _upload = new();
	private readonly PhongChucNangClient _pcn = new();
	private readonly PhienKhamBenhClient _pkBenhClient = new();
	private readonly PhienKhamThietBiClient _pkThietBiClient = new();
	private readonly PhienKhamClsClient _pkClsClient = new();
	private readonly ToaThuocClient _toaThuocClient = new();
	private readonly BenhNhanClient _benhNhanClient = new();
	private readonly HoSoBenhAnClient _hoSoBenhAnClient = new();
	#endregion
	private async Task LoadData()
	{
		try
		{
			IsLoading = true;

			await Load_PhienKham();
			await Load_Benh();
			await Load_ThietBi();
			await Load_CLS();
		}
		finally
		{
			IsLoading = false;
		}
	}
	#region LoadData
	private bool _isLoading;
	public bool IsLoading
	{
		get => _isLoading;
		set { _isLoading = value; OnPropertyChanged(); }
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	private void OnPropertyChanged([CallerMemberName] string name = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
	public ObservableCollection<PhienKhamBenhReadModel> BenhItems { get; set; } = new();
	public ObservableCollection<PhienKhamThietBiReadModel> ThietBiItems { get; set; } = new();
	public ObservableCollection<PhienKhamClsReadListModel> CLSItems { get; set; } = new();

	private async Task Load_PhienKham()
	{
		IsLoading = true;
		var res = await _client.Detail(_id);
		if (!res.Success)
		{
			SnackbarHelper.ShowError(res.Message);
		}
		var data = res.Data;
		_benhNhan = data.BenhNhan;
		txtBenhNhan.Text = data.BenhNhan.Name;
		txtNhanVien.Text = data.NhanVien.Name;
		dtpNgayKham.SelectedDate = data.NgayKham;
		txtTrangThai.Text = data.TrangThai;
		txtTrieuChung.Text = data.TrieuChung;
		txtChanDoanCuoi.Text = data.ChanDoanCuoi;
		txtGhiChu.Text = data.GhiChu;
		cboPhong.SelectedValue = data.PhongChucNangID;
		if (!string.IsNullOrEmpty(data.HinhAnh))
		{
			var url = $"https://hoanmyclinic.s3.ap-southeast-2.amazonaws.com/{data.HinhAnh}";
			pic.Source = new BitmapImage(new Uri(url));
		}
	}
	private async Task Load_Benh()
	{
		var res = await _pkBenhClient.GetByPhienKhamId(_id);

		if (!res.Success) return;

		BenhItems.Clear();

		foreach (var item in res.Data)
			BenhItems.Add(item);

		gridBenh.ItemsSource = BenhItems;
	}
	private async Task Load_ThietBi()
	{
		var res = await _pkThietBiClient.GetByPhienKham(_id);

		if (!res.Success) return;

		ThietBiItems.Clear();

		foreach (var item in res.Data)
			ThietBiItems.Add(item);

		gridThietBi.ItemsSource = ThietBiItems;
	}
	private async Task Load_CLS()
	{
		var res = await _pkClsClient.GetByPhienKham(_id);

		if (!res.Success) return;

		CLSItems.Clear();

		foreach (var item in res.Data)
			CLSItems.Add(item);

		gridCLS.ItemsSource = CLSItems;
	}
	#endregion
	private void SetupColumns()
	{
		// ===== BỆNH =====
		gridBenh.Columns.Clear();

		gridBenh.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên bệnh",
			Binding = new Binding("LoaiBenh.Name"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		gridBenh.Columns.Add(new DataGridTextColumn
		{
			Header = "Loại chẩn đoán",
			Binding = new Binding("LoaiChanDoan"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		// ===== THIẾT BỊ =====
		gridThietBi.Columns.Clear();

		gridThietBi.Columns.Add(new DataGridTextColumn
		{
			Header = "Thiết bị",
			Binding = new Binding("TenThietBi"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		gridThietBi.Columns.Add(new DataGridTextColumn
		{
			Header = "Phòng",
			Binding = new Binding("TenPhong"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		gridThietBi.Columns.Add(new DataGridTextColumn
		{
			Header = "Ghi chú",
			Binding = new Binding("GhiChu"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		// ===== CLS =====
		gridCLS.Columns.Clear();

		gridCLS.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên CLS",
			Binding = new Binding("TenCLS"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		gridCLS.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		gridCLS.Columns.Add(new DataGridTextColumn
		{
			Header = "Kết quả",
			Binding = new Binding("KetQua"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		gridCLS.Columns.Add(new DataGridTextColumn
		{
			Header = "Ghi chú",
			Binding = new Binding("GhiChu"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
	}
	private void Back_Click(object sender, RoutedEventArgs e)
	{
		var parent = Window.GetWindow(this) as appClinic;
		parent?.OpenPage(new PhienKhamCaNhanPage(), "Phiên khám cá nhân");
	}


	#region Popup
	private void btnAction_Click(object sender, RoutedEventArgs e)
	{
		ActionPopup.IsOpen = !ActionPopup.IsOpen;
	}

	private async void btnMedicine_Click(object sender, RoutedEventArgs e)
	{
		var parentWindow = Window.GetWindow(this);
		var overlay = parentWindow?.FindName("Overlay") as Border;

		if (overlay != null)
			overlay.Visibility = Visibility.Visible;

		try
		{
			var res = await _toaThuocClient.Exists(_id);
			if (!res.Data)
			{
				SnackbarHelper.ShowSuccess("Phiên khám chưa có toa thuốc");
				return;
			}

			var win = new XemToaThuoc(_id)
			{
				Owner = parentWindow
			};

			var result = win.ShowDialog();

			if (result == true)
			{
				await LoadData();
			}
		}
		finally
		{
			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}
	private async void btnPatientRecord_Click(object sender, RoutedEventArgs e)
	{
		var parentWindow = Window.GetWindow(this);
		var overlay = parentWindow?.FindName("Overlay") as Border;
		if (_benhNhan == null || _benhNhan.Id <= 0)
		{
			SnackbarHelper.ShowError("Không tìm thấy thông tin bệnh nhân!");
			return;
		}
		try
		{
			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var res = await _hoSoBenhAnClient.GetByBenhNhanId(_benhNhan.Id);

			if (!res.Success || res.Data == null)
			{
				SnackbarHelper.ShowError("Không tìm thấy hồ sơ bệnh án cho bệnh nhân này!");
				return;
			}

			var win = new XemHoSo(_benhNhan.Id, _benhNhan.Name)
			{
				Owner = parentWindow
			};

			var result = win.ShowDialog();

			if (result == true)
			{
				await LoadData();
			}
		}
		catch (Exception ex)
		{
			SnackbarHelper.ShowError($"Có lỗi xảy ra: {ex.Message}");
		}
		finally
		{
			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}

	#endregion
	private async void Refresh_Click(object sender, RoutedEventArgs e)
	{
		await LoadData();
	}
}

