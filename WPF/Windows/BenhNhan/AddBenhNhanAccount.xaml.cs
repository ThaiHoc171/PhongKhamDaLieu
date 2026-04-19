using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.BenhNhan;

public partial class AddBenhNhanAccount : Window
{
    public AddBenhNhanAccount(int id,string name)
    {
        InitializeComponent();
		txtName.Text = name;
		_id = id;
	}
	private readonly int _id;
	private readonly TaiKhoanClient _taikhoan = new();
	private readonly ThongTinCaNhanClient _thongtin = new();
	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			DragMove();
		}
	}
	private async void btnSave_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtEmail.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập email!");
			return;
		}
		string password = string.IsNullOrWhiteSpace(txtPassword.Password) ? "123456" : txtPassword.Password;
		var req = new TaiKhoanRequestDTO
		{
			Email = txtEmail.Text,
			MatKhau = password,
			VaiTro = "Bệnh nhân"
		};

		try
		{
			btnSave.IsEnabled = false;
			btnClose.IsEnabled = false;

			var result = await _taikhoan.Create(req);

			if (result.Success)
			{
				int taiKhoanId = result.Data;
				var output = await _thongtin.LinkTaiKhoan(_id, taiKhoanId, txtEmail.Text);
				if(output.Success)
				{
					SnackbarHelper.ShowSuccess(output.Message);
					this.DialogResult = true;
					this.Close();
				}
				else
				{
					SnackbarHelper.ShowError(output.Message);
				}
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
			btnSave.IsEnabled = true;
			btnClose.IsEnabled = true;
		}
	}

	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}

