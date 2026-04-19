using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.CaKham;

public partial class Cancel : Window
{
    public Cancel(int id)
    {
        InitializeComponent();
		_id = id;
	}
	private readonly CaKhamClient _client = new();
	private readonly int _id;
	private async void Cancel_Loaded(object sender, RoutedEventArgs e)
	{
		txtID.Text = _id.ToString();
		var result = await _client.GetDetail(_id);
		if (!result.Success || result.Data == null)
		{
			SnackbarHelper.ShowError(result.Message ?? "Không tìm thấy ca khám!");
			Close();
			return;
		}

		txtName.Text = $"{result.Data.TenKhungGio} / {result.Data.NgayKham:dd/MM/yyyy}";
		txtUser.Text = result.Data.HoTen ?? "";
	}
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
		if (string.IsNullOrWhiteSpace(txtLyDo.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập lý do từ chối!");
			return;
		}
		var req = new CaKhamTrangThaiDTO
		{
			TrangThai = "Đã hủy",
			GhiChu = txtLyDo.Text.Trim()
		};

		try
		{
			ToggleUI(false);

			var result = await _client.UpdateTrangThai(_id,req);

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
