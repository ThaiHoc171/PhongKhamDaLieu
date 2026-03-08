using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using Clinic.WinForms.Forms.BenhNhan;
using Clinic.WinForms.Forms.CaKham;
using Clinic.WinForms.Forms.LichLamViec;
using Clinic.WinForms.Forms.LoaiBenh;
using Clinic.WinForms.Forms.NhanVien;
using Clinic.WinForms.Forms.PhienKham;
using Clinic.WinForms.Forms.PhongChucNang;
using Clinic.WinForms.Forms.ThietBi;
using Clinic.WinForms.Forms.Thuoc;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms
{
	public partial class FormMain : Form
	{
		public FormMain(LoginResponseDTO user)
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);
			_user = user;
		}
		private Form _currentChildForm;
		private LoginResponseDTO _user;
		private bool _isFullscreen = false;
		private string _headerName = "";
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
			pnlPhienKhamContent.Visible = false;
			target.Visible = isOpening;
		}
		private void OpenChildForm(Form childForm)
		{
			if (_currentChildForm != null)
			{
				_currentChildForm.Close();
			}
			_currentChildForm = childForm;
			childForm.TopLevel = false;
			childForm.FormBorderStyle = FormBorderStyle.None;
			childForm.Dock = DockStyle.Fill;
			childForm.AutoScaleMode = AutoScaleMode.None;
			pnlPage.Controls.Add(childForm);
			pnlPage.Tag = childForm;
			childForm.BringToFront();
			childForm.Show();
		}
		public void OpenPage(string header, Form form)
		{
			_headerName = header;
			txtHeaderPage.Text = _headerName;
			OpenChildForm(form);
		}
		//public MainFrm()
		//{
		//	InitializeComponent();
		//	_user = new LoginResponseDTO
		//	{
		//		HoTen = "Admin",
		//		ChucVu = "Admin",
		//		VaiTro = "Admin"
		//	};
		//}

		private void MainFrm_Load(object sender, EventArgs e)
		{

			lbRole.Text = _user.ChucVu ?? _user.VaiTro;
			lbName.Text = "Xin chào, " + _user.HoTen;
			pnlContent.Visible = true;
		}


		private void btnBenhNhanHeader_Click(object sender, EventArgs e)
		{
			ToggleGroup(pnlBenhNhanContent);
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
		private void btnPhienKhamHeader_Click(object sender, EventArgs e)
		{
			ToggleGroup(pnlPhienKhamContent);
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

		private void btnAvatar_Click(object sender, EventArgs e)
		{
			var location = btnAvatar.PointToScreen(
				new Point(btnAvatar.Width - cmsAvatar.Width, btnAvatar.Height)
			);

			cmsAvatar.Show(location);
		}

		private void tsmiLogout_Click(object sender, EventArgs e)
		{
			Session.Clear();
			this.Close();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void tsmiSetting_Click(object sender, EventArgs e)
		{
			if (!_isFullscreen)
			{
				this.FormBorderStyle = FormBorderStyle.None;
				this.WindowState = FormWindowState.Maximized;
				_isFullscreen = true;
			}
			else
			{
				this.FormBorderStyle = FormBorderStyle.None;
				this.WindowState = FormWindowState.Normal;

				this.Size = new Size(1366, 768);
				this.StartPosition = FormStartPosition.CenterScreen;

				_isFullscreen = false;
			}
		}

		private void btnChucVuSidebar_Click(object sender, EventArgs e)
		{
			OpenPage("Chức vụ", new ChucVuForm());

		}

		private void btnNhanVienSidebar_Click(object sender, EventArgs e)
		{
			OpenPage("Nhân viên", new NhanVienForm());
		}

		private void btnLichLamViecSidebar_Click(object sender, EventArgs e)
		{
			OpenPage("Lịch làm việc", new LichLamViecForm());
		}

		private void btnAdminLichSidebar_Click(object sender, EventArgs e)
		{
			OpenPage("Quản lí lịch làm việc", new AdminLichForm());
		}

		private void btnPhongChucNangSidebar_Click(object sender, EventArgs e)
		{
			OpenPage("Phòng chức năng", new PhongChucNangForm());
		}

		private void btnThietBiSidebar_Click(object sender, EventArgs e)
		{
			OpenPage("Thiết bị", new ThietBiForm());
		}

		private void btnCanLamSangSidebar_Click(object sender, EventArgs e)
		{
			OpenPage("Cận lâm sàng", new CanLamSangForm());
		}

		private void btnLoaiBenhSideBar_Click(object sender, EventArgs e)
		{
			OpenPage("Loại Bệnh", new LoaiBenhForm());
		}

		private void btnThuocSidebar_Click(object sender, EventArgs e)
		{
			OpenPage("Thuốc", new ThuocForm());
		}

		private void btnDanhSachSidebar_Click(object sender, EventArgs e)
		{
			OpenPage("Danh sách bệnh nhân", new BenhNhanForm());
		}

		private void btnCaKhamSidebar_Click(object sender, EventArgs e)
		{
			OpenPage("Ca Khám", new CaKhamForm());
		}

		private void btnPhienKhamSidebar_Click(object sender, EventArgs e)
		{
			OpenPage("Phiên khám", new PhienKhamForm());
		}

		private void btnAdminPhienKhamSidebar_Click(object sender, EventArgs e)
		{
			OpenPage("Phiên khám", new AdminViewPhienKhamForm());
		}

		private void btnKham_Click(object sender, EventArgs e)
		{
			OpenPage("Danh sách bệnh nhân chờ khám", new ChoKhamForm());
		}
	}
}
