using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using Clinic.WinForms.Forms.NhanVien;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.PhongChucNang
{
	public partial class PhongChucNangForm : Form
	{
		public PhongChucNangForm()
		{
			InitializeComponent();
			_pcnClient = new PhongChucNangClient();
			 LoadDataAsync();
		}
		private readonly PhongChucNangClient _pcnClient;

		private async Task LoadDataAsync()
		{
			try
			{
				dgvPhongChucNang.DataSource = null;

				var data = await _pcnClient.GetPhongAsync();

				dgvPhongChucNang.DataSource = data;
				dgvPhongChucNang.Refresh();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi load phòng chức năng:\n" + ex.Message);
			}
		}

		private Image ResizeImage(Image img, int width, int height)
		{
			return new Bitmap(img, new Size(width, height));
		}
		private void SetupDataGridViewPhongChucNang()
		{
			dgvPhongChucNang.AutoGenerateColumns = false;
			dgvPhongChucNang.Columns.Clear();

			// ===== STYLE GRID =====
			dgvPhongChucNang.BackgroundColor = Color.White;
			dgvPhongChucNang.BorderStyle = BorderStyle.None;
			dgvPhongChucNang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvPhongChucNang.GridColor = Color.FromArgb(230, 230, 230);

			dgvPhongChucNang.EnableHeadersVisualStyles = false;
			dgvPhongChucNang.ColumnHeadersHeight = 40;
			dgvPhongChucNang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
			dgvPhongChucNang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

			dgvPhongChucNang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
			dgvPhongChucNang.DefaultCellStyle.SelectionForeColor = Color.Black;

			dgvPhongChucNang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

			// ===== COLUMNS =====

			dgvPhongChucNang.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "Id",
				DataPropertyName = "Id",
				HeaderText = "Mã phòng",
				FillWeight = 10
			});

			dgvPhongChucNang.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TenPhong",
				DataPropertyName = "TenPhong",
				HeaderText = "Tên phòng",
				FillWeight = 20
			});

			dgvPhongChucNang.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "LoaiPhong",
				DataPropertyName = "LoaiPhong",
				HeaderText = "Loại phòng",
				FillWeight = 15
			});

			dgvPhongChucNang.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TrangThai",
				DataPropertyName = "TrangThai",
				HeaderText = "Trạng thái",
				FillWeight = 15
			});

			dgvPhongChucNang.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "NgayTao",
				DataPropertyName = "NgayTao",
				HeaderText = "Ngày tạo",
				FillWeight = 15,
				DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
			});

			// ===== BUTTON VIEW =====
			var btnView = new DataGridViewImageColumn
			{
				Name = "btnView",
				HeaderText = "",
				Image = ResizeImage(Properties.Resources.file, 25, 25),
				Width = 40,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};

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

			// ===== BUTTON TOGGLE STATUS =====
			var btnToggle = new DataGridViewImageColumn
			{
				Name = "btnToggle",
				HeaderText = "",
				Image = ResizeImage(Properties.Resources.refesh, 25, 25),
				Width = 40,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};

			dgvPhongChucNang.Columns.Add(btnView);
			dgvPhongChucNang.Columns.Add(btnEdit);
			dgvPhongChucNang.Columns.Add(btnToggle);
		}
		private async void dgvPhongChucNang_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			var row = dgvPhongChucNang.Rows[e.RowIndex];
			int id = (int)row.Cells["Id"].Value;
			string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "";

			// ===== VIEW =====
			if (dgvPhongChucNang.Columns[e.ColumnIndex].Name == "btnView")
			{
				int pcnid = (int)row.Cells["Id"].Value;
				using (var viewForm = new ViewPCNForm(pcnid))
				{
					if (viewForm.ShowDialog() == DialogResult.OK)
					{
						await LoadDataAsync();
					}
				}
			}

			// ===== EDIT =====
			if (dgvPhongChucNang.Columns[e.ColumnIndex].Name == "btnEdit")
			{
				int pcnid = (int)row.Cells["Id"].Value;
				using (var viewForm = new EditPCNForm(pcnid))
				{
					if (viewForm.ShowDialog() == DialogResult.OK)
					{
						await LoadDataAsync();
					}
				}
			}

			if (dgvPhongChucNang.Columns[e.ColumnIndex].Name == "btnToggle")
			{
				string newStatus = GetNextStatus(trangThai);

				var confirm = MessageHelper.Confirm($"Chuyển trạng thái sang '{newStatus}'");

				if (confirm == DialogResult.Yes)
				{
					try
					{
						var rp = await _pcnClient.UpdateSatusAsync(id, newStatus);
						if(rp != null)						
						{
							MessageHelper.ShowMessage("Cập nhật trạng thái thành công.");
							await LoadDataAsync();
						}
						else
						{
							MessageHelper.ShowMessage("Cập nhật trạng thái thất bại.");
						}

					}
					catch (Exception ex)
					{
						MessageHelper.ShowMessage("Lỗi: " + ex.Message);
					}
				}
			}
		}
		private string GetNextStatus(string current)
		{
			switch (current)
			{
				case "Hoạt động":
					return "Hong";

				case "Bảo trì":
					return "HoatDong";

				case "Hỏng":
					return "BaoTri"; ;

				default:
					return "HoatDong";
			}
		}

		private void dgvPhongChucNang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (dgvPhongChucNang.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
			{
				switch (e.Value.ToString())
				{
					case "Hoạt động":
						e.CellStyle.ForeColor = Color.Green;
						break;
					case "Bảo trì":
						e.CellStyle.ForeColor = Color.Orange;
						break;
					case "Hỏng":
						e.CellStyle.ForeColor = Color.Red;
						break;
				}
			}
		}

		private async void PhongChucNangForm_Load(object sender, EventArgs e)
		{
			SetupDataGridViewPhongChucNang();
			await LoadDataAsync();
		}

		private async void btnRefesh_Click(object sender, EventArgs e)
		{
			await LoadDataAsync();
		}

		private async void btnAdd_Click(object sender, EventArgs e)
		{
			using (var addForm = new AddPCNForm())
			{
				if (addForm.ShowDialog() == DialogResult.OK)
				{
					await LoadDataAsync();
				}
			}
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
			var result = await _pcnClient.SearchAsync(keyword);
			dgvPhongChucNang.DataSource = result;
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			SearchTimer.Stop();
			SearchTimer.Start();
		}
	}
}
