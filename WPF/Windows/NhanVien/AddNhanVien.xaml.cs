using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.NhanVien;

public partial class AddNhanVien : Window
{
    public AddNhanVien()
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
	private async Task<bool> ValidateForm()
	{
		if (string.IsNullOrWhiteSpace(txtHoTen.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập họ tên!");
			txtHoTen.Focus();
			return false;
		}

		if (string.IsNullOrWhiteSpace(txtSDT.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập số điện thoại!");
			txtSDT.Focus();
			return false;
		}

		if (string.IsNullOrWhiteSpace(txtEmail.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập email!");
			txtEmail.Focus();
			return false;
		}

		if (cboChucVu.SelectedValue == null)
		{
			await MessageHelper.ShowMessage("Vui lòng chọn chức vụ!");
			cboChucVu.Focus();
			return false;
		}

		if (cboPhongLamViec.SelectedValue == null)
		{
			await MessageHelper.ShowMessage("Vui lòng chọn phòng làm việc!");
			cboPhongLamViec.Focus();
			return false;
		}

		if (dtpNgaySinh.SelectedDate == null)
		{
			await MessageHelper.ShowMessage("Vui lòng chọn ngày sinh!");
			return false;
		}

		if (dtpNgaySinh.SelectedDate > DateTime.Today)
		{
			await MessageHelper.ShowMessage("Ngày sinh không hợp lệ!");
			return false;
		}

		if (dtpNgayVaoLam.SelectedDate == null)
		{
			await MessageHelper.ShowMessage("Vui lòng chọn ngày vào làm!");
			return false;
		}

		return true;
	}
	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
		btnChonAVt.IsEnabled = isEnabled;
	}
	// ================= LƯU =================
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (!await ValidateForm())
			return;

		try
		{
			ToggleUI(false);

			string? avatarUrl = null;

			// upload avatar nếu có
			if (!string.IsNullOrEmpty(_avatarPath))
			{
				var uploadResult = await _upload.UploadImage(_avatarPath, "nhanvien");

				if (!uploadResult.Success)
				{
					await MessageHelper.ShowMessage(uploadResult.Message);
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
				await MessageHelper.ShowMessage(result.Message);
			}
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

	// ================= HỦY =================
	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}