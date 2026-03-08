using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.CaKham
{
	public partial class CaKhamForm : Form
	{
		private readonly CaKhamClient _client= new CaKhamClient();
		private readonly PhienKhamClient _pkClient = new PhienKhamClient();
		private int _currentPage = 1;
		private int _pageSize = 15;
		private int _totalPages = 1;

		public CaKhamForm()
		{
			InitializeComponent();

			dtpNgayKham.Value = DateTime.Now;
			dtpNgayKham.Format = DateTimePickerFormat.Custom;
			dtpNgayKham.CustomFormat = "MM/dd/yyyy";
			txtSizePage.Text = _pageSize.ToString();
		}

		private async void CaKhamForm_Load(object sender, EventArgs e)
		{
			SetupDgv();
			LoadCombobox();
			ApplyGridByTrangThai();
			await LoadDataAsync();
		}
		private void LoadCombobox()
		{
			cbbLoaiCaKham.DataSource = new List<string>(LookupData.LoaiCaKham);
			cbbTrangThai.DataSource = new List<string>(LookupData.TrangThaiCaKham);
		}
		private async Task LoadDataAsync()
		{
			try
			{
				if (cbbTrangThai.SelectedItem == null ||
					cbbLoaiCaKham.SelectedItem == null)
					return;

				DateTime ngayKham = dtpNgayKham.Value.Date;

				string trangThai = cbbTrangThai.SelectedItem.ToString();
				string loaiCaKham = cbbLoaiCaKham.SelectedItem.ToString();

				var result = await _client.GetPagedAsync(ngayKham,trangThai,loaiCaKham,_currentPage,_pageSize);

				if (result == null || result.Items == null)
				{
					dgvContent.DataSource = null;
					return;
				}

				dgvContent.DataSource = result.Items;

				_totalPages = result.TotalCount > 0
					? (int)Math.Ceiling((double)result.TotalCount / _pageSize)
					: 1;

				lbcurrentPage.Text = $"Trang {_currentPage} / {_totalPages}";
				UpdateButtonState();
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Không thể tải dữ liệu: " + ex.Message);
			}
		}
		private void UpdateButtonState()
		{
			btnFirst.Enabled = _currentPage > 1;
			btnPrevious.Enabled = _currentPage > 1;
			btnNext.Enabled = _currentPage < _totalPages;
			btnEnd.Enabled = _currentPage < _totalPages;
		}

		private void SetupDgv()
		{
			SetupDatagridview.ApplyGridStyle(dgvContent);

			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "CaKhamID",
				DataPropertyName = "CaKhamID",
				HeaderText = "ID",
				FillWeight = 10
			});

			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TenKhungGio",
				DataPropertyName = "TenKhungGio",
				HeaderText = "Khung giờ",
				FillWeight = 20
			});

			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TenPhong",
				DataPropertyName = "TenPhong",
				HeaderText = "Phòng",
				FillWeight = 25
			});

			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "HoTen",
				DataPropertyName = "HoTen",
				HeaderText = "Bệnh nhân",
				FillWeight = 30
			});

			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "LyDoKham",
				DataPropertyName = "LyDoKham",
				HeaderText = "Lý do khám",
				FillWeight = 35
			});

			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TrangThai",
				DataPropertyName = "TrangThai",
				HeaderText = "Trạng thái",
				FillWeight = 20
			});
		}
		private bool IsPastTimeSlot(string tenKhungGio, DateTime ngayKham)
		{
			try
			{
				var parts = tenKhungGio.Split('-');

				if (parts.Length == 0)
					return false;

				string startTimeStr = parts[0].Trim();

				if (!TimeSpan.TryParse(startTimeStr, out TimeSpan startTime))
					return false;

				DateTime startDateTime = ngayKham.Date.Add(startTime);

				return startDateTime < DateTime.Now;
			}
			catch
			{
				return false;
			}
		}
		private void ApplyGridByTrangThai()
		{
			string trangThai = cbbTrangThai.SelectedItem?.ToString();

			if (string.IsNullOrEmpty(trangThai))
				return;

			// reset
			dgvContent.Columns["HoTen"].Visible = true;
			dgvContent.Columns["LyDoKham"].Visible = true;

			RemoveButton("btnDatLich");
			RemoveButton("btnXacNhan");
			RemoveButton("btnHuy");
			RemoveButton("btnBatDau");
			RemoveButton("btnKhongDen");

			if (trangThai == "Trống")
			{
				dgvContent.Columns["HoTen"].Visible = false;
				dgvContent.Columns["LyDoKham"].Visible = false;
				dgvContent.Columns.Add(SetupDatagridview.CreateButtonColumn("btnDatLich", Resources.register, "Đặt lịch cho bệnh nhân"));
			}

			else if (trangThai == "Đã đặt")
			{
				dgvContent.Columns.Add(SetupDatagridview.CreateButtonColumn("btnXacNhan", Resources.check_mark, "Xác nhận ca khám"));
				dgvContent.Columns.Add(SetupDatagridview.CreateButtonColumn("btnHuy", Resources.letter_x, "Hủy ca khám"));
			}

			else if (trangThai == "Đã xác nhận")
			{
				dgvContent.Columns.Add(SetupDatagridview.CreateButtonColumn("btnBatDau", Resources.play, "Tạo phiên khám"));
				dgvContent.Columns.Add(SetupDatagridview.CreateButtonColumn("btnKhongDen", Resources.letter_x, "Bệnh nhân không đến"));
			}

			else if (trangThai == "Hoàn thành")
			{
				// chỉ xem
			}

			else if (trangThai == "Đã hủy" || trangThai == "Không đến")
			{
				// readonly
			}
		}
		private void RemoveButton(string name)
		{
			if (dgvContent.Columns.Contains(name))
				dgvContent.Columns.Remove(name);
		}
		private async void btnRefesh_Click(object sender, EventArgs e)
		{
			_currentPage = 1;
			await LoadDataAsync();
		}

		private async void txtSizePage_TextChanged(object sender, EventArgs e)
		{
			if (int.TryParse(txtSizePage.Text, out int newSize) && newSize > 0)
			{
				_pageSize = newSize;
				_currentPage = 1;
				await LoadDataAsync();
			}
		}

		private async void btnFirst_Click(object sender, EventArgs e)
		{
			_currentPage = 1;
			await LoadDataAsync();
		}

		private async void btnPrevious_Click(object sender, EventArgs e)
		{
			if (_currentPage > 1)
			{
				_currentPage--;
				await LoadDataAsync();
			}
		}

		private async void btnNext_Click(object sender, EventArgs e)
		{
			if (_currentPage < _totalPages)
			{
				_currentPage++;
				await LoadDataAsync();
			}
		}

		private async void btnEnd_Click(object sender, EventArgs e)
		{
			_currentPage = _totalPages;
			await LoadDataAsync();
		}

		private async void cbbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
		{
			_currentPage = 1;
			ApplyGridByTrangThai();
			await LoadDataAsync();
		}

		private async void cbbLoaiCaKham_SelectedIndexChanged(object sender, EventArgs e)
		{
			_currentPage = 1;
			await LoadDataAsync();
		}

		private async void dtpNgayKham_ValueChanged(object sender, EventArgs e)
		{
			_currentPage = 1;
			await LoadDataAsync();
		}

		private void dgvContent_Paint(object sender, PaintEventArgs e)
		{
			if (dgvContent.Rows.Count == 0)
			{
				string text = "Không có dữ liệu";

				using (Font font = new Font("Segoe UI", 12, FontStyle.Italic))
				{
					SizeF size = e.Graphics.MeasureString(text, font);
					e.Graphics.DrawString(
						text,
						font,
						Brushes.Gray,
						(dgvContent.Width - size.Width) / 2,
						(dgvContent.Height - size.Height) / 2
					);
				}
			}
		}

		private async void dgvContent_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			int id = Convert.ToInt32(dgvContent.Rows[e.RowIndex].Cells["CaKhamID"].Value);
			string column = dgvContent.Columns[e.ColumnIndex].Name;
			string tenKhung = dgvContent.Rows[e.RowIndex].Cells["TenKhungGio"].Value?.ToString();

			if (column == "btnDatLich")
			{
				if (IsPastTimeSlot(tenKhung, dtpNgayKham.Value))
				{
					MessageHelper.ShowMessage("Khung giờ này đã qua, không thể đặt lịch.");
					return;
				}

				using (var frm = new RegisterCaKhamForm(id))
				{
					if (frm.ShowDialog() == DialogResult.OK)
					{
						await LoadDataAsync();
					}
				}
			}
			if (column == "btnXacNhan")
			{
				await _client.CapNhatTrangThaiAsync(id, "Đã xác nhận");
				await LoadDataAsync();
			}

			if (column == "btnHuy")
			{
				await _client.CapNhatTrangThaiAsync(id, "Đã hủy");
				await LoadDataAsync();
			}
			if (column == "btnKhongDen")
			{
				await _client.CapNhatTrangThaiAsync(id, "Không đến");
				await LoadDataAsync();
			}
			if (column == "btnBatDau")
			{
				var result = await _pkClient.TaoMoiAsync(id);
				MessageHelper.ShowMessage("Tạo phiên khám thành công! Mã: PK" + result);
				await _client.CapNhatTrangThaiAsync(id, "Đang khám");
				await LoadDataAsync();
			}
		}

		private async void btnAdd_Click(object sender, EventArgs e)
		{
			using (var frm = new AddCaKhamForm())
			{
				if (frm.ShowDialog() == DialogResult.OK)
				{
					await LoadDataAsync();
				}
			}
		}
	}
}