using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.HSBenhAn;

public partial class CapNhatHoSo : Window
{
	public CapNhatHoSo(int id, string name)
	{
		InitializeComponent();
		_id = id;
		txtName.Text = name;
	}
	private readonly int _id;
	private HoSoBenhAnReadModel? _data;
	private readonly HoSoBenhAnClient _client = new();
	private async void CapNhatHoSo_Loaded(object sender, RoutedEventArgs e)
	{
		try
		{
			var result = await _client.GetByBenhNhanId(_id);

			if (!result.Success || result.Data == null)
			{
				SnackbarHelper.ShowError(result.Message ?? "Không tìm thấy dữ liệu!");
				Close();
				return;
			}
			_data = result.Data;
			txtAllergy.Text = _data.DiUng ?? "";
			txtUnderlying.Text = _data.BenhNen ?? "";
			txtMedHistory.Text = _data.TienSuBenh ?? "";
			txtFamHistory.Text = _data.TienSuGiaDinh ?? "";
			txtLifestyle.Text = _data.ThoiQuenSong ?? "";
			txtNotes.Text = _data.ThongTinKhac ?? "";
			dtpDateCreate.SelectedDate = _data.NgayTao;
			dtpDateUpdate.SelectedDate = _data.NgayCapNhat;
		}
		catch
		{
			SnackbarHelper.ShowError("Không thể tải dữ liệu, vui lòng thử lại!");
			Close();
		}
	}
	private string Normalize(string? text)
	{
		return string.IsNullOrWhiteSpace(text) ? "Không có" : text.Trim();
	}
	private bool IsChanged(HoSoBenhAnReadModel model)
	{
		return model.DiUng != Normalize(txtAllergy.Text)
			|| model.BenhNen != Normalize(txtUnderlying.Text)
			|| model.TienSuBenh != Normalize(txtMedHistory.Text)
			|| model.TienSuGiaDinh != Normalize(txtFamHistory.Text)
			|| model.ThoiQuenSong != Normalize(txtLifestyle.Text)
			|| model.ThongTinKhac != Normalize(txtNotes.Text);
	}

	private async void btnLuu_Click(object sender, RoutedEventArgs e)
	{
		if (_data == null)
		{
			SnackbarHelper.ShowError("Dữ liệu chưa được tải!");
			return;
		}

		if (!IsChanged(_data))
		{
			SnackbarHelper.ShowWarning("Không có thay đổi nào được thực hiện!");
			return;
		}

		var req = new HoSoBenhAnUpdate
		{
			DiUng = Normalize(txtAllergy.Text),
			BenhNen = Normalize(txtUnderlying.Text),
			TienSuBenh = Normalize(txtMedHistory.Text),
			TienSuGiaDinh = Normalize(txtFamHistory.Text),
			ThoiQuenSong = Normalize(txtLifestyle.Text),
			ThongTinKhac = Normalize(txtNotes.Text)
		};

		try
		{
			btnLuu.IsEnabled = false;
			btnHuy.IsEnabled = false;
			var result = await _client.Update(_data.HoSoBenhAnID, req);

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
			btnLuu.IsEnabled = true;
			btnHuy.IsEnabled = true;
		}
	}
	private void btnHuy_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}
	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			DragMove();
		}
	}
}
