using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.ThietBi;

public partial class UpdateThietBi : Window
{
	public UpdateThietBi(int id)
	{
		InitializeComponent();
		_id = id;
	}
	private readonly int _id;
	private readonly ThietBiClient _client = new ThietBiClient();
	private ThietBiReadModel _current = new ThietBiReadModel();
	private async void UpdateThietBi_Loaded(object sender, RoutedEventArgs e)
	{
		var result = await _client.GetDetail(_id);
		if (result != null && result.Data != null)
		{
			_current = result.Data;
			txtName.Text = result.Data.TenTB;
			txtCategory.Text = result.Data.LoaiTB;
			dtpDateCreate.Text = result.Data.NgayTao.ToString("dd/MM/yyyy");
			dtpDateUpdate.Text = result.Data.NgayCapNhat?.ToString("dd/MM/yyyy") ?? "";
			btnActive.IsChecked = true ? result.Data.TrangThai == "Hoạt động" : result.Data.TrangThai == "Vô hiệu";
		}
		else
		{
			await MessageHelper.ShowMessage("Không tìm thấy thiết bị!");
			this.Close();
		}
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
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập tên thiết bị!");
			return;
		}
		if (string.IsNullOrWhiteSpace(txtCategory.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập loại thiết bị!");
			return;
		}
		var req = new ThietBiRequest
		{
			TenTB = txtName.Text.Trim(),
			LoaiTB = txtCategory.Text.Trim(),
			TrangThai = btnActive.IsChecked == true ? "Hoạt động" : "Vô hiệu"
		};

		if (req.TenTB == _current.TenTB && req.LoaiTB == _current.LoaiTB && req.TrangThai == _current.TrangThai)
		{
			await MessageHelper.ShowMessage("Không có thay đổi nào để cập nhật!");
			return;
		}
		try
		{
			ToggleUI(false);
			var result = await _client.Update(_id, req);

			if (result.Success)
			{
				this.DialogResult = true;
				this.Close();
			}
			else
			{
				await MessageHelper.ShowMessage(result.Message);
			}
		}
		catch (Exception)
		{
			await MessageHelper.ShowMessage("Có lỗi xảy ra, vui lòng thử lại!");
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
