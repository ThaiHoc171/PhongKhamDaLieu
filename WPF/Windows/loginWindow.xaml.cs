using System.Windows;
using System.Windows.Input;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows
{
	public partial class loginWindow : Window
	{
		private readonly Auth _authClient = new Auth();

		public LoginResponseDTO LoginResult { get; private set; }

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

		private async void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			btnLogin.IsEnabled = false;
			checkShowPassword.IsChecked = false;
			checkShowPassword.IsEnabled = false;
			try
			{
				string email = txtEmail.Text.Trim();
				string password = checkShowPassword.IsChecked == true
					? txtPasswordVisible.Text
					: txtPassword.Password;

				if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
				{
					await MessageHelper.ShowMessage("Vui lòng nhập đầy đủ thông tin!");
					return;
				}

				Session.Clear();

				var loginData = new LoginRequestDTO
				{
					Email = email,
					MatKhau = password
				};

				var response = await _authClient.Login(loginData);

				// 1. Không nhận được response
				if (response == null)
				{
					await MessageHelper.ShowMessage("Không thể kết nối server!");
					return;
				}

				// 2. API trả lỗi
				if (!response.Success)
				{
					await MessageHelper.ShowMessage("Lỗi: " + (response.Message ?? "Đăng nhập thất bại!"));
					return;
				}

				var result = response.Data;

				// 3. Không có token => sai tài khoản mật khẩu
				if (result == null || string.IsNullOrEmpty(result.AccessToken))
				{
					await MessageHelper.ShowMessage("Tài khoản hoặc mật khẩu không đúng!");
					return;
				}

				// 4. Lưu session
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

				DialogResult = true;
				Close();
			}
			catch (Exception ex)
			{
				await MessageHelper.ShowMessage("Không thể kết nối server!\n" + ex.Message);
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
	}
}