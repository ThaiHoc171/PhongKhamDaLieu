using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.NhanVien
{
	public partial class AddNhanVienForm : Form
	{
		public AddNhanVienForm()
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);
			rdoNam.Checked = true;
		}
		private readonly ChucVuClient _chucvu = new ChucVuClient();
		private readonly NhanVienClient _nhanvien = new NhanVienClient();
		private readonly PhongChucNangClient _phongchucnang = new PhongChucNangClient();
		private string _avatarFileName = string.Empty;
		
		private async void LoadCombobox()
		{
			var chucvuResult = await _chucvu.GetComboboxAsync();

			if (chucvuResult != null)
			{
				chucvuResult.Insert(0, new DTOs.ComboboxResult { Id = 0, Name = "Chọn chức vụ" });
				cbbChucVu.DataSource = null;
				cbbChucVu.DisplayMember = "Name";
				cbbChucVu.ValueMember = "Id"; 
				cbbChucVu.DataSource = chucvuResult;

				cbbChucVu.SelectedIndex = 0;
			}
			var phongResult = await _phongchucnang.GetComboboxAsync();

			if (phongResult != null)
			{
				phongResult.Insert(0, new DTOs.ComboboxResult { Id = 0, Name = "Chọn phòng" });
				cbbPhong.DataSource = null;
				cbbPhong.DisplayMember = "Name";
				cbbPhong.ValueMember = "Id";
				cbbPhong.DataSource = phongResult;

				cbbPhong.SelectedIndex = 0;
			}
		}
		private void lbHeader_Click(object sender, EventArgs e)
		{

		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void AddNhanVienForm_Load(object sender, EventArgs e)
		{
			LoadCombobox();
		}

		private void btnThemAvt_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.Title = "Chọn ảnh đại diện";
				ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					string avatarFolder = Path.Combine(Application.StartupPath, "Resources", "Images", "Avatars");

					// Tạo folder nếu chưa tồn tại
					if (!Directory.Exists(avatarFolder))
						Directory.CreateDirectory(avatarFolder);

					// Tạo tên file mới tránh trùng
					string newFileName = Guid.NewGuid().ToString()
										 + Path.GetExtension(ofd.FileName);

					string newFullPath = Path.Combine(avatarFolder, newFileName);

					File.Copy(ofd.FileName, newFullPath, true);

					_avatarFileName = newFileName;

					// Load ảnh lên PictureBox (không lock file)
					using (var stream = new FileStream(newFullPath, FileMode.Open, FileAccess.Read))
					{
						picAvt.Image = Image.FromStream(stream);
					}

					picAvt.SizeMode = PictureBoxSizeMode.Zoom;
				}
			}
		}


		private async void btnLuu_Click(object sender, EventArgs e)
		{
			if(string.IsNullOrWhiteSpace(txtHoTen.Text))
			{
				MessageHelper.ShowMessage("Họ tên không được để trống.");
				return;
			}
			if(string.IsNullOrWhiteSpace(txtSDT.Text))
			{
				MessageHelper.ShowMessage("Số điện thoại không được để trống.");
				return;
			}
			if(cbbChucVu.SelectedIndex == 0)
			{
				MessageHelper.ShowMessage("Vui lòng chọn chức vụ.");
				return;
			}
			if(cbbPhong.SelectedIndex == 0)
			{
				MessageHelper.ShowMessage("Vui lòng chọn phòng.");
				return;
			}
			if(string.IsNullOrWhiteSpace(_avatarFileName))
			{
				MessageHelper.ShowMessage("Vui lòng chọn ảnh đại diện.");
				return;
			}
			if(string.IsNullOrWhiteSpace(txtDiaChi.Text))
			{
				MessageHelper.ShowMessage("Địa chỉ không được để trống.");
				return;
			}
			if(string.IsNullOrWhiteSpace(txtEmail.Text))
			{
				MessageHelper.ShowMessage("Email không được để trống.");
				return;
			}
			if (string.IsNullOrWhiteSpace(txtKinhNghiem.Text)){
				MessageHelper.ShowMessage("Kinh nghiệm không được để trống.");
				return;
			}
			if(string.IsNullOrWhiteSpace(txtBangCap.Text))
			{
				MessageHelper.ShowMessage("Bằng cấp không được để trống.");
				return;
			}
			var dto = new NhanVienRequestDTO
			{
				ThongTin = new ThongTinRequestDTO
				{
					HoTen = txtHoTen.Text.Trim(),
					NgaySinh = dtpNgaySinh.Value,
					GioiTinh = rdoNam.Checked ? "Nam" : "Nữ",
					SDT = txtSDT.Text.Trim(),
					EmailLienHe = txtEmail.Text.Trim(),
					DiaChi = txtDiaChi.Text.Trim(),
					Avatar = _avatarFileName
				},

				ChucVuID = (int)cbbChucVu.SelectedValue,
				PhongChucNangID = (int)cbbPhong.SelectedValue,
				NgayVaoLam = dtpNgayVaoLam.Value,

				BangCap = txtBangCap.Text.Trim(),
				KinhNghiem = txtKinhNghiem.Text.Trim()
			};
			btnLuu.Enabled = false;
			var result = await _nhanvien.CreateNhanVienAsync(dto);
			btnLuu .Enabled = true;


			if (result)
				MessageHelper.ShowMessage("Thêm nhân viên thành công.");
			else
				MessageHelper.ShowMessage("Thêm nhân viên thất bại.");
		}

		private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}
	}
}
