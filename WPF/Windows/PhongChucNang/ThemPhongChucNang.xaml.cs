using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.PhongChucNang;
public partial class ThemPhongChucNang : Window
{
	public ThemPhongChucNang()
	{
		InitializeComponent();
	}
	private readonly PhongChucNangClient _client = new();
	private async void btnLuu_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập tên phòng!");
			return;
		}
		if (string.IsNullOrWhiteSpace(txtDescription.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập mô tả!");
			return;
		}
		var req = new PhongChucNangRequestDTO
		{
			TenPhong = txtName.Text.Trim(),
			MoTa = txtDescription.Text.Trim(),
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
