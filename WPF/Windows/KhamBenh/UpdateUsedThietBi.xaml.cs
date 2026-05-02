using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;

namespace HoanMyClinic.Windows.KhamBenh;

public partial class UpdateUsedThietBi : Window
{
	public UpdateUsedThietBi(int id, string name)
	{
		InitializeComponent();
		_id = id;
		txtName.Text = name;
	}
	private readonly int _id;
	private readonly PhienKhamThietBiClient _client = new();
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
	private async void btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			ToggleUI(false);

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
			ToggleUI(true);
		}
	}

	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}
