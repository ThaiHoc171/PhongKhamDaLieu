using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.PhongChucNang
{
	public partial class EditPCNForm : Form
	{
		private readonly int _pcnId;
		private readonly PhongChucNangClient _pcnClient;
		public EditPCNForm(int pcnId)
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);
			_pcnId = pcnId;
			_pcnClient = new PhongChucNangClient();
		}
		public async Task LoadDataAsync()
		{
			var pcn = await _pcnClient.GetPhongByIdAsync(_pcnId);
			if (pcn == null)
			{
				MessageHelper.ShowMessage("Không tìm thấy phòng chức năng.");
				Close();
				return;
			}
			txtTenPhong.Text = pcn.TenPhong;
			txtLoaiPhong.Text = pcn.LoaiPhong;
			txtMoTa.Text = pcn.MoTa;
		}

		private async void EditPCNForm_Load(object sender, System.EventArgs e)
		{
			await LoadDataAsync();
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private async void btnLuu_Click(object sender, System.EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtTenPhong.Text))
			{
				MessageHelper.ShowMessage("Vui lòng nhập tên phòng.");
				return;
			}

			if (string.IsNullOrWhiteSpace(txtLoaiPhong.Text))
			{
				MessageHelper.ShowMessage("Vui lòng nhập loại phòng.");
				return;
			}

			var dto = new PCNUpdateDTO
			{
				TenPhong = txtTenPhong.Text.Trim(),
				LoaiPhong = txtLoaiPhong.Text.Trim(),
				MoTa = txtMoTa.Text.Trim()
			};

			btnLuu.Enabled = false;

			var result = await _pcnClient.UpdateAsync(_pcnId, dto);

			btnLuu.Enabled = true;

			if (result)
			{
				MessageHelper.ShowMessage("Cập nhật phòng chức năng thành công.");
				this.DialogResult = DialogResult.OK; 
				this.Close();
			}
			else
			{
				MessageHelper.ShowMessage("Cập nhật thất bại.");
			}
		}
	}
}
