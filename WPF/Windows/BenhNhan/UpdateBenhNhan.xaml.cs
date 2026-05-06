using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.BenhNhan;

public partial class UpdateBenhNhan : Window
{
	public UpdateBenhNhan(int id)
	{
		InitializeComponent();
		_id = id;
		cboGender.ItemsSource = new List<string>
		{
			"Nam", "Nữ", "Khác"
		};
		cboGender.SelectedIndex = 0;
		dtpBirth.SelectedDate = DateTime.Today;
		Loaded += async (_, __) => await LoadData();
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
	private async Task LoadData()
	{
		var result = await _client.Detail(_id);

		if (result?.Data == null)
		{
			await MessageHelper.ShowMessage("Không tìm thấy bệnh nhân.");
			Close();
			return;
		}

		_current = result.Data;

		txtName.Text = _current.HoTen;
		txtPhone.Text = _current.SDT;
		txtEmail.Text = _current.EmailLienHe;
		txtAddress.Text = _current.DiaChi;
		txtNotes.Text = _current.GhiChu;

		dtpBirth.SelectedDate = _current.NgaySinh;
		dtpCreateDate.SelectedDate = _current.NgayTao;
		dtpUpdateDate.SelectedDate = _current.NgayCapNhat;

		cboGender.SelectedItem = _current.GioiTinh;

		btnAccount.Visibility = _current.TaiKhoanID == null
			? Visibility.Visible
			: Visibility.Collapsed;

		// avatar
		if (!string.IsNullOrEmpty(_current.Avatar))
		{
			var url = $"https://hoanmyclinic.s3.ap-southeast-2.amazonaws.com/{_current.Avatar}";
			imgAvatar.Source = new BitmapImage(new Uri(url));
		}
	}

	private void btnAvt_Click(object sender, RoutedEventArgs e)
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
	private async Task<bool> ValidateInput()
	{
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập họ tên!");
			return false;
		}

		if (string.IsNullOrWhiteSpace(txtEmail.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập email!");
			return false;
		}

		return true;
	}
	private bool IsChanged()
	{
		return
			txtName.Text.Trim() != _current.HoTen ||
			txtPhone.Text.Trim() != _current.SDT ||
			txtEmail.Text.Trim() != _current.EmailLienHe ||
			txtAddress.Text.Trim() != _current.DiaChi ||
			txtNotes.Text.Trim() != _current.GhiChu ||
			(cboGender.SelectedItem?.ToString() ?? "Khác") != _current.GioiTinh ||
			(dtpBirth.SelectedDate ?? DateTime.Today) != _current.NgaySinh ||
			!string.IsNullOrEmpty(_avatarPath);
	}
	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
		btnAvt.IsEnabled = isEnabled;
	}
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (!await ValidateInput()) return;

		if (!IsChanged())
		{
			await MessageHelper.ShowMessage("Không có thay đổi nào!");
			return;
		}

		string? avatarUrl = _current.Avatar;

		try
		{
			ToggleUI(false);

			if (!string.IsNullOrEmpty(_avatarPath))
			{
				var uploadResult = await _upload.UploadImage(_avatarPath, "profile");

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

			var req = new BenhNhanUpdateRequest
			{
				HoTen = txtName.Text.Trim(),
				GioiTinh = cboGender.SelectedItem?.ToString() ?? "Khác",
				NgaySinh = dtpBirth.SelectedDate ?? DateTime.Today,
				SDT = txtPhone.Text.Trim(),
				EmailLienHe = txtEmail.Text.Trim(),
				DiaChi = txtAddress.Text.Trim(),
				Avatar = avatarUrl,
				GhiChu = txtNotes.Text.Trim()
			};

			var result = await _client.Update(_id, req);

			if (!result.Success)
			{
				await MessageHelper.ShowMessage(result.Message);
				return;
			}

			DialogResult = true;
			Close();
		}
		catch (Exception ex)
		{
			await MessageHelper.ShowMessage("Có lỗi xảy ra, vui lòng thử lại!\n" + ex.Message);
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

	private async void btnAccount_Click(object sender, RoutedEventArgs e)
	{
		var parentWindow = Window.GetWindow(this);
		var overlay = parentWindow.FindName("Overlay") as Border;

		if (overlay != null)
			overlay.Visibility = Visibility.Visible;
		var win = new AddBenhNhanAccount(_current.ThongTinID, _current.HoTen)
		{
			Owner = parentWindow
		};
		var result = win.ShowDialog();
		if (result == true)
		{
			await LoadData();
		}

		if (overlay != null)
			overlay.Visibility = Visibility.Collapsed;
	}
}