using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.BenhNhan;

public partial class ThemBenhNhan : Window
{
	private readonly BenhNhanClient _client = new();
	private readonly UploadClient _upload = new();

	private string? _avatarPath;

	public ThemBenhNhan()
	{
		InitializeComponent();
		cboGioiTinh.ItemsSource = new List<string> { "Nam", "Nữ", "Khác" };
		cboGioiTinh.SelectedIndex = 0;
	}
	private void btnChonAVt_Click(object sender, RoutedEventArgs e)
	{
		var dialog = new OpenFileDialog();
		dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

		if (dialog.ShowDialog() == true)
		{
			_avatarPath = dialog.FileName;

			using var stream = new FileStream(_avatarPath, FileMode.Open, FileAccess.Read);

			var bitmap = new BitmapImage();
			bitmap.BeginInit();
			bitmap.CacheOption = BitmapCacheOption.OnLoad;
			bitmap.StreamSource = stream;
			bitmap.EndInit();
			bitmap.Freeze();

			imgAvatar.Source = bitmap;
		}
	}

	// ================= LƯU =================
	private async void btnLuu_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtHoTen.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập họ tên!");
			return;
		}

		try
		{
			btnLuu.IsEnabled = false;
			btnHuy.IsEnabled = false;

			string? avatarUrl = null;

			// upload avatar nếu có
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

			var req = new BenhNhanRequest
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

			var result = await _client.Create(req);

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
			SnackbarHelper.ShowError("Có lỗi xảy ra!");
		}
		finally
		{
			btnLuu.IsEnabled = true;
			btnHuy.IsEnabled = true;
		}
	}

	// ================= HỦY =================
	private void btnHuy_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}