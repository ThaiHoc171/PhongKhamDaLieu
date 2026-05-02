using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;

namespace HoanMyClinic.Windows.BuoiDieuTri;
public partial class ViewBuoiDieuTri : Window
{
	public ViewBuoiDieuTri(int buoiDieuTriID,string name)
	{
		InitializeComponent();
		_id = buoiDieuTriID;
		txtBenhNhan.Text = name;
		Loaded += async (_, __) => await LoadData();
	}
	private readonly int _id;
	private readonly BuoiDieuTriClient _client = new();
	private readonly NhanVienClient _nhanVien = new();
	private async Task LoadCombobox()
	{
		var Doctor = await _nhanVien.GetCombobox(2);
		if (Doctor.Success)
		{
			cboDoctor.ItemsSource = Doctor.Data;
			cboDoctor.DisplayMemberPath = "Name";
			cboDoctor.SelectedValuePath = "Id";
			cboDoctor.SelectedIndex = -1;
		}
	}
	private async Task LoadData()
	{
		await LoadCombobox();
		var res = await _client.Detail(_id);
		if (!res.Success)
		{
			SnackbarHelper.ShowError("Không tìm thấy phiếu tái khám!");
			this.Close();
		}
		if (res.Data == null)
			return;
		var data = res.Data;
		txtBuoi.Text = data.SoBuoi.ToString();
		txtCaKhamId.Text =  data.CaKhamID.ToString();
		cboDoctor.SelectedIndex = (int)data.NhanVienID!;
		txtNotes.Text = data.GhiChu;
		dtpDate.SelectedDate = data.NgayDuKien;
		dtpDateThucHien.SelectedDate = data.NgayThucHien;
		txtTrangThai.Text = data.TrangThai;
	}

	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}

	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}
