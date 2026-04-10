using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.HSBenhAn;

public partial class ThemHoSo : Window
{
	public ThemHoSo(int id, string name)
	{
		InitializeComponent();
		_id = id;
		txtName.Text = name;
	}
	private readonly int _id;
	private readonly HoSoBenhAnClient _client = new();
	private string Normalize(string? text)
	{
		return string.IsNullOrWhiteSpace(text) ? "Không có" : text.Trim();
	}
	private async void btnLuu_Click(object sender, EventArgs e)
	{
		var req = new HoSoBenhAnRequest
		{
			BenhNhanID = _id,
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
			btnLuu.IsEnabled = true;
			btnHuy.IsEnabled = true;
		}
	}

	private void btnHuy_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}

	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
}
