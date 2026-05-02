using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.Public;

public partial class AddBacSi : Window
{
	public AddBacSi(int NhanVienId, string NhanVienName)
	{
		InitializeComponent();
		txtName.Text = NhanVienName;
		txtName.IsReadOnly = true;
		_id = NhanVienId;
	}
	private readonly int _id;
	private readonly BacSiProfileClient _client = new();
	private readonly UploadClient _upload = new();
	private readonly ChucVuClient _cv = new();
	private readonly PhongChucNangClient _pcn = new();
	private string? _avatarPath;
	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
	private void btnChooseAvt_Click(object sender, RoutedEventArgs e)
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
	private bool ValidateForm()
	{
		if (string.IsNullOrWhiteSpace(txtChuyenMon.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập chuyên môn!");
			txtChuyenMon.Focus();
			return false;
		}
		if (string.IsNullOrWhiteSpace(txtGioiThieu.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập giới thiệu!");
			txtGioiThieu.Focus();
			return false;
		}
		if (string.IsNullOrWhiteSpace(txtKinhNghiem.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập kinh ngiệm!");
			txtKinhNghiem.Focus();
			return false;
		}
		if (string.IsNullOrWhiteSpace(txtThanhTuu.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập thành tựu!");
			txtThanhTuu.Focus();
			return false;
		}
		return true;
	}
	private void ToggleUI(bool isEnabled)
	{
		txtChuyenMon.IsReadOnly= !isEnabled;
		txtGioiThieu.IsReadOnly= !isEnabled;
		txtKinhNghiem.IsReadOnly = !isEnabled;
		txtThanhTuu.IsReadOnly= !isEnabled;
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
		btnChonAVt.IsEnabled = isEnabled;
	}
	// ================= LƯU =================
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm())
			return;

		try
		{
			ToggleUI(false);

			string? avatarUrl = null;

			// upload avatar nếu có
			if (!string.IsNullOrEmpty(_avatarPath))
			{
				var uploadResult = await _upload.UploadImage(_avatarPath, "doctor_profile");

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
			var req = new BacSiProfileRequestDTO
			{
				NhanVienID = _id,
				ThanhTuu = txtThanhTuu.Text,
				KinhNghiem = txtKinhNghiem.Text,
				ChuyenMon = txtChuyenMon.Text,
				GioiThieu = txtGioiThieu.Text,
				HinhAnh = avatarUrl
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
			ToggleUI(true);
		}
	}

	// ================= HỦY =================
	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}
