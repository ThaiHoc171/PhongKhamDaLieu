using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.Forms.ToaThuoc;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.PhienKham
{
	public partial class StartPhienKhamForm : Form
	{
		private readonly PhienKhamClient _client = new PhienKhamClient();

		private readonly int _id;

		private string _imageTempPath = "";
		private string _imageFileName = "";

		public StartPhienKhamForm(int id)
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);

			_id = id;
		}

		private async void StartPhienKhamForm_Load(object sender, EventArgs e)
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
				lbNgayKhamValue.Text = phienKham.NgayKham.ToString("dd/MM/yyyy");
				lbTrangThai.Text = phienKham.TrangThai;

				if (!string.IsNullOrWhiteSpace(phienKham.HinhAnhJSON))
				{
					string path = Path.Combine(	Application.StartupPath,"Resources","Images", "KetQuaKham", phienKham.HinhAnhJSON);

					if (File.Exists(path))
					{
						using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
						{
							picKetQua.Image = Image.FromStream(stream);
						}

						picKetQua.SizeMode = PictureBoxSizeMode.Zoom;
					}
				}
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Không thể tải dữ liệu: " + ex.Message);
			}
		}

		private void btnThemAnh_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.Title = "Chọn ảnh kết quả";
				ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					_imageTempPath = ofd.FileName;

					using (var stream = new FileStream(_imageTempPath, FileMode.Open, FileAccess.Read))
					{
						picKetQua.Image = Image.FromStream(stream);
					}

					picKetQua.SizeMode = PictureBoxSizeMode.Zoom;
				}
			}
		}

		private void btnThemToaThuoc_Click(object sender, EventArgs e)
		{
			//var frm = new AddToaThuocForm(_id);
			//frm.ShowDialog();
		}

		private void btnToaThuoc_Click(object sender, EventArgs e)
		{
			var frm = new ViewToaThuoc(_id);
			frm.ShowDialog();
		}

		private void btnThemChanDoan_Click(object sender, EventArgs e)
		{
			//var frm = new AddChanDoanForm(_id);
			//frm.ShowDialog();
		}

		private void btnChiDinhCls_Click(object sender, EventArgs e)
		{
			//var frm = new AddChiDinhCLSForm(_id);
			//frm.ShowDialog();
		}

		private void btnThietbidung_Click(object sender, EventArgs e)
		{
			//var frm = new AddThietBiSuDungForm(_id);
			//frm.ShowDialog();
		}

		private async void btnKetThuc_Click(object sender, EventArgs e)
		{
			try
			{
				string fileName = null;

				if (!string.IsNullOrWhiteSpace(_imageTempPath))
				{
					string folder = Path.Combine(Application.StartupPath, "Resources", "Images", "PhienKham");

					if (!Directory.Exists(folder))
						Directory.CreateDirectory(folder);

					string ext = Path.GetExtension(_imageTempPath);

					fileName = $"pk{_id}_{DateTime.Now:yyyyMMddHHmmss}{ext}";

					string newPath = Path.Combine(folder, fileName);

					File.Copy(_imageTempPath, newPath, true);
				}

				//await _client.KetThucPhienKhamAsync(_id, fileName);

				MessageHelper.ShowMessage("Kết thúc phiên khám thành công!");
				this.DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Lỗi: " + ex.Message);
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}