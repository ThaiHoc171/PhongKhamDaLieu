using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.CanLamSang
{
	public partial class EditClsForm : Form
	{
		public EditClsForm(int id)
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);
			_client = new CanLamSangClient();
			_id = id;
		}
		private readonly CanLamSangClient _client;
		private readonly int _id;

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}
		private async Task LoadDataAsync()
		{
			try
			{
				var data = await _client.GetByIdAsync( _id );
				if (data != null)
				{
					txtMaCLS.Text = _id.ToString();
					txtTen.Text = data.TenCLS;
					txtLoai.Text = data.LoaiXetNghiem;
					txtMoTa.Text = data.MoTa;
				}
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Lỗi: " + ex.Message);
			}
		}

		private async void EditClsForm_Load(object sender, EventArgs e)
		{
			await LoadDataAsync();
		}

		private async void btnLuu_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtTen.Text))
			{
				MessageHelper.ShowMessage("Tên cận lâm sàng không được để trống!");
				return;
			}
			if (string.IsNullOrWhiteSpace(txtMoTa.Text))
			{
				MessageHelper.ShowMessage("Mô tả không được để trống!");
				return;
			}
			if (string.IsNullOrWhiteSpace(txtLoai.Text))
			{
				MessageHelper.ShowMessage("Loại xét nghiệm không được để trống!");
				return;
			}

			var dto = new CanLamSangRequestDTO
			{
				TenCLS = txtTen.Text,
				MoTa = txtMoTa.Text,
				LoaiXetNghiem = txtLoai.Text
			};
			var result = await _client.UpdateAsync(_id,dto);
			if (result)
				MessageHelper.ShowMessage("Thêm cận lâm sàng thành công.");
			else
				MessageHelper.ShowMessage("Thêm cận lâm sàng thất bại.");
			Close();
		}
	}
}
