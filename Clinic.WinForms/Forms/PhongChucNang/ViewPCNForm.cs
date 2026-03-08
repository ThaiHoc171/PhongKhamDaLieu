using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using Clinic.WinForms.Forms.NhanVien;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.PhongChucNang
{
	public partial class ViewPCNForm : Form
	{
		private readonly int _pcnId;
		private readonly ThietBiClient _tbClient;
		private readonly PCNThietBiClient _pcnTBClient;
		private readonly ChiTietPCNTBClient _chiTietPCNTBClient;
		public ViewPCNForm(int pcnId)
		{
			InitializeComponent();
			_pcnId = pcnId;
			FormDragHelper.EnableDrag(pnlHeader,this);
			_tbClient = new ThietBiClient();
			_pcnTBClient = new PCNThietBiClient();
			_chiTietPCNTBClient = new ChiTietPCNTBClient();
		}

		private void lbPhong_Click(object sender, EventArgs e)
		{

		}

		private void lbGhiChu_Click(object sender, EventArgs e)
		{

		}
		public async Task LoadDataAsync()
		{
			var list = await _tbClient.GetComboboxAsync();

			if (list == null) return;

			list.Insert(0, new DTOs.ComboboxResult
			{
				Id = 0,
				Name = "Chọn thiết bị"
			});

			cbbThietBi.DataSource = null;
			cbbThietBi.DisplayMember = "Name";
			cbbThietBi.ValueMember = "Id";
			cbbThietBi.DataSource = list;
			cbbThietBi.SelectedIndex = 0;

			var thietBiPhong = await _pcnTBClient.GetByPhongAsync(_pcnId);

			if (thietBiPhong != null)
			{
				var data = thietBiPhong.Select(x => new
				{
					x.PCN_TB_ID,
					ThietBi = x.ThietBi.Name,
					x.TongSoLuong
				}).ToList();

				dgvThietBiPhong.DataSource = data;
			}

			dgvChiTiet.DataSource = null;
		}
		private void SetupDataGridView()
		{

			void ApplyCommonStyle(DataGridView dgv)
			{
				dgv.AutoGenerateColumns = false;
				dgv.AllowUserToAddRows = false;
				dgv.ReadOnly = true;
				dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
				dgv.MultiSelect = false;

				dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

				dgv.ColumnHeadersHeightSizeMode =
					DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
				dgv.ColumnHeadersHeight = 35;

				dgv.ColumnHeadersDefaultCellStyle.WrapMode =
					DataGridViewTriState.False;

				dgv.EnableHeadersVisualStyles = false;
			}

			// ===== dgvThietBiPhong =====
			ApplyCommonStyle(dgvThietBiPhong);
			dgvThietBiPhong.Columns.Clear();

			dgvThietBiPhong.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "PCN_TB_ID",
				DataPropertyName = "PCN_TB_ID",
				Visible = false
			});

			dgvThietBiPhong.Columns.Add(new DataGridViewTextBoxColumn
			{
				HeaderText = "Thiết bị",
				DataPropertyName = "ThietBi",
				Name = "ThietBi",
				FillWeight = 60
			});

			dgvThietBiPhong.Columns.Add(new DataGridViewTextBoxColumn
			{
				HeaderText = "Tổng số lượng",
				DataPropertyName = "TongSoLuong",
				Name = "TongSoLuong",
				FillWeight = 40
			});


			ApplyCommonStyle(dgvChiTiet);
			dgvChiTiet.Columns.Clear();

			dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "ChiTietID",
				HeaderText = "Mã chi tiêt",
				DataPropertyName = "ChiTietID",
				FillWeight = 10
			});

			dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
			{
				HeaderText = "Mã tài sản",
				DataPropertyName = "MaTaiSan",
				FillWeight = 20
			});

			dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
			{
				HeaderText = "Ngày nhập",
				DataPropertyName = "NgayNhap",
				DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" },
				FillWeight = 20
			});

			dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TinhTrang",
				HeaderText = "Tình trạng",
				DataPropertyName = "TinhTrang",
				FillWeight = 20
			});

			dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
			{
				HeaderText = "Ghi chú",
				DataPropertyName = "GhiChu",
				FillWeight = 20
			});
			var btnSync = new DataGridViewImageColumn
			{
				Name = "btnSync",
				HeaderText = "",
				Image = helper.ResizeImage(Properties.Resources.refesh, 25, 25),
				FillWeight = 5,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};


			var btnXoa = new DataGridViewImageColumn
			{
				Name = "btnXoa",
				HeaderText = "",
				Image = helper.ResizeImage(Properties.Resources.letter_x, 25, 25),
				FillWeight = 5,
				ImageLayout = DataGridViewImageCellLayout.Zoom,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			};

			dgvChiTiet.Columns.Add(btnSync);
			dgvChiTiet.Columns.Add(btnXoa);
		}

		private async void ViewPCNForm_Load(object sender, EventArgs e)
		{
			lbValuePhong.Text = _pcnId.ToString();
			SetupDataGridView();
			await LoadDataAsync();
		}

		private async void dgvThietBiPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			int pcnTbId = Convert.ToInt32(
				dgvThietBiPhong.Rows[e.RowIndex].Cells["PCN_TB_ID"].Value);

			var chiTiet = await _chiTietPCNTBClient.GetByPhongAsync(pcnTbId);

			if (chiTiet != null)
			{
				dgvChiTiet.DataSource = chiTiet;
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private async void btnLuu_Click(object sender, EventArgs e)
		{
			var selectedValue = cbbThietBi.SelectedValue;

			if (selectedValue == null || Convert.ToInt32(selectedValue) == 0)
			{
				MessageHelper.ShowMessage("Vui lòng chọn thiết bị.");
				return;
			}

			if (string.IsNullOrWhiteSpace(txtMaTaiSan.Text))
			{
				MessageHelper.ShowMessage("Vui lòng nhập mã tài sản.");
				return;
			}

			var dto = new ChiTietPCNThietBiCreateDTO
			{
				PhongChucNangID = _pcnId,
				ThietBiID = Convert.ToInt32(selectedValue),
				MaTaiSan = txtMaTaiSan.Text.Trim(),
				GhiChu = txtGhiChu.Text.Trim()
			};

			var result = await _chiTietPCNTBClient.CreateAsync(dto);

			if (result)
			{
				MessageHelper.ShowMessage("Thêm tài sản thành công.");
				txtMaTaiSan.Clear();
				txtGhiChu.Clear();
				cbbThietBi.SelectedIndex = 0;

				await LoadDataAsync();
			}
			else
			{
				MessageHelper.ShowMessage("Thêm tài sản thất bại.");
			}
		}
		private string GetNextStatus(string current)
		{
			switch (current)
			{
				case "HoatDong":
					return "Hong";

				case "BaoTri":
					return "HoatDong";

				case "Hong":
					return "BaoTri"; ;

				default:
					return "HoatDong";
			}
		}
		private async void dgvChiTiet_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex < 0)
				return;

			var row = dgvChiTiet.Rows[e.RowIndex];

			if (row.Cells["ChiTietID"].Value == null)
				return;

			int id = Convert.ToInt32(row.Cells["ChiTietID"].Value);
			string trangThai = row.Cells["TinhTrang"].Value?.ToString();
			string columnName = dgvChiTiet.Columns[e.ColumnIndex].Name;

			if (columnName == "btnSync")
			{
				string newStatus = GetNextStatus(trangThai);

				var confirm = MessageHelper.Confirm(
					$"Chuyển trạng thái sang '{newStatus}' ?");

				if (confirm == DialogResult.Yes)
				{
					try
					{
						var result = await _chiTietPCNTBClient
							.UpdateStatusAsync(id, newStatus);

						if (result)
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

			if (columnName == "btnXoa")
			{
				var confirm = MessageHelper.Confirm("Bạn có chắc muốn xóa tài sản này?");

				if (confirm == DialogResult.Yes)
				{
					try
					{
						var result = await _chiTietPCNTBClient.DeleteAsync(id);

						if (result)
						{
							MessageHelper.ShowMessage("Xóa thành công.");
							await LoadDataAsync();
						}
						else
						{
							MessageHelper.ShowMessage("Xóa thất bại.");
						}
					}
					catch (Exception ex)
					{
						MessageHelper.ShowMessage("Lỗi: " + ex.Message);
					}
				}
			}
		}
	}
}
