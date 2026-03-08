using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace Clinic.WinForms.Forms.BenhNhan
{
	public partial class AddBenhNhanForm : Form
	{
		private readonly BenhNhanClient _client = new BenhNhanClient();
		private string _avatarTempPath = string.Empty;
		private string _avatarFileName = string.Empty;
		public AddBenhNhanForm()
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);
			rdoNam.Checked = true;
		}
		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}
		private void btnThemAvt_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.Title = "Chọn ảnh đại diện";
				ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					_avatarTempPath = ofd.FileName;
					using (var stream = new FileStream(_avatarTempPath, FileMode.Open, FileAccess.Read))
					{
						picAvt.Image = Image.FromStream(stream);
					}
					picAvt.SizeMode = PictureBoxSizeMode.Zoom;
				}
			}
		}
		private bool IsValid()
		{
			if (string.IsNullOrWhiteSpace(txtHoTen.Text))
			{
				MessageHelper.ShowMessage("Họ tên không được để trống.");
				return false;
			}
			if (string.IsNullOrWhiteSpace(txtSDT.Text))
			{
				MessageHelper.ShowMessage("Số điện thoại không được để trống.");
				return false;
			}
			if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
			{
				MessageHelper.ShowMessage("Địa chỉ không được để trống.");
				return false;
			}
			if (string.IsNullOrWhiteSpace(_avatarTempPath))
			{
				MessageHelper.ShowMessage("Vui lòng chọn ảnh đại diện.");
				return false;
			}
			return true;
		}
		private async void btnLuu_Click(object sender, EventArgs e)
		{
			try
			{
				if (!IsValid())
					return;
				string avatarFolder = Path.Combine(Application.StartupPath, "Resources", "Images", "BenhNhan");
				if (!Directory.Exists(avatarFolder))
					Directory.CreateDirectory(avatarFolder);
				string ext = Path.GetExtension(_avatarTempPath);
				_avatarFileName = $"bn{txtSDT.Text.Trim()}_{DateTime.Now:yyyyMMddHHmmss}{ext}";
				string newFullPath = Path.Combine(avatarFolder, _avatarFileName);
				File.Copy(_avatarTempPath, newFullPath, true);
				var dto = new BenhNhanRequestDTO
				{
					HoTen = txtHoTen.Text.Trim(),
					NgaySinh = dtpNgaySinh.Value,
					GioiTinh = rdoNam.Checked ? "Nam" : "Nữ",
					SDT = txtSDT.Text.Trim(),
					EmailLienHe = txtEmail.Text.Trim(),
					DiaChi = txtDiaChi.Text.Trim(),
					Avatar = _avatarFileName,
					GhiChu = txtGhiChu.Text.Trim()
				};
				btnLuu.Enabled = false;
				var result = await _client.CreateAsync(dto);
				btnLuu.Enabled = true;
				if (result)
				{
					MessageHelper.ShowMessage("Thêm bệnh nhân thành công!");
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
				else
				{
					MessageHelper.ShowMessage("Thêm bệnh nhân thất bại!");
				}
			}
			catch (Exception ex)
			{
				btnLuu.Enabled = true;
				MessageHelper.ShowMessage("Lỗi: " + ex.Message);
			}
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