using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Clinic.WinForms.Forms.BenhNhan
{
	public partial class ViewHoSoBenhAnForm : Form
	{
		private readonly int _id;
		private HoSoBenhAnClient _client;
		private int _hoSoId;
		public ViewHoSoBenhAnForm(int id)
		{
			InitializeComponent();
			_id = id;
			_client = new HoSoBenhAnClient();
		}
		private async void ViewHoSoBenhAnForm_Load(object sender, EventArgs e)
		{
			await LoadData();
		}
		private async Task LoadData()
		{
			try
			{
				var data = await _client.GetByBenhNhanAsync(_id);
				if (data == null)
				{
					MessageHelper.ShowMessage("Chưa có hồ sơ bệnh án");
					return;
				}
				_hoSoId = data.HoSoBenhAnID;
				lbMa.Text = "BN" + _id.ToString("D3");
				lbMaHS_value.Text = "HS" + data.HoSoBenhAnID.ToString("D3");
				txtBenhNen.Text = data.BenhNen;
				txtDiUng.Text = data.DiUng;
				txtThoiQuenSong.Text = data.ThoiQuenSong;
				txtThongTinKhac.Text = data.ThongTinKhac;
				txtTienSuBenh.Text = data.TienSuBenh;
				txtTienSuGiaDinh.Text = data.TienSuGiaDinh;
				lbNgayTao_value.Text = data.NgayTao.ToString("dd/MM/yyyy HH:mm");
				lbNgayCapNhat_value.Text = data.NgayCapNhat.ToString("dd/MM/yyyy HH:mm");
				SetReadOnlyMode(true);
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Lỗi tải hồ sơ: " + ex.Message);
			}
		}
		private void SetReadOnlyMode(bool isReadOnly)
		{
			txtBenhNen.ReadOnly = isReadOnly;
			txtDiUng.ReadOnly = isReadOnly;
			txtThoiQuenSong.ReadOnly = isReadOnly;
			txtThongTinKhac.ReadOnly = isReadOnly;
			txtTienSuBenh.ReadOnly = isReadOnly;
			txtTienSuGiaDinh.ReadOnly = isReadOnly;
			btnLuu.Enabled = !isReadOnly;
		}
		private void btnEdit_Click(object sender, EventArgs e)
		{
			SetReadOnlyMode(false);
		}
		private async void btnLuu_Click(object sender, EventArgs e)
		{
			try
			{
				btnLuu.Enabled = false;
				var dto = new HoSoBenhAnUpdateDTO
				{
					BenhNen = txtBenhNen.Text.Trim(),
					DiUng = txtDiUng.Text.Trim(),
					TienSuBenh = txtTienSuBenh.Text.Trim(),
					TienSuGiaDinh = txtTienSuGiaDinh.Text.Trim(),
					ThoiQuenSong = txtThoiQuenSong.Text.Trim(),
					ThongTinKhac = txtThongTinKhac.Text.Trim(),
					NgayCapNhat = DateTime.Now
				};
				var result = await _client.UpdateAsync(_hoSoId, dto);
				if (!result)
				{
					MessageHelper.ShowMessage("Cập nhật hồ sơ thất bại");
					return;
				}
				MessageHelper.ShowMessage("Cập nhật hồ sơ thành công");
				await LoadData();
			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Lỗi: " + ex.Message);
			}
			finally
			{
				btnLuu.Enabled = true;
			}
		}
		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}