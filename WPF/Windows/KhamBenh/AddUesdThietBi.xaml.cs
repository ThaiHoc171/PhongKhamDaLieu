using System.Windows;
using System.Windows.Controls;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.KhamBenh;

public partial class AddUesdThietBi : Window
{
	public AddUesdThietBi(int id)
	{
		_id = id;
		InitializeComponent();
		LoadComboBox();
	}
	private readonly int _id;
	private readonly PhienKhamThietBiClient _client = new();
	private readonly ThietBiClient _thietBi = new();
	private readonly PhongChucNangClient _phongChucNang = new();
	private readonly PCNThietBiClient _pcnThietBi = new();
	private readonly ChiTietPCNThietBiClient _chiTietPCNThietBi = new();
	private async void LoadComboBox()
	{
		var pcn = await _phongChucNang.GetCombobox();
		if (pcn.Success)
		{
			cboRoom.ItemsSource = pcn.Data;
			cboRoom.DisplayMemberPath = "Name";
			cboRoom.SelectedValuePath = "Id";
			cboRoom.SelectedIndex = -1;
		}
		var tb = await _thietBi.GetCombobox();
		if (tb.Success)
		{
			cboEquipment.ItemsSource = tb.Data;
			cboEquipment.DisplayMemberPath = "Name";
			cboEquipment.SelectedValuePath = "Id";
			cboEquipment.SelectedIndex = -1;
		}
	}
	private async Task Load_Equipment()
	{
		if (cboRoom.SelectedValue == null)
			return;
		int pcnId = (int)cboRoom.SelectedValue;
		var result = await _pcnThietBi.GetCombobox(pcnId);
		if (result.Success && result.Data != null)
		{
			cboEquipment.ItemsSource = result.Data;
			cboEquipment.DisplayMemberPath = "Name";
			cboEquipment.SelectedValuePath = "Id";
			cboEquipment.SelectedIndex = -1;
		}
	}
	private async Task Load_Detail()
	{
		if (cboRoom.SelectedValue == null || cboEquipment.SelectedValue == null)
			return;

		int pcnId = (int)cboRoom.SelectedValue;
		int tbId = (int)cboEquipment.SelectedValue;

		var result = await _chiTietPCNThietBi.GetCombobox(pcnId, tbId);

		if (result.Success && result.Data != null)
		{
			cboEquipmentDetail.ItemsSource = result.Data;
			cboEquipmentDetail.DisplayMemberPath = "Name";
			cboEquipmentDetail.SelectedValuePath = "Id";
			cboEquipmentDetail.SelectedIndex = -1;
		}
	}
	private bool ValidateInput()
	{
		if (cboEquipmentDetail.SelectedIndex < 0)
		{
			SnackbarHelper.ShowError("Vui lòng chọn loại thiết bị!");
			return false;
		}
		return true;
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
		if (!ValidateInput())
			return;
		var req = new PhienKhamThietBiRequestDTO
		{
			PhienKhamID = _id,
			ChiTietID = (int)cboEquipmentDetail.SelectedValue,
			GhiChu = txtNotes.Text.Trim(),
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

	private async void cboRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		await Load_Equipment();
		await Load_Detail();
	}

	private async void cboEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		await Load_Detail();
	}
}
