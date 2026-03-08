using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Clinic.WinForms.Forms.PhienKham
{
	public partial class ChoKhamForm : Form
	{
		private readonly PhienKhamClient _client = new PhienKhamClient();
		private readonly int _nvId = Session.NhanVienId ?? 0;
		private int _currentPage = 1;
		private int _pageSize = 15;
		private int _totalPages = 1;
		public ChoKhamForm()
		{
			InitializeComponent();
			txtSizePage.Text = _pageSize.ToString();
		}
		private async void PhienKhamForm_Load(object sender, EventArgs e)
		{
			SetupDataGridView();
			await LoadDataAsync();
		}
		private async Task LoadDataAsync()
		{
			string trangThai = "Đang khám";
			if (_nvId == 0)
				return;
			var result = await _client.GetPagedAsync(_currentPage, _pageSize, _nvId, trangThai);
			if (result == null)
				return;
			dgvContent.DataSource = result.Items;
			_totalPages = result.TotalCount > 0
				? (int)Math.Ceiling((double)result.TotalCount / _pageSize)
				: 1;
			lbcurrentPage.Text = $"Trang {_currentPage} / {_totalPages}";
			UpdateButtonState();
		}
		private void SetupDataGridView()
		{
			SetupDatagridview.ApplyGridStyle(dgvContent);
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "PhienKhamID",
				DataPropertyName = "PhienKhamID",
				HeaderText = "Mã phiên",
				FillWeight = 10
			});
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "BenhNhan",
				DataPropertyName = "BenhNhan",
				HeaderText = "Bệnh nhân",
				FillWeight = 20
			});
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "NhanVien",
				DataPropertyName = "NhanVien",
				HeaderText = "Bác sĩ",
				FillWeight = 20
			});
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "NgayKham",
				DataPropertyName = "NgayKham",
				HeaderText = "Ngày khám",
				DefaultCellStyle = new DataGridViewCellStyle
				{
					Format = "dd/MM/yyyy HH:mm"
				},
				FillWeight = 20
			});
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TrangThai",
				DataPropertyName = "TrangThai",
				HeaderText = "Trạng thái",
				FillWeight = 15
			});
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "ChanDoanCuoi",
				DataPropertyName = "ChanDoanCuoi",
				HeaderText = "Chẩn đoán",
				FillWeight = 25
			});
			dgvContent.Columns.Add(SetupDatagridview.CreateButtonColumn("btnDetail", Properties.Resources.file, "Xem chi tiết phiên khám"));
		}
		private void UpdateButtonState()
		{
			btnFirst.Enabled = _currentPage > 1;
			btnPrevious.Enabled = _currentPage > 1;
			btnNext.Enabled = _currentPage < _totalPages;
			btnEnd.Enabled = _currentPage < _totalPages;
		}
		private async void btnRefesh_Click(object sender, EventArgs e)
		{
			_currentPage = 1;
			txtSearch.Text = "";
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
		private void dgvContent_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex < 0)
				return;
			var row = dgvContent.Rows[e.RowIndex];
			if (row.Cells["PhienKhamID"].Value == null)
				return;
			int id = Convert.ToInt32(row.Cells["PhienKhamID"].Value);
			string columnName = dgvContent.Columns[e.ColumnIndex].Name;
			if (columnName == "btnDetail")
			{
				var frm = new ViewPhienKhamForm(id);
				frm.ShowDialog();
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
			var result = await _client.SearchAsync(keyword, _currentPage, _pageSize, _nvId);
			dgvContent.DataSource = result.Items;
			_totalPages = result.TotalCount > 0
				? (int)Math.Ceiling((double)result.TotalCount / _pageSize)
				: 1;
			lbcurrentPage.Text = $"Trang {_currentPage} / {_totalPages}";
			UpdateButtonState();
		}
	}
}