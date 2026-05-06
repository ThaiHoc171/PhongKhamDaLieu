using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.BuoiDieuTri;

public partial class AddBuoiDieuTri : Window
{
    public AddBuoiDieuTri(int lieuTrinhID, string name)
    {
        InitializeComponent();
		_id = lieuTrinhID;
		txtName.Text = name;
		Loaded += async (_, __) => await LoadData();
	}
	private readonly int _id;
	private readonly BuoiDieuTriClient _client = new();
	private readonly KhungGioKhamClient _khungGio = new();
	private readonly CaKhamClient _caKham = new();
	private readonly NhanVienClient _nhanVien = new();
	private async Task LoadData()
	{
		await LoadDoctors();
	}
	private async Task LoadDoctors()
	{
		var doctor = await _nhanVien.GetCombobox(2);
		if (!doctor.Success || doctor.Data == null)
		{
			await MessageHelper.ShowMessage(doctor.Message);
			return;
		}

		cboDoctor.ItemsSource = doctor.Data;
		cboDoctor.DisplayMemberPath = "Name";
		cboDoctor.SelectedValuePath = "Id";
	}
	private async Task LoadKhungGio()
	{
		if (cboDoctor.SelectedValue == null || dtpDate.SelectedDate == null)
			return;

		int nhanVienId = (int)cboDoctor.SelectedValue;
		DateTime ngay = dtpDate.SelectedDate.Value;

		var comboRes = await _khungGio.GetCombobox();
		if (!comboRes.Success || comboRes.Data == null)
		{
			await MessageHelper.ShowMessage(comboRes.Message);
			return;
		}

		var slotRes = await _caKham.GetKhungGioTrong(ngay, "Điều trị", nhanVienId);
		if (!slotRes.Success || slotRes.Data == null)
		{
			await MessageHelper.ShowMessage(slotRes.Message);
			return;
		}

		var filtered = comboRes.Data
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
		await LoadKhungGio();
	}
	private async void cboDoctor_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
	{
		await LoadKhungGio();
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
		if (dtpDate.SelectedDate == null || dtpDate.SelectedDate < DateTime.Now)
		{
			await MessageHelper.ShowMessage("Ngày điều trị không hợp lệ!");
			return;
		}

		if (cboDoctor.SelectedValue == null)
		{
			await MessageHelper.ShowMessage("Vui lòng chọn bác sĩ!");
			return;
		}

		if (cboKhungGio.SelectedValue == null)
		{
			await MessageHelper.ShowMessage("Vui lòng chọn khung giờ!");
			return;
		}

		DateTime ngay = dtpDate.SelectedDate.Value;
		int khungGio = (int)cboKhungGio.SelectedValue;
		int nhanVienId = (int)cboDoctor.SelectedValue;

		var cakham = await _caKham.GetCaKhamTrong(ngay, khungGio, "Điều trị", nhanVienId);

		if (!cakham.Success || cakham.Data == 0)
		{
			await MessageHelper.ShowMessage(cakham.Message);
			return;
		}
		var req = new BuoiDieuTriRequestDTO
		{
			LieuTrinhID = _id,
			CaKhamID = cakham.Data
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
				await MessageHelper.ShowMessage(result.Message);
			}
		}
		catch (Exception)
		{
			await MessageHelper.ShowMessage("Có lỗi xảy ra, vui lòng thử lại!");
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
