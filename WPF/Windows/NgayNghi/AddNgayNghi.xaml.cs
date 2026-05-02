using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.NgayNghi;

public partial class AddNgayNghi : Window
{
	private readonly NgayNghiNhanVienClient _client = new();
	private readonly ChucVuClient _chucVuClient = new();
	private readonly NhanVienClient _nhanVienClient = new();

	public AddNgayNghi()
	{
		InitializeComponent();
		LoadComboBox();
		cboChucVu.SelectionChanged += CboChucVu_SelectionChanged;
	}

	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
			DragMove();
	}

	private async void LoadComboBox()
	{
		var res = await _chucVuClient.GetCombobox();
		if (res.Success)
		{
			cboChucVu.ItemsSource = res.Data;
			cboChucVu.DisplayMemberPath = "Name";
			cboChucVu.SelectedValuePath = "Id";
		}
	}

	private async void CboChucVu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
	{
		cboNhanVien.ItemsSource = null;
		cboNhanVien.SelectedValue = null;

		if (cboChucVu.SelectedValue is not int chucVuId) return;

		var res = await _nhanVienClient.GetCombobox(chucVuId);
		if (res.Success)
		{
			cboNhanVien.ItemsSource = res.Data;
			cboNhanVien.DisplayMemberPath = "Name";
			cboNhanVien.SelectedValuePath = "Id";
		}
	}

	private bool ValidateForm()
	{
		if (cboChucVu.SelectedValue == null)
		{
			SnackbarHelper.ShowError("Vui lòng chọn chức vụ!");
			cboChucVu.Focus();
			return false;
		}

		if (cboNhanVien.SelectedValue == null)
		{
			SnackbarHelper.ShowError("Vui lòng chọn nhân viên!");
			cboNhanVien.Focus();
			return false;
		}

		if (dtpNgay.SelectedDate == null)
		{
			SnackbarHelper.ShowError("Vui lòng chọn ngày nghỉ!");
			return false;
		}

		return true;
	}

	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
	}

	// ================= LƯU =================
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm()) return;

		try
		{
			ToggleUI(false);

			var req = new NgayNghiRequestDTO
			{
				NhanVienID = (int)(cboNhanVien.SelectedValue ?? 0),
				Ngay = dtpNgay.SelectedDate ?? DateTime.Today,
				LyDo = txtLyDo.Text.Trim()
			};

			var result = await _client.Create(req);

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
			SnackbarHelper.ShowError("Có lỗi xảy ra!");
		}
		finally
		{
			ToggleUI(true);
		}
	}

	// ================= HỦY =================
	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}
}