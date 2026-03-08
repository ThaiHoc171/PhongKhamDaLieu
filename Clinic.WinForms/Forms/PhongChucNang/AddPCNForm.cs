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

namespace Clinic.WinForms.Forms.PhongChucNang
{
	public partial class AddPCNForm : Form
	{
		public AddPCNForm()
		{
			InitializeComponent();
			_pcnClient = new PhongChucNangClient();
		}
		private readonly PhongChucNangClient _pcnClient;

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private async void btnLuu_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtTenPhong.Text))
			{
				MessageHelper.ShowMessage("Tên phòng chức năng không được để trống.");
				return;
			}
			if (string.IsNullOrEmpty(txtLoaiPhong.Text))
			{
				MessageHelper.ShowMessage("Loại phòng chức năng không được để trống.");
				return;
			}
			if (string.IsNullOrEmpty(txtMoTa.Text))
			{
				MessageHelper.ShowMessage("Mô tả không được để trống.");
				return;
			}
			var dto = new PhongChucNangRequestDTO
			{
				TenPhong = txtTenPhong.Text.Trim(),
				LoaiPhong = txtLoaiPhong.Text.Trim(),
				MoTa = txtMoTa.Text.Trim()
			};
			var result = await _pcnClient.CreatePhongAsync(dto);
			if (result)
				MessageHelper.ShowMessage("Thêm phòng chức năng thành công.");
			else
				MessageHelper.ShowMessage("Thêm phòng chức năng thất bại.");
			Close();
		}

		private void txtLoaiPhong_TextChanged(object sender, EventArgs e)
		{

		}

		private void lbMoTa_Click(object sender, EventArgs e)
		{

		}

		private void lbLoaiPhong_Click(object sender, EventArgs e)
		{

		}

		private void lbTenPhong_Click(object sender, EventArgs e)
		{

		}

		private void pnlHeader_Paint(object sender, PaintEventArgs e)
		{

		}

		private void txtMoTa_TextChanged(object sender, EventArgs e)
		{

		}

		private void lbHeader_Click(object sender, EventArgs e)
		{

		}

		private void pnlNhanVien_Paint(object sender, PaintEventArgs e)
		{

		}

		private void txtTenPhong_TextChanged(object sender, EventArgs e)
		{

		}

		private void pnlContent_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
