using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
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
	public partial class UpdateNhanVienForm : Form
	{
		private int _nhanvienId;
		private int _thongtinId;
		public UpdateNhanVienForm(int nhanvienId)
		{
			InitializeComponent();
			_nhanvienId = nhanvienId;
		}
		private readonly ChucVuClient _chucvu = new ChucVuClient();
		private readonly NhanVienClient _nhanvien = new NhanVienClient();
		private readonly PhongChucNangClient _phongchucnang = new PhongChucNangClient();
		private readonly ThongTinClient _thongtin = new ThongTinClient();
		private string _avatarFileName = string.Empty;
		private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private async void UpdateNhanVienForm_Load(object sender, EventArgs e)
		{
			await LoadDataAsync();

		}
		private async Task LoadDataAsync()
		{
			try
			{
				FormDragHelper.EnableDrag(pnlHeader, this);
				LoadCombobox();

				var data = await _nhanvien.GetNhanVienByIdAsync(_nhanvienId);
				_avatarFileName = data.Avatar ?? string.Empty;
				_thongtinId = data.ThongTinID;
				if (data != null)
				{
					txtHoTen.Text = data.HoTen;
					txtEmail.Text = data.EmailLienHe;
					txtSDT.Text = data.SDT;
					txtDiaChi.Text = data.DiaChi;

					dtpNgaySinh.Value = data.NgaySinh ?? DateTime.Now;
					dtpNgayVaoLam.Value = data.NgayVaoLam ?? DateTime.Now;

					txtBangCap.Text = data.BangCap;
					txtKinhNghiem.Text = data.KinhNghiem;

					lbMaNV_value.Text = "NV" + data.NhanVienID.ToString("D3");
					lbMaTTCN_value.Text = "HS" + data.ThongTinID.ToString("D3");

					rdoNam.Checked = data.GioiTinh == "Nam";
					rdoNu.Checked = data.GioiTinh == "Nữ";

					cbbChucVu.SelectedValue = data.ChucVuID;
					cbbPhong.SelectedValue = data.PhongChucNangID;

					if (!string.IsNullOrWhiteSpace(data.Avatar))
					{
						string avatarPath = Path.Combine(
							Application.StartupPath,
							"Resources",
							"Images",
							"Avatars",
							data.Avatar);

						if (File.Exists(avatarPath))
						{
							// Giải phóng ảnh cũ trước
							if (picAvt.Image != null)
							{
								picAvt.Image.Dispose();
								picAvt.Image = null;
							}

							using (FileStream fs = new FileStream(
								avatarPath,
								FileMode.Open,
								FileAccess.Read))
							{
								picAvt.Image = Image.FromStream(fs);
							}

							picAvt.SizeMode = PictureBoxSizeMode.Zoom;
						}
					}
				}
				SetReadOnlyMode(true);
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Lỗi tải dữ liệu: " + ex.Message);
			}
			finally
			{
				this.Enabled = true;
				Cursor = Cursors.Default;
			}
		}

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

		private void btnDoiAvt_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.Title = "Chọn ảnh đại diện";
				ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					string avatarFolder = Path.Combine(
						Application.StartupPath,
						"Resources",
						"Images",
						"Avatars");

					// Tạo folder nếu chưa có
					if (!Directory.Exists(avatarFolder))
						Directory.CreateDirectory(avatarFolder);

					// Tạo tên file unique
					string newFileName = Guid.NewGuid() + Path.GetExtension(ofd.FileName);
					string newFullPath = Path.Combine(avatarFolder, newFileName);

					// Copy file
					File.Copy(ofd.FileName, newFullPath, true);

					// Lưu tên file vào biến
					_avatarFileName = newFileName;

					// Giải phóng ảnh cũ nếu có
					if (picAvt.Image != null)
					{
						picAvt.Image.Dispose();
						picAvt.Image = null;
					}

					// Load ảnh không lock file
					using (var fs = new FileStream(newFullPath, FileMode.Open, FileAccess.Read))
					{
						picAvt.Image = Image.FromStream(fs);
					}

					picAvt.SizeMode = PictureBoxSizeMode.Zoom;
				}
			}
		}
		private void SetReadOnlyMode(bool isReadOnly)
		{
			// TextBox
			txtHoTen.ReadOnly = isReadOnly;
			txtEmail.ReadOnly = isReadOnly;
			txtSDT.ReadOnly = isReadOnly;
			txtDiaChi.ReadOnly = isReadOnly;
			txtBangCap.ReadOnly = isReadOnly;
			txtKinhNghiem.ReadOnly = isReadOnly;

			// DateTimePicker
			dtpNgaySinh.Enabled = !isReadOnly;
			dtpNgayVaoLam.Enabled = !isReadOnly;

			// RadioButton
			rdoNam.Enabled = !isReadOnly;
			rdoNu.Enabled = !isReadOnly;

			// ComboBox
			cbbChucVu.Enabled = !isReadOnly;
			cbbPhong.Enabled = !isReadOnly;

			// Button đổi avatar
			btnDoiAvt.Enabled = !isReadOnly;

			// Nếu có nút Save
			btnLuu.Enabled = !isReadOnly;
		}
		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			SetReadOnlyMode(false);
		}

		private async void btnLuu_Click(object sender, EventArgs e)
		{
			try
			{
				btnLuu.Enabled = false;

				if (string.IsNullOrWhiteSpace(txtHoTen.Text))
				{
					MessageHelper.ShowMessage("Vui lòng nhập họ tên");
					return;
				}

				if ((int)cbbChucVu.SelectedValue == 0)
				{
					MessageHelper.ShowMessage("Vui lòng chọn chức vụ");
					return;
				}

				if ((int)cbbPhong.SelectedValue == 0)
				{
					MessageHelper.ShowMessage("Vui lòng chọn phòng");
					return;
				}

				var thongTinDto = new DTOs.CapNhatThongTinCaNhanDTO
				{
					HoTen = txtHoTen.Text.Trim(),
					NgaySinh = dtpNgaySinh.Value,
					GioiTinh = rdoNam.Checked ? "Nam" : "Nữ",
					SDT = txtSDT.Text.Trim(),
					EmailLienHe = txtEmail.Text.Trim(),
					DiaChi = txtDiaChi.Text.Trim(),
					Avatar = _avatarFileName // nếu không đổi vẫn giữ giá trị cũ
				};

				var resultThongTin =
					await _thongtin.UpdateThongTinAsync(_thongtinId, thongTinDto);

				if (!resultThongTin)
				{
					MessageHelper.ShowMessage("Cập nhật thông tin cá nhân thất bại");
					return;
				}

				var nhanVienDto = new DTOs.CapNhatNhanVienDTO
				{
					ChucVuID = (int)cbbChucVu.SelectedValue,
					PhongChucNangID = (int)cbbPhong.SelectedValue,
					NgayVaoLam = dtpNgayVaoLam.Value,
					BangCap = txtBangCap.Text.Trim(),
					KinhNghiem = txtKinhNghiem.Text.Trim()
				};

				var resultNhanVien =
					await _nhanvien.UpdateNhanVienAsync(_nhanvienId, nhanVienDto);

				if (!resultNhanVien)
				{
					MessageHelper.ShowMessage("Cập nhật thông tin nhân viên thất bại");
					return;
				}

				MessageHelper.ShowMessage("Cập nhật thành công");

				SetReadOnlyMode(true);
				DialogResult = DialogResult.OK;
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Lỗi: " + ex.Message);
			}
			finally
			{
				btnLuu.Enabled = true;
			}
		}
	}
}
