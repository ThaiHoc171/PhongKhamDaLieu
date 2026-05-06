using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.LoaiBenh;

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
	private async Task<bool> ValidateInput()
	{
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			txtName.Focus();
			await MessageHelper.ShowMessage("Vui lòng nhập tên bệnh!");
			return false;
		}
		if (string.IsNullOrWhiteSpace(txtScienceName.Text))
		{
			txtScienceName.Focus();
			await MessageHelper.ShowMessage("Vui lòng nhập tên khoa học!");
			return false;
		}
		if (string.IsNullOrWhiteSpace(txtGroup.Text))
		{
			txtGroup.Focus();
			await MessageHelper.ShowMessage("Vui lòng nhập nhóm bệnh!");
			return false;
		}
		if (cboSeverity.SelectedIndex == -1)
		{
			await MessageHelper.ShowMessage("Vui lòng chọn độ nghiêm trọng!");
			return false;
		}
		if (cboPopularity.SelectedIndex == -1 )
		{
			await MessageHelper.ShowMessage("Vui lòng chọn độ phổ biến!");
			return false;
		}
		if (string.IsNullOrWhiteSpace(txtDescription.Text))
		{
			txtDescription.Focus();
			await MessageHelper.ShowMessage("Vui lòng nhập mô tả!");
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
		if (!await ValidateInput())
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
