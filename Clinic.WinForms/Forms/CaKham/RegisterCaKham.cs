using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.CaKham
{
	public partial class RegisterCaKhamForm : Form
	{
		private readonly CaKhamClient _client = new CaKhamClient();
		private readonly ThongTinClient _ttClient = new ThongTinClient();
		private readonly int _id;
		public RegisterCaKhamForm(int id)
		{
			InitializeComponent();
			_id = id;
		}
		private async Task LoadCombobox()
		{

			var list = await _ttClient.GetComboboxAsync();

			if (list == null) return;

			list.Insert(0, new ComboboxResult
			{
				Id = 0,
				Name = "Chọn bệnh nhân"
			});
			cbbBenhNhan.DataSource = null;
			cbbBenhNhan.DisplayMember = "Name";
			cbbBenhNhan.ValueMember = "Id";
			cbbBenhNhan.DataSource = list;
			cbbBenhNhan.MaxDropDownItems = 6;
			cbbBenhNhan.AutoCompleteSource = AutoCompleteSource.ListItems;
			cbbBenhNhan.AutoCompleteMode = AutoCompleteMode.None;

			cbbBenhNhan.SelectedIndex = 0;
			cbbBenhNhan.Focus();
		}


		private async void RegisterCaKhamForm_Load(object sender, EventArgs e)
		{
			FormDragHelper.EnableDrag(pnlHeader, this);
			lbMa.Text = "CK"+_id.ToString();
			await LoadCombobox();
		}

		private async void btnLuu_Click(object sender, EventArgs e)
		{
			if(cbbBenhNhan.SelectedIndex == 0 || string.IsNullOrWhiteSpace(cbbBenhNhan.Text))
			{
				MessageHelper.ShowMessage("Vui lòng chọn bệnh nhân");
				return;
			}
			if (string.IsNullOrWhiteSpace(txtGhiGhu.Text))
			{
				MessageHelper.ShowMessage("Vui lòng nhập ghi chú");
				return;
			}
			if (string.IsNullOrWhiteSpace(txtLyDo.Text))
			{
				MessageHelper.ShowMessage("Vui lòng nhập lý do");
				return;
			}

			try
			{
				var dto = new DangKyCaKhamDTO
				{
					ThongTinID = Convert.ToInt32(cbbBenhNhan.SelectedValue),
					LyDoKham = txtLyDo.Text,
					GhiChu = txtGhiGhu.Text,
					NgayDat = DateTime.Now
				};
				var data = await _client.DangKyAsync(_id, dto);
				MessageHelper.ShowMessage("Đăng ký thành công!");

				DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Lỗi: "+  ex.Message);
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
