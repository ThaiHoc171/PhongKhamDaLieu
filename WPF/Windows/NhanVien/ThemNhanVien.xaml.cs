using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.NhanVien;

public partial class ThemNhanVien : Window
{
    public ThemNhanVien()
    {
        InitializeComponent();
		LoadComboBox();
	}
	private readonly NhanVienClient _client = new();
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
	private async void LoadComboBox()
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
	private bool ValidateForm()
	{
		if (string.IsNullOrWhiteSpace(txtHoTen.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập họ tên!");
			txtHoTen.Focus();
			return false;
		}

		if (string.IsNullOrWhiteSpace(txtSDT.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập số điện thoại!");
			txtSDT.Focus();
			return false;
		}

		if (string.IsNullOrWhiteSpace(txtEmail.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập email!");
			txtEmail.Focus();
			return false;
		}

		if (cboChucVu.SelectedValue == null)
		{
			SnackbarHelper.ShowError("Vui lòng chọn chức vụ!");
			cboChucVu.Focus();
			return false;
		}

		if (cboPhongLamViec.SelectedValue == null)
		{
			SnackbarHelper.ShowError("Vui lòng chọn phòng làm việc!");
			cboPhongLamViec.Focus();
			return false;
		}

		if (dtpNgaySinh.SelectedDate == null)
		{
			SnackbarHelper.ShowError("Vui lòng chọn ngày sinh!");
			return false;
		}

		if (dtpNgaySinh.SelectedDate > DateTime.Today)
		{
			SnackbarHelper.ShowError("Ngày sinh không hợp lệ!");
			return false;
		}

		if (dtpNgayVaoLam.SelectedDate == null)
		{
			SnackbarHelper.ShowError("Vui lòng chọn ngày vào làm!");
			return false;
		}

		return true;
	}
	// ================= LƯU =================
	private async void btnLuu_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm())
			return;

		try
		{
			btnLuu.IsEnabled = false;
			btnHuy.IsEnabled = false;

			string? avatarUrl = null;

			// upload avatar nếu có
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
			var thongtin = new ThongTinRequestDTO
			{
				HoTen = txtHoTen.Text.Trim(),
				GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Khác",
				NgaySinh = dtpNgaySinh.SelectedDate ?? DateTime.Today,
				SDT = txtSDT.Text.Trim(),
				EmailLienHe = txtEmail.Text.Trim(),
				DiaChi = txtDiaChi.Text.Trim(),
				Avatar = avatarUrl
			};

			var req = new NhanVienRequestDTO
			{
				ThongTin = thongtin,
				ChucVuID = (int)(cboChucVu.SelectedValue ?? 0),
				PhongChucNangID = (int)(cboPhongLamViec.SelectedValue ?? 0),
				NgayVaoLam = dtpNgayVaoLam.SelectedDate ?? DateTime.Today,
				BangCap = txtBangCap.Text.Trim(),
				KinhNghiem = txtKinhNghiem.Text.Trim()
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