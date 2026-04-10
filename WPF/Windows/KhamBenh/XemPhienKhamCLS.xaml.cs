using System.Diagnostics;
using System.Windows;
using WPF.Client;
using WPF.Common;

namespace WPF.Windows.KhamBenh;

public partial class XemPhienKhamCLS : Window
{
	public XemPhienKhamCLS(int id)
	{
		InitializeComponent();
		_id = id;
	}
	private readonly int _id;
	private string? _filePath;
	private readonly PhienKhamClsClient _client = new();
	private async void ThucHienCLS_Loaded(object sender, RoutedEventArgs e)
	{
		var result = await _client.Detail(_id);
		if (!result.Success || result.Data == null)
		{
			SnackbarHelper.ShowError("Không tìm thấy phiên khám.");
			this.Close();
			return;
		}
		var data = result.Data;
		txtCls.Text = data.TenCLS;
		txtRequestedBy.Text = data.NhanVienChiDinh;
		txtPerformedBy.Text = data.NhanVienThucHien?.Name ?? "Chưa thực hiện";
		txtResult.Text = data.KetQua;
		txtStatus.Text = data.TrangThai;
		if (!string.IsNullOrEmpty(data.FileDinhKem))
		{
			_filePath = data.FileDinhKem;
			btnViewFile.Visibility = Visibility.Visible;
			txtFilePath.Text = System.IO.Path.GetFileName(_filePath);
		}
		else
		{
			btnViewFile.Visibility = Visibility.Collapsed;
		}
		dtpDate.SelectedDate = data.NgayThucHien;
	}

	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}

	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
	private void btnViewFile_Click(object sender, RoutedEventArgs e)
	{
		if (_filePath == null)
		{
			SnackbarHelper.ShowWarning("Không có file đính kèm!");
			return;
		}
		var url = $"https://hoanmyclinic.s3.ap-southeast-2.amazonaws.com/{_filePath}";
		Process.Start(new ProcessStartInfo
		{
			FileName = url,
			UseShellExecute = true
		});
	}
}
