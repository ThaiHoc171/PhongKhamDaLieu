using System.Security.Cryptography;
using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.PhongChucNang;

public partial class CapNhatPhongChucNang : Window
{
    public CapNhatPhongChucNang(int id)
    {
        InitializeComponent();
		_id = id;
	}
	private readonly int _id;
	private readonly PhongChucNangClient _client = new();
	private PhongChucNangRequestDTO _current = new();

	private async void CapNhatPhongChucNang_Loaded(object sender, RoutedEventArgs e)
	{
		var result = await _client.GetById(_id);
		if (result != null && result.Data != null)
		{
			txtName.Text = _current.TenPhong = result.Data.TenPhong;
			txtDescription.Text = _current.MoTa =  result.Data.MoTa;
			dtpDateCreate.Text = result.Data.NgayTao.ToString("dd/MM/yyyy");
			dtpDateUpdate.Text = result.Data.NgayCapNhat?.ToString("dd/MM/yyyy") ?? "";
			txtStatus.Text = result.Data.TrangThai;
		}
		else
		{
			SnackbarHelper.ShowError("Không tìm thấy phòng chức năng.");
			this.Close();
		}
	}

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
		if(_current == req)
		{
			SnackbarHelper.ShowWarning("Không có thay đổi nào được thực hiện!");
			return;
		}
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
		if(e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
}
