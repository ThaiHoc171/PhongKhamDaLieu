using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.Khach;

public partial class CapNhatKhach : Window
{
	public CapNhatKhach(int id)
	{
		InitializeComponent();
		_id = id;

		cboGioiTinh.ItemsSource = new List<string> { "Nam", "Nữ", "Khác" };
		cboGioiTinh.SelectedIndex = 0;
	}

	private readonly int _id;
	private readonly ThongTinCaNhanClient _client = new();
	private readonly UploadClient _upload = new();

	private ThongTinReadModel _current = new();
	private string? _avatarPath;

	private async void CapNhatKhach_Loaded(object sender, RoutedEventArgs e)
	{
		var result = await _client.Detail(_id);

		if (result?.Data == null)
		{
			SnackbarHelper.ShowError("Không tìm thấy khách.");
			Close();
			return;
		}

		_current = result.Data;

		txtHoTen.Text = _current.HoTen ?? "";
		txtSDT.Text = _current.SDT ?? "";
		txtEmail.Text = _current.EmailLienHe ?? "";
		txtDiaChi.Text = _current.DiaChi ?? "";

		dtpNgaySinh.SelectedDate = _current.NgaySinh;
		dtpDateCreate.SelectedDate = _current.NgayTao;
		dtpDateUpdate.SelectedDate = _current.NgayCapNhat;

		cboGioiTinh.SelectedItem = _current.GioiTinh;

		if (!string.IsNullOrWhiteSpace(_current.Avatar))
		{
			var url = $"https://hoanmyclinic.s3.ap-southeast-2.amazonaws.com/{_current.Avatar}";
			imgAvatar.Source = new BitmapImage(new Uri(url));
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
			if (!string.IsNullOrWhiteSpace(_avatarPath))
			{
				var uploadResult = await _upload.UploadImage(_avatarPath, "profile");

				if (!uploadResult.Success)
				{
					SnackbarHelper.ShowError(uploadResult.Message);
					return;
				}

				if (!string.IsNullOrWhiteSpace(uploadResult.Data))
				{
					var uri = new Uri(uploadResult.Data);
					avatarUrl = uri.AbsolutePath.TrimStart('/');
				}
			}

			var req = new ThongTinUpdateRequestDTO
			{
				HoTen = txtHoTen.Text?.Trim() ?? "",
				GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Khác",
				NgaySinh = dtpNgaySinh.SelectedDate ?? DateTime.Today,
				SDT = txtSDT.Text?.Trim() ?? "",
				EmailLienHe = string.IsNullOrWhiteSpace(txtEmail.Text)
					? null
					: txtEmail.Text.Trim(),
				DiaChi = txtDiaChi.Text?.Trim() ?? "",
				Avatar = avatarUrl,
				Loai = "Khách"
			};

			var result = await _client.Update(_id, req);

			if (result.Success)
			{
				DialogResult = true;
				Close();
			}
			else
			{
				SnackbarHelper.ShowError(result.Message);
			}
		}
		catch
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
		Close();
	}
}