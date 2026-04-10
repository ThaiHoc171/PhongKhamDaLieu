using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.CaKham;

public partial class DangKyCaKham : Window
{
	public DangKyCaKham(int id, string name , DateTime ngay)
	{
		InitializeComponent();
		_id = id;
		_name = name;
		_ngay = ngay;
	}
	private readonly int _id;
	private readonly string _name;
	private readonly DateTime _ngay;
	private readonly BenhNhanClient _benhNhanClient = new();
	private readonly CaKhamClient _client = new();
	private async void DangKyCaKham_Loaded(object sender, RoutedEventArgs e)
	{
		await LoadComoboBox();
		txtID.Text = _id.ToString();
		txtName.Text = _name +" / "+ _ngay.ToString("dd/MM/yyyy");
	}
	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
	private async Task LoadComoboBox()
	{
		var list = await _benhNhanClient.GetCombobox();
		if (list != null)
		{
			_benhNhanView = CollectionViewSource.GetDefaultView(list.Data);
			cboUser.ItemsSource = list.Data;
			cboUser.DisplayMemberPath = "Name";
			cboUser.SelectedValuePath = "Id";
		}
	}
	private ICollectionView _benhNhanView;
	private void cboUser_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
	{
		if (_benhNhanView == null) return;

		string text = cboUser.Text.ToLower();

		_benhNhanView.Filter = obj =>
		{
			var item = obj as NameHelper;
			return item.Name.ToLower().Contains(text);
		};

		_benhNhanView.Refresh();
		cboUser.IsDropDownOpen = true;
	}
	private async void btnLuu_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(txtLyDo.Text))
		{
			SnackbarHelper.ShowError("Vui lòng nhập lý do!");
			return;
		}
		var req = new CaKhamRegister
		{
			ThongTinID = (int)cboUser.SelectedValue,
			LyDoKham = txtLyDo.Text.Trim(),
			GhiChu = txtNote.Text.Trim() ?? "",
			NgayDat = DateTime.Today
		};

		try
		{
			btnLuu.IsEnabled = false;
			btnHuy.IsEnabled = false;

			var result = await _client.Register(_id,req);

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
}
