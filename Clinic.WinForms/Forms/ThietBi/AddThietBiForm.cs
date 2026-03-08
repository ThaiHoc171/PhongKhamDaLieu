using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.ThietBi
{
	public partial class AddThietBiForm : Form
	{
		public AddThietBiForm()
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);
			_client = new ThietBiClient();
		}
		private readonly ThietBiClient _client;
		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
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
			var dto = new ThietBiRequestDTO
			{
				TenTB = txtTen.Text,
				LoaiTB = txtLoai.Text
			};
			var result = await _client.CreateAsync(dto);
			if (result)
				MessageHelper.ShowMessage("Thêm thiết bị thành công.");
			else
				MessageHelper.ShowMessage("Thêm thiết bị thất bại.");
			Close();
		}
	}
}
