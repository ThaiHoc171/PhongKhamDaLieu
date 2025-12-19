using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GUI
{
	public partial class FrmTaiKhoan : Form
	{
		public FrmTaiKhoan()
		{
			InitializeComponent();
		}
		//private void LoadData()
		//{
		//	DataTable dt = TaiKhoanBUS.LayDanhSachTaiKhoan();
		//	dgvDSTaiKhoan.DataSource = dt;

		//	dgvDSTaiKhoan.Columns["TaiKhoanID"].HeaderText = "Mã Tài Khoản";
		//	dgvDSTaiKhoan.Columns["Email"].HeaderText = "Email";
		//	dgvDSTaiKhoan.Columns["Role"].HeaderText = "Quyền";
		//	dgvDSTaiKhoan.Columns["Status"].HeaderText = "Trạng Thái";
		//	dgvDSTaiKhoan.Columns["NgayTao"].HeaderText = "Ngày Tạo";
		//	dgvDSTaiKhoan.Columns["NgayCapNhat"].Visible = false;

		//	dgvDSTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		//	dgvDSTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		//	dgvDSTaiKhoan.ReadOnly = true;
		//}
		//private void FrmTaiKhoan_Load(object sender, EventArgs e)
		//{
		//	LoadData();
		//	LoadComboBox();
		//}
		//private void LoadComboBox()
		//{
		//	cboRole.DataSource = new List<object> { "" }
		//		.Concat(Enum.GetValues(typeof(RoleAccount)).Cast<object>())
		//		.ToList();

		//	cboStatus.DataSource = new List<object> { "" }
		//		.Concat(Enum.GetValues(typeof(StatusAccount)).Cast<object>())
		//		.ToList();

		//	cboRole.SelectedIndex = -1;
		//	cboStatus.SelectedIndex = -1;
		//}

		//private void pnlAction_Paint(object sender, PaintEventArgs e)
		//{

		//}

		//private void dgvDSTaiKhoan_SelectionChanged(object sender, EventArgs e)
		//{
		//	if (dgvDSTaiKhoan.SelectedRows.Count == 0) return;
		//	if (dgvDSTaiKhoan.CurrentRow == null) return;
		//	DataGridViewRow row = dgvDSTaiKhoan.SelectedRows[0];

		//	txtEmail.Text = row.Cells["Email"].Value.ToString();
		//	txtNgayCapNhat.Text = row.Cells["NgayCapNhat"].Value.ToString();
		//	txtNgayTao.Text = row.Cells["NgayTao"].Value.ToString();
		//	var rolevalue = row.Cells["Role"]?.Value.ToString();
		//	if (!string.IsNullOrEmpty(rolevalue))
		//	{
		//		cboRole.SelectedItem = Enum.Parse(typeof(RoleAccount), rolevalue);
		//	}
		//	else
		//	{
		//		cboRole.SelectedIndex = -1;
		//	}
		//	var statusvalue = row.Cells["Status"]?.Value.ToString();
		//	if (!string.IsNullOrEmpty(statusvalue))
		//	{
		//		cboStatus.SelectedItem = Enum.Parse(typeof(StatusAccount), statusvalue);
		//	}
		//	else
		//	{
		//		cboStatus.SelectedIndex = -1;
		//	}
		//}

		//private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	if(cboRole.SelectedIndex == 0)
		//	{
		//		cboRole.SelectedIndex = 1;
		//	}
		//}

		//private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	if(cboStatus.SelectedIndex == 0)
		//	{
		//		cboStatus.SelectedIndex = 1;
		//	}
		//}
	}
}
