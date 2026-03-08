using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Windows.Forms;

namespace GUI
{
	public partial class FrmDangNhap : Form
	{
		private readonly AuthClient _authClient = new AuthClient();

		public LoginResponseDTO LoginResult { get; private set; }
		public FrmDangNhap()
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);
		}


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

			try
			{
				string email = txtUsername.Text.Trim();
				string password = txtMatKhau.Text;

				if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
				{
					lblErrorRpt.Text = "Vui lòng nhập đầy đủ thông tin!";
					return;
				}

				// Clear session trước khi login mới
				Session.Clear();

				var loginData = new LoginDTO
				{
					Email = email,
					MatKhau = password
				};

				var loginResult = await _authClient.LoginAsync(loginData);

				if (loginResult == null || string.IsNullOrEmpty(loginResult.AccessToken))
				{
					lblErrorRpt.Text = "Sai tài khoản hoặc mật khẩu!";
					return;
				}

				// Lưu session
				Session.Token = loginResult.AccessToken;
				Session.UserId = loginResult.Id;
				Session.NhanVienId = loginResult.NhanVienId;

				LoginResult = loginResult;

				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception ex)
			{
				lblErrorRpt.Text = "Không thể kết nối server!";
				MessageHelper.ShowMessage(ex.Message);
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
