using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF.Client;
using WPF.Common;
using WPF.Models;
using WPF.Windows.TaiKham;

namespace WPF.ViewModels.PhienKham;

public class ConsultationViewModel : BaseViewModel
{
	private readonly int _id;
	private int _benhNhanId;

	public ConsultationViewModel(int id)
	{
		_id = id;
	}

	#region CLIENTS
	private readonly PhienKhamClient _client = new();
	private readonly UploadClient _upload = new();
	private readonly PhongChucNangClient _pcn = new();
	private readonly PhienKhamBenhClient _pkBenhClient = new();
	private readonly PhienKhamThietBiClient _pkThietBiClient = new();
	private readonly PhienKhamClsClient _pkClsClient = new();
	private readonly CaKhamClient _caKhamClient = new();
	private readonly ToaThuocClient _toaThuocClient = new();
	private readonly TaiKhamClient _taiKham = new();
	private readonly HoSoBenhAnClient _hoso = new();
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

	private bool _isSaved = true;
	private bool _imgChanged;
	private string? _imgPath;

	private int _caKhamId;

	#endregion

	#region MAIN INFO

	private string _benhNhan = "";
	public string BenhNhan
	{
		get => _benhNhan;
		set { _benhNhan = value; OnPropertyChanged(); }
	}

	private string _bacSi = "";
	public string BacSi
	{
		get => _bacSi;
		set { _bacSi = value; OnPropertyChanged(); }
	}

	private DateTime? _ngayKham;
	public DateTime? NgayKham
	{
		get => _ngayKham;
		set { _ngayKham = value; OnPropertyChanged(); }
	}

	private string _trangThai = "";
	public string TrangThai
	{
		get => _trangThai;
		set { _trangThai = value; OnPropertyChanged(); }
	}

	private string _trieuChung = "";
	public string TrieuChung
	{
		get => _trieuChung;
		set
		{
			_trieuChung = value;
			_isSaved = false;
			OnPropertyChanged();
		}
	}

	private string _chanDoan = "";
	public string ChanDoan
	{
		get => _chanDoan;
		set
		{
			_chanDoan = value;
			_isSaved = false;
			OnPropertyChanged();
		}
	}

	private string _ghiChu = "";
	public string GhiChu
	{
		get => _ghiChu;
		set
		{
			_ghiChu = value;
			_isSaved = false;
			OnPropertyChanged();
		}
	}

	private BitmapImage? _image;
	public BitmapImage? Image
	{
		get => _image;
		set { _image = value; OnPropertyChanged(); }
	}

	#endregion

	#region COLLECTIONS

	public ObservableCollection<PhienKhamBenhReadModel> BenhList { get; set; } = new();
	public ObservableCollection<PhienKhamThietBiReadModel> ThietBiList { get; set; } = new();
	public ObservableCollection<PhienKhamClsReadListModel> CLSList { get; set; } = new();
	public ObservableCollection<NameHelper> Phongs { get; set; } = new();

	private NameHelper? _selectedPhong;
	public NameHelper? SelectedPhong
	{
		get => _selectedPhong;
		set { _selectedPhong = value; OnPropertyChanged(); }
	}

	#endregion

	#region INIT

	public async Task Init()
	{
		await LoadCombobox();
		await LoadData();
	}

	#endregion
	private async Task SaveAsync()
	{
		if (string.IsNullOrWhiteSpace(TrieuChung))
		{
			SnackbarHelper.ShowError("Vui lòng nhập triệu chứng!");
			return;
		}

		IsLoading = true;

		try
		{
			string? path = _imgPath;

			if (!string.IsNullOrEmpty(_imgPath) && _imgChanged)
			{
				var upload = await _upload.UploadImage(_imgPath, "KetQuaKham");

				if (!upload.Success)
				{
					SnackbarHelper.ShowError(upload.Message);
					return;
				}

				path = new Uri(upload.Data!).AbsolutePath.TrimStart('/');
			}

			var req = new PhienKhamUpdateDTO
			{
				TrieuChung = TrieuChung,
				GhiChu = GhiChu,
				HinhAnh = path
			};

			var res = await _client.Update(_id, req);

			if (!res.Success)
			{
				SnackbarHelper.ShowError(res.Message);
				return;
			}

			_isSaved = true;
			SnackbarHelper.ShowSuccess("Lưu thành công!");
		}
		finally
		{
			IsLoading = false;
		}
	}
	#region COMMANDS

	public ICommand RefreshCommand => new RelayCommand(async () =>
	{
		await LoadData();
	});

	public ICommand ToggleMenuCommand => new RelayCommand(() =>
	{
		IsMenuOpen = !IsMenuOpen;
	});

	public ICommand AddPictureCommand => new RelayCommand(() =>
	{
		var dlg = new OpenFileDialog
		{
			Filter = "Image Files|*.jpg;*.jpeg;*.png"
		};

		if (dlg.ShowDialog() == true)
		{
			_imgPath = dlg.FileName;
			_imgChanged = true;
			_isSaved = false;

			Image = new BitmapImage(new Uri(_imgPath));
		}
	});

	public ICommand SaveCommand => new RelayCommand(async () =>
	{
		await SaveAsync();
	});

	public ICommand CompleteCommand => new RelayCommand(async () =>
	{
		if (string.IsNullOrWhiteSpace(ChanDoan))
		{
			SnackbarHelper.ShowError("Nhập chẩn đoán!");
			return;
		}

		if (!_isSaved)
		{
			var confirmSave = await MessageHelper.Confirm(
				"Dữ liệu chưa lưu. Bạn có muốn lưu trước không?");

			if (confirmSave)
				await SaveAsync();
			else
				return;
		}

		var confirm = await MessageHelper.Confirm("Hoàn thành phiên khám?");
		if (!confirm) return;

		var res = await _client.Complete(_id, ChanDoan);

		if (!res.Success)
		{
			SnackbarHelper.ShowError(res.Message);
			return;
		}

		await _caKhamClient.UpdateTrangThai(
			_caKhamId,
			new CaKhamTrangThaiDTO { TrangThai = "Hoàn thành" });
		var taikham = await _taiKham.GetId(_caKhamId);
		if (taikham.Success && taikham.Data != 0)
			await _taiKham.Complete(taikham.Data);

		SnackbarHelper.ShowSuccess("Đã hoàn thành!");

		var export = await MessageHelper.Confirm("Bạn có muốn xuất PDF không?");
		if (!export) return;
		var dto = new PhienKhamPdfDto
		{
			BenhNhan = BenhNhan,
			BacSi = BacSi,
			NgayKham = NgayKham,
			TrangThai = TrangThai,
			TrieuChung = TrieuChung,
			ChanDoan = ChanDoan,
			GhiChu = GhiChu,
			BenhList = BenhList.ToList(),
			CLSList = CLSList.ToList(),
			ThietBiList = ThietBiList.ToList()
		};

		try
		{
			var helper = new PdfHelper();

			var path = new PdfHelper().ExportPdf(dto);
			if (path != null)
				SnackbarHelper.ShowSuccess($"Đã lưu: {path}");
		}
		catch (Exception ex)
		{
			SnackbarHelper.ShowError("Xuất PDF thất bại: " + ex.Message);
		}
	});
	public ICommand CancelCommand => new RelayCommand(async () =>
	{
		var confirm = await MessageHelper.Confirm("Hủy phiên khám?");
		if (!confirm) return;

		var res = await _client.Cancel(_id);

		if (!res.Success)
		{
			SnackbarHelper.ShowError(res.Message);
			return;
		}

		SnackbarHelper.ShowSuccess("Đã hủy!");
	});

	#endregion
	#region DataGrid Commands
	public ICommand EditBenhCommand => new RelayCommandWithParam<PhienKhamBenhReadModel>(async item =>
	{
		if (item == null) return;

		var win = new WPF.Windows.KhamBenh.UpdateChanDoan(item.Id)
		{
			Owner = Application.Current.MainWindow
		};

		var result = win.ShowDialog();
		if (result == true)
		{
			await LoadData();
			SnackbarHelper.ShowSuccess("Cập nhật chẩn đoán thành công!");
		}
	});
	public ICommand DeleteBenhCommand => new RelayCommandWithParam<PhienKhamBenhReadModel>(async item =>
	{
		if (item == null) return;

		var confirm = await MessageHelper.Confirm($"Xóa chẩn đoán: {item.LoaiBenh?.Name} ?");
		if (!confirm) return;

		var res = await _pkBenhClient.Delete(item.Id);

		if (!res.Success)
		{
			SnackbarHelper.ShowError(res.Message);
			return;
		}

		await LoadData();
		SnackbarHelper.ShowSuccess("Đã xóa chẩn đoán!");
	});
	public ICommand EditThietBiCommand => new RelayCommandWithParam<PhienKhamThietBiReadModel>(async item =>
	{
		if (item == null) return;

		var win = new WPF.Windows.KhamBenh.UpdateUsedThietBi(item.PhienKhamThietBiID, item.TenThietBi)
		{
			Owner = Application.Current.MainWindow
		};

		var result = win.ShowDialog();
		if (result == true)
		{
			await LoadData();
			SnackbarHelper.ShowSuccess("Cập nhật thiết bị thành công!");
		}
	});
	public ICommand DeleteThietBiCommand => new RelayCommandWithParam<PhienKhamThietBiReadModel>(async item =>
	{
		if (item == null) return;

		var confirm = await MessageHelper.Confirm($"Xóa thiết bị: {item.TenThietBi} ?");
		if (!confirm) return;

		var res = await _pkThietBiClient.Delete(item.PhienKhamThietBiID);

		if (!res.Success)
		{
			SnackbarHelper.ShowError(res.Message);
			return;
		}

		await LoadData();
		SnackbarHelper.ShowSuccess("Đã xóa thiết bị!");
	});
	public ICommand ViewClsCommand => new RelayCommandWithParam<PhienKhamClsReadListModel>(async item =>
	{
		if (item == null) return;

		var win = new WPF.Windows.KhamBenh.ViewCls(item.PhienKhamCLSID)
		{
			Owner = Application.Current.MainWindow
		};

		var result = win.ShowDialog();
		if (result == true)
		{
			await LoadData();
		}
	});
	public ICommand CancelClsCommand => new RelayCommandWithParam<PhienKhamClsReadListModel>(async item =>
	{
		if (item == null) return;

		var confirm = await MessageHelper.Confirm($"Hủy CLS: {item.TenCLS} ?");
		if (!confirm) return;

		var res = await _pkClsClient.Cancel(item.PhienKhamCLSID);

		if (!res.Success)
		{
			SnackbarHelper.ShowError(res.Message);
			return;
		}

		await LoadData();
		SnackbarHelper.ShowSuccess("Đã hủy CLS!");
	});
	#endregion
	#region POPUP ACTION COMMANDS

	public ICommand AddDiagnosisCommand => new RelayCommand(() =>
	{
		OpenWindow(() =>
		{
			var win = new WPF.Windows.KhamBenh.AddChanDoan(_id);
			return win;
		});
	});

	public ICommand AddEquipmentCommand => new RelayCommand(() =>
	{
		OpenWindow(() =>
		{
			var win = new WPF.Windows.KhamBenh.AddUesdThietBi(_id);
			return win;
		});
	});

	public ICommand OrderLabCommand => new RelayCommand(() =>
	{
		OpenWindow(() =>
		{
			var win = new WPF.Windows.KhamBenh.ChiDinhCLS(_id);
			return win;
		});
	});

	public ICommand MedicineCommand => new RelayCommand(async () =>
	{
		var res = await _toaThuocClient.Exists(_id);

		OpenWindow(() =>
		{
			if (!res.Data)
				return new WPF.Windows.ToaThuoc.AddToaThuoc(_id);

			return new WPF.Windows.ToaThuoc.UpdateToaThuoc(_id);
		});
	});

	public ICommand RecordCommand => new RelayCommand(async() =>
	{
		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		var res = await _hoso.GetByBenhNhanId(_benhNhanId);

		Window win = res.Success && res.Data != null
			? new WPF.Windows.HSBenhAn.UpdateHoSo(_benhNhanId, BenhNhan)
			: new WPF.Windows.HSBenhAn.AddHoSo(_benhNhanId, BenhNhan);

		win.Owner = Application.Current.MainWindow;
		win.ShowDialog();

		OverlayHelper.Hide(overlay);
	});
	public ICommand RecheckCommand => new RelayCommand(() =>
	{
		OpenWindow(() =>
		{
			return new AddTaiKham(_id, _benhNhanId);
		});
	});

	#endregion
	private void OpenWindow(Func<Window> factory)
	{
		var win = factory();
		win.Owner = Application.Current.MainWindow;

		var overlay = Application.Current.MainWindow.FindName("Overlay") as System.Windows.Controls.Border;
		if (overlay != null) overlay.Visibility = Visibility.Visible;

		try
		{
			var result = win.ShowDialog();
			if (result == true)
			{
				_ = LoadData();
			}
		}
		finally
		{
			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
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

	private async Task LoadCombobox()
	{
		var res = await _pcn.GetCombobox();
		if (!res.Success || res.Data == null) return;

		Phongs.Clear();
		foreach (var item in res.Data)
			Phongs.Add(item);
	}

	private async Task LoadPhienKham()
	{
		var res = await _client.Detail(_id);
		if (!res.Success || res.Data == null) return;

		var data = res.Data;

		_caKhamId = data.CaKhamID;
		_benhNhanId = data.BenhNhan.Id;
		BenhNhan = data.BenhNhan.Name;
		BacSi = data.NhanVien.Name;
		NgayKham = data.NgayKham;
		TrangThai = data.TrangThai;
		TrieuChung = data.TrieuChung ?? "";
		ChanDoan = data.ChanDoanCuoi ?? "";
		GhiChu = data.GhiChu ?? "";

		SelectedPhong = Phongs.FirstOrDefault(x => x.Id == data.PhongChucNangID);

		if (!string.IsNullOrEmpty(data.HinhAnh))
		{
			var url = $"https://hoanmyclinic.s3.ap-southeast-2.amazonaws.com/{data.HinhAnh}";
			Image = new BitmapImage(new Uri(url));
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