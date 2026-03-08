using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.ThietBi
{
	public partial class ThietBiForm : Form
	{
		public ThietBiForm()
		{
			InitializeComponent();
			_client = new ThietBiClient();
			
		}
		private readonly ThietBiClient _client;

		private async Task LoadDataAsync()
		{
			try
			{
				dgvThietBi.DataSource = null;

				var data = await _client.GetAllAsync();

				dgvThietBi.DataSource = data;
				dgvThietBi.Refresh();
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Lỗi load phòng chức năng:\n" + ex.Message);
			}
		}
		private Image ResizeImage(Image img, int width, int height)
		{
			return new Bitmap(img, new Size(width, height));
		}
		private void SetupDataGridView()
		{
			dgvThietBi.AutoGenerateColumns = false;
			dgvThietBi.Columns.Clear();

			// ===== STYLE GRID =====
			dgvThietBi.BackgroundColor = Color.White;
			dgvThietBi.BorderStyle = BorderStyle.None;
			dgvThietBi.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvThietBi.GridColor = Color.FromArgb(230, 230, 230);

			dgvThietBi.EnableHeadersVisualStyles = false;
			dgvThietBi.ColumnHeadersHeight = 40;
			dgvThietBi.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
			dgvThietBi.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

			dgvThietBi.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
			dgvThietBi.DefaultCellStyle.SelectionForeColor = Color.Black;

			dgvThietBi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

			// ===== COLUMNS =====

			dgvThietBi.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "Id",
				DataPropertyName = "Id",
				HeaderText = "Mã thiết bị",
				FillWeight = 20
			});

			dgvThietBi.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TenTB",
				DataPropertyName = "TenTB",
				HeaderText = "Tên thiết bị",
				FillWeight = 40
			});

			dgvThietBi.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "LoaiTB",
				DataPropertyName = "LoaiTb",
				HeaderText = "Loại thiết bị",
				FillWeight = 40
			});


			// ===== BUTTON EDIT =====
			var btnEdit = new DataGridViewImageColumn
			{
				Name = "btnEdit",
				HeaderText = "",
				Image = ResizeImage(Properties.Resources.edit1, 25, 25),
				Width = 40,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};

			dgvThietBi.Columns.Add(btnEdit);
		}
		private async void dgvThietBi_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			var row = dgvThietBi.Rows[e.RowIndex];

			// ===== EDIT =====
			if (dgvThietBi.Columns[e.ColumnIndex].Name == "btnEdit")
			{
				int id = (int)row.Cells["Id"].Value;
				using (var viewForm = new EditThietBiForm(id))
				{
					if (viewForm.ShowDialog() == DialogResult.OK)
					{
						await LoadDataAsync();
					}
				}
			}
		}

		private async Task ThietBiForm_Load(object sender, EventArgs e)
		{
			SetupDataGridView();
			await LoadDataAsync();
		}

		private async void btnAdd_Click(object sender, EventArgs e)
		{
			using (var addForm = new AddThietBiForm())
			{
				if (addForm.ShowDialog() == DialogResult.OK)
				{
					await LoadDataAsync();
				}
			}
		}

		private async void btnRefesh_Click(object sender, EventArgs e)
		{
			txtSearch.Text = "";
			await LoadDataAsync();
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
			dgvThietBi.DataSource = result;
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			SearchTimer.Stop();
			SearchTimer.Start();
		}

		private async void ThietBiForm1_Load(object sender, EventArgs e)
		{
			SetupDataGridView();
			await LoadDataAsync();
		}
	}
}
