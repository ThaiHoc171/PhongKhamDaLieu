using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.ThietBi;

public partial class ThemThietBi : Window
{
	public ThemThietBi()
	{
		InitializeComponent();
		btnActive.IsChecked = true;
	}
	private readonly ThietBiClient _client = new ThietBiClient();
	private async void btnLuu_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập tên thiết bị!");
			return;
		}
		if (string.IsNullOrWhiteSpace(txtCategory.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập loại thiết bị!");
			return;
		}
		var req = new ThietBiRequest
		{
			TenTB = txtName.Text.Trim(),
			LoaiTB = txtCategory.Text.Trim(),
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
}
