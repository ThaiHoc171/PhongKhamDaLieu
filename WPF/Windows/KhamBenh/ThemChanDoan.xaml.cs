using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.KhamBenh;

public partial class ThemChanDoan : Window
{
    public ThemChanDoan(int id)
    {
		_id = id;
		InitializeComponent();
		LoadComboBox();
	}
	private readonly int _id;
	private readonly PhienKhamBenhClient _client = new();
	private readonly LoaiBenhClient _loaibenh = new();
	private async void LoadComboBox()
	{
		var list = await _loaibenh.Combobox();
		if (list.Success)
		{
			cboDisease.ItemsSource = list.Data;
			cboDisease.DisplayMemberPath = "Name";
			cboDisease.SelectedValuePath = "Id";
			cboDisease.SelectedIndex = -1;
		}
		cboDiagnosisType.ItemsSource = new List<string> { "Chẩn đoán chính", "Chẩn đoán phát sinh" };
		cboDiagnosisType.SelectedIndex = -1;
	}
	private bool ValidateInput()
	{
		if (cboDisease.SelectedIndex < 0 )
		{
			SnackbarHelper.ShowError("Vui lòng chọn loại bệnh!");
			return false;
		}
		if (cboDiagnosisType.SelectedIndex < 0)
		{
			SnackbarHelper.ShowError("Vui lòng chọn loại chẩn đoán!");
			return false;
		}
		return true;
	}
	private async void btnLuu_Click(object sender, EventArgs e)
	{
		if (!ValidateInput())
			return;
		var req = new PhienKhamBenhRequestDTO
		{
			PhienKhamID = _id,
			LoaiBenhID = (int)cboDisease.SelectedValue,
			LoaiChanDoan = cboDiagnosisType.SelectedItem.ToString()!,
			GhiChu = txtNotes.Text.Trim(),
		};

		try
		{
			btnLuu.IsEnabled = false;
			btnHuy.IsEnabled = false;

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
