using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WPF.Client;
using WPF.Common;

namespace WPF.Windows.TaiKham;

public partial class Confirm : Window
{
	public Confirm(int CaKhamId)
	{
		InitializeComponent();
		_id = CaKhamId;
	}
	private readonly int _id;
	private readonly TaiKhamClient _client = new();
	private void Header_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (e.LeftButton == MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
	}
	private async void btnSave_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txId.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập mã phiếu tái khám!");
			return;
		}
		int TaiKhamId = Convert.ToInt32(txId.Text);
		try
		{
			ToggleUI(false);
			var result = await _client.AssignCaKham(TaiKhamId,_id);

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
	private void TxId_PreviewTextInput(object sender, TextCompositionEventArgs e)
	{
		e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
	}
	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}
