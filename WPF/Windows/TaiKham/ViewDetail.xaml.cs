using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.TaiKham;


public partial class ViewDetail : Window
{
	public ViewDetail(int TaiKhamId)
	{
		InitializeComponent();
		Loaded += async (_, __) => await LoadData();
		_id = TaiKhamId;
	}
	private readonly int _id;
	private readonly TaiKhamClient _client = new();
	private async Task LoadData()
	{
		var res = await _client.Detail(_id);
		if (!res.Success)
		{
			SnackbarHelper.ShowError("Không tìm thấy phiếu tái khám!");
			this.Close();
		}
		if(res.Data == null)
			return;
		var data = res.Data;
		txtTaiKhamId.Text = _id.ToString();
		txtPhienKhamId.Text = data.PhienKhamID.ToString();
		txtBenhNhan.Text = data.BenhNhan.Name;
		txtReason.Text = data.LyDo;
		dtpDate.SelectedDate = data.NgayDuKien.Date;
		dtpDateCreate.SelectedDate = data.NgayTao.Date;
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
