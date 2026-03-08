using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Clinic.WinForms.Forms.BenhNhan
{
	public partial class BenhNhanForm : Form
	{
		private readonly BenhNhanClient _client;
		private int _currentPage = 1;
		private int _pageSize = 15;
		private int _totalPages = 1;
		public BenhNhanForm()
		{
			InitializeComponent();
			SetupDataGridView();
			_client = new BenhNhanClient();
			txtSizePage.Text = _pageSize.ToString();
		}
		private async void BenhNhanForm_Load(object sender, EventArgs e)
		{
			await LoadDataAsync();
		}
		private async Task LoadDataAsync()
		{
			var result = await _client.GetAllAsync(_currentPage, _pageSize);
			if (result == null) return;
			dgvContent.DataSource = result.Items;
			_totalPages = result.TotalCount > 0
				? (int)Math.Ceiling((double)result.TotalCount / _pageSize)
				: 1;
			lbcurrentPage.Text = $"Trang {_currentPage} / {_totalPages}";
			UpdateButtonState();
		}
		private void UpdateButtonState()
		{
			btnFirst.Enabled = _currentPage > 1;
			btnPrevious.Enabled = _currentPage > 1;
			btnNext.Enabled = _currentPage < _totalPages;
			btnEnd.Enabled = _currentPage < _totalPages;
		}
		private void SetupDataGridView()
		{
			dgvContent.AutoGenerateColumns = false;
			dgvContent.Columns.Clear();
			dgvContent.BackgroundColor = Color.White;
			dgvContent.BorderStyle = BorderStyle.None;
			dgvContent.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvContent.GridColor = Color.FromArgb(230, 230, 230);
			dgvContent.EnableHeadersVisualStyles = false;
			dgvContent.ColumnHeadersHeight = 40;
			dgvContent.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
			dgvContent.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			dgvContent.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
			dgvContent.DefaultCellStyle.SelectionForeColor = Color.Black;
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "BenhNhanID",
				DataPropertyName = "BenhNhanID",
				HeaderText = "Mã BN",
				FillWeight = 10
			});
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "HoTen",
				DataPropertyName = "HoTen",
				HeaderText = "Họ tên",
				FillWeight = 30
			});
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "SDT",
				DataPropertyName = "SDT",
				HeaderText = "SĐT",
				FillWeight = 15
			});
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "EmailLienHe",
				DataPropertyName = "EmailLienHe",
				HeaderText = "Email",
				FillWeight = 20
			});
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "GhiChu",
				DataPropertyName = "GhiChu",
				HeaderText = "Ghi chú",
				FillWeight = 20
			});
			var btnViewHS = new DataGridViewImageColumn
			{
				Name = "btnViewHS",
				HeaderText = "",
				Image = helper.ResizeImage(Properties.Resources.file, 25, 25),
				Width = 40,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};
			var btnViewPK = new DataGridViewImageColumn
			{
				Name = "btnViewPK",
				HeaderText = "",
				Image = helper.ResizeImage(Properties.Resources.journal, 25, 25),
				Width = 40,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};
			var btnEdit = new DataGridViewImageColumn
			{
				Name = "btnEdit",
				HeaderText = "",
				Image = helper.ResizeImage(Properties.Resources.edit1, 25, 25),
				Width = 40,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};
			dgvContent.Columns.Add(btnViewHS);
			dgvContent.Columns.Add(btnViewPK);
			dgvContent.Columns.Add(btnEdit);
		}
		private async void dgvContent_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex < 0)
				return;
			var row = dgvContent.Rows[e.RowIndex];
			if (row.Cells["BenhNhanID"].Value == null)
				return;
			int id = Convert.ToInt32(row.Cells["BenhNhanID"].Value);
			string name = row.Cells["HoTen"].Value.ToString();
			string columnName = dgvContent.Columns[e.ColumnIndex].Name;
			if (columnName == "btnViewHS")
			{
				var frm = new ViewHoSoBenhAnForm(id);
				if (frm.ShowDialog() == DialogResult.OK)
					await LoadDataAsync();
			}
			if (columnName == "btnViewPK")
			{
				var main = Application.OpenForms["FormMain"] as FormMain;

				if (main != null)
				{
					main.OpenPage("Bệnh nhân / Lịch sử phiên khám bệnh nhân", new ViewPhienKhamBenhNhanForm(id,name));
				}
			}
			if (columnName == "btnEdit")
			{
				var frm = new UpdateBenhNhanForm(id);
				if (frm.ShowDialog() == DialogResult.OK)
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
			dgvContent.DataSource = result;
		}
		private async void btnRefesh_Click(object sender, EventArgs e)
		{
			_currentPage = 1;
			txtSearch.Text = "";
			await LoadDataAsync();
		}
		private async void btnAdd_Click(object sender, EventArgs e)
		{
			using (var addForm = new AddBenhNhanForm())
			{
				if (addForm.ShowDialog() == DialogResult.OK)
				{
					await LoadDataAsync();
				}
			}
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

		private void dgvContent_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
		{
			if (e.RowIndex < 0) return;

			if (dgvContent.Columns[e.ColumnIndex].Name == "btnViewHS")
			{
				e.ToolTipText = "Xem hồ sơ bệnh án";
			}
			if (dgvContent.Columns[e.ColumnIndex].Name == "btnViewPK")
			{
				e.ToolTipText = "Xem lịch sử phiên khám";
			}
			if (dgvContent.Columns[e.ColumnIndex].Name == "btnEdit")
			{
				e.ToolTipText = "Cập nhật bệnh nhân";
			}
		}
	}
}