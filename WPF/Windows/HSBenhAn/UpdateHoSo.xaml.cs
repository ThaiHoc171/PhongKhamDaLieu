using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.HSBenhAn;

public partial class UpdateHoSo : Window
{
	public UpdateHoSo(int id, string name)
	{
		InitializeComponent();

		_id = id;
		txtName.Text = name;

		Loaded += async (_, __) => await LoadData();
	}

	private readonly int _id;
	private readonly HoSoBenhAnClient _client = new();

	private HoSoBenhAnReadModel? _current;

	// ================= LOAD =================
	private async Task LoadData()
	{
		try
		{
			var result = await _client.GetByBenhNhanId(_id);

			if (result?.Data == null)
			{
				SnackbarHelper.ShowError(result?.Message ?? "Không tìm thấy dữ liệu!");
				Close();
				return;
			}

			_current = result.Data;

			txtAllergy.Text = _current.DiUng ?? "";
			txtUnderlying.Text = _current.BenhNen ?? "";
			txtMedHistory.Text = _current.TienSuBenh ?? "";
			txtFamHistory.Text = _current.TienSuGiaDinh ?? "";
			txtLifestyle.Text = _current.ThoiQuenSong ?? "";
			txtNotes.Text = _current.ThongTinKhac ?? "";

			dtpDateCreate.SelectedDate = _current.NgayTao;
			dtpDateUpdate.SelectedDate = _current.NgayCapNhat;
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

	private bool IsChanged()
	{
		if (_current == null) return false;

		return
			Normalize(txtAllergy.Text) != _current.DiUng ||
			Normalize(txtUnderlying.Text) != _current.BenhNen ||
			Normalize(txtMedHistory.Text) != _current.TienSuBenh ||
			Normalize(txtFamHistory.Text) != _current.TienSuGiaDinh ||
			Normalize(txtLifestyle.Text) != _current.ThoiQuenSong ||
			Normalize(txtNotes.Text) != _current.ThongTinKhac;
	}
	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
	}

	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (_current == null)
		{
			SnackbarHelper.ShowError("Dữ liệu chưa được tải!");
			return;
		}

		if (!IsChanged())
		{
			SnackbarHelper.ShowWarning("Không có thay đổi nào!");
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
			ToggleUI(false);

			var result = await _client.Update(_current.HoSoBenhAnID, req);

			if (!result.Success)
			{
				SnackbarHelper.ShowError(result.Message);
				return;
			}

			DialogResult = true;
			Close();
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

	private void btnClose_Click(object sender, RoutedEventArgs e)
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