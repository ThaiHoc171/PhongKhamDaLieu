using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.Thuoc
{
	public partial class UpdateThuocForm : Form
	{
		private readonly ThuocClient _client;
		private readonly int _id;

		public UpdateThuocForm(int id)
		{
			InitializeComponent();
			_id = id;
			_client = new ThuocClient();
			FormDragHelper.EnableDrag(pnlHeader, this);
		}

		private async Task LoadData()
		{
			var data = await _client.GetByIdAsync(_id);

			if (data == null) return;

			lbMa.Text = _id.ToString();
			txtTen.Text = data.TenThuoc;
			txtHoatChat.Text = data.HoatChat;
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private bool IsValid()
		{
			if (string.IsNullOrWhiteSpace(txtTen.Text))
				return false;

			if (string.IsNullOrWhiteSpace(txtHoatChat.Text))
				return false;

			return true;
		}

		private async void btnLuu_Click(object sender, EventArgs e)
		{
			try
			{
				if (!IsValid())
				{
					MessageHelper.ShowMessage("Vui lòng nhập đầy đủ thông tin!");
					return;
				}

				var dto = new ThuocRequestDTO
				{
					TenThuoc = txtTen.Text.Trim(),
					HoatChat = txtHoatChat.Text.Trim()
				};

				btnLuu.Enabled = false;

				var result = await _client.UpdateAsync(_id, dto);

				btnLuu.Enabled = true;

				if (result)
				{
					MessageHelper.ShowMessage("Cập nhật thuốc thành công!");
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
				else
				{
					MessageHelper.ShowMessage("Cập nhật thất bại!");
				}
			}
			catch (Exception ex)
			{
				btnLuu.Enabled = true;
				MessageHelper.ShowMessage("Lỗi: " + ex.Message);
			}
		}

		private async void UpdateThuocForm_Load(object sender, EventArgs e)
		{
			await LoadData();
		}
	}
}