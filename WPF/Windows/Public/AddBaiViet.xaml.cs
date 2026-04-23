using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.Public;

public partial class AddBaiViet : Window
    {
	public AddBaiViet()
	{
		InitializeComponent();
		Loaded += async (_, __) => await LoadComboBox();
	}
	private readonly BaiVietClient _client = new();
	private readonly UploadClient _upload = new();
	private readonly LoaiBenhClient _loaibenh = new();
	private string? _filePath;
	private void Header_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (e.LeftButton == MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
	private async Task LoadComboBox()
	{
		var list = await _loaibenh.Combobox();
		if (list.Success)
		{
			cboDisease.ItemsSource = list.Data;
			cboDisease.DisplayMemberPath = "Name";
			cboDisease.SelectedValuePath = "Id";
			cboDisease.SelectedIndex = -1;
		}
	}
	private void btnChoosePic_Click(object sender, RoutedEventArgs e)
	{
		var dialog = new OpenFileDialog();
		dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

		if (dialog.ShowDialog() == true)
		{
			_filePath = dialog.FileName;

			using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);

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
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập tiêu đề!");
			txtName.Focus();
			return false;
		}
		if (string.IsNullOrWhiteSpace(txtTomtat.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập tóm tắt!");
			txtTomtat.Focus();
			return false;
		}
		if (string.IsNullOrWhiteSpace(txtContent.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập kinh ngiệm!");
			txtContent.Focus();
			return false;
		}
		if(cboDisease.SelectedIndex < 0)
		{
			SnackbarHelper.ShowError("Vui lòng chọn loại bệnh cho bài viết!");
			return false;
		}
		if (Session.UserId == 0)
		{
			SnackbarHelper.ShowError("Không xác định được tác giả");
			return false;
		}
		return true;
	}
	private void ToggleUI(bool isEnabled)
	{
		txtName.IsReadOnly = !isEnabled;
		txtTomtat.IsReadOnly = !isEnabled;
		txtContent.IsReadOnly = !isEnabled;
		cboDisease.IsEnabled = isEnabled;
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
		btnChoosePic.IsEnabled = isEnabled;
	}
	// ================= LƯU =================
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm())
			return;

		try
		{
			ToggleUI(false);

			string? filePath = null;

			if (!string.IsNullOrEmpty(_filePath))
			{
				var uploadResult = await _upload.UploadImage(_filePath, "baiviet");

				if (!uploadResult.Success)
				{
					SnackbarHelper.ShowError(uploadResult.Message);
					return;
				}
				if (!string.IsNullOrEmpty(uploadResult.Data))
				{
					var uri = new Uri(uploadResult.Data);
					filePath = uri.AbsolutePath.TrimStart('/');
				}
			}
			var req = new ThemBaiVietDTO
			{
				TacGiaID = Session.UserId,
				TieuDe = txtName.Text,
				TomTat = txtTomtat.Text,
				LoaiBenhID = (int)cboDisease.SelectedValue,
				NoiDung = txtContent.Text,
				HinhAnh = filePath
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