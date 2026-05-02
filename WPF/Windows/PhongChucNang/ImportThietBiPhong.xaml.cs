using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.PhongChucNang;
public partial class ImportThietBiPhong : Window
{
    public ImportThietBiPhong()
    {
		InitializeComponent();
	}
	private readonly ChiTietPCNThietBiClient _client = new();
	private readonly ExcelClient _excel = new ExcelClient();
	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
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
	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
		btnPreview.IsEnabled = isEnabled;
		btnValidate.IsEnabled = isEnabled;
		cbSheet.IsEnabled = isEnabled;
	}
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			var list = gridPreview.ItemsSource as List<ChiTietPCNThietBiRequest>;

			if (list == null || list.Count == 0)
			{
				SnackbarHelper.ShowError("Chưa có dữ liệu preview");
				return;
			}

			var confirm = await MessageHelper.Confirm("Bạn có chắc muốn lưu các thiết bị - phòng này không?");
			if (!confirm) return;

			ToggleUI(false);

			var importList = list.Select(x => new ChiTietPCNThietBiRequest
			{
				PhongChucNangID = x.PhongChucNangID,
				ThietBiID = x.ThietBiID,
				MaTaiSan = x.MaTaiSan,
				GhiChu = x.GhiChu
			}).ToList();

			var res = await _client.ConfirmImport(importList);

			if (res.Success == true)
			{
				SnackbarHelper.ShowSuccess("Thêm thiết bị - phòng thành công");
				DialogResult = true;
				Close();
				return;
			}

			SnackbarHelper.ShowError($"Lỗi khi lưu: {res.Message}");
		}
		catch (Exception ex)
		{
			SnackbarHelper.ShowError("Có lỗi xảy ra khi lưu: " + ex.Message);
		}
		finally
		{
			ToggleUI(true);
		}
	}

	private async void btnValidate_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			var list = gridPreview.ItemsSource as List<ChiTietPCNThietBiRequest>;
			if (list == null || !list.Any())
			{
				SnackbarHelper.ShowError("Chưa có dữ liệu preview");
				return;
			}

			var validateResult = await _client.ValidateImport(list);

			if (!validateResult.Success)
			{
				SnackbarHelper.ShowError($"Validate API lỗi: {validateResult.Message}");
				return;
			}

			var errors = validateResult.Data?.Errors;
			if (errors != null && errors.Any())
			{
				lstErrors.ItemsSource = errors.SelectMany(x => x.Errors).ToList();
				SnackbarHelper.ShowError("Có lỗi trong dữ liệu");
				btnSave.IsEnabled = false;
				return;
			}
			btnSave.IsEnabled = true;
			SnackbarHelper.ShowSuccess($"Validate thành công! {validateResult.Data?.Data.Count ?? 0} dòng hợp lệ.");
		}
		catch (Exception ex)
		{
			SnackbarHelper.ShowError("Có lỗi xảy ra khi validate: "+ ex.Message);
		}
	}
	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}
