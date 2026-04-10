using System.Windows;
using WPF.Client;
using WPF.Common;

namespace WPF.Windows.KhamBenh;

public partial class CapNhatThietBiDung : Window
{
	public CapNhatThietBiDung(int id, string name)
	{
		InitializeComponent();
		_id = id;
		txtName.Text = name;
	}
	private readonly int _id;
	private readonly PhienKhamThietBiClient _client = new();
	private async void btnLuu_Click(object sender, EventArgs e)
	{
		try
		{
			btnLuu.IsEnabled = false;
			btnHuy.IsEnabled = false;
			
			if (string.IsNullOrWhiteSpace(txtNotes.Text))
			{
				SnackbarHelper.ShowWarning("Vui lòng nhập ghi chú");
				return;
			}
			var req = txtNotes.Text.Trim();
			var result = await _client.Update(_id,req);

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
