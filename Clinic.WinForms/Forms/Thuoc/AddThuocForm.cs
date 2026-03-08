using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.Thuoc
{
	public partial class AddThuocForm : Form
	{
		private readonly ThuocClient _client;

		public AddThuocForm()
		{
			InitializeComponent();
			_client = new ThuocClient();
			FormDragHelper.EnableDrag(pnlHeader, this);
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private bool IsValid()
		{
			if (string.IsNullOrWhiteSpace(txtTen.Text))
				return false;

			if (string.IsNullOrWhiteSpace(txtHoatChat.Text))
				return false;

			return true;
		}

		private async void btnLuu_Click(object sender, EventArgs e)
		{
			try
			{
				if (!IsValid())
				{
					MessageHelper.ShowMessage("Vui lòng nhập đầy đủ thông tin!");
					return;
				}

				var dto = new ThuocRequestDTO
				{
					TenThuoc = txtTen.Text.Trim(),
					HoatChat = txtHoatChat.Text.Trim()
				};

				btnLuu.Enabled = false;

				var result = await _client.CreateAsync(dto);

				btnLuu.Enabled = true;

				if (result)
				{
					MessageHelper.ShowMessage("Thêm thuốc thành công!");
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
				else
				{
					MessageHelper.ShowMessage("Thêm thuốc thất bại!");
				}
			}
			catch (Exception ex)
			{
				btnLuu.Enabled = true;
				MessageHelper.ShowMessage("Lỗi: " + ex.Message);
			}
		}
	}
}