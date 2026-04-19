using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.ChucVu;


public partial class UpdateChucVu : Window
{
	public UpdateChucVu(int id)
	{
		InitializeComponent();
		_id = id;
	}
	private readonly int _id;
	private readonly ChucVuClient _client = new ChucVuClient();
	private ChucVuReadModel _current = new ChucVuReadModel();
	private async void UpdateChucVu_Loaded(object sender, RoutedEventArgs e)
	{
		var result = await _client.Detail(_id);
		if (result != null && result.Data != null)
		{
			_current = result.Data;
			txtName.Text = result.Data.TenChucVu;
			txtDescription.Text = result.Data.MoTa;
			dtpDateCreate.Text = result.Data.NgayTao.ToString("dd/MM/yyyy");
			dtpDateUpdate.Text = result.Data.NgayCapNhat?.ToString("dd/MM/yyyy")?? "";
			btnActive.IsChecked = true ? result.Data.TrangThai == "Hoạt động" : result.Data.TrangThai == "Vô hiệu";
		}
		else
		{
			SnackbarHelper.ShowError("Không tìm thấy chức vụ.");
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
		if(req.TenChucVu == _current.TenChucVu && req.MoTa == _current.MoTa && req.TrangThai == _current.TrangThai)
		{
			SnackbarHelper.ShowWarning("Không có thay đổi nào để cập nhật!");
			return;
		}
		try
		{
			ToggleUI(false);
			var result = await _client.Update(_id,req);

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
