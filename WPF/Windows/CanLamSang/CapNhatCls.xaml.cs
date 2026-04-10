using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.CanLamSang;

public partial class CapNhatCls : Window
{
    public CapNhatCls(int id)
    {
        InitializeComponent();
		_id = id;
    }
	private readonly int _id;
	private readonly CanLamSangClient _client = new CanLamSangClient();
	private CanLamSangReadModel _current = new CanLamSangReadModel();
	private async void CapNhatCls_Loaded(object sender, RoutedEventArgs e)
	{
		var result = await _client.GetDetail(_id);
		if (result != null && result.Data != null)
		{
			_current = result.Data;
			txtName.Text = result.Data.TenCLS;
			txtDescription.Text = result.Data.MoTa;
			txtCategory.Text = result.Data.LoaiXetNghiem;
			dtpDateCreate.Text = result.Data.NgayTao.ToString("dd/MM/yyyy");
			dtpDateUpdate.Text = result.Data.NgayCapNhat?.ToString("dd/MM/yyyy") ?? "";
			btnActive.IsChecked = true ? result.Data.TrangThai == "Hoạt động" : result.Data.TrangThai == "Vô hiệu";
		}
		else
		{
			SnackbarHelper.ShowError("Không tìm thấy chức vụ.");
			this.Close();
		}
	}
	private async void btnLuu_Click(object sender, EventArgs e)
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
		var req = new CanLamSangRequest
		{
			TenCLS = txtName.Text.Trim(),
			MoTa = txtDescription.Text.Trim(),
			LoaiXetNghiem = txtCategory.Text.Trim(),
			TrangThai = btnActive.IsChecked == true ? "Hoạt động" : "Vô hiệu"
		};
		if (req.TenCLS == _current.TenCLS && req.MoTa == _current.MoTa && req.TrangThai == _current.TrangThai)
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
