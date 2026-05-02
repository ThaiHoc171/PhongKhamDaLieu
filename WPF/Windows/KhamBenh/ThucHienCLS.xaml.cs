using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.KhamBenh;

public partial class ThucHienCLS : Window
{
    public ThucHienCLS(int id)
    {
		_id = id;
		InitializeComponent();
	}
	private readonly int _id;
	private string? _filePath;
	private readonly PhienKhamClsClient _client = new();
	private readonly UploadClient _upload = new();
	private async void ThucHienCLS_Loaded(object sender, RoutedEventArgs e)
	{
		var result = await _client.Detail(_id);
		if(!result.Success || result.Data == null)
		{
			SnackbarHelper.ShowError("Không tìm thấy phiên khám.");
			this.Close();
			return;
		}
		var data = result.Data;
		if(data.NhanVienThucHien?.Id != Session.NhanVienId)
		{
			CheckUpdate();
		}
		txtCls.Text = data.TenCLS;
		txtRequestedBy.Text = data.NhanVienChiDinh;
		txtPerformedBy.Text = data.NhanVienThucHien?.Name ?? "Chưa thực hiện";
	}

	private void CheckUpdate()
	{
		SnackbarHelper.ShowWarning("Bạn không phải là người thực hiện CLS này!");
		txtResult.IsEnabled = false;
		txtNotes.IsEnabled = false;
		btnUploadFile.IsEnabled = false;
		btnSave.IsEnabled = false;
	}

	private bool ValidateInput()
	{
		if (Session.NhanVienId == null)
		{
			SnackbarHelper.ShowError("Không xác định được nhân viên chỉ định!");
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
		if (!ValidateInput())
			return;


		if (!string.IsNullOrEmpty(_filePath))
		{
			var uploadResult = await _upload.UploadFiles(_filePath, "KetQuaCLS");

			if (!uploadResult.Success)
			{
				SnackbarHelper.ShowError(uploadResult.Message);
				return;
			}

			if (!string.IsNullOrEmpty(uploadResult.Data))
			{
				var uri = new Uri(uploadResult.Data);
				_filePath = uri.AbsolutePath.TrimStart('/');
			}
		}

		var req = new PkClsUpdateRequestDTO
		{
			KetQua = txtResult.Text.Trim(),
			FileDinhKem = _filePath,
			GhiChu = txtNotes.Text.Trim(),
		};

		try
		{
			ToggleUI(false);

			var result = await _client.Complete(_id,req);

			if (result.Success)
			{
				this.DialogResult = true;
				this.Close();
			}
			else
			{
				SnackbarHelper.ShowError(result.Message);
			}
		}
		catch (Exception)
		{
			SnackbarHelper.ShowError("Có lỗi xảy ra, vui lòng thử lại!");
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

	private void btnViewFile_Click(object sender, RoutedEventArgs e)
	{
		if(_filePath == null)
		{
			SnackbarHelper.ShowWarning("Không có file đính kèm!");
			return;
		}
		var url = _filePath;
		Process.Start(new ProcessStartInfo
		{
			FileName = url,
			UseShellExecute = true
		});
	}
	private void btnUploadFile_Click(object sender, RoutedEventArgs e)
	{
		var dlg = new OpenFileDialog
		{
			Filter = @"All Supported Files|*.jpg;*.jpeg;*.png;*.pdf;*.doc;*.docx;*.xls;*.xlsx|
			Image Files|*.jpg;*.jpeg;*.png|
			PDF Files|*.pdf|
			Word Documents|*.doc;*.docx|	
			Excel Files|*.xls;*.xlsx|
			All Files|*.*"
		};

		if (dlg.ShowDialog() == true)
		{
			_filePath = dlg.FileName;
			txtFilePath.Text = _filePath;
		}

	}
}
