using Microsoft.Win32;
using System.IO;
using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.NgayNghi;

public partial class ImportNgayNghi : Window
{
	private readonly NgayNghiNhanVienClient _client = new();
	private readonly ExcelClient _excel = new();

	// ================= TRẠNG THÁI =================
	private bool _isPreviewed = false;
	private bool _isValidated = false;
	private List<NgayNghiRequestDTO>? _previewData = null;

	public ImportNgayNghi()
	{
		InitializeComponent();
	}

	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
			DragMove();
	}

	// ================= RESET STATE =================
	private void ResetState()
	{
		_isPreviewed = false;
		_isValidated = false;
		_previewData = null;
		btnSave.IsEnabled = false;
		gridPreview.ItemsSource = null;
		lstErrors.ItemsSource = null;
		txtSummary.Text = "";
	}

	private void UpdateSaveButton()
	{
		btnSave.IsEnabled = _isPreviewed && _isValidated;
	}

	private void ToggleUI(bool isEnabled)
	{
		btnChooseFile.IsEnabled = isEnabled;
		cbSheet.IsEnabled = isEnabled;
		btnPreview.IsEnabled = isEnabled;
		btnValidate.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
	}

	// ================= CHỌN FILE =================
	private void btnChooseFile_Click(object sender, RoutedEventArgs e)
	{
		var dlg = new OpenFileDialog { Filter = "Excel (*.xlsx)|*.xlsx" };

		if (dlg.ShowDialog() == true)
		{
			txtFile.Text = dlg.FileName;
			ResetState();
			LoadSheets(dlg.FileName);
			SnackbarHelper.ShowSuccess($"Đã chọn file: {Path.GetFileName(dlg.FileName)}");
		}
	}

	private async void LoadSheets(string file)
	{
		var res = await _excel.GetSheets(file);
		if (!res.Success || res.Data == null || res.Data.Count == 0)
		{
			await MessageHelper.ShowMessage("Không lấy được danh sách sheet");
			cbSheet.ItemsSource = null;
			return;
		}
		cbSheet.ItemsSource = res.Data;
		cbSheet.SelectedIndex = 0;
	}

	// ================= PREVIEW =================
	private async void btnPreview_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtFile.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng chọn file Excel!");
			return;
		}
		if (cbSheet.SelectedItem == null)
		{
			await MessageHelper.ShowMessage("Vui lòng chọn sheet!");
			return;
		}

		try
		{
			ToggleUI(false);
			ResetState();

			var res = await _client.PreviewImport(txtFile.Text);

			if (!res.Success || res.Data == null)
			{
				await MessageHelper.ShowMessage(res.Message);
				return;
			}

			_previewData = res.Data;
			_isPreviewed = true;
			_isValidated = false;

			gridPreview.ItemsSource = _previewData;
			txtSummary.Text = $"Đã preview: {_previewData.Count} dòng — Chưa validate";
			lstErrors.ItemsSource = null;

			UpdateSaveButton();
			SnackbarHelper.ShowSuccess("Preview thành công! Hãy Validate trước khi lưu.");
		}
		catch
		{
			await MessageHelper.ShowMessage("Có lỗi xảy ra khi preview!");
		}
		finally
		{
			ToggleUI(true);
		}
	}

	// ================= VALIDATE =================
	private async void btnValidate_Click(object sender, RoutedEventArgs e)
	{
		if (!_isPreviewed || _previewData == null)
		{
			await MessageHelper.ShowMessage("Vui lòng Preview dữ liệu trước!");
			return;
		}

		try
		{
			ToggleUI(false);
			_isValidated = false;
			UpdateSaveButton();

			var res = await _client.ValidateImport(_previewData);

			if (!res.Success || res.Data == null)
			{
				await MessageHelper.ShowMessage(res.Message);
				return;
			}

			var errors = res.Data
				.Where(x => x == null)  
				.Select(x => "Lỗi dữ liệu")
				.ToList();

			lstErrors.ItemsSource = errors;

			if (errors.Count > 0)
			{
				txtSummary.Text = $"Preview: {_previewData.Count} dòng — Validate: {errors.Count} lỗi ❌";
				await MessageHelper.ShowMessage($"Có {errors.Count} lỗi, không thể lưu!");
				_isValidated = false;
			}
			else
			{
				txtSummary.Text = $"Preview: {_previewData.Count} dòng — Validate: OK ✔";
				SnackbarHelper.ShowSuccess("Validate thành công! Có thể lưu.");
				_isValidated = true;
			}

			UpdateSaveButton();
		}
		catch
		{
			await MessageHelper.ShowMessage("Có lỗi xảy ra khi validate!");
		}
		finally
		{
			ToggleUI(true);
		}
	}

	// ================= LƯU =================
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (!_isPreviewed)
		{
			await MessageHelper.ShowMessage("Vui lòng Preview trước khi lưu!");
			return;
		}
		if (!_isValidated)
		{
			await MessageHelper.ShowMessage("Vui lòng Validate trước khi lưu!");
			return;
		}
		if (_previewData == null || _previewData.Count == 0)
		{
			await MessageHelper.ShowMessage("Không có dữ liệu để lưu!");
			return;
		}

		try
		{
			ToggleUI(false);

			var res = await _client.Import(_previewData);

			if (res.Success)
			{
				DialogResult = true;
				Close();
			}
			else
			{
				await MessageHelper.ShowMessage(res.Message);
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

	// ================= ĐÓNG =================
	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}
}