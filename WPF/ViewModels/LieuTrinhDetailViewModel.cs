using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.ViewModels.LieuTrinh;

public class LieuTrinhDetailViewModel : BaseViewModel
{
	private readonly int _id;
	private readonly LieuTrinhDieuTriClient _client = new();
	private readonly BuoiDieuTriClient _buoiDieuTri = new();

	public LieuTrinhDetailViewModel(int id)
	{
		_id = id;
	}

	#region STATE

	private bool _isLoading;
	public bool IsLoading
	{
		get => _isLoading;
		set { _isLoading = value; OnPropertyChanged(); }
	}

	#endregion

	#region MAIN INFO

	private int _lieuTrinhID;
	public int LieuTrinhID
	{
		get => _lieuTrinhID;
		set { _lieuTrinhID = value; OnPropertyChanged(); }
	}

	private string _tenLieuTrinh = "";
	public string TenLieuTrinh
	{
		get => _tenLieuTrinh;
		set { _tenLieuTrinh = value; OnPropertyChanged(); }
	}

	private string _benhNhan = "";
	public string BenhNhan
	{
		get => _benhNhan;
		set { _benhNhan = value; OnPropertyChanged(); }
	}

	private int _tongSoBuoi;
	public int TongSoBuoi
	{
		get => _tongSoBuoi;
		set { _tongSoBuoi = value; OnPropertyChanged(); }
	}

	private string? _trangThai;
	public string? TrangThai
	{
		get => _trangThai;
		set { _trangThai = value; OnPropertyChanged(); }
	}

	private DateTime? _ngayBatDau;
	public DateTime? NgayBatDau
	{
		get => _ngayBatDau;
		set { _ngayBatDau = value; OnPropertyChanged(); }
	}

	private DateTime? _ngayKetThuc;
	public DateTime? NgayKetThuc
	{
		get => _ngayKetThuc;
		set { _ngayKetThuc = value; OnPropertyChanged(); }
	}

	private string? _ghiChu;
	public string? GhiChu
	{
		get => _ghiChu;
		set { _ghiChu = value; OnPropertyChanged(); }
	}

	#endregion

	#region COLLECTION

	public ObservableCollection<BuoiDieuTriListReadModel> Items { get; set; } = new();

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

	public ICommand AddCommand => new RelayCommand(async () =>
	{
		var win = new HoanMyClinic.Windows.BuoiDieuTri.AddBuoiDieuTri(_id,BenhNhan)
		{
			Owner = App.Current.MainWindow
		};

		var result = win.ShowDialog();
		if (result == true)
		{
			await LoadData();
			SnackbarHelper.ShowSuccess("Thêm buổi điều trị thành công!");
		}
	});
	public ICommand ViewCommand =>
		new RelayCommandWithParam<BuoiDieuTriListReadModel>(item =>
		{
			if (item == null) return Task.CompletedTask;

			var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
			OverlayHelper.Show(overlay);

			try
			{
				new HoanMyClinic.Windows.BuoiDieuTri.ViewBuoiDieuTri(item.BuoiDieuTriID, BenhNhan)
				{
					Owner = Application.Current.MainWindow
				}.ShowDialog();
			}
			finally
			{
				OverlayHelper.Hide(overlay);
			}

			return Task.CompletedTask;
		});

	#endregion

	#region LOAD DATA

	private async Task LoadData()
	{
		try
		{
			IsLoading = true;

			await LoadDetail();
			await LoadBuoiDieuTri();
		}
		finally
		{
			IsLoading = false;
		}
	}

	private async Task LoadDetail()
	{
		var res = await _client.Detail(_id);
		if (!res.Success || res.Data == null)
		{
			SnackbarHelper.ShowError(res.Message);
			return;
		}

		var data = res.Data;

		LieuTrinhID = data.LieuTrinhID;
		TenLieuTrinh = data.TenLieuTrinh;
		BenhNhan = data.BenhNhan?.Name ?? "";
		TongSoBuoi = data.TongSoBuoi;
		TrangThai = data.TrangThai;
		NgayBatDau = data.NgayBatDau;
		NgayKetThuc = data.NgayKetThuc;
		GhiChu = data.GhiChu;
	}

	private async Task LoadBuoiDieuTri()
	{
		var res = await _buoiDieuTri.GetByLieuTrinh(_id);
		if (!res.Success || res.Data == null) return;

		Items.Clear();
		foreach (var item in res.Data)
			Items.Add(item);
	}

	#endregion
}