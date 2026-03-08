using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using Clinic.WinForms.Forms.PhienKham;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Clinic.WinForms.Forms.BenhNhan
{
	public partial class ViewPhienKhamBenhNhanForm : Form
	{
		private readonly int _id;
		private readonly string _name;
		private readonly PhienKhamClient _client;
		private int _currentPage = 1;
		private int _pageSize = 10;
		private int _totalPages = 1;
		public ViewPhienKhamBenhNhanForm(int id,string name)
		{
			InitializeComponent();
			_id = id;
			_name = name;
			_client = new PhienKhamClient();
			SetupDataGridView();
			txtSizePage.Text = _pageSize.ToString();
		}
		private async void ViewPhienKhamBenhNhanForm_Load(object sender, EventArgs e)
		{
			await LoadDataAsync();
		}
		private async Task LoadDataAsync()
		{
			try
			{
				var paged = await _client.GetByBenhNhanAsync(_id, _currentPage, _pageSize);

				if (paged == null || paged.Items == null || paged.Items.Count == 0)
				{
					dgvContent.DataSource = null;
					lbcurrentPage.Text = "Không có dữ liệu";
					return;
				}

				dgvContent.DataSource = null;
				dgvContent.DataSource = paged.Items;

				lbName.Text = _name;

				_totalPages = paged.TotalCount > 0
					? (int)Math.Ceiling((double)paged.TotalCount / _pageSize)
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
		private void SetupDataGridView()
		{
			SetupDatagridview.ApplyGridStyle(dgvContent);
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "PhienKhamID",
				DataPropertyName = "PhienKhamID",
				HeaderText = "Mã phiên",
				FillWeight = 15
			});
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "BenhNhan",
				DataPropertyName = "BenhNhan",
				HeaderText = "Bệnh nhân",
				FillWeight = 20,
				Visible = false
			});
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "NhanVien",
				DataPropertyName = "NhanVien",
				HeaderText = "Bác sĩ",
				FillWeight = 25
			});
			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "NgayKham",
				DataPropertyName = "NgayKham",
				HeaderText = "Ngày khám",
				DefaultCellStyle = new DataGridViewCellStyle
				{
					Format = "dd/MM/yyyy"
				},
				FillWeight = 15
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
				HeaderText = "Chẩn đoán cuối",
				FillWeight = 20
			});
			var btnView = new DataGridViewImageColumn
			{
				Name = "btnView",
				HeaderText = "",
				Image = helper.ResizeImage(Properties.Resources.file, 25, 25),
				Width = 40,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};
			dgvContent.Columns.Add(btnView);
			dgvContent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
		private async void btnPrevious_Click(object sender, EventArgs e)
		{
			if (_currentPage > 1)
			{
				_currentPage--;
				await LoadDataAsync();
			}
		}
		private async void btnFirst_Click(object sender, EventArgs e)
		{
			_currentPage = 1;
			await LoadDataAsync();
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
			if (columnName == "btnView")
			{
				var frm = new ViewPhienKhamForm(id);
				frm.ShowDialog();
			}
		}
		private void btnBack_Click(object sender, EventArgs e)
		{
			var main = Application.OpenForms["FormMain"] as FormMain;
			if (main != null)
			{
				main.OpenPage("Danh sách bệnh nhân", new BenhNhanForm());
			}
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
	}
}