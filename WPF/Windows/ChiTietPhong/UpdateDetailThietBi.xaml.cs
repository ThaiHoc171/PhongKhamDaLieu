using System.Security.Cryptography;
using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.ChiTietPhong;
public partial class UpdateDetailThietBi : Window
{
	public UpdateDetailThietBi(int id)
	{
		_id = id;
		InitializeComponent();
		cboTinhTrang.ItemsSource = new List<string> { "Hoạt động", "Hỏng", "Bảo trì" };
	}
	private readonly int _id;
	private readonly ChiTietPCNThietBiClient _client = new();
	private ChiTietPCNThietBiUpdate _current = new();
	private async void UpdateDetail_Load(object sender, RoutedEventArgs e)
	{
		var result = await _client.GetById(_id);
		if (result != null && result.Data != null)
		{
			txtName.Text = _current.MaTaiSan = result.Data.MaTaiSan;
			txtNote.Text = _current.GhiChu = result.Data.GhiChu;
			dtpDateCreate.Text = result.Data.NgayNhap.ToString("dd/MM/yyyy");
			txtPhong.Text = result.Data.PhongChucNang;
			txtThietBi.Text = result.Data.ThietBi;
			cboTinhTrang.SelectedItem = result.Data.TinhTrang;
		}
		else
		{
			SnackbarHelper.ShowError("Không tìm thấy phòng chức năng.");
			this.Close();
		}
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
		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập tên phòng!");
			return;
		}
		var req = new ChiTietPCNThietBiUpdate
		{
			MaTaiSan = txtName.Text.Trim(),
			GhiChu = txtNote.Text.Trim(),
			TinhTrang = cboTinhTrang.SelectedItem?.ToString() ?? "Hoạt động"
		};
		if (_current == req)
		{
			SnackbarHelper.ShowWarning("Không có thay đổi nào được thực hiện!");
			return;
		}
		try
		{
			ToggleUI(false);

			var result = await _client.Update(_id, req);

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
}
