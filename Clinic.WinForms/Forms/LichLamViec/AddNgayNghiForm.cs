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

namespace Clinic.WinForms.Forms.LichLamViec
{
	public partial class AddNgayNghiForm : Form
	{
		public AddNgayNghiForm()
		{
			InitializeComponent();
			_cvClient = new ChucVuClient();
			_nvClient = new NhanVienClient();
			_ngayNghiClient = new NgayNghiClient();

			dtpNgay.Value = DateTime.Now;
		}
		private readonly ChucVuClient _cvClient;
		private readonly NhanVienClient _nvClient;
		private readonly NgayNghiClient _ngayNghiClient;

		private async Task LoadCombobox()
		{
			var listChucVu = await _cvClient.GetComboboxAsync();

			if (listChucVu == null) return;

			listChucVu.Insert(0, new DTOs.ComboboxResult
			{
				Id = 0,
				Name = "Chọn chức vụ"
			});

			cbbChucVu.DataSource = null;
			cbbChucVu.DisplayMember = "Name";
			cbbChucVu.ValueMember = "Id";
			cbbChucVu.DataSource = listChucVu;
			cbbChucVu.SelectedIndex = 0;

			cbbNhanVien.DataSource = null;
			cbbNhanVien.DisplayMember = "Name";
			cbbNhanVien.ValueMember = "Id";
			cbbNhanVien.DataSource = new List<DTOs.ComboboxResult>
			{
				new DTOs.ComboboxResult { Id = 0, Name = "Chọn nhân viên" }
			};

		}

		private async void cbbChucVu_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbbChucVu.SelectedValue == null) return;

			int chucVuId;
			if (!int.TryParse(cbbChucVu.SelectedValue.ToString(), out chucVuId))
				return;

			if (chucVuId == 0)
			{
				cbbNhanVien.DataSource = new List<ComboboxResult>
				{
					new ComboboxResult { Id = 0, Name = "Chọn nhân viên" }
				};
				return;
			}

			var listNhanVien = await _nvClient.GetComboboxAsync(chucVuId);

			if (listNhanVien == null) return;

			listNhanVien.Insert(0, new DTOs.ComboboxResult
			{
				Id = 0,
				Name = "Chọn nhân viên"
			});

			cbbNhanVien.DataSource = null;
			cbbNhanVien.DisplayMember = "Name";
			cbbNhanVien.ValueMember = "Id";
			cbbNhanVien.DataSource = listNhanVien;
			cbbNhanVien.SelectedIndex = 0;
		}

		private async void AddNgayNghiForm_Load(object sender, EventArgs e)
		{
			await LoadCombobox();
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private async void btnLuu_Click(object sender, EventArgs e)
		{
			try
			{
				if (cbbNhanVien.SelectedValue == null)
				{
					MessageHelper.ShowMessage("Vui lòng chọn nhân viên");
					return;
				}

				if (!int.TryParse(cbbNhanVien.SelectedValue.ToString(), out int nhanVienId))
				{
					MessageHelper.ShowMessage("Nhân viên không hợp lệ");
					return;
				}

				if (string.IsNullOrWhiteSpace(txtLyDo.Text))
				{
					MessageHelper.ShowMessage("Vui lòng nhập lý do nghỉ");
					return;
				}

				var dto = new NgayNghiRequestDTO
				{
					NhanVienID = nhanVienId,
					Ngay = dtpNgay.Value.Date,
					LyDo = txtLyDo.Text.Trim()
				};

				var result = await _ngayNghiClient.CreateNgayNghiAsync(dto);

				if (result)
				{
					MessageHelper.ShowMessage("Thêm ngày nghỉ thành công");

					txtLyDo.Clear();
					dtpNgay.Value = DateTime.Now;
				}
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage($"Lỗi: {ex.Message}");
			}
		}

		private void lbNgay_Click(object sender, EventArgs e)
		{

		}
	}
}
