using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.Forms.BenhNhan;
using Clinic.WinForms.Forms.ToaThuoc;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.PhienKham
{
	public partial class ViewPhienKhamForm : Form
	{
		private readonly PhienKhamClient _client = new PhienKhamClient();
		private readonly PhienKham_BenhClient _benhClient = new PhienKham_BenhClient();
		private readonly PhienKham_ClsClient _clsClient = new PhienKham_ClsClient();
		private readonly PhienKham_ThietBiClient _thietBiClient = new PhienKham_ThietBiClient();
		private readonly int _id;
		public ViewPhienKhamForm(int id)
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);
			_id = id;
			SetupDgvBenh();
			SetupDgvCls();
			SetupDgvThietBi();
		}

		private async void ViewPhienKhamForm_Load(object sender, EventArgs e)
		{
			await LoadDataAsync();
		}

		private async Task LoadDataAsync()
		{
			try
			{
				var phienKham = await _client.GetByIdAsync(_id);

				if (phienKham == null)
					return;

				lbPhienKhamId.Text = phienKham.PhienKhamID.ToString();
				lbCaKhamValue.Text = phienKham.CaKhamID.ToString();
				lbNameBnValue.Text = phienKham.BenhNhan;
				lbNameBsValue.Text = phienKham.NhanVien;
				lbPhongValue.Text = phienKham.PhongChucNangID?.ToString();
				lbGhiChuValue.Text = phienKham.GhiChu ?? "";
				lbTrieuChungValue.Text = phienKham.TrieuChung ?? "";
				lbChanDoanCuoiValue.Text = phienKham.ChanDoanCuoi ?? "";
				lbTrangThaiValue.Text = phienKham.TrangThai;
				lbNgayKhamValue.Text = phienKham.NgayKham.ToString("dd/MM/yyyy");

				// ===== load bảng =====

				var benh = await _benhClient.GetByPhienKhamAsync(_id);
				var cls = await _clsClient.GetByPhienKhamAsync(_id);
				var thietBi = await _thietBiClient.GetByPhienKhamAsync(_id);

				dgvBenh.DataSource = benh;
				dgvCls.DataSource = cls;
				dgvThietBi.DataSource = thietBi;
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Không thể tải dữ liệu: " + ex.Message);
			}
		}
		private void SetupDgvBenh()
		{
			SetupDatagridview.ApplyGridStyle(dgvBenh);

			dgvBenh.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "LoaiBenh",
				HeaderText = "Loại bệnh",
				FillWeight = 30
			});

			dgvBenh.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "LoaiChanDoan",
				DataPropertyName = "LoaiChanDoan",
				HeaderText = "Loại chẩn đoán",
				FillWeight = 20
			});

			dgvBenh.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "GhiChu",
				DataPropertyName = "GhiChu",
				HeaderText = "Ghi chú",
				FillWeight = 50
			});

			dgvBenh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		}
		private void SetupDgvCls()
		{
			SetupDatagridview.ApplyGridStyle(dgvCls);

			dgvCls.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "PhienKhamCLSID",
				DataPropertyName = "PhienKhamCLSID",
				HeaderText = "ID",
				Visible = false
			});

			dgvCls.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TenCLS",
				DataPropertyName = "TenCLS",
				HeaderText = "Tên CLS",
				FillWeight = 25
			});

			dgvCls.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TrangThai",
				DataPropertyName = "TrangThai",
				HeaderText = "Trạng thái",
				FillWeight = 20
			});

			dgvCls.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "KetQua",
				DataPropertyName = "KetQua",
				HeaderText = "Kết quả",
				FillWeight = 35
			});

			dgvCls.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "NgayThucHien",
				DataPropertyName = "NgayThucHien",
				HeaderText = "Ngày thực hiện",
				DefaultCellStyle = new DataGridViewCellStyle
				{
					Format = "dd/MM/yyyy"
				},
				FillWeight = 20
			});

			dgvCls.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		}
		private void SetupDgvThietBi()
		{
			SetupDatagridview.ApplyGridStyle(dgvThietBi);

			dgvThietBi.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TenThietBi",
				DataPropertyName = "TenThietBi",
				HeaderText = "Thiết bị",
				FillWeight = 40
			});

			dgvThietBi.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TenPhong",
				DataPropertyName = "TenPhong",
				HeaderText = "Phòng",
				FillWeight = 30
			});

			dgvThietBi.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "GhiChu",
				DataPropertyName = "GhiChu",
				HeaderText = "Ghi chú",
				FillWeight = 30
			});

			dgvThietBi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		}
		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnToaThuoc_Click(object sender, EventArgs e)
		{
			var frm = new ViewToaThuoc(_id);
			frm.ShowDialog();
		}
	}
}
