using System.Diagnostics;
using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.BenhNhan;

public partial class ThemTaiKhoan : Window
{
    public ThemTaiKhoan(int id,string name)
    {
        InitializeComponent();
		txtName.Text = name;
		_id = id;
	}
	private readonly int _id;
	private readonly TaiKhoanClient _client = new();
	private readonly ThongTinCaNhanClient _thongtinclient = new();
	private async void btnLuu_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtEmail.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập email!");
			return;
		}
		string password = string.IsNullOrWhiteSpace(txtPassword.Text) ? "123456" : txtPassword.Text;
		var req = new TaiKhoanRequestDTO
		{
			Email = txtEmail.Text,
			MatKhau = password,
			VaiTro = "Bệnh nhân"
		};

		try
		{
			btnLuu.IsEnabled = false;
			btnHuy.IsEnabled = false;

			var result = await _client.Create(req);

			if (result.Success)
			{
				int taiKhoanId = result.Data;
				var output = await _thongtinclient.LinkTaiKhoan(_id, taiKhoanId, txtEmail.Text);
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
			btnLuu.IsEnabled = true;
			btnHuy.IsEnabled = true;
		}
	}

	private void btnHuy_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}

