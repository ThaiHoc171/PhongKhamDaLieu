using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.Thuoc;

public partial class UpdateThuoc : Window
{
	public UpdateThuoc(int id)
	{
		InitializeComponent();
		_id = id;
	}
	private readonly int _id;
	private readonly ThuocClient _client = new();
	private ThuocRequest _current = new();
	private async void UpdateThuoc_Loaded(object sender, RoutedEventArgs e)
	{
		var result = await _client.Detail(_id);
		if (result.Success && result.Data != null)
		{
			txtName.Text = _current.TenThuoc = result.Data.TenThuoc;
			txtActiveIngredient.Text = _current.HoatChat = result.Data.HoatChat;
		}
		else
		{
			await MessageHelper.ShowMessage(result.Message);
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
	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
	}
	private async void btnSave_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập tên thuốc!");
			return;
		}
		if (string.IsNullOrWhiteSpace(txtActiveIngredient.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập hoạt chất!");
			return;
		}
		var req = new ThuocRequest
		{
			TenThuoc = txtName.Text.Trim(),
			HoatChat = txtActiveIngredient.Text.Trim()
		};
		if(req == _current)
		{
			await MessageHelper.ShowMessage("Không có thay đổi nào để lưu!");
			return;
		}
		try
		{
			ToggleUI(false);
			var result = await _client.Update(_id,req);

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

}
