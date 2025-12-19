using DTO;
using GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UI
{
    public partial class FrmMain : Form
    {
		private TaiKhoanDTO _taiKhoan;
        private bool isLogin()
        {
            if (_taiKhoan == null)
            {
                FrmDangNhap frm = new FrmDangNhap();
                frm.Show();
                this.Close();
                return false;
			}
                return true;
		}
        private void Role()
        {
            if (_taiKhoan.Role == "employee")
            {

            }
            else if (_taiKhoan.Role == "Admin")
            {

            }
        }
		public FrmMain(TaiKhoanDTO tk)
        {
            InitializeComponent();
            _taiKhoan = tk;

		}

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private Form _currentForm;

        private void OpenChildForm(Form childForm)
        {
            if (_currentForm != null)
            {
                _currentForm.Close();
            }

            _currentForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(childForm);

            childForm.Show();
        }

        private void btnLogo_Click(object sender, EventArgs e)
        {

        }

		private void FrmMain_Load(object sender, EventArgs e)
		{
            if(!isLogin())
                return; 
            Role();
		}

		private void btnTaiKhoan_Click(object sender, EventArgs e)
		{
			//FrmTaiKhoan FrmTaiKhoan = new FrmTaiKhoan();
			//OpenChildForm(FrmTaiKhoan);
		}
	}
}
