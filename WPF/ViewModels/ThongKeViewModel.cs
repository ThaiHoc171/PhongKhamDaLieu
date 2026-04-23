using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.ViewModels;

public class ThongKeViewModel : BaseViewModel
{
	private readonly ThongKeClient _client = new();

	public event Action? TabChanged;

	public List<string> LoaiKhoangList { get; } = new() { "day", "week", "month", "year" };

	private string _selectedLoaiKhoang = "month";
	public string SelectedLoaiKhoang
	{
		get => _selectedLoaiKhoang;
		set { _selectedLoaiKhoang = value; OnPropertyChanged(); }
	}

	private DateTime? _tuNgay;
	public DateTime? TuNgay
	{
		get => _tuNgay;
		set { _tuNgay = value; OnPropertyChanged(); }
	}

	private DateTime? _denNgay;
	public DateTime? DenNgay
	{
		get => _denNgay;
		set { _denNgay = value; OnPropertyChanged(); }
	}

	private ThongKeFilterRequest BuildFilter() => new()
	{
		LoaiKhoang = SelectedLoaiKhoang,
		TuNgay = TuNgay,
		DenNgay = DenNgay,
	};


	public ICommand ApplyFilterCommand { get; }


	private int _activeTab = 0;

	private void SetTab(int idx)
	{
		_activeTab = idx;
		OnPropertyChanged(nameof(IsTabBenhNhan));
		OnPropertyChanged(nameof(IsTabCaKham));
		OnPropertyChanged(nameof(IsTabPhienKham));
		OnPropertyChanged(nameof(IsTabToaThuoc));
		OnPropertyChanged(nameof(IsTabNhanVien));
		OnPropertyChanged(nameof(BenhNhanVisibility));
		OnPropertyChanged(nameof(CaKhamVisibility));
		OnPropertyChanged(nameof(PhienKhamVisibility));
		OnPropertyChanged(nameof(ToaThuocVisibility));
		OnPropertyChanged(nameof(NhanVienVisibility));
		TabChanged?.Invoke();
		_ = LoadTabData();
	}

	public bool IsTabBenhNhan
	{
		get => _activeTab == 0;
		set { if (value) SetTab(0); }
	}
	public bool IsTabCaKham
	{
		get => _activeTab == 1;
		set { if (value) SetTab(1); }
	}
	public bool IsTabPhienKham
	{
		get => _activeTab == 2;
		set { if (value) SetTab(2); }
	}
	public bool IsTabToaThuoc
	{
		get => _activeTab == 3;
		set { if (value) SetTab(3); }
	}
	public bool IsTabNhanVien
	{
		get => _activeTab == 4;
		set { if (value) SetTab(4); }
	}

	public Visibility BenhNhanVisibility => _activeTab == 0 ? Visibility.Visible : Visibility.Collapsed;
	public Visibility CaKhamVisibility => _activeTab == 1 ? Visibility.Visible : Visibility.Collapsed;
	public Visibility PhienKhamVisibility => _activeTab == 2 ? Visibility.Visible : Visibility.Collapsed;
	public Visibility ToaThuocVisibility => _activeTab == 3 ? Visibility.Visible : Visibility.Collapsed;
	public Visibility NhanVienVisibility => _activeTab == 4 ? Visibility.Visible : Visibility.Collapsed;

	// ─── Loading text ────────────────────────────────────────────────────────

	private string _loadingText = "Sẵn sàng";
	public string LoadingText
	{
		get => _loadingText;
		set { _loadingText = value; OnPropertyChanged(); }
	}

	// ─── Tab 0: Bệnh nhân ────────────────────────────────────────────────────

	public TongQuanBenhNhanReadModel? TongQuanBN { get; set; }
	public ObservableCollection<BenhNhanTheoNgayReadModel> BenhNhanTheoNgay { get; } = new();
	public ObservableCollection<BenhNhanTheoGioiTinhReadModel> BenhNhanTheoGioiTinh { get; } = new();
	public ObservableCollection<BenhNhanTheoDoTuoiReadModel> BenhNhanTheoDoTuoi { get; } = new();

	// ─── Tab 1: Ca khám ──────────────────────────────────────────────────────

	public TongQuanCaKhamReadModel? TongQuanCK { get; set; }
	public ObservableCollection<CaKhamTheoKhoangReadModel> CaKhamTheoKhoang { get; } = new();

	// ─── Tab 2: Phiên khám ───────────────────────────────────────────────────

	public TongQuanPhienKhamReadModel? TongQuanPK { get; set; }
	public ObservableCollection<PhienKhamTheoNgayReadModel> PhienKhamTheoNgay { get; } = new();
	public ObservableCollection<PhienKhamTheoPhongReadModel> PhienKhamTheoPhong { get; } = new();
	public ObservableCollection<PhienKhamTheoLoaiBenhReadModel> PhienKhamTheoLoaiBenh { get; } = new();

	// ─── Tab 3: Toa thuốc ────────────────────────────────────────────────────

	public TongQuanToaThuocReadModel? TongQuanTT { get; set; }
	public ObservableCollection<ToaThuocTheoKhoangReadModel> ToaThuocTheoKhoang { get; } = new();
	public ObservableCollection<TopThuocReadModel> TopThuoc { get; } = new();
	public ObservableCollection<TopBacSiKeDonReadModel> TopBacSiKeDon { get; } = new();

	// ─── Tab 4: Nhân viên ────────────────────────────────────────────────────

	public TongQuanNhanVienReadModel? TongQuanNV { get; set; }
	public ObservableCollection<NhanVienTheoChucVuReadModel> NhanVienTheoChucVu { get; } = new();
	public ObservableCollection<NhanVienTheoPhongReadModel> NhanVienTheoPhong { get; } = new();
	public ObservableCollection<HieuSuatBacSiReadModel> HieuSuatBacSi { get; } = new();
	public ObservableCollection<NgayNghiNhanVienReadModel> NgayNghiNhanVien { get; } = new();

	// ─── Constructor ─────────────────────────────────────────────────────────

	public ThongKeViewModel()
	{
		ApplyFilterCommand = new RelayCommand(async () => await LoadTabData());
	}

	public async Task Init()
	{
		await LoadTabData();
	}

	// ─── Load data per active tab ────────────────────────────────────────────

	private async Task LoadTabData()
	{
		IsLoading = true;
		LoadingText = "Đang tải...";

		var f = BuildFilter();

		try
		{
			switch (_activeTab)
			{
				case 0: await LoadBenhNhan(f); break;
				case 1: await LoadCaKham(f); break;
				case 2: await LoadPhienKham(f); break;
				case 3: await LoadToaThuoc(f); break;
				case 4: await LoadNhanVien(f); break;
			}
			LoadingText = $"Cập nhật lúc {DateTime.Now:HH:mm}";
		}
		catch
		{
			LoadingText = "Lỗi tải dữ liệu";
		}
		finally
		{
			IsLoading = false;
		}
	}

	private async Task LoadBenhNhan(ThongKeFilterRequest f)
	{
		var t1 = _client.GetTongQuanBenhNhan(f);
		var t2 = _client.GetBenhNhanTheoNgay(f);
		var t3 = _client.GetBenhNhanTheoGioiTinh(f);
		var t4 = _client.GetBenhNhanTheoDoTuoi(f);
		await Task.WhenAll(t1, t2, t3, t4);

		if (t1.Result.Success) { TongQuanBN = t1.Result.Data; OnPropertyChanged(nameof(TongQuanBN)); }
		Fill(BenhNhanTheoNgay, t2.Result.Data);
		Fill(BenhNhanTheoGioiTinh, t3.Result.Data);
		Fill(BenhNhanTheoDoTuoi, t4.Result.Data);
	}

	private async Task LoadCaKham(ThongKeFilterRequest f)
	{
		var t1 = _client.GetTongQuanCaKham(f);
		var t2 = _client.GetCaKhamTheoKhoang(f);
		await Task.WhenAll(t1, t2);

		if (t1.Result.Success) { TongQuanCK = t1.Result.Data; OnPropertyChanged(nameof(TongQuanCK)); }
		Fill(CaKhamTheoKhoang, t2.Result.Data);
	}

	private async Task LoadPhienKham(ThongKeFilterRequest f)
	{
		var t1 = _client.GetTongQuanPhienKham(f);
		var t2 = _client.GetPhienKhamTheoNgay(f);
		var t3 = _client.GetPhienKhamTheoPhong(f);
		var t4 = _client.GetPhienKhamTheoLoaiBenh(f);
		await Task.WhenAll(t1, t2, t3, t4);

		if (t1.Result.Success) { TongQuanPK = t1.Result.Data; OnPropertyChanged(nameof(TongQuanPK)); }
		Fill(PhienKhamTheoNgay, t2.Result.Data);
		Fill(PhienKhamTheoPhong, t3.Result.Data);
		Fill(PhienKhamTheoLoaiBenh, t4.Result.Data);
	}

	private async Task LoadToaThuoc(ThongKeFilterRequest f)
	{
		var t1 = _client.GetTongQuanToaThuoc(f);
		var t2 = _client.GetToaThuocTheoKhoang(f);
		var t3 = _client.GetTopThuoc(f);
		var t4 = _client.GetTopBacSiKeDon(f);
		await Task.WhenAll(t1, t2, t3, t4);

		if (t1.Result.Success) { TongQuanTT = t1.Result.Data; OnPropertyChanged(nameof(TongQuanTT)); }
		Fill(ToaThuocTheoKhoang, t2.Result.Data);
		Fill(TopThuoc, t3.Result.Data);
		Fill(TopBacSiKeDon, t4.Result.Data);
	}

	private async Task LoadNhanVien(ThongKeFilterRequest f)
	{
		var t1 = _client.GetTongQuanNhanVien();
		var t2 = _client.GetNhanVienTheoChucVu();
		var t3 = _client.GetNhanVienTheoPhong();
		var t4 = _client.GetHieuSuatBacSi(f);
		var t5 = _client.GetNgayNghiNhanVien(f);
		await Task.WhenAll(t1, t2, t3, t4, t5);

		if (t1.Result.Success) { TongQuanNV = t1.Result.Data; OnPropertyChanged(nameof(TongQuanNV)); }
		Fill(NhanVienTheoChucVu, t2.Result.Data);
		Fill(NhanVienTheoPhong, t3.Result.Data);
		Fill(HieuSuatBacSi, t4.Result.Data);
		Fill(NgayNghiNhanVien, t5.Result.Data);
	}

	// ─── Helper ──────────────────────────────────────────────────────────────

	private static void Fill<T>(ObservableCollection<T> col, List<T>? data)
	{
		col.Clear();
		if (data is null) return;
		foreach (var item in data) col.Add(item);
	}
}