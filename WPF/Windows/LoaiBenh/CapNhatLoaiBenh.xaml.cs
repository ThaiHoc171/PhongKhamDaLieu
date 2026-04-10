using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.LoaiBenh;

public partial class CapNhatLoaiBenh : Window
{
	public CapNhatLoaiBenh(int id)
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

	private async void CapNhatLoaiBenh_Loaded(object sender, RoutedEventArgs e)
	{
		try
		{
			var result = await _client.Detail(_id);

			if (!result.Success || result.Data == null)
			{
				SnackbarHelper.ShowError(result.Message ?? "Không tìm thấy dữ liệu!");
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
			SnackbarHelper.ShowError("Không thể tải dữ liệu, vui lòng thử lại!");
			Close();
		}
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

		if (cboSeverity.SelectedItem == null)
		{
			cboSeverity.Focus();
			SnackbarHelper.ShowError("Vui lòng chọn độ nghiêm trọng!");
			return false;
		}

		if (cboPopularity.SelectedItem == null)
		{
			cboPopularity.Focus();
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

	private bool IsChanged(LoaiBenhReadModel model)
	{
		return model.TenBenh != txtName.Text
			|| model.TenKhoaHoc != txtScienceName.Text
			|| model.NhomBenh != txtGroup.Text
			|| model.MucDoNghiemTrong != cboSeverity.SelectedItem?.ToString()
			|| model.DoPhoBien != cboPopularity.SelectedItem?.ToString()
			|| model.MoTa != txtDescription.Text;
	}

	private async void btnLuu_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateInput())
			return;

		if (_data == null)
		{
			SnackbarHelper.ShowError("Dữ liệu chưa được tải!");
			return;
		}

		if (!IsChanged(_data))
		{
			SnackbarHelper.ShowWarning("Không có thay đổi nào được thực hiện!");
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
			btnLuu.IsEnabled = false;
			btnHuy.IsEnabled = false;
			var result = await _client.Update(_id, req);

			if (result.Success)
			{
				DialogResult = true;
				Close();
			}
			else
			{
				SnackbarHelper.ShowError(result.Message);
			}
		}
		catch
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
		Close();
	}
	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			DragMove();
		}
	}
}