using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.NhanVien
{
	public partial class NhanVienForm : Form
	{
		public NhanVienForm()
		{
			InitializeComponent();

		}
		private int _currentPage = 1;
		private int _pageSize = 20;
		private int _totalPages = 1;
		
		private readonly NhanVienClient _client = new NhanVienClient();

		private void UpdateButtonState()
		{
			btnFirst.Enabled = _currentPage > 1;
			btnPrevious.Enabled = _currentPage > 1;

			btnNext.Enabled = _currentPage < _totalPages;
			btnEnd.Enabled = _currentPage < _totalPages;
		}
		private async Task LoadDataAsync()
		{
			var result = await _client.GetNhanVienPagedAsync(_currentPage, _pageSize);

			dgvNhanVien.DataSource = result.Items;

			if (result.TotalCount > 0)
				_totalPages = (int)Math.Ceiling((double)result.TotalCount / _pageSize);
			else
				_totalPages = 1;

			lbcurrentPage.Text = "Trang " + _currentPage + " / " + _totalPages;

			UpdateButtonState();
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

		private async void NhanVienForm_Load(object sender, EventArgs e)
		{
			txtSizePage.Text = _pageSize.ToString();
			SetupDataGridView();
			await LoadDataAsync();
		}

		private async void txtSizePage_TextChanged(object sender, EventArgs e)
		{
			int newSize;

			if (int.TryParse(txtSizePage.Text, out newSize) && newSize > 0)
			{
				_pageSize = newSize;
				_currentPage = 1;
				await LoadDataAsync();
			}
		}


		private void SetupDataGridView()
		{
			dgvNhanVien.AutoGenerateColumns = false;
			dgvNhanVien.Columns.Clear();

			// ===== STYLE GRID =====
			dgvNhanVien.BackgroundColor = Color.White;
			dgvNhanVien.BorderStyle = BorderStyle.None;
			dgvNhanVien.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvNhanVien.GridColor = Color.FromArgb(230, 230, 230);

			dgvNhanVien.EnableHeadersVisualStyles = false;
			dgvNhanVien.ColumnHeadersHeight = 40;
			dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
			dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			//dgvNhanVien.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

			dgvNhanVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
			dgvNhanVien.DefaultCellStyle.SelectionForeColor = Color.Black;


			dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "NhanVienID",
				DataPropertyName = "NhanVienID",
				HeaderText = "Mã NV",
				FillWeight = 10
			});

			dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "HoTen",
				DataPropertyName = "HoTen",
				HeaderText = "Họ tên",
				FillWeight = 20
			});

			dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "Email",
				DataPropertyName = "Email",
				HeaderText = "Email",
				FillWeight = 25
			});

			dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TenChucVu",
				DataPropertyName = "TenChucVu",
				HeaderText = "Chức vụ",
				FillWeight = 15
			});

			dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TrangThai",
				DataPropertyName = "TrangThai",
				HeaderText = "Trạng thái",
				FillWeight = 15
			});

			var btnView = new DataGridViewImageColumn
			{
				Name = "btnView",
				HeaderText = "",
				Image = helper.ResizeImage(Properties.Resources.edit1, 25, 25),
				Width = 40,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};


			var btnNghiViec = new DataGridViewImageColumn
			{
				Name = "btnNghiViec",
				HeaderText = "",
				Image = helper.ResizeImage(Properties.Resources.letter_x, 25, 25),
				Width = 40,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};

			dgvNhanVien.Columns.Add(btnView);
			dgvNhanVien.Columns.Add(btnNghiViec);
		}

		private async void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex < 0)
				return;

			var row = dgvNhanVien.Rows[e.RowIndex];

			if (row.Cells["NhanVienID"].Value == null)
				return;

			var id = Convert.ToInt32(row.Cells["NhanVienID"].Value);
			var trangThai = row.Cells["TrangThai"].Value?.ToString();
			var columnName = dgvNhanVien.Columns[e.ColumnIndex].Name;

			if (columnName == "btnView")
			{
				var frm = new UpdateNhanVienForm(id);

				if (frm.ShowDialog() == DialogResult.OK)
					await LoadDataAsync();

				return;
			}
			if (columnName == "btnNghiViec")
			{
				if (trangThai == "Nghỉ việc")
				{
					MessageHelper.ShowMessage("Nhân viên này đã nghỉ việc.");
					return;
				}
				if (trangThai == "Đang làm việc")
				{
					var confirm = MessageHelper.Confirm(
						"Bạn có chắc muốn cho nhân viên này nghỉ việc?");

					if (confirm != DialogResult.Yes)
						return;

					try
					{
						await _client.UpdateSatusAsync(id, "Nghỉ việc");
						MessageHelper.ShowMessage("Cập nhật trạng thái thành công.");
						await LoadDataAsync();
					}
					catch (Exception ex)
					{
						MessageHelper.ShowMessage("Có lỗi xảy ra: " + ex.Message);
					}
				}
			}
		}

		private void dgvNhanVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{

		}

		private async void btnAdd_Click(object sender, EventArgs e)
		{
			using (var addForm = new AddNhanVienForm())
			{
				if (addForm.ShowDialog() == DialogResult.OK)
				{
					await LoadDataAsync();
				}
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			SearchTimer.Stop();
			SearchTimer.Start();
		}

		private async void SearchTimer_Tick(object sender, EventArgs e)
		{
			SearchTimer.Stop();

			var keyword = txtSearch.Text.Trim();

			if (string.IsNullOrWhiteSpace(keyword))
			{
				await LoadDataAsync();
				return;
			}
			var result = await _client.SearchNhanVienAsync(keyword, 1, _pageSize);
			dgvNhanVien.DataSource = result.Items;
		}

		private async void btnRefesh_Click(object sender, EventArgs e)
		{
			_currentPage = 1;
			txtSearch.Text = "";
			await LoadDataAsync();
		}

	}
}
