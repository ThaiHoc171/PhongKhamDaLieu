using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.Public;
public partial class UpdateBacSi : Window
{
	public UpdateBacSi(int NhanVienId)
	{
		InitializeComponent();
		_nhanVienID = NhanVienId;
		txtName.IsReadOnly = true;
		Loaded += async (_, __) => await LoadData();

	}

	private readonly int _nhanVienID;
	private int _id;
	private readonly BacSiProfileClient _client = new();
	private readonly UploadClient _upload = new();

	private string? _filePath;
	private string? _newPath;

	private async Task LoadData()
	{
		try
		{
			var result = await _client.GetByNhanVien(_nhanVienID);

			if (result == null || !result.Success || result.Data == null)
			{
				await MessageHelper.ShowMessage("Không tìm thấy nhân viên.");
				Close();
				return;
			}
			var data = result.Data;
			_id = data.BacSiProfileID;
			txtChuyenMon.Text = data.ChuyenMon;
			txtGioiThieu.Text = data.GioiThieu;
			txtKinhNghiem.Text = data.KinhNghiem;
			txtThanhTuu.Text = data.ThanhTuu;
			txtName.Text = data.HoTen;
			_filePath = data.HinhAnh;
			// ===== Load avatar =====
			if (!string.IsNullOrWhiteSpace(data.HinhAnh))
			{
				var url = $"https://hoanmyclinic.s3.ap-southeast-2.amazonaws.com/{data.HinhAnh}";
				imgAvatar.Source = new BitmapImage(new Uri(url));
			}
		}
		catch (Exception)
		{
			await MessageHelper.ShowMessage("Không thể tải dữ liệu nhân viên.");
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
			_newPath = dlg.FileName;
			imgAvatar.Source = new BitmapImage(new Uri(_newPath));
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
		btnSave.IsEnabled = isEnabled;
	}
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		string? avatarUrl = _filePath;

		try
		{
			ToggleUI(false);

			// upload avatar nếu chọn mới
			if (!string.IsNullOrEmpty(_newPath))
			{
				var uploadResult = await _upload.UploadImage(_newPath, "doctor_profile");

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
			var req = new BacSiProfileUpdateDTO
			{
				KinhNghiem = txtKinhNghiem.Text,
				ChuyenMon = txtChuyenMon.Text,
				GioiThieu = txtGioiThieu.Text,
				ThanhTuu = txtThanhTuu.Text,
				HinhAnh = avatarUrl
			};
			var result = await _client.Update(_id,req);

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
		catch (Exception)
		{
			await MessageHelper.ShowMessage("Có lỗi xảy ra, vui lòng thử lại!");
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
