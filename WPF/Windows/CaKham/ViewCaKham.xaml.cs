using System.Windows;
using WPF.Client;
using WPF.Common;

namespace WPF.Windows.CaKham;
public partial class ViewCaKham : Window
{
	public ViewCaKham(int id)
	{
		InitializeComponent();
		_id = id;
	}
	private readonly int _id;
	private readonly CaKhamClient _client = new();
	private async void ViewCaKham_Loaded(object sender, RoutedEventArgs e)
	{
		try
		{
			var result = await _client.GetDetail(_id);
			if (result?.Data == null)
			{
				SnackbarHelper.ShowError("Không tìm thấy ca khám.");
				Close();
				return;
			}
			txtID.Text = _id.ToString();
			txtName.Text = result.Data.TenKhungGio + " / " + result.Data.NgayKham.ToString("dd/MM/yyyy");
			txtUser.Text = result.Data.HoTen;
			txtStatus.Text = result.Data.TrangThai;
			txtLyDo.Text = result.Data.LyDoKham;
			txtGhiChu.Text = result.Data.GhiChu;
			dtpRegistration.SelectedDate = result.Data.NgayDat;
		}
		catch (Exception)
		{
			SnackbarHelper.ShowError("Có lỗi xảy ra, vui lòng thử lại!");
			this.Close();
		}
	}
	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}

	private void btnHuy_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}


}