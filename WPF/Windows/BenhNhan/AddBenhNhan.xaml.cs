using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.BenhNhan;

public partial class AddBenhNhan : Window
{
	private readonly BenhNhanClient _bn = new();
	private readonly UploadClient _upload = new();

	private string? _avatarPath;

	public AddBenhNhan()
	{
		InitializeComponent();

		cboGender.ItemsSource = new List<string>
		{
			"Nam", "Nữ", "Khác"
		};
		cboGender.SelectedIndex = 0;
		dtpBirth.SelectedDate = DateTime.Today;
	}
	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			DragMove();
		}
	}
	private void btnAvt_Click(object sender, RoutedEventArgs e)
	{
		var dialog = new OpenFileDialog
		{
			Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
		};

		if (dialog.ShowDialog() != true) return;

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
	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
		btnAvt.IsEnabled = isEnabled;
	}
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập họ tên!");
			return;
		}

		try
		{
			ToggleUI(false);

			string? avatarUrl = null;

			if (!string.IsNullOrEmpty(_avatarPath))
			{
				var upload = await _upload.UploadImage(_avatarPath, "profile");

				if (!upload.Success)
				{
					await MessageHelper.ShowMessage(upload.Message);
					return;
				}

				if (!string.IsNullOrEmpty(upload.Data))
				{
					avatarUrl = new Uri(upload.Data).AbsolutePath.TrimStart('/');
				}
			}

			var req = new BenhNhanRequest
			{
				HoTen = txtName.Text.Trim(),
				GioiTinh = cboGender.SelectedItem?.ToString() ?? "Other",
				NgaySinh = dtpBirth.SelectedDate ?? DateTime.Today,
				SDT = txtPhone.Text.Trim(),
				EmailLienHe = txtEmail.Text.Trim(),
				DiaChi = txtAddress.Text.Trim(),
				Avatar = avatarUrl,
				GhiChu = txtNotes.Text.Trim()
			};

			var res = await _bn.Create(req);

			if (!res.Success)
			{
				await MessageHelper.ShowMessage(res.Message);
				return;
			}

			DialogResult = true;
			Close();
		}
		catch
		{
			await MessageHelper.ShowMessage("Có lỗi xảy ra!");
		}
		finally
		{
			ToggleUI(true);
		}
	}

	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}
}