using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.BenhNhan
{
	public partial class UpdateBenhNhanForm : Form
	{
		private readonly int _id;
		private int _thongTinId;

		private readonly BenhNhanClient _client = new BenhNhanClient();
		private readonly ThongTinClient _thongtin = new ThongTinClient();

		private string _avatarFileName = string.Empty;
		private string _avatarTempPath = string.Empty;

		public UpdateBenhNhanForm(int id)
		{
			InitializeComponent();
			_id = id;

			FormDragHelper.EnableDrag(pnlHeader, this);
		}

		private async void UpdateBenhNhanForm_Load(object sender, EventArgs e)
		{
			await LoadData();
		}

		private async Task LoadData()
		{
			try
			{
				var data = await _client.GetByIdAsync(_id);

				if (data == null) return;

				_thongTinId = data.ThongTinID;
				_avatarFileName = data.Avatar ?? "";

				lbMa.Text = "BN" + data.BenhNhanID.ToString("D3");
				lbMaThongtin_value.Text = "HS" + data.ThongTinID.ToString("D3");

				txtHoTen.Text = data.HoTen;
				txtSDT.Text = data.SDT;
				txtEmail.Text = data.EmailLienHe;
				txtDiaChi.Text = data.DiaChi;
				txtGhiChu.Text = data.GhiChu;

				dtpNgaySinh.Value = data.NgaySinh ?? DateTime.Now;

				rdoNam.Checked = data.GioiTinh == "Nam";
				rdoNu.Checked = data.GioiTinh == "Nữ";

				if (!string.IsNullOrWhiteSpace(data.Avatar))
				{
					string avatarPath = Path.Combine(
						Application.StartupPath,
						"Resources",
						"Images",
						"BenhNhan",
						data.Avatar);

					if (File.Exists(avatarPath))
					{
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

				SetReadOnlyMode(true);
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Lỗi tải dữ liệu: " + ex.Message);
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
					_avatarTempPath = ofd.FileName;

					using (var stream = new FileStream(_avatarTempPath, FileMode.Open, FileAccess.Read))
					{
						picAvt.Image = Image.FromStream(stream);
					}

					picAvt.SizeMode = PictureBoxSizeMode.Zoom;
				}
			}
		}

		private void SetReadOnlyMode(bool isReadOnly)
		{
			txtHoTen.ReadOnly = isReadOnly;
			txtSDT.ReadOnly = isReadOnly;
			txtEmail.ReadOnly = isReadOnly;
			txtDiaChi.ReadOnly = isReadOnly;
			txtGhiChu.ReadOnly = isReadOnly;

			dtpNgaySinh.Enabled = !isReadOnly;

			rdoNam.Enabled = !isReadOnly;
			rdoNu.Enabled = !isReadOnly;

			btnDoiAvt.Enabled = !isReadOnly;

			btnLuu.Enabled = !isReadOnly;
		}

		private bool IsValid()
		{
			if (string.IsNullOrWhiteSpace(txtHoTen.Text))
			{
				MessageHelper.ShowMessage("Họ tên không được để trống");
				return false;
			}

			if (string.IsNullOrWhiteSpace(txtSDT.Text))
			{
				MessageHelper.ShowMessage("SĐT không được để trống");
				return false;
			}

			return true;
		}

		private async void btnLuu_Click(object sender, EventArgs e)
		{
			try
			{
				if (!IsValid()) return;

				btnLuu.Enabled = false;

				// nếu đổi avatar
				if (!string.IsNullOrWhiteSpace(_avatarTempPath))
				{
					string folder = Path.Combine(
						Application.StartupPath,
						"Resources",
						"Images",
						"BenhNhan");

					if (!Directory.Exists(folder))
						Directory.CreateDirectory(folder);

					string ext = Path.GetExtension(_avatarTempPath);

					_avatarFileName =
						$"bn{txtSDT.Text}_{DateTime.Now:yyyyMMddHHmmss}{ext}";

					string newPath = Path.Combine(folder, _avatarFileName);

					File.Copy(_avatarTempPath, newPath, true);
				}

				// update thông tin cá nhân
				var thongTinDto = new CapNhatThongTinCaNhanDTO
				{
					HoTen = txtHoTen.Text.Trim(),
					NgaySinh = dtpNgaySinh.Value,
					GioiTinh = rdoNam.Checked ? "Nam" : "Nữ",
					SDT = txtSDT.Text.Trim(),
					EmailLienHe = txtEmail.Text.Trim(),
					DiaChi = txtDiaChi.Text.Trim(),
					Avatar = _avatarFileName
				};

				var resultThongTin =
					await _thongtin.UpdateThongTinAsync(_thongTinId, thongTinDto);

				if (!resultThongTin)
				{
					MessageHelper.ShowMessage("Cập nhật thông tin thất bại");
					return;
				}

				// update ghi chú bệnh nhân
				var benhNhanDto = new CapNhatBenhNhanDTO
				{
					GhiChu = txtGhiChu.Text.Trim()
				};

				var resultBenhNhan =
					await _client.UpdateAsync(_id, benhNhanDto);

				if (!resultBenhNhan)
				{
					MessageHelper.ShowMessage("Cập nhật bệnh nhân thất bại");
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

		private void btnEdit_Click(object sender, EventArgs e)
		{
			SetReadOnlyMode(false);
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
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