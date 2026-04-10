using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.Thuoc;
public partial class ThemThuoc : Window
{
	public ThemThuoc()
	{
		InitializeComponent();
	}
	private readonly ThuocClient _client = new();
	private async void btnLuu_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập tên thuốc!");
			return;
		}
		if (string.IsNullOrWhiteSpace(txtActiveIngredient.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập hoạt chất!");
			return;
		}
		var req = new ThuocRequest
		{
			TenThuoc = txtName.Text.Trim(),
			HoatChat = txtActiveIngredient.Text.Trim()
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
