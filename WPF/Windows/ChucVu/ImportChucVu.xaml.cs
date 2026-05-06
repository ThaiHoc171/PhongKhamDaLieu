using Microsoft.Win32;
using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;
using System.IO;

namespace HoanMyClinic.Windows.ChucVu;

public partial class ImportChucVu : Window
{
	public ImportChucVu()
	{
		InitializeComponent();
		btnValidate.Click += btnValidate_Click;
	}

	private readonly ChucVuClient _client = new();
	private readonly ExcelClient _excel = new();

	// data sau từng bước
	private List<ChucVuRequest>? _previewData;   // kết quả preview (tất cả dòng hợp lệ cú pháp)
	private List<ChucVuRequest>? _validatedData; // kết quả validate (tất cả dòng hợp lệ business)

	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
			DragMove();
	}

	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
		cbSheet.IsEnabled = isEnabled;
		btnChooseFile.IsEnabled = isEnabled;
		btnPreview.IsEnabled = isEnabled;
		btnValidate.IsEnabled = isEnabled;
	}

	private void btnChooseFile_Click(object sender, RoutedEventArgs e)
	{
		var dlg = new OpenFileDialog { Filter = "Excel (*.xlsx)|*.xlsx" };

		if (dlg.ShowDialog() == true)
		{
			txtFile.Text = dlg.FileName;
			_previewData = null;
			_validatedData = null;
			gridPreview.ItemsSource = null;
			lstErrors.ItemsSource = null;
			txtSummary.Text = "";
			SnackbarHelper.ShowSuccess($"Đã chọn file: {Path.GetFileName(dlg.FileName)}");
			LoadSheets(dlg.FileName);
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
		SnackbarHelper.ShowSuccess($"Đã load {res.Data.Count} sheet");
	}
	private async void btnPreview_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtFile.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng chọn file!");
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
			_validatedData = null;

			var res = await _client.PreviewImport(txtFile.Text, cbSheet.Text);

			if (!res.Success || res.Data == null)
			{
				SnackbarHelper.ShowError(res.Message);
				return;
			}

			_previewData = res.Data.Data;
			gridPreview.ItemsSource = _previewData;
			lstErrors.ItemsSource = res.Data.Errors
				.SelectMany(e => e.Errors)
				.ToList();
			txtSummary.Text = $"Total: {res.Data.TotalRows} | Hợp lệ: {res.Data.SuccessRows} | Lỗi: {res.Data.Errors.Count}";
		}
		finally
		{
			ToggleUI(true);
		}
	}

	private async void btnValidate_Click(object sender, RoutedEventArgs e)
	{
		if (_previewData == null || _previewData.Count == 0)
		{
			await MessageHelper.ShowMessage("Vui lòng preview trước!");
			return;
		}

		try
		{
			ToggleUI(false);
			_validatedData = null;

			var res = await _client.ValidateImport(_previewData);

			if (!res.Success || res.Data == null)
			{
				SnackbarHelper.ShowError(res.Message);
				return;
			}

			_validatedData = res.Data.Data;
			gridPreview.ItemsSource = _validatedData;
			lstErrors.ItemsSource = res.Data.Errors
				.SelectMany(e => e.Errors)
				.ToList();
			txtSummary.Text = $"Validate — Hợp lệ: {res.Data.SuccessRows} | Lỗi: {res.Data.Errors.Count}";

			if (res.Data.Errors.Count == 0)
				SnackbarHelper.ShowSuccess("Validate thành công, có thể lưu!");
			else
				SnackbarHelper.ShowError("Có lỗi, kiểm tra danh sách lỗi bên dưới");
		}
		finally
		{
			ToggleUI(true);
		}
	}
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (_validatedData == null || _validatedData.Count == 0)
		{
			await MessageHelper.ShowMessage("Vui lòng validate trước khi lưu!");
			return;
		}

		var errors = lstErrors.ItemsSource as List<string>;
		if (errors != null && errors.Count > 0)
		{
			await MessageHelper.ShowMessage("Còn lỗi trong dữ liệu, không thể lưu!");
			return;
		}

		try
		{
			ToggleUI(false);

			var res = await _client.ConfirmImport(_validatedData);

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

	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}
}