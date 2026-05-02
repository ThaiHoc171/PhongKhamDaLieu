using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.ChiTietPhong;

public partial class AddThietBiPhong : Window
{
    public AddThietBiPhong(int phong,string name)
    {
        InitializeComponent();
		LoadComboBox();
		_id = phong;
		txtPhong.Text = name;
	}
	private readonly int _id;
	private readonly ChiTietPCNThietBiClient _client = new();
	private ChiTietPCNThietBiUpdate _current = new();
	private readonly PhongChucNangClient _phongClient = new();
	private readonly ThietBiClient _thietBiClient = new();
	private async void LoadComboBox()
	{
		var listThietBi = await _thietBiClient.GetCombobox();
		if (listThietBi.Success)
		{
			cboThietBi.ItemsSource = listThietBi.Data;
			cboThietBi.DisplayMemberPath = "Name";
			cboThietBi.SelectedValuePath = "Id";
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
			SnackbarHelper.ShowError("Vui lòng nhập mã tài sản");
			return;
		}
		if(cboThietBi.SelectedValue == null)
		{
			SnackbarHelper.ShowError("Vui lòng chọn thiết bị");
			return;
		}
		var req = new ChiTietPCNThietBiRequest
		{
			PhongChucNangID = _id,
			ThietBiID = (int)cboThietBi.SelectedValue,
			MaTaiSan = txtName.Text.Trim(),
			GhiChu = txtNote.Text.Trim()
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

