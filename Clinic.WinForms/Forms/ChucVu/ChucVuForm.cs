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
using System.Windows.Forms.VisualStyles;

namespace Clinic.WinForms
{
	public partial class ChucVuForm : Form
	{
		public ChucVuForm()
		{
			InitializeComponent();
		}

		private BindingSource _binding = new BindingSource();
		private readonly ChucVuClient _client = new ChucVuClient();
		private Timer _searchTimer = new Timer();

		private void SetupDataGridView()
		{
			dgvChucVu.AutoGenerateColumns = false;
			dgvChucVu.Columns.Clear();

			// ===== STYLE GRID =====
			dgvChucVu.BackgroundColor = Color.White;
			dgvChucVu.BorderStyle = BorderStyle.None;
			dgvChucVu.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvChucVu.GridColor = Color.FromArgb(230, 230, 230);

			dgvChucVu.EnableHeadersVisualStyles = false;
			dgvChucVu.ColumnHeadersHeight = 40;
			dgvChucVu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
			dgvChucVu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

			dgvChucVu.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
			dgvChucVu.DefaultCellStyle.SelectionForeColor = Color.Black;


			// ===== COLUMNS =====
			dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "ChucVuID",
				HeaderText = "Mã chức vụ",
				Width = 90
			});

			dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "TenChucVu",
				HeaderText = "Tên chức vụ",
				Width = 180
			});

			dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "MoTa",
				HeaderText = "Mô tả",
				AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
			});

			dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "NgayTao",
				HeaderText = "Ngày tạo",
				Width = 120,
				DefaultCellStyle = new DataGridViewCellStyle
				{
					Format = "dd/MM/yyyy"
				}
			});
			dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn
			{
				DataPropertyName = "TrangThai",
				HeaderText = "Trạng Thái",
				Width = 80
			});

			var btnEdit = new DataGridViewButtonColumn
			{
				Name = "btnEdit",
				HeaderText = "",
				Width = 80,
				UseColumnTextForButtonValue = false,
				FlatStyle = FlatStyle.Flat
			};

			btnEdit.DefaultCellStyle.BackColor = Color.FromArgb(255, 193, 7); // vàng
			btnEdit.DefaultCellStyle.ForeColor = Color.Black;
			btnEdit.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 193, 7);
			btnEdit.DefaultCellStyle.SelectionForeColor = Color.Black;

			var btnToggle = new DataGridViewButtonColumn
			{
				Name = "btnToggle",
				HeaderText = "",
				Width = 120,
				UseColumnTextForButtonValue = false,
				FlatStyle = FlatStyle.Flat
			};

			btnToggle.DefaultCellStyle.ForeColor = Color.White;
			btnToggle.DefaultCellStyle.SelectionForeColor = Color.White;

			dgvChucVu.Columns.Add(btnEdit);
			dgvChucVu.Columns.Add(btnToggle);
		}
		private void dgvChucVu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < 0) return;

			var data = dgvChucVu.Rows[e.RowIndex].DataBoundItem as ChucVuResponseDTO;
			if (data == null) return;

			// ===== NÚT SỬA =====
			if (dgvChucVu.Columns[e.ColumnIndex].Name == "btnEdit")
			{
				e.Value = "Sửa";

				if (data.TrangThai != "Hoạt động")
				{
					e.CellStyle.BackColor = Color.Gray;
					e.CellStyle.SelectionBackColor = Color.Gray;
					e.CellStyle.ForeColor = Color.White;
				}
				else
				{
					e.CellStyle.BackColor = Color.FromArgb(255, 193, 7);
					e.CellStyle.SelectionBackColor = Color.FromArgb(255, 193, 7);
					e.CellStyle.ForeColor = Color.Black;
				}
			}

			// ===== NÚT TOGGLE =====
			if (dgvChucVu.Columns[e.ColumnIndex].Name == "btnToggle")
			{
				bool isActive = data.TrangThai == "Hoạt động";

				e.Value = isActive ? "Vô hiệu hóa" : "Kích hoạt";

				if (isActive)
				{
					e.CellStyle.BackColor = Color.IndianRed;
					e.CellStyle.SelectionBackColor = Color.IndianRed;
				}
				else
				{
					e.CellStyle.BackColor = Color.ForestGreen;
					e.CellStyle.SelectionBackColor = Color.ForestGreen;
				}

				e.CellStyle.ForeColor = Color.White;
			}
		}
		private async Task LoadDataAsync()
		{
			var list = await _client.GetAllChucVuAsync();
			_binding.DataSource = list;
			dgvChucVu.DataSource = _binding;
		}

		private async void ChucVuForm_Load(object sender, EventArgs e)
		{
			SetupDataGridView();
			await LoadDataAsync();
		}

		private async void btnAdd_Click(object sender, EventArgs e)
		{
			using (var addForm = new AddChucVuForm())
			{
				if (addForm.ShowDialog() == DialogResult.OK)
				{
					await LoadDataAsync();
				}
			}
		}

		private async void btnRefesh_Click(object sender, EventArgs e)
		{
			await LoadDataAsync();
		}
		//private void SetupSearch()
		//{
		//	_searchTimer.Interval = 500;
		//	_searchTimer.Tick += async (s, e) =>
		//	{
		//		_searchTimer.Stop();
		//		await SearchAsync(txtSearch.Text.Trim());
		//	};
		//}
		private void txtSearch_TextChanged(object sender, EventArgs e)
		{

		}
		private async Task ToggleTrangThai(ChucVuResponseDTO data)
		{
			string newStatus = data.TrangThai == "Hoạt động"
				? "Không hoạt động"
				: "Hoạt động";

			var success = await _client.UpdateStatusAsync(data.ChucVuID, newStatus);

			if (success)
			{
				data.TrangThai = newStatus;
				_binding.ResetBindings(false);
			}
			else
			{
				MessageHelper.ShowMessage("Cập nhật trạng thái thất bại.");
			}
		}
		private async void dgvChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			var data = dgvChucVu.Rows[e.RowIndex].DataBoundItem as ChucVuResponseDTO;
			if (data == null) return;

			var columnName = dgvChucVu.Columns[e.ColumnIndex].Name;

			// Sửa
			if (columnName == "btnEdit")
			{
				if (data.TrangThai != "Hoạt động")
					return;
				using (var frm = new UpdateChucVuForm(data.ChucVuID))
				{
					if (frm.ShowDialog() == DialogResult.OK)
					{
						await LoadDataAsync();
					}
				}
			}

			// Toggle trạng thái
			if (columnName == "btnToggle")
			{
				await ToggleTrangThai(data);
			}
		}

		private void dgvChucVu_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
		}
	}
}
