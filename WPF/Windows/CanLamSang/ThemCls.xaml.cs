using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.CanLamSang;

public partial class ThemCls : Window
{
    public ThemCls()
    {
        InitializeComponent();
		btnActive.IsChecked = true;
	}
	private readonly CanLamSangClient _client = new CanLamSangClient();
	private async void btnLuu_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập tên cận lâm sàng!");
			return;
		}
		if (string.IsNullOrWhiteSpace(txtDescription.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập mô tả!");
			return;
		}
		if (string.IsNullOrWhiteSpace(txtCategory.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập loại xét nghiệm!");
			return;
		}
		var req = new CanLamSangRequest
		{
			TenCLS = txtName.Text.Trim(),
			MoTa = txtDescription.Text.Trim(),
			LoaiXetNghiem =txtCategory.Text.Trim(),
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
