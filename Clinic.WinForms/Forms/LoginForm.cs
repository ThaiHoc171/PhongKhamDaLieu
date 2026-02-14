using Clinic.WinForms;
using Clinic.WinForms.DTOs;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
	public partial class FrmDangNhap : Form
	{
		private static readonly HttpClient client = new HttpClient
		{
			BaseAddress = new Uri("https://clinicjwt-api-bperhwd0dne7c9c0.southeastasia-01.azurewebsites.net/")
		};
		public LoginResponseDTO LoginResult { get; private set; }
		public FrmDangNhap()
		{
			InitializeComponent();
		}

		#region Drag Form

		[DllImport("user32.dll")]
		private static extern bool ReleaseCapture();

		[DllImport("user32.dll")]
		private static extern int SendMessage(
			IntPtr hWnd, int Msg, int wParam, int lParam);

		private const int WM_NCLBUTTONDOWN = 0xA1;
		private const int HTCAPTION = 0x2;

		private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
			}
		}

		#endregion

		private void btnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void chkHienMK_CheckedChanged(object sender, EventArgs e)
		{
			txtMatKhau.UseSystemPasswordChar = !chkHienMK.Checked;
		}

		private async void btnSubmit_Click(object sender, EventArgs e)
		{
			lblErrorRpt.Text = "";
			btnSubmit.Enabled = false;

			string email = txtUsername.Text.Trim();
			string password = txtMatKhau.Text;

			if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
			{
				lblErrorRpt.Text = "Vui lòng nhập đầy đủ thông tin!";
				btnSubmit.Enabled = true;
				return;
			}

			try
			{
				var loginData = new
				{
					Email = email,
					MatKhau = password
				};

				var json = JsonConvert.SerializeObject(loginData);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				var response = await client.PostAsync("api/TaiKhoan/dangnhap", content);

				if (!response.IsSuccessStatusCode)
				{
					lblErrorRpt.Text = "Sai tài khoản hoặc mật khẩu!";
					btnSubmit.Enabled = true;
					return;
				}

				var responseString = await response.Content.ReadAsStringAsync();

				var loginResult = JsonConvert.DeserializeObject<LoginResponseDTO>(responseString);

				if (loginResult == null || string.IsNullOrEmpty(loginResult.AccessToken))
				{
					lblErrorRpt.Text = "Không nhận được token từ server!";
					btnSubmit.Enabled = true;
					return;
				}

				// Gắn token cho các request sau
				client.DefaultRequestHeaders.Authorization =
					new System.Net.Http.Headers.AuthenticationHeaderValue(
						"Bearer",
						loginResult.AccessToken
					);
				LoginResult = loginResult;
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception ex)
			{
				lblErrorRpt.Text = "Không thể kết nối server!";
				MessageBox.Show(ex.Message);
			}
			finally
			{
				btnSubmit.Enabled = true;
			}
		}

		private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				btnSubmit.PerformClick();
		}

		private void pnlBackGround_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
