using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.CanLamSang
{
	public partial class AddClsForm : Form
	{
		public AddClsForm()
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);
			_client = new CanLamSangClient();
		}
		private readonly CanLamSangClient _client;

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private async void btnLuu_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtTen.Text))
			{
				MessageHelper.ShowMessage("Tên cận lâm sàng không được để trống!");
				return;
			}
			if (string.IsNullOrWhiteSpace(txtMoTa.Text))
			{
				MessageHelper.ShowMessage("Mô tả không được để trống!");
				return;
			}
			if (string.IsNullOrWhiteSpace(txtLoai.Text))
			{
				MessageHelper.ShowMessage("Loại xét nghiệm không được để trống!");
				return;
			}

			var dto = new CanLamSangRequestDTO
			{
				TenCLS = txtTen.Text,
				MoTa = txtMoTa.Text,
				LoaiXetNghiem = txtLoai.Text
			};
			var result = await _client.CreateAsync(dto);
			if (result)
				MessageHelper.ShowMessage("Thêm cận lâm sàng thành công.");
			else
				MessageHelper.ShowMessage("Thêm cận lâm sàng thất bại.");
			Close();
		}
	}
}
