using System.Windows;
using System.Windows.Input;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.CaNhan;


public partial class DoiMatKhau : Window
{
	public DoiMatKhau()
	{
		InitializeComponent();
		_id = Session.UserId;
		txtEmail.Text = Session.Email;
	}
	private readonly int _id;
	private readonly TaiKhoanClient _taikhoan = new();
	private void Header_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (e.LeftButton == MouseButtonState.Pressed)
		{
			DragMove();
		}
	}
	private bool _showPassword = false;
	private bool _showNewPassword = false;

	private void btnTogglePassword_Click(object sender, RoutedEventArgs e)
	{
		_showPassword = !_showPassword;
		if (_showPassword)
		{
			txtPasswordVisible.Text = txtPassword.Password;
			txtPassword.Visibility = Visibility.Collapsed;
			txtPasswordVisible.Visibility = Visibility.Visible;
			iconTogglePassword.Kind = MaterialDesignThemes.Wpf.PackIconKind.EyeOff;
			txtPasswordVisible.Focus();
			txtPasswordVisible.CaretIndex = txtPasswordVisible.Text.Length;
		}
		else
		{
			txtPassword.Password = txtPasswordVisible.Text;
			txtPasswordVisible.Visibility = Visibility.Collapsed;
			txtPassword.Visibility = Visibility.Visible;
			iconTogglePassword.Kind = MaterialDesignThemes.Wpf.PackIconKind.Eye;
			txtPassword.Focus();
		}
	}

	private void btnToggleNewPassword_Click(object sender, RoutedEventArgs e)
	{
		_showNewPassword = !_showNewPassword;
		if (_showNewPassword)
		{
			txtNewPasswordVisible.Text = txtNewPassword.Password;
			txtNewPassword.Visibility = Visibility.Collapsed;
			txtNewPasswordVisible.Visibility = Visibility.Visible;
			iconToggleNewPassword.Kind = MaterialDesignThemes.Wpf.PackIconKind.EyeOff;
			txtNewPasswordVisible.Focus();
			txtNewPasswordVisible.CaretIndex = txtNewPasswordVisible.Text.Length;
		}
		else
		{
			txtNewPassword.Password = txtNewPasswordVisible.Text;
			txtNewPasswordVisible.Visibility = Visibility.Collapsed;
			txtNewPassword.Visibility = Visibility.Visible;
			iconToggleNewPassword.Kind = MaterialDesignThemes.Wpf.PackIconKind.Eye;
			txtNewPassword.Focus();
		}
	}
	private async void btnSave_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtPassword.Password))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập mật khẩu");
			txtPassword.Focus();
			return;
		}
		if (string.IsNullOrWhiteSpace(txtNewPassword.Password))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập mật khẩu mới");
			txtNewPassword.Focus();
			return;
		}
		if (txtNewPassword.Password.Length < 6)
		{
			await MessageHelper.ShowMessage("Mật khẩu mới phải có ít nhất 6 ký tự");
			txtNewPassword.Focus();
			return;
		}
		var req = new ChangePasswordRequestDTO
		{
			MatKhauCu = _showPassword ? txtPasswordVisible.Text : txtPassword.Password,
			MatKhauMoi = _showNewPassword ? txtNewPasswordVisible.Text : txtNewPassword.Password
		};

		try
		{
			btnSave.IsEnabled = false;
			btnClose.IsEnabled = false;

			var result = await _taikhoan.ChangePassword(_id,req);

			if (result.Success)
			{
				SnackbarHelper.ShowSuccess(result.Message);
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
			btnSave.IsEnabled = true;
			btnClose.IsEnabled = true;
		}
	}

	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}
