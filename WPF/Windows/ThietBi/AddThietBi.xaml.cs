using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.ThietBi;

public partial class AddThietBi : Window
{
	public AddThietBi()
	{
		InitializeComponent();
		btnActive.IsChecked = true;
	}
	private readonly ThietBiClient _client = new ThietBiClient();
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
		btnActive.IsEnabled = isEnabled;
	}
	private async void btnSave_Click(object sender, EventArgs e)
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
