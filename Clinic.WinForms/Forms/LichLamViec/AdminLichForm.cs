using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using Clinic.WinForms.Forms.ChucVu;
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
	public partial class AdminLichForm : Form
	{
		public AdminLichForm()
		{
			InitializeComponent();
			_chucVuClient = new ChucVuClient();
			_nhanVienClient = new NhanVienClient();
			_lichClient = new LichLamViecClient();
			_ngayNghiClient = new NgayNghiClient();
		}
		private readonly ChucVuClient _chucVuClient;
		private readonly NhanVienClient _nhanVienClient;
		private List<LichLamViecCreateItemDTO> _temp = new List<LichLamViecCreateItemDTO>();
		private readonly LichLamViecClient _lichClient;
		private readonly NgayNghiClient _ngayNghiClient;

		private async Task LoadCombobox()
		{
			var listChucVu = await _chucVuClient.GetComboboxAsync();

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
			cbbNhanVien.DataSource = new List<ComboboxResult>
			{
				new DTOs.ComboboxResult { Id = 0, Name = "Chọn nhân viên" }
			};

			var listCa = new List<dynamic>
			{
				new { Id = 0, Name = "Chọn ca làm việc" },
				new { Id = 1, Name = "Ca sáng" },
				new { Id = 2, Name = "Ca chiều" }
			};

			cbbCaLamViec.DataSource = null;
			cbbCaLamViec.DisplayMember = "Name";
			cbbCaLamViec.ValueMember = "Id";
			cbbCaLamViec.DataSource = listCa;
			cbbCaLamViec.SelectedIndex = 0;
		}

		private void lbNhanVien_Click(object sender, EventArgs e)
		{

		}

		private async void AdminLichForm_Load(object sender, EventArgs e)
		{
			await LoadCombobox();
			SetupGridLichTam();

			LoadMonthAndYear();   
			SetupGridNgayNghi();      
			await LoaddgvNgayNghi();
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

			var listNhanVien = await _nhanVienClient.GetComboboxAsync(chucVuId);

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
		private void SetupGridLichTam()
		{
			dgvLichTam.AutoGenerateColumns = false;
			dgvLichTam.Columns.Clear();

			// ===== Style =====
			dgvLichTam.BackgroundColor = Color.White;
			dgvLichTam.BorderStyle = BorderStyle.None;
			dgvLichTam.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvLichTam.GridColor = Color.FromArgb(230, 230, 230);

			dgvLichTam.EnableHeadersVisualStyles = false;
			dgvLichTam.ColumnHeadersHeight = 40;
			dgvLichTam.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
			dgvLichTam.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

			dgvLichTam.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
			dgvLichTam.DefaultCellStyle.SelectionForeColor = Color.Black;

			dgvLichTam.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


			dgvLichTam.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "NhanVienID",
				HeaderText = "Nhân viên ID",
				DataPropertyName = "NhanVienID",
				FillWeight = 15
			});

			dgvLichTam.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "ChucVuID",
				HeaderText = "Chức vụ ID",
				DataPropertyName = "ChucVuID",
				FillWeight = 15
			});

			dgvLichTam.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "Ngay",
				HeaderText = "Ngày",
				DataPropertyName = "Ngay",
				FillWeight = 20
			});

			dgvLichTam.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "Ca",
				HeaderText = "Ca làm việc",
				DataPropertyName = "Ca",
				FillWeight = 20
			});

			dgvLichTam.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "GhiChu",
				HeaderText = "Ghi chú",
				DataPropertyName = "GhiChu",
				FillWeight = 30
			});
			var btnXoa = new DataGridViewImageColumn
			{
				Name = "btnXoa",
				HeaderText = "",
				Image = ResizeImage(Properties.Resources.letter_x, 25, 25),
				Width = 40,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};

			dgvLichTam.Columns.Add(btnXoa);
		}
		private Image ResizeImage(Image img, int width, int height)
		{
			return new Bitmap(img, new Size(width, height));
		}
		private void LoadGridTam()
		{
			dgvLichTam.DataSource = null;
			dgvLichTam.DataSource = _temp.Select((x, index) => new
				{
					STT = index + 1,
					x.NhanVienID,
					x.ChucVuID,
					Ngay = x.Ngay.ToString("dd/MM/yyyy"),
					Ca = x.CaLamViec == 1 ? "Ca sáng"
						 : x.CaLamViec == 2 ? "Ca chiều"
						 : "Không xác định",
					x.GhiChu
				})
				.ToList();
		}
		private void btnAdd_Click(object sender, EventArgs e)
		{
			if ((int)cbbChucVu.SelectedValue == 0)
			{
				MessageHelper.ShowMessage("Vui lòng chọn chức vụ");
				return;
			}

			if ((int)cbbNhanVien.SelectedValue == 0)
			{	
				MessageHelper.ShowMessage("Vui lòng chọn nhân viên");
				return;
			}

			if ((int)cbbCaLamViec.SelectedValue == 0)
			{
				MessageHelper.ShowMessage("Vui lòng chọn ca làm việc");
				return;
			}

			var item = new LichLamViecCreateItemDTO
			{
				NhanVienID = (int)cbbNhanVien.SelectedValue,
				ChucVuID = (int)cbbChucVu.SelectedValue,
				Ngay = dtpNgayTaoLich.Value.Date,
				CaLamViec = (int)cbbCaLamViec.SelectedValue,
				GhiChu = txtGhiChu.Text
			};

			bool isDuplicate = _temp.Any(x =>
				x.NhanVienID == item.NhanVienID &&
				x.Ngay == item.Ngay &&
				x.CaLamViec == item.CaLamViec);

			if (isDuplicate)
			{
				MessageBox.Show("Nhân viên đã có lịch trong ca này rồi");
				return;
			}

			_temp.Add(item);
			LoadGridTam();
		}

		private void dgvLichTam_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			if (dgvLichTam.Columns[e.ColumnIndex].Name == "btnXoa")
			{
				_temp.RemoveAt(e.RowIndex);
				LoadGridTam();
			}
		}

		private async void btnLuu_Click(object sender, EventArgs e)
		{
			if (_temp == null || !_temp.Any())
			{
				MessageHelper.ShowMessage("Chưa có lịch nào để lưu");
				return;
			}

			var confirm = MessageHelper.Confirm("Bạn có chắc muốn lưu lịch làm việc?");

			if (confirm != DialogResult.Yes)
				return;

			try
			{
				var request = new LichLamViecRequestDTO
				{
					Thang = dtpNgayTaoLich.Value.Month,
					Nam = dtpNgayTaoLich.Value.Year,
					LichLamViecs = _temp
				};

				await _lichClient.CreateLichLamViecAsync(request);

				MessageHelper.ShowMessage("Lưu lịch thành công!");

				_temp.Clear();
				LoadGridTam();
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Lỗi khi lưu: " + ex.Message);
			}
		}

		//pageNgayNghi
		private void LoadMonthAndYear()
		{
			var months = Enumerable.Range(1, 12)
				.Select(m => new { Id = m, Name = $"Tháng {m}" })
				.ToList();

			cbbMonth.DataSource = months;
			cbbMonth.DisplayMember = "Name";
			cbbMonth.ValueMember = "Id";
			cbbMonth.SelectedValue = DateTime.Now.Month;

			var years = Enumerable.Range(DateTime.Now.Year - 5, 10)
				.Select(y => new { Id = y, Name = $"Năm {y}" })
				.ToList();

			cbbYear.DataSource = years;
			cbbYear.DisplayMember = "Name";
			cbbYear.ValueMember = "Id";
			cbbYear.SelectedValue = DateTime.Now.Year;
		}
		private async Task LoaddgvNgayNghi()
		{
			if (cbbMonth.SelectedValue == null || cbbYear.SelectedValue == null)
				return;

			if (!int.TryParse(cbbMonth.SelectedValue.ToString(), out int thang))
				return;

			if (!int.TryParse(cbbYear.SelectedValue.ToString(), out int nam))
				return;

			var result = await _ngayNghiClient.GetByMonth(thang, nam)
						 ?? new List<NgayNghiResponseDTO>();

			dgvNgayNghiNhanVien.DataSource = null;
			dgvNgayNghiNhanVien.DataSource = result;
		}
		private async void pageNgayNghi_Click(object sender, EventArgs e)
		{
			LoadMonthAndYear();
			await LoaddgvNgayNghi();
		}

		private async void cbbYear_SelectedIndexChanged(object sender, EventArgs e)
		{
			await LoaddgvNgayNghi();
		}

		private async void cbbMonth_SelectedIndexChanged(object sender, EventArgs e)
		{
			await LoaddgvNgayNghi();
		}
		private void SetupGridNgayNghi()
		{
			dgvNgayNghiNhanVien.AutoGenerateColumns = false;
			dgvNgayNghiNhanVien.Columns.Clear();

			dgvNgayNghiNhanVien.BackgroundColor = Color.White;
			dgvNgayNghiNhanVien.BorderStyle = BorderStyle.None;
			dgvNgayNghiNhanVien.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvNgayNghiNhanVien.GridColor = Color.FromArgb(230, 230, 230);

			dgvNgayNghiNhanVien.EnableHeadersVisualStyles = false;
			dgvNgayNghiNhanVien.ColumnHeadersHeight = 40;
			dgvNgayNghiNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
			dgvNgayNghiNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

			dgvNgayNghiNhanVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
			dgvNgayNghiNhanVien.DefaultCellStyle.SelectionForeColor = Color.Black;

			dgvNgayNghiNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvNgayNghiNhanVien.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "NgayNghiID",
				HeaderText = "ID",
				FillWeight = 10
			});

			dgvNgayNghiNhanVien.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "NhanVienID",
				HeaderText = "Nhân viên ID",
				FillWeight = 20
			});
			dgvNgayNghiNhanVien.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "Ngay",
				HeaderText = "Ngày nghỉ",
				FillWeight = 30,
				DefaultCellStyle = new DataGridViewCellStyle
				{
					Format = "dd/MM/yyyy"
				}
			});

			dgvNgayNghiNhanVien.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "LyDo",
				HeaderText = "Lý do",
				FillWeight = 70
			});
		}

		private async void btnAddNgayNghi_Click(object sender, EventArgs e)
		{
			using (var addForm = new AddNgayNghiForm())
			{
				if (addForm.ShowDialog() == DialogResult.OK)
				{
					await LoaddgvNgayNghi();
				}
			}
		}
	}
}
