using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.ChucVu;

public partial class ThemChucVu : Window
{
	public ThemChucVu()
	{
		InitializeComponent();
		btnActive.IsChecked = true;
	}
	private readonly ChucVuClient _client = new ChucVuClient();
	private async void btnLuu_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập tên chức vụ!");
			return;
		}
		if (string.IsNullOrWhiteSpace(txtDescription.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập mô tả!");
			return;
		}
		var req = new ChucVuRequest
		{
			TenChucVu = txtName.Text.Trim(),
			MoTa = txtDescription.Text.Trim(),
			TrangThai = btnActive.IsChecked == true ? "Hoạt động" : "Vô hiệu"
		};

		try
		{
			btnLuu.IsEnabled = false;
			btnHuy.IsEnabled = false;

			var result = await _client.Create(req);

			if (result.Success)
			{
				this.DialogResult = true;
				this.Close();
			}
			else
			{
				SnackbarHelper.ShowError(result.Message);
			}
		}
		catch (Exception)
		{
			SnackbarHelper.ShowError("Có lỗi xảy ra, vui lòng thử lại!");
		}
		finally
		{
			btnLuu.IsEnabled = true;
			btnHuy.IsEnabled = true;
		}
	}

	private void btnHuy_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}

	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
}
