using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.LieuTrinh;

public partial class UpdateLieuTrinh : Window
{
	public UpdateLieuTrinh(int lieuTrinhId)
	{
		InitializeComponent();
		_id = lieuTrinhId;
	}

	private readonly int _id;
	private readonly LieuTrinhDieuTriClient _client = new();
	private LieuTrinhDieuTriUpdateDTO _current = new();

	private async void UpdateLieuTrinh_Loaded(object sender, RoutedEventArgs e)
	{
		var result = await _client.Detail(_id);

		if (!result.Success || result.Data == null)
		{
			await MessageHelper.ShowMessage("Không tìm thấy liệu trình.");
			Close();
			return;
		}

		var data = result.Data;

		txtPatient.Text = data.BenhNhan.Name;
		txtName.Text = _current.TenLieuTrinh = data.TenLieuTrinh;
		txtNumber.Text = (_current.TongSoBuoi = data.TongSoBuoi).ToString();
		dtpNgayBatDau.SelectedDate = data.NgayBatDau;
		dtpNgayKetThuc.SelectedDate = _current.NgayKetThuc = data.NgayKetThuc ?? DateTime.Today;
		txtNotes.Text = data.GhiChu;
	}

	private void Header_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (e.LeftButton == MouseButtonState.Pressed)
			DragMove();
	}

	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
	}

	private void txtNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
	{
		e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
	}

	private async Task<bool> Validate()
	{
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập tên liệu trình!");
			return false;
		}

		if (!int.TryParse(txtNumber.Text, out int soBuoi) || soBuoi <= 0)
		{
			await MessageHelper.ShowMessage("Số buổi không hợp lệ!");
			return false;
		}

		if (dtpNgayKetThuc.SelectedDate == null)
		{
			await MessageHelper.ShowMessage("Vui lòng chọn ngày kết thúc!");
			return false;
		}

		if (dtpNgayBatDau.SelectedDate.HasValue &&
			dtpNgayKetThuc.SelectedDate.Value.Date < dtpNgayBatDau.SelectedDate.Value.Date)
		{
			await MessageHelper.ShowMessage("Ngày kết thúc không thể trước ngày bắt đầu!");
			return false;
		}

		return true;
	}

	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (!await Validate()) return;

		var req = new LieuTrinhDieuTriUpdateDTO
		{
			TenLieuTrinh = txtName.Text.Trim(),
			TongSoBuoi = Convert.ToInt32(txtNumber.Text),
			NgayKetThuc = dtpNgayKetThuc.SelectedDate!.Value,
		};

		if (req.TenLieuTrinh == _current.TenLieuTrinh &&
			req.TongSoBuoi == _current.TongSoBuoi &&
			req.NgayKetThuc == _current.NgayKetThuc)
		{
			await MessageHelper.ShowMessage("Không có thay đổi nào được thực hiện!");
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
				await MessageHelper.ShowMessage(result.Message);
			}
		}
		catch
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
		Close();
	}
}