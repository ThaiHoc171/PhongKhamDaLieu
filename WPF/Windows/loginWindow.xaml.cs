using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HoanMyClinic.Windows
{
	public partial class loginWindow : Window
	{
		private readonly Auth _authClient = new Auth();

		public loginWindow()
		{
			InitializeComponent();
		}

		private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
				DragMove();
		}

		private void BtnExit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void checkShowPassword_Checked(object sender, RoutedEventArgs e)
		{
			txtPasswordVisible.Text = txtPassword.Password;
			txtPassword.Visibility = Visibility.Collapsed;
			txtPasswordVisible.Visibility = Visibility.Visible;
		}

		private void checkShowPassword_Unchecked(object sender, RoutedEventArgs e)
		{
			txtPassword.Password = txtPasswordVisible.Text;
			txtPassword.Visibility = Visibility.Visible;
			txtPasswordVisible.Visibility = Visibility.Collapsed;
		}
		private void ClearErrors()
		{
			txtEmailError.Visibility = Visibility.Collapsed;
			txtEmailError.Text = "";
			txtPasswordError.Visibility = Visibility.Collapsed;
			txtPasswordError.Text = "";
		}

		private void ShowFieldError(TextBlock errorBlock, string message)
		{
			errorBlock.Text = message;
			errorBlock.Visibility = Visibility.Visible;
		}

		private bool ValidateForm(string email, string password)
		{
			ClearErrors();
			bool isValid = true;

			if (string.IsNullOrWhiteSpace(email))
			{
				ShowFieldError(txtEmailError, "Vui lòng nhập email!");
				isValid = false;
			}
			else if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
			{
				ShowFieldError(txtEmailError, "Email không đúng định dạng!");
				isValid = false;
			}

			if (string.IsNullOrWhiteSpace(password))
			{
				ShowFieldError(txtPasswordError, "Vui lòng nhập mật khẩu!");
				isValid = false;
			}
			else if (password.Length < 6)
			{
				ShowFieldError(txtPasswordError, "Mật khẩu phải có ít nhất 6 ký tự!");
				isValid = false;
			}

			return isValid;
		}

		private async void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			string email = txtEmail.Text.Trim();
			string password = checkShowPassword.IsChecked == true
				? txtPasswordVisible.Text
				: txtPassword.Password;

			if (!ValidateForm(email, password))
				return;

			btnLogin.IsEnabled = false;
			checkShowPassword.IsChecked = false;
			checkShowPassword.IsEnabled = false;

			try
			{
				Session.Clear();

				var loginData = new LoginRequestDTO
				{
					Email = email,
					MatKhau = password
				};

				var response = await _authClient.Login(loginData);

				if (response == null)
				{
					SnackbarHelper.ShowError("Không thể kết nối server!");
					return;
				}

				if (!response.Success || response.Data == null || string.IsNullOrEmpty(response.Data.AccessToken))
				{
					ShowFieldError(txtPasswordError, "Tài khoản hoặc mật khẩu không đúng!");
					txtPassword.Clear();
					txtPasswordVisible.Text = "";
					txtPassword.Focus();
					return;
				}

				var result = response.Data;
				Session.UserId = result.Id;
				Session.Email = result.Email;
				Session.Token = result.AccessToken;
				Session.ChucVu = result.ChucVu;
				Session.NhanVienId = result.NhanVienId;
				Session.RefreshToken = result.RefreshToken;
				Session.HoTen = new NameHelper
				{
					Id = result.HoTen?.Id ?? 0,
					Name = result.HoTen?.Name ?? ""
				};
				Session.VaiTro = result.VaiTro;
				Session.Permissions = result.Quyen ?? new List<string>();

				DialogResult = true;
				Close();
			}
			catch (Exception ex)
			{
				SnackbarHelper.ShowError("Có lỗi xảy ra: " + ex.Message);
			}
			finally
			{
				btnLogin.IsEnabled = true;
				checkShowPassword.IsEnabled = true;
			}
		}

		private void Password_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				btnLogin_Click(sender, e);
		}

		private async void btnAdminSupport_Click(object sender, RoutedEventArgs e)
		{
			await MessageHelper.ShowMessage("Vui lòng liên hệ admin tại \n Admin@clinic.com");
		}
	}
}