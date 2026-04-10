using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.CaKham;

public partial class ThemLich : Window
{
	public ThemLich()
	{
		InitializeComponent();
	}
	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			DragMove();
		}
	}
	private readonly CaKhamClient _client = new();
	private async void btnAdd_Click(object sender, RoutedEventArgs e)
	{
		if (dtpStart.SelectedDate == null || dtpEnd.SelectedDate == null)
		{
			SnackbarHelper.ShowError("Vui lòng chọn ngày bắt đầu và kết thúc!");
			return;
		}
		if (dtpStart.SelectedDate > dtpEnd.SelectedDate)
		{
			SnackbarHelper.ShowError("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc!");
			return;
		}

		var req = new CaKhamGenerate
		{
			TuNgay = dtpStart.SelectedDate.Value,
			DenNgay = dtpEnd.SelectedDate.Value,
		};

		try
		{
			btnAdd.IsEnabled = false;
			btnCancel.IsEnabled = false;

			var result = await _client.AssignLichLamViec(req);

			if (result.Success && result.Data != null)
			{
				SnackbarHelper.ShowSuccess("Thêm lịch khám thành công!");
				txtComplete.Text = result.Data.SoCaDaCapNhat.ToString();
				txtInComplete.Text = result.Data.TongCaChuaGan.ToString();
				txtSum.Text = result.Data.DaThucHien.ToString();
				txtMessage.Text = result.Message;
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
			btnAdd.IsEnabled = true;
			btnCancel.IsEnabled = true;
		}
	}

	private void btnCancel_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = true;
		Close();
	}
}
