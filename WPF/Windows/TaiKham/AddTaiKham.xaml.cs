using System.Linq;
using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.TaiKham;
public partial class AddTaiKham : Window
{
	public AddTaiKham(int phienKhamId, int benhNhanId)
	{
		InitializeComponent();
		dtpDate.SelectedDate = DateTime.Now;
		_id = phienKhamId;
		_benhNhanId = benhNhanId;
		txId.Text = _id.ToString();
		Loaded += async (_, __) => await LoadData();
	}
	private readonly int _id;
	private readonly int _benhNhanId;
	private int _thongTinId;
	private readonly TaiKhamClient _client = new();
	private readonly KhungGioKhamClient _khungGio = new();
	private readonly CaKhamClient _caKham = new();
	private readonly BenhNhanClient _benhNhan = new();
	private async Task LoadData()
	{
		var res = await _benhNhan.Detail(_benhNhanId);
		if(res.Success && res.Data != null)
		{
			txtName.Text = res.Data.HoTen;
			_thongTinId = res.Data.ThongTinID;
		}

		await LoadCombobox();
	}
	private async Task LoadCombobox()
	{
		if (dtpDate.SelectedDate == null)
			return;

		DateTime ngay = dtpDate.SelectedDate.Value;

		if (Session.NhanVienId == null)
		{
			SnackbarHelper.ShowError("Không xác định được nhân viên kê đơn!");
			return;
		}

		int nhanVienId = Session.NhanVienId.Value;

		var comboRes = await _khungGio.GetCombobox();

		if (!comboRes.Success || comboRes.Data == null)
		{
			SnackbarHelper.ShowError(comboRes.Message);
			return;
		}

		var all = comboRes.Data;

		var slotRes = await _caKham.GetKhungGioTrong(ngay, "Khám", nhanVienId);

		if (!slotRes.Success || slotRes.Data == null)
		{
			SnackbarHelper.ShowError(slotRes.Message);
			return;
		}

		var filtered = all
			.Where(x => slotRes.Data.Contains(x.Id))
			.ToList();

		cboKhungGio.ItemsSource = filtered;
		cboKhungGio.DisplayMemberPath = "Name";
		cboKhungGio.SelectedValuePath = "Id";

		if (filtered.Any())
			cboKhungGio.SelectedIndex = 0;
	}
	private async void dtpDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
	{
		await LoadCombobox();
	}
	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
	}
	private async void btnSave_Click(object sender, EventArgs e)
	{
		if (dtpDate.SelectedDate < DateTime.Now)
			SnackbarHelper.ShowError("Ngày tái khám không thể ở quá khứ!");

		DateTime ngay = dtpDate.SelectedDate!.Value;
		if (Session.NhanVienId == null)
		{
			SnackbarHelper.ShowError("Không xác định được nhân viên kê đơn!");
			return;
		}
		int nhanVienId = Session.NhanVienId ?? 0;
		if (cboKhungGio.SelectedIndex == -1)
		{
			SnackbarHelper.ShowError("Vui lòng chọn khung giờ!");
			return;
		}
		int khungGio = (int)cboKhungGio.SelectedValue;
		var cakham = await _caKham.GetCaKhamTrong(ngay, khungGio, "Khám", nhanVienId);
		var caKhamReq = new CaKhamRegister
		{
			ThongTinID = _thongTinId,
			LyDoKham = "Tái khám",
			GhiChu = "",
			NgayDat = DateTime.Today
		};
		var res = await _caKham.Register(cakham.Data, caKhamReq);
		if (!res.Success)
		{	
			SnackbarHelper.ShowError("Đăng ký ca cho tái khám thất bại!\n" + res.Message);
			return;
		}
		var req = new TaiKhamRequestDTO
		{
			PhienKhamID = _id,
			CaKhamID = cakham.Data,
			LyDo = String.IsNullOrWhiteSpace(txtReason.Text?.Trim()) ? null : txtReason.Text.Trim(),
			NgayDuKien = dtpDate.SelectedDate!.Value,
		};
		try
		{
			ToggleUI(false);
			var result = await _client.Create(req);

			if (result.Success)
			{
				this.DialogResult = true;
				this.Close();
			}
			else
			{
				SnackbarHelper.ShowError(result.Message);
			}
		}
		catch (Exception)
		{
			SnackbarHelper.ShowError("Có lỗi xảy ra, vui lòng thử lại!");
		}
		finally
		{
			ToggleUI(true);
		}
	}

	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}
