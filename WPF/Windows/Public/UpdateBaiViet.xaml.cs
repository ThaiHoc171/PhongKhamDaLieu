using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HoanMyClinic.Windows.Public;

public partial class UpdateBaiViet : Window
{
	private readonly int _baiVietId;
	private readonly BaiVietClient _client = new();
	private readonly UploadClient _upload = new();
	private readonly LoaiBenhClient _loaibenh = new();

	private string? _filePath;

	public UpdateBaiViet(int baiVietId)
	{
		InitializeComponent();
		_baiVietId = baiVietId;

		Loaded += async (_, __) =>
		{
			await LoadComboBox();
			await LoadData();
		};
	}

	// ================= LOAD DATA =================
	private async Task LoadData()
	{
		try
		{
			var result = await _client.Detail(_baiVietId);

			if (!result.Success || result.Data == null)
			{
				await MessageHelper.ShowMessage("Không tìm thấy bài viết");
				Close();
				return;
			}

			var data = result.Data;

			txtName.Text = data.TieuDe;
			txtTomtat.Text = data.TomTat;
			txtContent.Text = data.NoiDung;

			cboDisease.SelectedValue = data.LoaiBenhID;

			txtTrangThai.Text = data.TrangThai;
			txtLuotXem.Text = data.LuotXem.ToString();
			txtNgayDang.Text = data.NgayDang.ToString("dd/MM/yyyy HH:mm");
			txtNgayCapNhat.Text = data.NgayCapNhat?.ToString("dd/MM/yyyy HH:mm");

			_filePath = data.HinhAnh;

			if (!string.IsNullOrWhiteSpace(data.HinhAnh))
			{
				var url = $"https://hoanmyclinic.s3.ap-southeast-2.amazonaws.com/{data.HinhAnh}";
				imgAvatar.Source = new BitmapImage(new Uri(url));
			}
		}
		catch
		{
			await MessageHelper.ShowMessage("Lỗi load dữ liệu");
			Close();
		}
	}

	// ================= LOAD COMBO =================
	private async Task LoadComboBox()
	{
		var list = await _loaibenh.Combobox();

		if (list.Success)
		{
			cboDisease.ItemsSource = list.Data;
			cboDisease.DisplayMemberPath = "Name";
			cboDisease.SelectedValuePath = "Id";
		}
	}

	// ================= CHỌN ẢNH =================
	private void btnChoosePic_Click(object sender, RoutedEventArgs e)
	{
		var dialog = new OpenFileDialog
		{
			Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
		};

		if (dialog.ShowDialog() == true)
		{
			_filePath = dialog.FileName;
			imgAvatar.Source = new BitmapImage(new Uri(_filePath));
		}
	}

	// ================= VALIDATE =================
	private async Task<bool> ValidateForm()
	{
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			await MessageHelper.ShowMessage("Nhập tiêu đề");
			return false;
		}

		if (string.IsNullOrWhiteSpace(txtContent.Text))
		{
			await MessageHelper.ShowMessage("Nhập nội dung");
			return false;
		}

		if (cboDisease.SelectedValue == null)
		{
			await MessageHelper.ShowMessage("Chọn loại bệnh");
			return false;
		}

		return true;
	}

	private void ToggleUI(bool enable)
	{
		txtName.IsReadOnly = !enable;
		txtTomtat.IsReadOnly = !enable;
		txtContent.IsReadOnly = !enable;
		cboDisease.IsEnabled = enable;
	}

	// ================= UPDATE =================
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (!await ValidateForm()) return;

		try
		{
			ToggleUI(false);

			string? filePath = _filePath;

			if (!string.IsNullOrEmpty(_filePath) && File.Exists(_filePath))
			{
				var upload = await _upload.UploadImage(_filePath, "baiviet");

				if (!upload.Success)
				{
					await MessageHelper.ShowMessage(upload.Message);
					return;
				}

				filePath = new Uri(upload.Data).AbsolutePath.TrimStart('/');
			}

			var req = new CapNhatBaiVietDTO
			{
				TieuDe = txtName.Text,
				TomTat = txtTomtat.Text,
				NoiDung = txtContent.Text,
				LoaiBenhID = (int)cboDisease.SelectedValue,
				HinhAnh = filePath
			};

			var result = await _client.Update(_baiVietId, req);

			if (result.Success)
			{
				SnackbarHelper.ShowSuccess("Cập nhật thành công");
				DialogResult = true; 
				await LoadData();
			}
			else
			{
				await MessageHelper.ShowMessage(result.Message);
			}
		}
		finally
		{
			ToggleUI(true);
		}
	}

	// ================= POST =================
	private async void btnPost_Click(object sender, RoutedEventArgs e)
	{
		var res = await _client.Post(_baiVietId);

		if (res.Success)
		{
			SnackbarHelper.ShowSuccess("Đã đăng bài");
			DialogResult = true; 
			await LoadData();
		}
		else await MessageHelper.ShowMessage(res.Message);
	}

	// ================= HIDE =================
	private async void btnHide_Click(object sender, RoutedEventArgs e)
	{
		var res = await _client.Hide(_baiVietId);

		if (res.Success)
		{
			SnackbarHelper.ShowSuccess("Đã ẩn bài");
			DialogResult = true; 
			await LoadData();
		}
		else await MessageHelper.ShowMessage(res.Message);
	}

	// ================= SAVE DRAFT =================
	private async void btnSaveDraft_Click(object sender, RoutedEventArgs e)
	{
		var res = await _client.Save(_baiVietId);

		if (res.Success)
		{
			SnackbarHelper.ShowSuccess("Đã lưu nháp");
			DialogResult = true; 
			await LoadData();
		}
		else await MessageHelper.ShowMessage(res.Message);
	}

	// ================= DELETE =================
	private async void btnDelete_Click(object sender, RoutedEventArgs e)
	{
		var confirm = await MessageHelper.Confirm("Xóa bài viết?");

		if (!confirm) return;

		var res = await _client.Delete(_baiVietId);

		if (res.Success)
		{
			DialogResult = true; 
			Close();
		}
		else await MessageHelper.ShowMessage(res.Message);
	}

	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = true;
		Close();
	}

	private void Header_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (e.LeftButton == MouseButtonState.Pressed)
			DragMove();
	}
}