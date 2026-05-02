using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.Khach;

public partial class UpdateKhach : Window
{
	public UpdateKhach(int id)
	{
		InitializeComponent();
		_id = id;

		cboGender.ItemsSource = new List<string> { "Nam", "Nữ", "Khác" };
		cboGender.SelectedIndex = 0;
	}

	private readonly int _id;
	private readonly ThongTinCaNhanClient _client = new();
	private readonly UploadClient _upload = new();

	private ThongTinReadModel _current = new();
	private string? _avatarPath;

	private async void UpdateKhach_Loaded(object sender, RoutedEventArgs e)
	{
		var result = await _client.Detail(_id);

		if (result?.Data == null)
		{
			SnackbarHelper.ShowError("Không tìm thấy khách.");
			Close();
			return;
		}

		_current = result.Data;

		txtName.Text = _current.HoTen ?? "";
		txtPhone.Text = _current.SDT ?? "";
		txtEmail.Text = _current.EmailLienHe ?? "";
		txtAddress.Text = _current.DiaChi ?? "";

		dtpBirth.SelectedDate = _current.NgaySinh;
		dtpDateCreate.SelectedDate = _current.NgayTao;
		dtpDateUpdate.SelectedDate = _current.NgayCapNhat;

		cboGender.SelectedItem = _current.GioiTinh;

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
	}
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập họ tên!");
			return;
		}

		string? avatarUrl = _current.Avatar;

		try
		{
			ToggleUI(false);

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
				HoTen = txtName.Text?.Trim() ?? "",
				GioiTinh = cboGender.SelectedItem?.ToString() ?? "Khác",
				NgaySinh = dtpBirth.SelectedDate ?? DateTime.Today,
				SDT = txtPhone.Text?.Trim() ?? "",
				EmailLienHe = string.IsNullOrWhiteSpace(txtEmail.Text)
					? null
					: txtEmail.Text.Trim(),
				DiaChi = txtAddress.Text?.Trim() ?? "",
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
			ToggleUI(true);
		}
	}

	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}
}