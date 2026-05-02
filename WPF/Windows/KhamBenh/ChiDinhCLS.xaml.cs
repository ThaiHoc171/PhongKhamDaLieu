using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.KhamBenh;

public partial class ChiDinhCLS : Window
{
    public ChiDinhCLS(int id)
    {
		InitializeComponent();
		_id = id;
		txtID.Text = id.ToString();
		txtName.Text = Session.HoTen.Name;
		LoadComboBox();
	}
	private readonly int _id;
	private readonly PhienKhamClsClient _client = new();
	private readonly CanLamSangClient _cls = new();
	private async void LoadComboBox()
	{
		var list = await _cls.GetCombobox();
		if (list.Success)
		{
			cboCLS.ItemsSource = list.Data;
			cboCLS.DisplayMemberPath = "Name";
			cboCLS.SelectedValuePath = "Id";
			cboCLS.SelectedIndex = -1;
		}
	}
	private bool ValidateInput()
	{
		if (cboCLS.SelectedIndex < 0)
		{
			SnackbarHelper.ShowError("Vui lòng chọn cận lâm sàng!");
			return false;
		}
		if(Session.NhanVienId == null)
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
		var req = new PkClsRequestDTO
		{
			PhienKhamID = _id,
			NhanVienChiDinhID = Session.NhanVienId ?? 0,
			CLSID = (int)cboCLS.SelectedValue,
			GhiChu = txtNotes.Text.Trim(),
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

