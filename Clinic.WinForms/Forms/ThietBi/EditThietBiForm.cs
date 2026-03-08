using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.ThietBi
{
	public partial class EditThietBiForm : Form
	{
		public EditThietBiForm(int id)
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);
			LoadDataAsync(id);
		}
		private readonly ThietBiClient _client = new ThietBiClient();
		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}
		private async void LoadDataAsync(int id)
		{
			try
			{
				var data = await _client.GetByIdAsync(id);
				if(data != null)
				{
					txtMa.Text = id.ToString();
					txtTen.Text = data.TenTB;
					txtLoai.Text = data.LoaiTB;
				}
			} catch(Exception ex)
			{
				MessageHelper.ShowMessage("Lỗi: "+ ex.Message);
			}
		}
		private async void btnLuu_Click(object sender, EventArgs e)
		{

			if (string.IsNullOrWhiteSpace(txtTen.Text))
			{
				MessageHelper.ShowMessage("Tên thiết bị không được để trống!");
				return;
			}
			if (string.IsNullOrWhiteSpace(txtLoai.Text))
			{
				MessageHelper.ShowMessage("Loại thiết bị không được để trống!");
				return;
			}
			if (!int.TryParse(txtMa.Text, out int id))
			{
				MessageHelper.ShowMessage("Mã thiết bị không hợp lệ!");
				return;
			}
			var dto = new ThietBiRequestDTO
			{
				TenTB = txtTen.Text,
				LoaiTB = txtLoai.Text
			};
			var result = await _client.UpdateAsync(id, dto);
			if (result)
				MessageHelper.ShowMessage("Cập nhật thiết bị thành công.");
			else
				MessageHelper.ShowMessage("Cập nhật thiết bị thất bại.");
			Close();
		}
	}
}
