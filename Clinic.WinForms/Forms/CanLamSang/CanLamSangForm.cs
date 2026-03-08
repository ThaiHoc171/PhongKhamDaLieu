using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.Forms.CanLamSang;
using Clinic.WinForms.Forms.ThietBi;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms
{
	public partial class CanLamSangForm : Form
	{
		public CanLamSangForm()
		{
			InitializeComponent();
			SetupDataGridView();
			_client = new CanLamSangClient();
		}

		private int _currentPage = 1;
		private int _pageSize = 15;
		private int _totalPages = 1;

		private readonly CanLamSangClient _client;

		private void UpdateButtonState()
		{
			btnFirst.Enabled = _currentPage > 1;
			btnPrevious.Enabled = _currentPage > 1;

			btnNext.Enabled = _currentPage < _totalPages;
			btnEnd.Enabled = _currentPage < _totalPages;
		}

		private async Task LoadDataAsync()
		{
			var result = await _client.GetAllAsync(_currentPage, _pageSize);

			dgvCLS.DataSource = result.Items;

			_totalPages = result.TotalCount > 0
				? (int)Math.Ceiling((double)result.TotalCount / _pageSize)
				: 1;

			lbcurrentPage.Text = $"Trang {_currentPage} / {_totalPages}";

			UpdateButtonState();
		}


		private async void CanLamSangForm_Load(object sender, EventArgs e)
		{
			SetupDataGridView();
			txtSizePage.Text = _pageSize.ToString();
			await LoadDataAsync();
		}


		private void SetupDataGridView()
		{
			dgvCLS.AutoGenerateColumns = false;
			dgvCLS.Columns.Clear();

			// ===== STYLE GRID =====
			dgvCLS.BackgroundColor = Color.White;
			dgvCLS.BorderStyle = BorderStyle.None;
			dgvCLS.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvCLS.GridColor = Color.FromArgb(230, 230, 230);

			dgvCLS.EnableHeadersVisualStyles = false;
			dgvCLS.ColumnHeadersHeight = 40;
			dgvCLS.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
			dgvCLS.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

			dgvCLS.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
			dgvCLS.DefaultCellStyle.SelectionForeColor = Color.Black;

			// ===== Columns =====

			dgvCLS.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "CanLamSangID",
				DataPropertyName = "CanLamSangID",
				HeaderText = "ID",
				FillWeight = 10
			});

			dgvCLS.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TenCLS",
				DataPropertyName = "TenCLS",
				HeaderText = "Tên CLS",
				FillWeight = 35
			});

			dgvCLS.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "LoaiXetNghiem",
				DataPropertyName = "LoaiXetNghiem",
				HeaderText = "Loại",
				FillWeight = 20
			});

			dgvCLS.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TrangThai",
				DataPropertyName = "TrangThai",
				HeaderText = "Trạng thái",
				FillWeight = 15
			});

			dgvCLS.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "NgayTao",
				DataPropertyName = "NgayTao",
				HeaderText = "Ngày tạo",
				FillWeight = 20,
				DefaultCellStyle = new DataGridViewCellStyle
				{
					Format = "dd/MM/yyyy"
				}
			});


			var btnEdit = new DataGridViewImageColumn
			{
				Name = "btnEdit",
				HeaderText = "",
				Image = helper.ResizeImage(Properties.Resources.edit1, 25, 25),
				Width = 40,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};

			var btnToggle = new DataGridViewImageColumn
			{
				Name = "btnToggle",
				HeaderText = "",
				Width = 40,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};

			dgvCLS.Columns.Add(btnEdit);
			dgvCLS.Columns.Add(btnToggle);
		}


		private async void dgvCLS_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex < 0)
				return;

			var row = dgvCLS.Rows[e.RowIndex];

			if (row.Cells["CanLamSangID"].Value == null)
				return;

			int id = Convert.ToInt32(row.Cells["CanLamSangID"].Value);
			string trangThai = row.Cells["TrangThai"].Value?.ToString();
			string columnName = dgvCLS.Columns[e.ColumnIndex].Name;

			if (columnName == "btnEdit")
			{
				var frm = new EditClsForm(id);

				if (frm.ShowDialog() == DialogResult.OK)
					await LoadDataAsync();

				return;
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


		private async void txtSizePage_TextChanged(object sender, EventArgs e)
		{
			if (int.TryParse(txtSizePage.Text, out int newSize) && newSize > 0)
			{
				_pageSize = newSize;
				_currentPage = 1;
				await LoadDataAsync();
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

			var result = await _client.SearchAsync(keyword);
			dgvCLS.DataSource = result;
		}

		private async void btnRefresh_Click(object sender, EventArgs e)
		{
			_currentPage = 1;
			txtSearch.Text = "";
			await LoadDataAsync();
		}

		private void dgvCLS_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < 0 || dgvCLS.Columns[e.ColumnIndex].Name != "btnToggle")
				return;

			var trangThai = dgvCLS.Rows[e.RowIndex]
				.Cells["TrangThai"].Value?.ToString();

			if (trangThai == "Hoạt động")
			{
				e.Value = helper.ResizeImage(
					Properties.Resources.activities, 25, 25);
			}
			else
			{
				e.Value = helper.ResizeImage(
					Properties.Resources.inactive, 25, 25);
			}

			e.FormattingApplied = true;
		}

		private async void btnAdd_Click(object sender, EventArgs e)
		{
			using (var addForm = new AddClsForm())
			{
				if (addForm.ShowDialog() == DialogResult.OK)
				{
					await LoadDataAsync();
				}
			}
		}
	}
}