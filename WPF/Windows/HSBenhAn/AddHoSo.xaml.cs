using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.HSBenhAn;

public partial class AddHoSo : Window
{
	public AddHoSo(int id, string name)
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
	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
	}
	private async void btnSave_Click(object sender, EventArgs e)
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

	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
}
