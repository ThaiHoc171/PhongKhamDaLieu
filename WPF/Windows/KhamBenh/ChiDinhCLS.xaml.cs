using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.KhamBenh;

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
	private async void btnLuu_Click(object sender, EventArgs e)
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
			btnLuu.IsEnabled = false;
			btnHuy.IsEnabled = false;

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
			btnLuu.IsEnabled = true;
			btnHuy.IsEnabled = true;
		}
	}

	private void btnHuy_Click(object sender, RoutedEventArgs e)
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
}

