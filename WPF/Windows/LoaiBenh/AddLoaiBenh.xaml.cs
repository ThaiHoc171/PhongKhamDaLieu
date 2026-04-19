using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.LoaiBenh;

public partial class AddLoaiBenh : Window
{
	public AddLoaiBenh()
	{
		InitializeComponent();
		LoadComboBox();
	}
	private readonly LoaiBenhClient _client = new();
	private void LoadComboBox()
	{
		cboSeverity.ItemsSource = new List<string> { "nhẹ", "trung bình", "nặng" };
		cboPopularity.ItemsSource = new List<string> { "phổ biến", "ít gặp", "hiếm" };
		cboSeverity.SelectedIndex = -1;
		cboPopularity.SelectedIndex = -1;
	}
	private bool ValidateInput()
	{
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			txtName.Focus();
			SnackbarHelper.ShowError("Vui lòng nhập tên bệnh!");
			return false;
		}
		if (string.IsNullOrWhiteSpace(txtScienceName.Text))
		{
			txtScienceName.Focus();
			SnackbarHelper.ShowError("Vui lòng nhập tên khoa học!");
			return false;
		}
		if (string.IsNullOrWhiteSpace(txtGroup.Text))
		{
			txtGroup.Focus();
			SnackbarHelper.ShowError("Vui lòng nhập nhóm bệnh!");
			return false;
		}
		if (cboSeverity.SelectedIndex == -1)
		{
			SnackbarHelper.ShowError("Vui lòng chọn độ nghiêm trọng!");
			return false;
		}
		if (cboPopularity.SelectedIndex == -1 )
		{
			SnackbarHelper.ShowError("Vui lòng chọn độ phổ biến!");
			return false;
		}
		if (string.IsNullOrWhiteSpace(txtDescription.Text))
		{
			txtDescription.Focus();
			SnackbarHelper.ShowError("Vui lòng nhập mô tả!");
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
		var req = new LoaiBenhRequest
		{
			TenBenh = txtName.Text,
			TenKhoaHoc = txtScienceName.Text,
			NhomBenh = txtGroup.Text,
			DoPhoBien = cboPopularity.Text,
			MucDoNghiemTrong = cboSeverity.Text,
			MoTa = txtDescription.Text
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
