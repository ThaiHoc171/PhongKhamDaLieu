using Microsoft.Win32;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WPF.Client;
using WPF.Common;
using WPF.Models;
using WPF.Windows.BenhNhan;

namespace WPF.Windows.NhanVien;

public partial class CapNhatNhanVien : Window
{
    public CapNhatNhanVien(int id)
    {
        InitializeComponent();
		_id = id;
		
	}

	private readonly int _id;
	private int _thongtinid;
	private readonly NhanVienClient _client = new();
	private readonly ThongTinCaNhanClient _thongTinClient = new();
	private readonly UploadClient _upload = new();
	private readonly ChucVuClient _cv = new();
	private readonly PhongChucNangClient _pcn = new();
	private NhanVienRequestUpdateDTO _current = new();
	private ThongTinUpdateRequestDTO _thongTin = new();
	private string? _avatarPath;
	private async Task LoadComboBox()
	{
		cboGioiTinh.ItemsSource = new List<string> { "Nam", "Nữ", "Khác" };
		cboGioiTinh.SelectedIndex = 0;

		var listCV = await _cv.GetCombobox();
		var listPCN = await _pcn.GetCombobox();
		cboChucVu.ItemsSource = listCV.Data;
		cboChucVu.DisplayMemberPath = "Name";
		cboChucVu.SelectedValuePath = "Id";
		cboPhongLamViec.ItemsSource = listPCN.Data;
		cboPhongLamViec.DisplayMemberPath = "Name";
		cboPhongLamViec.SelectedValuePath = "Id";
	}
	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
	private bool IsThongTinChanged(ThongTinUpdateRequestDTO newData)
	{
		return newData.HoTen != _thongTin.HoTen
			|| newData.GioiTinh != _thongTin.GioiTinh
			|| newData.NgaySinh != _thongTin.NgaySinh
			|| newData.SDT != _thongTin.SDT
			|| newData.EmailLienHe != _thongTin.EmailLienHe
			|| newData.DiaChi != _thongTin.DiaChi
			|| newData.Avatar != _thongTin.Avatar;
	}
	private bool IsNhanVienChanged(NhanVienRequestUpdateDTO newData)
	{
		return newData.ChucVuID != _current.ChucVuID
			|| newData.PhongChucNangID != _current.PhongChucNangID
			|| newData.NgayVaoLam != _current.NgayVaoLam
			|| newData.BangCap != _current.BangCap
			|| newData.KinhNghiem != _current.KinhNghiem;
	}
	private async void CapNhatNhanVien_Loaded(object sender, RoutedEventArgs e)
	{
	 	await LoadComboBox();
		try
		{
			var result = await _client.Detail(_id);

			if (result == null || !result.Success || result.Data == null)
			{
				SnackbarHelper.ShowError("Không tìm thấy nhân viên.");
				Close();
				return;
			}
			var data = result.Data;
			// ===== Thông tin cá nhân =====
			_thongtinid = data.ThongTinID;
			txtHoTen.Text = _thongTin.HoTen = data.HoTen;
			txtSDT.Text = _thongTin.SDT = data.SDT;
			txtEmail.Text = _thongTin.EmailLienHe = data.EmailLienHe;
			txtDiaChi.Text = _thongTin.DiaChi = data.DiaChi;
			dtpNgaySinh.SelectedDate = _thongTin.NgaySinh = data.NgaySinh;
			cboGioiTinh.SelectedItem = _thongTin.GioiTinh = data.GioiTinh;
			_thongTin.Avatar = data.Avatar;

			// ===== Thông tin nhân viên =====
			cboChucVu.SelectedValue = _current.ChucVuID = data.ChucVu?.Id ?? 0;
			cboPhongLamViec.SelectedValue = _current.PhongChucNangID = data.PhongChucNang?.Id ??0;
			dtpNgayVaoLam.SelectedDate = _current.NgayVaoLam = data.NgayVaoLam;
			txtBangCap.Text = _current.BangCap = data.BangCap ?? "";
			txtKinhNghiem.Text = _current.KinhNghiem = data.KinhNghiem ?? "";

			// ===== Thông tin hệ thống =====
			dtpNgayTao.SelectedDate = data.NgayTao;
			dtpNgayCapNhat.SelectedDate = data.NgayCapNhat;
			txtTrangThai.Text = data.TrangThai;

			// ===== Load avatar =====
			if (!string.IsNullOrWhiteSpace(data.Avatar))
			{
				var url = $"https://hoanmyclinic.s3.ap-southeast-2.amazonaws.com/{data.Avatar}";
				imgAvatar.Source = new BitmapImage(new Uri(url));
			}
		}
		catch (Exception)
		{
			SnackbarHelper.ShowError("Không thể tải dữ liệu nhân viên.");
			Close();
		}
	}

	private void btnChonAvt_Click(object sender, RoutedEventArgs e)
	{
		var dlg = new OpenFileDialog
		{
			Filter = "Image Files|*.jpg;*.jpeg;*.png"
		};

		if (dlg.ShowDialog() == true)
		{
			_avatarPath = dlg.FileName;
			imgAvatar.Source = new BitmapImage(new Uri(_avatarPath));
		}
	}

	private async void btnLuu_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtHoTen.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập họ tên!");
			return;
		}

		string? avatarUrl = _thongTin.Avatar;

		try
		{
			btnLuu.IsEnabled = false;
			btnHuy.IsEnabled = false;

			// upload avatar nếu chọn mới
			if (!string.IsNullOrEmpty(_avatarPath))
			{
				var uploadResult = await _upload.UploadImage(_avatarPath, "nhanvien");

				if (!uploadResult.Success)
				{
					SnackbarHelper.ShowError(uploadResult.Message);
					return;
				}

				if (!string.IsNullOrEmpty(uploadResult.Data))
				{
					var uri = new Uri(uploadResult.Data);
					avatarUrl = uri.AbsolutePath.TrimStart('/');
				}
			}

			var thongtin = new ThongTinUpdateRequestDTO
			{
				HoTen = txtHoTen.Text.Trim(),
				GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Khác",
				NgaySinh = dtpNgaySinh.SelectedDate ?? DateTime.Today,
				SDT = txtSDT.Text.Trim(),
				EmailLienHe = txtEmail.Text.Trim(),
				DiaChi = txtDiaChi.Text.Trim(),
				Avatar = avatarUrl,
				Loai = "Nhân viên"
			};
			var nhanvien = new NhanVienRequestUpdateDTO
			{
				ChucVuID = (int)(cboChucVu.SelectedValue ?? 0),
				PhongChucNangID = (int)(cboPhongLamViec.SelectedValue ?? 0),
				NgayVaoLam = dtpNgayVaoLam.SelectedDate ?? DateTime.Today,
				BangCap = txtBangCap.Text.Trim(),
				KinhNghiem = txtKinhNghiem.Text.Trim(),
			};
			if (!IsThongTinChanged(thongtin) && !IsNhanVienChanged(nhanvien))
			{
				SnackbarHelper.ShowWarning("Không có thay đổi nào để lưu!");
				return;
			}
			ApiResult<bool>? result = null;
			ApiResult<bool>? thongTinResult = null;

			if (IsNhanVienChanged(nhanvien))
				result = await _client.Update(_id, nhanvien);

			if (IsThongTinChanged(thongtin))
				thongTinResult = await _thongTinClient.Update(_thongtinid, thongtin);

			bool nvSuccess = result == null || result.Success;
			bool ttSuccess = thongTinResult == null || thongTinResult.Success;

			if (nvSuccess && ttSuccess)
			{
				DialogResult = true;
				Close();
			}
			else
			{
				string message =
					result?.Message ??
					thongTinResult?.Message ??
					"Cập nhật thất bại!";

				SnackbarHelper.ShowError(message);
			}
		}
		catch (Exception)
		{
			SnackbarHelper.ShowError("Có lỗi xảy ra, vui lòng thử lại!");
		}
		finally
		{
			btnLuu.IsEnabled = true;
			btnHuy.IsEnabled = true;
		}
	}

	private void btnHuy_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}
