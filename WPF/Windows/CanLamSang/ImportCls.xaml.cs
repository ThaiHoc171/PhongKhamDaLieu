using Microsoft.Win32;
using System.Windows;
using WPF.Client;
using WPF.Common;
using System.IO;
using WPF.Models;

namespace WPF.Windows.CanLamSang;
public partial class ImportCls : Window
{
	public ImportCls()
	{
		InitializeComponent();
	}
	private readonly CanLamSangClient _client = new CanLamSangClient();
	private readonly ExcelClient _excel = new ExcelClient();
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
		var result = await _excel.GetSheets(file);

		if (!result.Success || result.Data == null || result.Data.Count == 0)
		{
			SnackbarHelper.ShowError("Không lấy được danh sách sheet");
			cbSheet.ItemsSource = null;
			return;
		}

		cbSheet.ItemsSource = result.Data;
		cbSheet.SelectedIndex = 0; // chọn sheet đầu tiên mặc định
		SnackbarHelper.ShowSuccess($"Đã load {result.Data.Count} sheet");
	}
	private async void btnPreview_Click(object sender, RoutedEventArgs e)
	{
		var result = await _client.PreviewImport(txtFile.Text, cbSheet.Text);
		if (!result.Success || result.Data == null)
		{
			SnackbarHelper.ShowSuccess(result.Message);
			return;
		}
		gridPreview.ItemsSource = result.Data.Data;

		lstErrors.ItemsSource = result.Data.Errors;

		txtSummary.Text = $"Total: {result.Data.TotalRows} | Valid: {result.Data.SuccessRows}";
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
		cbSheet.IsEnabled = isEnabled;
		btnChooseFile.IsEnabled = isEnabled;
		btnPreview.IsEnabled = isEnabled;
	}
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		var list = gridPreview.ItemsSource as List<CanLamSangRequest>;
		if (gridPreview.ItemsSource == null)
		{
			SnackbarHelper.ShowSuccess("Chưa có dữ liệu preview");
			return;
		}
		var errors = lstErrors.ItemsSource as List<string>;
		if (errors != null && errors.Count > 0)
		{
			SnackbarHelper.ShowError("Có lỗi trong dữ liệu, không thể lưu.");
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
				SnackbarHelper.ShowError(res.Message);
			}
		}
		catch (Exception ex)
		{
			SnackbarHelper.ShowError("Lỗi khi lưu dữ liệu: " + ex.Message);
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