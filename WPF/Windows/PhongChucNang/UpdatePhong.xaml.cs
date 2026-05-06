using System.Security.Cryptography;
using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.PhongChucNang;

public partial class UpdatePhong : Window
{
    public UpdatePhong(int id)
    {
        InitializeComponent();
		_id = id;
	}
	private readonly int _id;
	private readonly PhongChucNangClient _client = new();
	private PhongChucNangRequestDTO _current = new();

	private async void UpdatePhong_Loaded(object sender, RoutedEventArgs e)
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
			await MessageHelper.ShowMessage("Không tìm thấy phòng chức năng.");
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
			await MessageHelper.ShowMessage("Vui lòng nhập tên phòng!");
			return;
		}
		if (string.IsNullOrWhiteSpace(txtDescription.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập mô tả!");
			return;
		}
		var req = new PhongChucNangRequestDTO
		{
			TenPhong = txtName.Text.Trim(),
			MoTa = txtDescription.Text.Trim(),
		};
		if(_current == req)
		{
			await MessageHelper.ShowMessage("Không có thay đổi nào được thực hiện!");
			return;
		}
		try
		{
			ToggleUI(false);

			var res = await _client.Update(_id,req);

			if (res.Success)
			{
				this.DialogResult = true;
				this.Close();
			}
			else
			{
				await MessageHelper.ShowMessage(res.Message);
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

	private void Window_Loaded(object sender, RoutedEventArgs e)
	{

	}
}
