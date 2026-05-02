using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.NgayNghi;

public partial class UpdateNgayNghi : Window
{
	private readonly int _id;
	private readonly NgayNghiNhanVienClient _client = new();
	private NgayNghiUpdateRequestDTO _current = new();

	public UpdateNgayNghi(int id)
	{
		InitializeComponent();
		_id = id;
	}

	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
			DragMove();
	}

	private async void UpdateNgayNghi_Loaded(object sender, RoutedEventArgs e)
	{
		try
		{
			var result = await _client.GetById(_id);

			if (result == null || !result.Success || result.Data == null)
			{
				SnackbarHelper.ShowError("Không tìm thấy ngày nghỉ.");
				Close();
				return;
			}

			var data = result.Data;

			txtNhanVien.Text = data.NhanVien?.Name ?? "";
			dtpNgay.SelectedDate = _current.Ngay = data.Ngay;
			txtLyDo.Text = _current.LyDo = data.LyDo ?? "";
		}
		catch
		{
			SnackbarHelper.ShowError("Không thể tải dữ liệu ngày nghỉ.");
			Close();
		}
	}

	private bool IsChanged(NgayNghiUpdateRequestDTO newData)
	{
		return newData.Ngay != _current.Ngay
			|| newData.LyDo != _current.LyDo;
	}

	private bool ValidateForm()
	{
		if (dtpNgay.SelectedDate == null)
		{
			SnackbarHelper.ShowError("Vui lòng chọn ngày nghỉ!");
			return false;
		}
		return true;
	}

	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
	}

	// ================= LƯU =================
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm()) return;

		var req = new NgayNghiUpdateRequestDTO
		{
			Ngay = dtpNgay.SelectedDate ?? DateTime.Today,
			LyDo = txtLyDo.Text.Trim()
		};

		if (!IsChanged(req))
		{
			SnackbarHelper.ShowWarning("Không có thay đổi nào để lưu!");
			return;
		}

		try
		{
			ToggleUI(false);

			var result = await _client.Update(_id, req);

			if (result.Success)
			{
				DialogResult = true;
				Close();
			}
			else
			{
				SnackbarHelper.ShowError(result.Message);
			}
		}
		catch
		{
			SnackbarHelper.ShowError("Có lỗi xảy ra, vui lòng thử lại!");
		}
		finally
		{
			ToggleUI(true);
		}
	}

	// ================= HỦY =================
	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}
}