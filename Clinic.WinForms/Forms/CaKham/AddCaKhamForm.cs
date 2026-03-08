using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.CaKham
{
	public partial class AddCaKhamForm : Form
	{
		public AddCaKhamForm()
		{
			InitializeComponent();
			FormDragHelper.EnableDrag(pnlHeader, this);
		}
		private readonly CaKhamClient _client = new CaKhamClient();
		private void AddCaKhamForm_Load(object sender, EventArgs e)
		{
			dtpNgayBatDau.Value = DateTime.Now;
			dtpNgayBatDau.Format = DateTimePickerFormat.Custom;
			dtpNgayBatDau.CustomFormat = "MM/dd/yyyy";

			dtpNgayKetThuc.Value = DateTime.Now.AddDays(1);
			dtpNgayKetThuc.Format = DateTimePickerFormat.Custom;
			dtpNgayKetThuc.CustomFormat = "MM/dd/yyyy";
		}

		private async void btnLuu_Click(object sender, EventArgs e)
		{
			if (dtpNgayBatDau.Value >= dtpNgayKetThuc.Value)
			{
				MessageHelper.ShowMessage("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!");
				return;
			}
			if (dtpNgayBatDau.Value.Date < DateTime.Today)
			{
				MessageHelper.ShowMessage("Ngày bắt đầu không được trong quá khứ!");
				return;
			}
			try
			{
				var dto = new TaoCaKhamDTO
				{
					NgayKham = dtpNgayBatDau.Value.Date,
					NgayKetThuc = dtpNgayKetThuc.Value.Date
				};

				var id = await _client.TaoMoiAsync(dto);

				MessageHelper.ShowMessage("Tạo ca khám thành công!");

				DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Tạo ca khám thất bại vui lòng kiểm tra lịch làm việc!\nLỗi:" + ex.Message);
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
