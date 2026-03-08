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
	public partial class UpdateChucVuForm : Form
	{
		private readonly int _chucVuId;
		private readonly ChucVuClient _client = new ChucVuClient();
		public UpdateChucVuForm(int chucVuId)
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);
			_chucVuId = chucVuId;
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private async void UpdateChucVuForm_Load(object sender, EventArgs e)
		{
			var data = await _client.GetByIdAsync(_chucVuId);

			if (data != null)
			{
				txtChucVuID.Text = data.ChucVuID.ToString();
				txtTenChucVu.Text = data.TenChucVu;
				txtMoTa.Text = data.MoTa;
				txtNgayTao.Text = data.NgayTao.ToString("dd/MM/yyyy");
			}
			else
			{
				MessageHelper.ShowMessage("Không tìm thấy chức vụ.");
				this.DialogResult = DialogResult.Cancel;
				this.Close();
			}
		}

		private void btnHuy_Click(object sender, EventArgs e)
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
			var success = await _client.UpdateChucVuAsync(_chucVuId, dto);
			btnLuu.Enabled = true;
			if (success)
			{
				MessageHelper.ShowMessage("Cập nhật chức vụ thành công!");
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

		private void txtNgayTao_Enter(object sender, EventArgs e)
		{
			this.ActiveControl = null;	
		}

		private void txtChucVuID_Enter(object sender, EventArgs e)
		{
			this.ActiveControl = null;
		}
	}
}
