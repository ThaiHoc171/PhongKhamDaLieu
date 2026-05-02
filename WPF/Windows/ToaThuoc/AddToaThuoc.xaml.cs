using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.ToaThuoc;

public partial class AddToaThuoc : Window
{
	public AddToaThuoc(int id)
	{
		InitializeComponent();
		_id = id;
		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns();
		GridContent.ItemsSource = Items;
	}
	private readonly int _id;
	private ToaThuocItem? _editingItem = null;
	private readonly ThuocClient _thuocClient = new();
	private readonly ToaThuocClient _client = new();
	private ObservableCollection<ToaThuocItem> Items = new();
	private async Task LoadComboBox()
	{
		var list = await _thuocClient.Combobox();
		cboMedicine.ItemsSource = list.Data;
		cboMedicine.DisplayMemberPath = "Name";
		cboMedicine.SelectedValuePath = "Id";
		cboMedicine.SelectedIndex = -1;
	}
	private void SetupColumns()
	{
		GridContent.Columns.Clear();

		// ID
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("ThuocID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		// NAME
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên thuốc",
			Binding = new Binding("TenThuoc"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Số lượng",
			Binding = new Binding("SoLuong"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Liều dùng",
			Binding = new Binding("LieuDung"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
	}
	private async void AddToaThuoc_Loaded(object sender, RoutedEventArgs e)
	{
		await LoadComboBox();
		txtDoctor.Text = Session.HoTen.Name;
		txtId.Text = _id.ToString();
	}
	private void btnAdd_Click(object sender, RoutedEventArgs e)
	{
		if (cboMedicine.SelectedValue == null)
		{
			SnackbarHelper.ShowError("Vui lòng chọn thuốc");
			return;
		}

		if (!int.TryParse(numQuantity.Text, out int soLuong))
		{
			SnackbarHelper.ShowError("Số lượng không hợp lệ");
			return;
		}

		if (_editingItem != null)
		{
			// UPDATE
			_editingItem.ThuocID = (int)cboMedicine.SelectedValue;
			_editingItem.TenThuoc = cboMedicine.Text;
			_editingItem.SoLuong = soLuong;
			_editingItem.LieuDung = txtDosage.Text;

			GridContent.Items.Refresh();
		}
		else
		{
			// ADD
			var item = new ToaThuocItem
			{
				ThuocID = (int)cboMedicine.SelectedValue,
				TenThuoc = cboMedicine.Text,
				SoLuong = soLuong,
				LieuDung = txtDosage.Text
			};

			Items.Add(item);
		}

		// reset form
		_editingItem = null;
		GridContent.SelectedItem = null;

		cboMedicine.SelectedIndex = -1;
		numQuantity.Text = "";
		txtDosage.Text = "";
	}
	private void GridContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (GridContent.SelectedItem is not ToaThuocItem item)
			return;

		_editingItem = item; 
		cboMedicine.SelectedValue = item.ThuocID;
		numQuantity.Text = item.SoLuong.ToString();
		txtDosage.Text = item.LieuDung;
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
		btnAdd.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
	}
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (Items.Count == 0)
		{
			SnackbarHelper.ShowError("Chưa có thuốc nào");
			return;
		}

		if (Session.NhanVienId == null)
		{
			SnackbarHelper.ShowError("Không xác định được nhân viên kê đơn!");
			return;
		}

		try
		{
			ToggleUI(false);

			var req = new ToaThuocRequest
			{
				PhienKhamID = _id,
				NhanVienKeDonID = Session.NhanVienId ?? 0,
				GhiChu = txtNotes.Text,
				Thuoc = Items.Select(x => new ChiTietToaThuocRequest
				{
					ThuocID = x.ThuocID,
					SoLuong = x.SoLuong,
					LieuDung = x.LieuDung
				}).ToList()
			};

			var result = await _client.Create(req);

			if (!result.Success)
			{
				SnackbarHelper.ShowError(result.Message);
				return;
			}

			DialogResult = true;
			Close();
		}
		catch
		{
			SnackbarHelper.ShowError("Có lỗi xảy ra!");
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
	private void NumberOnly(object sender, TextCompositionEventArgs e)
	{
		e.Handled = !int.TryParse(e.Text, out _);
	}
	public class ToaThuocItem
	{
		public int ThuocID { get; set; }
		public string TenThuoc { get; set; } = "";
		public int SoLuong { get; set; }
		public string? LieuDung { get; set; }
	}
}
