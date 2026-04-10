using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.BenhNhan;

public partial class CapNhatBenhNhan : Window
{
	public CapNhatBenhNhan(int id)
	{
		InitializeComponent();
		_id = id;
		btnThemTaiKhoan.Visibility = Visibility.Collapsed;
	}

	private readonly int _id;
	private readonly BenhNhanClient _client = new();
	private readonly UploadClient _upload = new();

	private BenhNhanReadModel _current = new();
	private string? _avatarPath;
	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
	private async void CapNhatBenhNhan_Loaded(object sender, RoutedEventArgs e)
	{
		var result = await _client.Detail(_id);

		if (result != null && result.Data != null)
		{
			_current = result.Data;

			txtHoTen.Text = _current.HoTen;
			txtSDT.Text = _current.SDT;
			txtEmail.Text = _current.EmailLienHe;
			txtDiaChi.Text = _current.DiaChi;
			txtGhiChu.Text = _current.GhiChu;

			dtpNgaySinh.SelectedDate = _current.NgaySinh;

			dtpDateCreate.SelectedDate = _current.NgayTao;
			dtpDateUpdate.SelectedDate = _current.NgayCapNhat;
			foreach (ComboBoxItem item in cboGioiTinh.Items)
			{
				if (item.Content.ToString() == _current.GioiTinh)
				{
					cboGioiTinh.SelectedItem = item;
					break;
				}
			}
			if(_current.TaiKhoanID == null)
			{
				txtTaiKhoan.Text = "Chưa có tài khoản";
				btnThemTaiKhoan.Visibility = Visibility.Visible;
			}
			else
			{
				txtTaiKhoan.Text = "Đã có tài khoản";
				btnThemTaiKhoan.Visibility = Visibility.Collapsed;
			}
			if (!string.IsNullOrEmpty(_current.Avatar))
			{
				var url = $"https://hoanmyclinic.s3.ap-southeast-2.amazonaws.com/{_current.Avatar}";
				imgAvatar.Source = new BitmapImage(new Uri(url));
			}
		}
		else
		{
			SnackbarHelper.ShowError("Không tìm thấy bệnh nhân.");
			this.Close();
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

		string? avatarUrl = _current.Avatar;

		try
		{
			btnLuu.IsEnabled = false;
			btnHuy.IsEnabled = false;

			// upload avatar nếu chọn mới
			if (!string.IsNullOrEmpty(_avatarPath))
			{
				var uploadResult = await _upload.UploadImage(_avatarPath, "profile");

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

			var req = new BenhNhanUpdateRequest
			{
				HoTen = txtHoTen.Text.Trim(),
				GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Khác",
				NgaySinh = dtpNgaySinh.SelectedDate ?? DateTime.Today,
				SDT = txtSDT.Text.Trim(),
				EmailLienHe = txtEmail.Text.Trim(),
				DiaChi = txtDiaChi.Text.Trim(),
				Avatar = avatarUrl,
				GhiChu = txtGhiChu.Text.Trim()
			};

			var result = await _client.Update(_id, req);

			if (result.Success)
			{
				this.DialogResult = true;
				this.Close();
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
			btnLuu.IsEnabled = true;
			btnHuy.IsEnabled = true;
		}
	}

	private void btnHuy_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}

	private void btnThemTaiKhoan_Click(object sender, RoutedEventArgs e)
	{
		var parentWindow = Window.GetWindow(this);
		var overlay = parentWindow.FindName("Overlay") as Border;

		if (overlay != null)
			overlay.Visibility = Visibility.Visible;
		var win = new ThemTaiKhoan(_current.ThongTinID, _current.HoTen)
		{
			Owner = parentWindow
		};
		var result = win.ShowDialog();
		if (result == true)
		{
			CapNhatBenhNhan_Loaded(sender, e);
		}

		if (overlay != null)
			overlay.Visibility = Visibility.Collapsed;
	}
}