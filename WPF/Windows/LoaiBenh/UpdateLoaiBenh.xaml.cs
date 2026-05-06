using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.LoaiBenh;

public partial class UpdateLoaiBenh : Window
{
	public UpdateLoaiBenh(int id)
	{
		InitializeComponent();
		_id = id;
		LoadComboBox();
	}

	private readonly int _id;
	private LoaiBenhReadModel? _data;
	private readonly LoaiBenhClient _client = new();

	private void LoadComboBox()
	{
		cboSeverity.ItemsSource = new List<string> { "nhẹ", "trung bình", "nặng" };
		cboPopularity.ItemsSource = new List<string> { "phổ biến", "ít gặp", "hiếm" };

		cboSeverity.SelectedIndex = -1;
		cboPopularity.SelectedIndex = -1;
	}

	private async void UpdateLoaiBenh_Loaded(object sender, RoutedEventArgs e)
	{
		try
		{
			var result = await _client.Detail(_id);

			if (!result.Success || result.Data == null)
			{
				await MessageHelper.ShowMessage(result.Message ?? "Không tìm thấy dữ liệu!");
				Close();
				return;
			}

			_data = result.Data;

			txtName.Text = _data.TenBenh;
			txtScienceName.Text = _data.TenKhoaHoc;
			txtGroup.Text = _data.NhomBenh;

			cboSeverity.SelectedItem = _data.MucDoNghiemTrong;
			cboPopularity.SelectedItem = _data.DoPhoBien;

			txtDescription.Text = _data.MoTa;
			dtpDateCreate.SelectedDate = _data.NgayTao;
		}
		catch
		{
			await MessageHelper.ShowMessage("Không thể tải dữ liệu, vui lòng thử lại!");
			Close();
		}
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

		if (cboSeverity.SelectedItem == null)
		{
			cboSeverity.Focus();
			await MessageHelper.ShowMessage("Vui lòng chọn độ nghiêm trọng!");
			return false;
		}

		if (cboPopularity.SelectedItem == null)
		{
			cboPopularity.Focus();
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

	private bool IsChanged(LoaiBenhReadModel model)
	{
		return model.TenBenh != txtName.Text
			|| model.TenKhoaHoc != txtScienceName.Text
			|| model.NhomBenh != txtGroup.Text
			|| model.MucDoNghiemTrong != cboSeverity.SelectedItem?.ToString()
			|| model.DoPhoBien != cboPopularity.SelectedItem?.ToString()
			|| model.MoTa != txtDescription.Text;
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
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (!await ValidateInput())
			return;

		if (_data == null)
		{
			await MessageHelper.ShowMessage("Dữ liệu chưa được tải!");
			return;
		}

		if (!IsChanged(_data))
		{
			await MessageHelper.ShowMessage("Không có thay đổi nào được thực hiện!");
			return;
		}

		var req = new LoaiBenhRequest
		{
			TenBenh = txtName.Text,
			TenKhoaHoc = txtScienceName.Text,
			NhomBenh = txtGroup.Text,
			DoPhoBien = cboPopularity.SelectedItem!.ToString()!,
			MucDoNghiemTrong = cboSeverity.SelectedItem!.ToString()!,
			MoTa = txtDescription.Text
		};

		try
		{
			ToggleUI(false);
			var result = await _client.Update(_id, req);

			if (result.Success)
			{
				DialogResult = true;
				Close();
			}
			else
			{
				await MessageHelper.ShowMessage(result.Message);
			}
		}
		catch
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
		Close();
	}
}