using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.CaNhan;

public partial class HoSoCaNhan : Window
{
	public HoSoCaNhan()
	{
		InitializeComponent();
		if (Session.VaiTro == "Admin")
		{
			SnackbarHelper.ShowWarning("Bạn đang sử dụng tài khoản admin");
			return;
		}
		_id = Session.HoTen.Id;
	}

	private readonly int _id;
	private readonly ThongTinCaNhanClient _client = new();
	private readonly UploadClient _upload = new();
	private ThongTinUpdateRequestDTO _thongTin = new();
	private string? _avatarPath;
	private void LoadComboBox()
	{
		cboGioiTinh.ItemsSource = new List<string> { "Nam", "Nữ", "Khác" };
		cboGioiTinh.SelectedIndex = 0;
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
	private async void UpdateNhanVien_Loaded(object sender, RoutedEventArgs e)
	{
		LoadComboBox();
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
			txtHoTen.Text = _thongTin.HoTen = data.HoTen;
			txtSDT.Text = _thongTin.SDT = data.SDT;
			txtEmail.Text = _thongTin.EmailLienHe = data.EmailLienHe;
			txtDiaChi.Text = _thongTin.DiaChi = data.DiaChi;
			dtpNgaySinh.SelectedDate = _thongTin.NgaySinh = data.NgaySinh;
			cboGioiTinh.SelectedItem = _thongTin.GioiTinh = data.GioiTinh;
			_thongTin.Avatar = data.Avatar;

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

	private void btnChooseAvt_Click(object sender, RoutedEventArgs e)
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
	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
		btnChooseAvt.IsEnabled = isEnabled;
	}
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtHoTen.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập họ tên!");
			return;
		}

		string? avatarUrl = _thongTin.Avatar;

		try
		{
			ToggleUI(false);

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
			ApiResult<bool>? result = null;

			if (IsThongTinChanged(thongtin))
			{
				result = await _client.Update(_id, thongtin);

				if (result != null && result.Success)
				{
					DialogResult = true;
					Close();
				}
				else
				{
					SnackbarHelper.ShowError(result?.Message ?? "Cập nhật thất bại!");
				}
			}
			else
			{
				SnackbarHelper.ShowWarning("Không có thay đổi nào!");
			}
		}
		catch (Exception)
		{
			SnackbarHelper.ShowError("Có lỗi xảy ra, vui lòng thử lại!");
		}
		finally
		{
			ToggleUI(true);
		}
	}

	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}
