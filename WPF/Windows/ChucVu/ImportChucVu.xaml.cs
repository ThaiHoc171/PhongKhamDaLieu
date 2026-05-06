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
	}
	private readonly ChucVuClient _client = new ChucVuClient();
	private readonly ExcelClient _excel = new ExcelClient();
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
		cbSheet.IsEnabled = isEnabled;
		btnChooseFile.IsEnabled = isEnabled;
		btnPreview.IsEnabled = isEnabled;
	}
	private void btnChooseFile_Click(object sender, RoutedEventArgs e)
	{
		var dlg = new OpenFileDialog();
		dlg.Filter = "Excel (*.xlsx)|*.xlsx";

		if (dlg.ShowDialog() == true)
		{
			txtFile.Text = dlg.FileName;

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
		cbSheet.SelectedIndex = 0; // chọn sheet đầu tiên mặc định
		SnackbarHelper.ShowSuccess($"Đã load {res.Data.Count} sheet");
	}
	private async void btnPreview_Click(object sender, RoutedEventArgs e)
	{
		var res = await _client.PreviewImport(txtFile.Text, cbSheet.Text);
		if (!res.Success || res.Data == null)
		{
			SnackbarHelper.ShowSuccess(res.Message);
			return;
		}
		gridPreview.ItemsSource = res.Data.Data;

		lstErrors.ItemsSource = res.Data.Errors;

		txtSummary.Text = $"Total: {res.Data.TotalRows} | Valid: {res.Data.SuccessRows}";
	}
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		var list = gridPreview.ItemsSource as List<ChucVuRequest>;
		if (gridPreview.ItemsSource == null)
		{
			SnackbarHelper.ShowSuccess("Chưa có dữ liệu preview");
			return;
		}
		var errors = lstErrors.ItemsSource as List<string>;
		if (errors != null && errors.Count > 0)
		{
			await MessageHelper.ShowMessage("Có lỗi trong dữ liệu, không thể lưu.");
			return;
		}
		try
		{
			ToggleUI(false);

			var res = await _client.ConfirmImport(list!);

			if (res.Success)
			{
				this.DialogResult = true;
				this.Close();
			}
			else
			{
				await MessageHelper.ShowMessage(res.Message);
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
