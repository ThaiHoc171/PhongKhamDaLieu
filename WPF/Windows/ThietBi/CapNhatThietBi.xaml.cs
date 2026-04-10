using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.ThietBi;

public partial class CapNhatThietBi : Window
{
	public CapNhatThietBi(int id)
	{
		InitializeComponent();
		_id = id;
	}
	private readonly int _id;
	private readonly ThietBiClient _client = new ThietBiClient();
	private ThietBiReadModel _current = new ThietBiReadModel();
	private async void CapNhatThietBi_Loaded(object sender, RoutedEventArgs e)
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
			SnackbarHelper.ShowError("Không tìm thấy thiết bị!");
			this.Close();
		}
	}
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

		if (req.TenTB == _current.TenTB && req.LoaiTB == _current.LoaiTB && req.TrangThai == _current.TrangThai)
		{
			SnackbarHelper.ShowWarning("Không có thay đổi nào để cập nhật!");
			return;
		}
		try
		{
			btnLuu.IsEnabled = false;
			btnHuy.IsEnabled = false;

			var result = await _client.Update(_id, req);

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
