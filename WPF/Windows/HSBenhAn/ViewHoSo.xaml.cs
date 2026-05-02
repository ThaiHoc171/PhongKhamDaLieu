using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.HSBenhAn;


public partial class ViewHoSo : Window
{
	public ViewHoSo(int id, string name)
	{
		InitializeComponent();
		_id = id;
		txtName.Text = name;
		Loaded += async (_,__) => await LoadData();
	}
	private readonly int _id;
	private HoSoBenhAnReadModel? _data;
	private readonly HoSoBenhAnClient _client = new();
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
