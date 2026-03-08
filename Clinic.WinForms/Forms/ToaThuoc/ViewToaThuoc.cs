using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.ToaThuoc
{
	public partial class ViewToaThuoc : Form
	{
		private readonly int _id;
		private readonly ToaThuocClient _client = new ToaThuocClient();
		public ViewToaThuoc(int id)
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);
			_id = id;
			SetupDgv();
		}
		private async Task LoadDataAsync()
		{
			try
			{
				var toaThuoc = await _client.GetByPhienKhamAsync(_id);
				if (toaThuoc == null)
					return;
				lbToaThuocId.Text = toaThuoc.ToaThuocID.ToString();
				lbNameBsValue.Text = toaThuoc.NguoiLap.ToString();
				lbNgayValue.Text = toaThuoc.NgayLap.ToString("dd/MM/yyyy");
				lbGhiChuValue.Text = toaThuoc.GhiChu.ToString();

				var chiTiet = await _client.GetChiTietAsync(toaThuoc.ToaThuocID);
				if (chiTiet == null)
					return;
				dgvContent.DataSource = chiTiet;
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Không thể tải dữ liệu: " + ex.Message);
			}
		}
		private void SetupDgv()
		{
			SetupDatagridview.ApplyGridStyle(dgvContent);

			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TenThuoc",
				DataPropertyName = "TenThuoc",
				HeaderText = "Tên thuốc",
				FillWeight = 40
			});

			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "LieuDung",
				DataPropertyName = "LieuDung",
				HeaderText = "Liều dùng",
				FillWeight = 40
			});

			dgvContent.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "SoLuong",
				DataPropertyName = "SoLuong",
				HeaderText = "Số lượng",
				FillWeight = 20
			});
		}
		private void btnExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private async void ViewToaThuoc_Load(object sender, EventArgs e)
		{
			await LoadDataAsync();
		}
	}
}
