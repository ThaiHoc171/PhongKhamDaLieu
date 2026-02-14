using Clinic.WinForms.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Clinic.WinForms.Forms
{
	public partial class MainFrm : Form
	{
		private LoginResponseDTO _user;
		private void ToggleGroup(Panel target)
		{
			bool isOpening = !target.Visible;

			pnlBenhNhanContent.Visible = false;
			pnlCoSoVatChatContent.Visible = false;
			pnlDanhMucContent.Visible = false;
			pnlKhamBenhContent.Visible = false;
			pnlHeThongContent.Visible = false;
			pnlDieuTriContent.Visible = false;
			pnlNhanSuContent.Visible = false;

			target.Visible = isOpening;
		}

		public MainFrm(LoginResponseDTO user)
		{
			InitializeComponent();
			_user = user;
		}

		private void MainFrm_Load(object sender, EventArgs e)
		{
			
			lbRole.Text = _user.VaiTro;
		}


		private void btnBenhNhanHeader_Click(object sender, EventArgs e)
		{
			ToggleGroup(pnlBenhNhanContent);
		}

		private void pnlContent_Paint(object sender, PaintEventArgs e)
		{

		}

		private void btnCoSoVatChatHeader_Click(object sender, EventArgs e)
		{
			ToggleGroup(pnlCoSoVatChatContent);
		}

		private void btnDanhMucHeader_Click(object sender, EventArgs e)
		{
			ToggleGroup(pnlDanhMucContent);
		}

		private void btnKhamBenhHeader_Click(object sender, EventArgs e)
		{
			ToggleGroup(pnlKhamBenhContent);
		}

		private void btnHeThongHeader_Click(object sender, EventArgs e)
		{
			ToggleGroup(pnlHeThongContent);
		}

		private void btnDieuTriHeader_Click(object sender, EventArgs e)
		{
			ToggleGroup(pnlDieuTriContent);
		}

		private void btnNhanSuHeader_Click(object sender, EventArgs e)
		{
			ToggleGroup(pnlNhanSuContent);
		}
	}
}
