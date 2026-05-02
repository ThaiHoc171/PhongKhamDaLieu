using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.CanLamSang;

public partial class AddCls : Window
{
    public AddCls()
    {
        InitializeComponent();
		btnActive.IsChecked = true;
	}
	private readonly CanLamSangClient _client = new CanLamSangClient();
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
