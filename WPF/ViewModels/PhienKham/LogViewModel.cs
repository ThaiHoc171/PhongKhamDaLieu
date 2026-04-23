using System.Collections.ObjectModel;
using System.Windows;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF.Client;
using WPF.Common;
using WPF.Models;
using WPF.Pages.PhienKham;
using WPF.Windows;

namespace WPF.ViewModels.PhienKham;

public class LogViewModel : BaseViewModel
{
	private readonly int _id;

	public LogViewModel(int id)
	{
		_id = id;
	}

	#region CLIENTS
	private readonly PhienKhamClient _client = new();
	private readonly PhienKhamBenhClient _pkBenhClient = new();
	private readonly PhienKhamThietBiClient _pkThietBiClient = new();
	private readonly PhienKhamClsClient _pkClsClient = new();
	private readonly ToaThuocClient _toaThuocClient = new();
	private readonly HoSoBenhAnClient _hoSoBenhAnClient = new();
	#endregion

	#region STATE
	private bool _isLoading;
	public bool IsLoading
	{
		get => _isLoading;
		set { _isLoading = value; OnPropertyChanged(); }
	}

	private bool _isMenuOpen;
	public bool IsMenuOpen
	{
		get => _isMenuOpen;
		set { _isMenuOpen = value; OnPropertyChanged(); }
	}
	#endregion

	#region INFO
	public string BenhNhan { get; set; } = "";
	public string BacSi { get; set; } = "";
	public DateTime? NgayKham { get; set; }
	public string TrangThai { get; set; } = "";
	public string TrieuChung { get; set; } = "";
	public string ChanDoan { get; set; } = "";
	public string GhiChu { get; set; } = "";

	public BitmapImage? Image { get; set; }

	private NameHelper? _benhNhanObj;
	#endregion

	#region COLLECTIONS
	public ObservableCollection<PhienKhamBenhReadModel> BenhList { get; set; } = new();
	public ObservableCollection<PhienKhamThietBiReadModel> ThietBiList { get; set; } = new();
	public ObservableCollection<PhienKhamClsReadListModel> CLSList { get; set; } = new();
	#endregion

	#region INIT
	public async Task Init()
	{
		await LoadData();
	}
	#endregion

	#region COMMANDS

	public ICommand RefreshCommand => new RelayCommand(async () =>
	{
		await LoadData();
	});

	public ICommand ToggleMenuCommand => new RelayCommand(() =>
	{
		IsMenuOpen = !IsMenuOpen;
	});

	public ICommand MedicineCommand => new RelayCommand(async () =>
	{
		var res = await _toaThuocClient.Exists(_id);

		OpenWindow(() =>
		{
			return new WPF.Windows.ToaThuoc.XemToaThuoc(_id);
		});
	});

	public ICommand RecordCommand => new RelayCommand(async () =>
	{
		if (_benhNhanObj == null) return;

		var res = await _hoSoBenhAnClient.GetByBenhNhanId(_benhNhanObj.Id);
		if (!res.Success || res.Data == null)
		{
			SnackbarHelper.ShowError("Không có hồ sơ!");
			return;
		}

		OpenWindow(() =>
		{
			return new WPF.Windows.HSBenhAn.ViewHoSo(_benhNhanObj.Id, _benhNhanObj.Name);
		});
	});
	public ICommand ExportCommand => new RelayCommand(() =>
	{
		try
		{
			var helper = new PdfHelper();

			var dto = new PhienKhamPdfDto
			{
				BenhNhan = BenhNhan,
				BacSi = BacSi,
				NgayKham = NgayKham,
				TrangThai = TrangThai,
				TrieuChung = TrieuChung,
				ChanDoan = ChanDoan,
				GhiChu = GhiChu,
				BenhList = BenhList?.ToList(),
				CLSList = CLSList?.ToList(),
				ThietBiList = ThietBiList?.ToList()
			};
			var path = new PdfHelper().ExportPdf(dto);
			if (path != null)
				SnackbarHelper.ShowSuccess($"Đã lưu: {path}");
		}
		catch (Exception ex)
		{
			SnackbarHelper.ShowError("Xuất PDF thất bại: " + ex.Message);
		}
	});

	public ICommand BackCommand => new RelayCommand(() =>
	{
		var parent = Application.Current.MainWindow as appClinic;
		parent?.OpenPage(new PersonalPage(), "Phiên khám cá nhân");
	});

	#endregion

	private void OpenWindow(Func<Window> factory)
	{
		var win = factory();
		win.Owner = Application.Current.MainWindow;

		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		try
		{
			win.ShowDialog();
		}
		finally
		{
			OverlayHelper.Hide(overlay);
		}
	}

	#region LOAD

	private async Task LoadData()
	{
		try
		{
			IsLoading = true;

			await LoadPhienKham();
			await LoadBenh();
			await LoadThietBi();
			await LoadCLS();
		}
		finally
		{
			IsLoading = false;
		}
	}

	private async Task LoadPhienKham()
	{
		var res = await _client.Detail(_id);
		if (!res.Success || res.Data == null) return;

		var d = res.Data;

		_benhNhanObj = d.BenhNhan;

		BenhNhan = d.BenhNhan.Name;
		BacSi = d.NhanVien.Name;
		NgayKham = d.NgayKham;
		TrangThai = d.TrangThai;
		TrieuChung = d.TrieuChung ?? "";
		ChanDoan = d.ChanDoanCuoi ?? "";
		GhiChu = d.GhiChu ?? "";

		OnPropertyChanged(nameof(BenhNhan));
		OnPropertyChanged(nameof(BacSi));
		OnPropertyChanged(nameof(NgayKham));
		OnPropertyChanged(nameof(TrangThai));
		OnPropertyChanged(nameof(TrieuChung));
		OnPropertyChanged(nameof(ChanDoan));
		OnPropertyChanged(nameof(GhiChu));

		if (!string.IsNullOrEmpty(d.HinhAnh))
		{
			var url = $"https://hoanmyclinic.s3.ap-southeast-2.amazonaws.com/{d.HinhAnh}";
			Image = new BitmapImage(new Uri(url));
			OnPropertyChanged(nameof(Image));
		}
	}

	private async Task LoadBenh()
	{
		var res = await _pkBenhClient.GetByPhienKhamId(_id);
		if (!res.Success || res.Data == null) return;

		BenhList.Clear();
		foreach (var i in res.Data)
			BenhList.Add(i);
	}

	private async Task LoadThietBi()
	{
		var res = await _pkThietBiClient.GetByPhienKham(_id);
		if (!res.Success || res.Data == null) return;

		ThietBiList.Clear();
		foreach (var i in res.Data)
			ThietBiList.Add(i);
	}

	private async Task LoadCLS()
	{
		var res = await _pkClsClient.GetByPhienKham(_id);
		if (!res.Success || res.Data == null) return;

		CLSList.Clear();
		foreach (var i in res.Data)
			CLSList.Add(i);
	}

	#endregion
}