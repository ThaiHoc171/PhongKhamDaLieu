using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.Thuoc;
public partial class AddThuoc : Window
{
	public AddThuoc()
	{
		InitializeComponent();
	}
	private readonly ThuocClient _client = new();
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
	}
	private async void btnSave_Click(object sender, EventArgs e)
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
			ToggleUI(false);

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
			ToggleUI(true);
		}
	}

	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}
