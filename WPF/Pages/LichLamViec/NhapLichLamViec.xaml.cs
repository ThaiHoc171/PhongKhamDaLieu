using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Pages.LichLamViec;

public partial class NhapLichLamViec : Page, INotifyPropertyChanged
{
    public NhapLichLamViec()
    {
        InitializeComponent();
		btnLuu.IsEnabled = false;
		this.DataContext = this;
	}
	private bool _isLoading;
	public bool IsLoading
	{
		get => _isLoading;
		set { _isLoading = value; OnPropertyChanged(); }
	}

	public event PropertyChangedEventHandler? PropertyChanged;
	private void OnPropertyChanged([CallerMemberName] string name = "")
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


	private readonly LichLamViecClient _client = new LichLamViecClient();
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
		try
		{
			IsLoading = true;
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
		finally
		{
			IsLoading = false;
		}
	}
	private async void btnLuu_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			IsLoading = true;
			var list = gridPreview.ItemsSource as List<LichLamViecRequest>;
			if (list == null || !list.Any())
			{
				SnackbarHelper.ShowError("Chưa có dữ liệu preview");
				return;
			}

			// Convert sang DTO server mong đợi
			var importList = list.Select(x => new LichLamViecRequest
			{
				NhanVienID = x.NhanVienID,
				Ngay = x.Ngay,
				CaLamViec = x.CaLamViec,
				GhiChu = x.GhiChu
			}).ToList();

			var confirm = await MessageHelper.Confirm("Bạn có chắc muốn lưu các lịch này không?");
			if (!confirm) return;

			var confirmResult = await _client.ConfirmImport(importList);

			if (confirmResult.Success == true)
			{
				SnackbarHelper.ShowSuccess("Thêm lịch thành công");
			}
			else
			{
				SnackbarHelper.ShowError($"Lỗi khi lưu: {confirmResult.Message}");
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine("Exception in btnLuu_Click: " + ex);
			SnackbarHelper.ShowError("Có lỗi xảy ra khi lưu, kiểm tra log debug.");
		}
		finally
		{
			IsLoading = false;
		}
	}

	private async void btnValidate_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			IsLoading = true;
			var list = gridPreview.ItemsSource as List<LichLamViecRequest>;
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
				btnLuu.IsEnabled = false;
				return;
			}
			btnLuu.IsEnabled = true;
			SnackbarHelper.ShowSuccess($"Validate thành công! {validateResult.Data?.Data.Count ?? 0} dòng hợp lệ.");
		}
		catch (Exception ex)
		{
			Debug.WriteLine("Exception in btnValidate_Click: " + ex);
			SnackbarHelper.ShowError("Có lỗi xảy ra khi validate, kiểm tra log debug.");
		}
		finally
		{
			IsLoading = false;
		}
	}
}
