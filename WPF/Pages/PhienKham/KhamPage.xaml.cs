using Microsoft.Win32;
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
using WPF.Windows.KhamBenh;
using WPF.Windows.ToaThuoc;


namespace WPF.Pages.PhienKham;

public partial class KhamPage : Page, INotifyPropertyChanged
{
	public KhamPage(int id)
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
			await LoadCombobox();
			await LoadData();
		};
	}
	private readonly int _id;
	private string? _imgPath;
	private bool _imgchanged = false;
	private NameHelper _benhNhan = new();
	private bool _isSaved = true;
	private int _cakhamId;
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
	private readonly CaKhamClient _caKhamClient = new();
	#endregion
	private async Task LoadCombobox()
	{
		var list = await _pcn.GetCombobox();
		if(list.Success)
		{
			cboPhong.ItemsSource = list.Data;
			cboPhong.DisplayMemberPath = "Name";
			cboPhong.SelectedValuePath = "Id";
			cboPhong.SelectedIndex = 0;
		}
	}
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
		_cakhamId = data.CaKhamID;
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
			_imgPath = data.HinhAnh;
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
		gridBenh.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Pencil", Edit_Benh_Click, "Sửa"));
		gridBenh.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Cancel", Delete_Benh_Click, "Xóa"));
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
		gridThietBi.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Pencil", Edit_TheitBi_Click, "Sửa"));
		gridThietBi.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Cancel", Delete_TheitBi_Click, "Xóa"));

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
		gridCLS.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Eye", View_CLS_Click, "Xem"));
		gridCLS.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Cancel", Cancel_CLS_Click, "Hủy yêu cầu"));
	}
	#region Benh
	private async void Edit_Benh_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PhienKhamBenhReadModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var win = new CapNhatChanDoan(item.Id)
			{
				Owner = parentWindow
			};
			var result = win.ShowDialog();
			if (result == true)
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Cập nhật chẩn đoán thành công!");
			}

			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}
	private async void Delete_Benh_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PhienKhamBenhReadModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var confirm = await MessageHelper.Confirm($"Bạn có chắc muốn xóa chẩn đoán bệnh: {item.LoaiBenh?.Name} không?");
			if (!confirm)
			{
				if (overlay != null)
					overlay.Visibility = Visibility.Collapsed;
				return;
			}
			var res = await _pkBenhClient.Delete(item.Id);
			if (!res.Success)
			{
				SnackbarHelper.ShowError(res.Message);
				if (overlay != null)
					overlay.Visibility = Visibility.Collapsed;
				return;
			}
			await LoadData();
			SnackbarHelper.ShowSuccess("Đã xóa chẩn đoán!");

			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}
	#endregion

	#region ThietBi
	private async void Edit_TheitBi_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PhienKhamThietBiReadModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var win = new CapNhatThietBiDung(item.PhienKhamThietBiID, item.TenThietBi)
			{
				Owner = parentWindow
			};
			var result = win.ShowDialog();
			if (result == true)
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Thêm ghi chú thành công!");
			}

			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}
	private async void Delete_TheitBi_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PhienKhamThietBiReadModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var confirm = await MessageHelper.Confirm($"Bạn có chắc muốn xóa thiết bị: {item.TenThietBi} không?");
			if (!confirm)
			{
				if (overlay != null)
					overlay.Visibility = Visibility.Collapsed;
				return;
			}
			var res = await _pkThietBiClient.Delete(item.PhienKhamThietBiID);
			if (!res.Success)
			{
				SnackbarHelper.ShowError(res.Message);
				if (overlay != null)
					overlay.Visibility = Visibility.Collapsed;
				return;
			}
			await LoadData();
			SnackbarHelper.ShowSuccess("Đã xóa chẩn đoán!");

			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}
	#endregion
	#region CLS
	private async void View_CLS_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PhienKhamClsReadListModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var win = new XemPhienKhamCLS(item.PhienKhamCLSID)
			{
				Owner = parentWindow
			};
			var result = win.ShowDialog();
			if (result == true)
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Thêm ghi chú thành công!");
			}

			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}
	private async void Cancel_CLS_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PhienKhamClsReadListModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var confirm = await MessageHelper.Confirm($"Bạn có chắc muốn hủy yêu cầu CLS: {item.TenCLS} không?");
			if (!confirm)
			{
				if (overlay != null)
					overlay.Visibility = Visibility.Collapsed;
				return;
			}
			var res = await _pkClsClient.Cancel(item.PhienKhamCLSID);
			if (!res.Success)
			{
				SnackbarHelper.ShowError(res.Message);
				if (overlay != null)
					overlay.Visibility = Visibility.Collapsed;
				return;
			}
			await LoadData();
			SnackbarHelper.ShowSuccess("Đã hủy yêu cầu CLS!");

			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}
	#endregion
	private void btnAddPic_Click(object sender, RoutedEventArgs e)
	{
		var dlg = new OpenFileDialog
		{
			Filter = "Image Files|*.jpg;*.jpeg;*.png"
		};

		if (dlg.ShowDialog() == true)
		{
			_imgchanged = true;
			_imgPath = dlg.FileName;
			pic.Source = new BitmapImage(new Uri(_imgPath));
		}
	}
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{

		string? _path = _imgPath;
		if (string.IsNullOrWhiteSpace(txtTrieuChung.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập triệu chứng!");
			txtTrieuChung.Focus();
			return;
		}
		try
		{
			btnAction.IsEnabled = false;
			btnSave.IsEnabled = false;

			if (!string.IsNullOrEmpty(_imgPath) && _imgchanged)
			{
				var uploadResult = await _upload.UploadImage(_imgPath, "KetQuaKham");

				if (!uploadResult.Success)
				{
					SnackbarHelper.ShowError(uploadResult.Message);
					return;
				}

				if (!string.IsNullOrEmpty(uploadResult.Data))
				{
					var uri = new Uri(uploadResult.Data);
					_path = uri.AbsolutePath.TrimStart('/');
				}
			}

			var req = new PhienKhamUpdateDTO
			{
				TrieuChung = txtTrieuChung.Text,
				GhiChu = txtGhiChu.Text,
				HinhAnh = _path
			};

			var result = await _client.Update(_id, req);

			if (result.Success)
			{
				_isSaved = true;
				SnackbarHelper.ShowSuccess("Lưu phiên khám thành công!");
			}
			else
			{
				SnackbarHelper.ShowError(result.Message);
			}
		}
		catch (Exception)
		{
			SnackbarHelper.ShowError("Có lỗi xảy ra, vui lòng thử lại!");
		}
		finally
		{
			btnSave.IsEnabled = true;
			btnAction.IsEnabled = true;
		}
	}
	private async void btnComplete_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtChanDoanCuoi.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập chẩn đoán cuối trước khi hoàn thành phiên khám!");
			txtChanDoanCuoi.Focus();
			return;
		}
		try
		{
			btnAction.IsEnabled = false;
			btnSave.IsEnabled = false;
			if (!_isSaved)
			{
				var confirmSave = await MessageHelper.Confirm(
					"Dữ liệu chưa được lưu. Bạn có muốn lưu trước khi hoàn thành phiên khám không?");

				if (confirmSave != true)
				{
					btnAction.IsEnabled = true;
					btnSave.IsEnabled = true;
					return;
				}

				btnSave_Click(sender, e);
			}
			var confirm = await MessageHelper.Confirm("Bạn có chắc muốn hoàn thành phiên khám này không? \n " +
				"Sau khi hoàn thành sẽ không thể chỉnh sửa thông tin phiên khám.");
			if (!confirm)
			{
				btnAction.IsEnabled = true;
				btnSave.IsEnabled = true;
				return;
			}
			var req = txtChanDoanCuoi.Text;

			var result = await _client.Complete(_id, req);

			if (result.Success)
			{
				var reqTrangThai = new CaKhamTrangThaiDTO
				{
					TrangThai = "Hoàn thành",
					GhiChu = null
				};
				var res = await _caKhamClient.UpdateTrangThai(_cakhamId, reqTrangThai);
				if (!res.Success)
				{
					SnackbarHelper.ShowError("Phiên khám đã được hoàn thành, nhưng cập nhật trạng thái ca khám thất bại: " + res.Message);
					return;
				}
				SnackbarHelper.ShowSuccess("Phiên khám đã được hoàn thành!");
			}
			else
			{
				SnackbarHelper.ShowError(result.Message);
			}
		}
		catch (Exception)
		{
			SnackbarHelper.ShowError("Có lỗi xảy ra, vui lòng thử lại!");
		}
		finally
		{
			btnSave.IsEnabled = true;
			btnAction.IsEnabled = true;
		}
	}
	private async void btnCancel_Click(object sender, RoutedEventArgs e)
	{

		try
		{
			btnAction.IsEnabled = false;
			btnSave.IsEnabled = false;
			var confirm = await MessageHelper.Confirm("Bạn có chắc muốn hủy phiên khám này không?");
			if (!confirm)
			{
				btnAction.IsEnabled = true;
				btnSave.IsEnabled = true;
				return;
			}
			var result = await _client.Cancel(_id);

			if (result.Success)
			{
				SnackbarHelper.ShowSuccess("Phiên khám đã được hủy!");
			}
			else
			{
				SnackbarHelper.ShowError(result.Message);
			}
		}
		catch (Exception)
		{
			SnackbarHelper.ShowError("Có lỗi xảy ra, vui lòng thử lại!");
		}
		finally
		{
			btnSave.IsEnabled = true;
			btnAction.IsEnabled = true;
		}
	}
	
	private async void Refresh_Click(object sender, RoutedEventArgs e)
	{
		await LoadData();
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

	private async void btnAddDiagnosis_Click(object sender, RoutedEventArgs e)
	{
		var parentWindow = Window.GetWindow(this);
		var overlay = parentWindow.FindName("Overlay") as Border;

		if (overlay != null)
			overlay.Visibility = Visibility.Visible;

		var win = new ThemChanDoan(_id)
		{
			Owner = parentWindow
		};
		var result = win.ShowDialog();
		if (result == true)
		{
			await LoadData();
			SnackbarHelper.ShowSuccess("Thêm chẩn đoán thành công!");
		}

		if (overlay != null)
			overlay.Visibility = Visibility.Collapsed;
	}
	private async void btnAddEquipment_Click(object sender, RoutedEventArgs e)
	{
		var parentWindow = Window.GetWindow(this);
		var overlay = parentWindow.FindName("Overlay") as Border;

		if (overlay != null)
			overlay.Visibility = Visibility.Visible;

		var win = new ThemThietBiDung(_id)
		{
			Owner = parentWindow
		};
		var result = win.ShowDialog();
		if (result == true)
		{
			await LoadData();
			SnackbarHelper.ShowSuccess("Thêm thiết bị đã dùng thành công!");
		}

		if (overlay != null)
			overlay.Visibility = Visibility.Collapsed;
	}
	private async void btnOrderLabTests_Click(object sender, RoutedEventArgs e)
	{
		var parentWindow = Window.GetWindow(this);
		var overlay = parentWindow.FindName("Overlay") as Border;

		if (overlay != null)
			overlay.Visibility = Visibility.Visible;

		var win = new ChiDinhCLS(_id)
		{
			Owner = parentWindow
		};
		var result = win.ShowDialog();
		if (result == true)
		{
			await LoadData();
			SnackbarHelper.ShowSuccess("Chỉ định cận lâm sàng thành công!");
		}

		if (overlay != null)
			overlay.Visibility = Visibility.Collapsed;
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
				var confirm = await MessageHelper.Confirm("Phiên khám này chưa có toa thuốc, tạo toa thuốc mới?");
				if (confirm != true)
					return;

				var createWin = new ThemToaThuoc(_id)
				{
					Owner = parentWindow
				};

				var createResult = createWin.ShowDialog();

				if (createResult != true)
					return;

				await LoadData();
				SnackbarHelper.ShowSuccess("Tạo toa thuốc thành công!");
				return;
			}

			var win = new CapNhatToaThuoc(_id)
			{
				Owner = parentWindow
			};

			var result = win.ShowDialog();

			if (result == true)
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Cập nhật toa thuốc thành công!");
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
				var confirm = await MessageHelper.Confirm("Bệnh nhân chưa có hồ sơ bệnh án, tạo hồ sơ mới?");
				if (confirm == true)
				{
					var createWin = new ThemHoSo(_benhNhan.Id, _benhNhan.Name)
					{
						Owner = parentWindow
					};
					createWin.ShowDialog();
				}
				return;
			}

			var win = new CapNhatHoSo(_benhNhan.Id, _benhNhan.Name)
			{
				Owner = parentWindow
			};

			var result = win.ShowDialog();

			if (result == true)
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Cập nhật bệnh án thành công!");
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

	private void txtTrieuChung_TextChanged(object sender, TextChangedEventArgs e)
	{
		_isSaved = false;
	}

	private void txtGhiChu_TextChanged(object sender, TextChangedEventArgs e)
	{
		_isSaved = false;
	}
}
