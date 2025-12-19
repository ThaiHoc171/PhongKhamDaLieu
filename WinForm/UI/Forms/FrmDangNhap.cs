using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI;
using DTO;

namespace GUI
{
	public partial class FrmDangNhap : Form
	{
		public FrmDangNhap()
		{
			InitializeComponent();
		}

		[DllImport("user32.dll")]
		private static extern bool ReleaseCapture();

		[DllImport("user32.dll")]
		private static extern int SendMessage(
			IntPtr hWnd, int Msg, int wParam, int lParam);

		private const int WM_NCLBUTTONDOWN = 0xA1;
		private const int HTCAPTION = 0x2;




		private void btnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
			}
		}

		private void chkHienMK_CheckedChanged(object sender, EventArgs e)
		{
			txtMatKhau.UseSystemPasswordChar = !chkHienMK.Checked;
		}

		private async void btnSubmit_Click(object sender, EventArgs e)
		{
			lblErrorRpt.Text = "";
			string email = txtUsername.Text.Trim();
			string password = txtMatKhau.Text;
			string errorRpt;
			try
			{
				using (HttpClient client = new HttpClient())
				{
					client.BaseAddress = new Uri("https://localhost:7034/"); // URL API
					var response = await client.PostAsJsonAsync("api/TaiKhoan/DangNhap",
						new { Email = email, PasswordHash = password });

					if (response.IsSuccessStatusCode)
					{
						var contentType = response.Content.Headers.ContentType.MediaType;
						if (contentType == "application/json")
						{
							var tk = await response.Content.ReadFromJsonAsync<TaiKhoanDTO>();
							if (tk != null)
							{
								FrmMain frmMain = new FrmMain(tk);
								frmMain.Show();
								this.Hide();
								lblErrorRpt.Text = "Đăng nhập thành công.";
							}
							else
							{
								lblErrorRpt.Text = "Đăng nhập thất bại!";
							}
						}
						else
						{
							lblErrorRpt.Text = "Server trả về không phải JSON!";
						}
					}
					else
					{
						// Lấy thông báo lỗi từ API
						var errorObj = await response.Content.ReadFromJsonAsync<ErrorResponse>();
						lblErrorRpt.Text = errorObj?.Message ?? "Đăng nhập thất bại!";
					}
				}
			}
			catch (Exception ex)
			{
				lblErrorRpt.Text = "Không thể kết nối server!"+ ex;
			}
		}

		private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				btnSubmit.PerformClick();
			}
		}

		// Class để nhận lỗi từ API
		public class ErrorResponse
		{
			public string Message { get; set; }
		}
	}
}
