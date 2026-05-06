using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.LieuTrinh;


public partial class AddLieuTrinh : Window
{
	public AddLieuTrinh(int phienKhamId, string benhNhanName)
	{
		InitializeComponent();
		dtpDate.SelectedDate = DateTime.Now;
		_id = phienKhamId;
		txId.Text = _id.ToString();
		txtPatient.Text = benhNhanName;
	}
	private readonly int _id;
	private readonly LieuTrinhDieuTriClient _client = new();
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
	private void txtNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
	{
		e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
	}
	private async Task<bool> Validate()
	{
		if (dtpDate.SelectedDate == null)
		{
			await MessageHelper.ShowMessage("Vui lòng chọn ngày bắt đầu!");
			return false;
		}

		if (dtpDate.SelectedDate.Value.Date < DateTime.Today)
		{
			await MessageHelper.ShowMessage("Ngày bắt đầu không thể ở quá khứ!");
			return false;
		}

		if (string.IsNullOrWhiteSpace(txtName.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập tên liệu trình!");
			return false;
		}
		if (string.IsNullOrWhiteSpace(txtNumber.Text))
		{
			await MessageHelper.ShowMessage("Vui lòng nhập số buổi!");
			return false;
		}

		if (!int.TryParse(txtNumber.Text, out int soBuoi))
		{
			await MessageHelper.ShowMessage("Số buổi không hợp lệ!");
			return false;
		}

		if (soBuoi <= 0)
		{
			await MessageHelper.ShowMessage("Số buổi không hợp lệ!");
			return false;
		}

		return true;
	}

	private async void btnSave_Click(object sender, EventArgs e)
	{
		if(!await Validate())
			return;
		var req = new LieuTrinhDieuTriRequestDTO
		{
			PhienKhamID = _id,
			TenLieuTrinh = txtName.Text.Trim(),
			TongSoBuoi = Convert.ToInt32(txtNumber.Text),
			GhiChu = txtNotes.Text.Trim(),
			NgayBatDau = dtpDate.SelectedDate!.Value,
		};

		try
		{
			ToggleUI(false);
			var result = await _client.Create(req);

			if (result.Success)
			{
				this.DialogResult = true;
				this.Close();
			}
			else
			{
				await MessageHelper.ShowMessage(result.Message);
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
