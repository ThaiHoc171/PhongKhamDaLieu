using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.ChucVu
{
	public partial class AddChucVuForm : Form
	{
		public AddChucVuForm()
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);
		}
		private readonly ChucVuClient _client = new ChucVuClient();
		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private async void btnLuu_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtTenChucVu.Text))
			{
				MessageHelper.ShowMessage("Tên chức vụ không được để trống.");
				return;
			}

			var dto = new ChucVuRequestDTO
			{
				TenChucVu = txtTenChucVu.Text.Trim(),
				MoTa = txtMoTa.Text.Trim()
			};
			btnLuu.Enabled = false;
			var success = await _client.CreateChucVuAsync(dto);
			btnLuu.Enabled = true;

			if (success)
			{
				MessageHelper.ShowMessage("Thêm chức vụ thành công!");
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			else
			{
				MessageHelper.ShowMessage("Thêm chức vụ thất bại!");
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void btnHuy_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
