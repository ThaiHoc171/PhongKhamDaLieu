using System.Windows;
using System.Xml.Linq;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.CaKham;
public partial class AddCaKham : Window
{
	public AddCaKham()
	{
		InitializeComponent();
		dtpBatDau.SelectedDate = DateTime.Today;
		dtpKetThuc.SelectedDate = DateTime.Today.AddDays(1);
	}
	private readonly CaKhamClient _client = new();
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
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (dtpBatDau.SelectedDate == null || dtpKetThuc.SelectedDate == null)
		{
			SnackbarHelper.ShowError("Vui lòng chọn ngày bắt đầu và kết thúc!");
			return;
		}
		if (dtpBatDau.SelectedDate > dtpKetThuc.SelectedDate)
		{
			SnackbarHelper.ShowError("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc!");
			return;
		}

		var req = new CaKhamGenerate
		{
			TuNgay = dtpBatDau.SelectedDate.Value,
			DenNgay = dtpKetThuc.SelectedDate.Value,
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
