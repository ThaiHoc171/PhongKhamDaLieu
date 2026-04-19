using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.ToaThuoc;

public partial class UpdateToaThuoc : Window
{
	public UpdateToaThuoc(int phienKhamId)
	{
		InitializeComponent();
		_id = phienKhamId;
		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns();
		GridContent.ItemsSource = Items;
	}
	private readonly int _id;
	private ToaThuocItem? _editingItem = null;
	private int _toaThuocId;
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
	private async void UpdateToaThuoc_Loaded(object sender, RoutedEventArgs e)
	{
		await LoadComboBox();
		var result = await _client.GetByPhienKham(_id);
		if (result.Success && result.Data != null)
		{
			var data = result.Data;
			if (data.NguoiLap.Id != Session.NhanVienId)
			{
				SnackbarHelper.ShowWarning("Bạn không phải nhân viên kê đơn!");
				IsUpdate();
			}
			txtDoctor.Text = data.NguoiLap.Name;
			txtId.Text = _id.ToString();
			_toaThuocId = data.ToaThuocID;
			txtNotes.Text = data.GhiChu;
			Items = new ObservableCollection<ToaThuocItem>(data.Thuoc.Select(x => new ToaThuocItem
			{
				ThuocID = x.ThuocID,
				TenThuoc = x.TenThuoc ?? "",
				SoLuong = x.SoLuong,
				LieuDung = x.LieuDung
			}));
			GridContent.ItemsSource = Items;
		}
	}
	private void IsUpdate()
	{
		cboMedicine.IsEnabled = false;
		numQuantity.IsEnabled = false;
		txtDosage.IsEnabled = false;
		btnAdd.IsEnabled = false;
		btnSave.IsEnabled = false;
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
		btnClose.IsEnabled = isEnabled;
		btnAdd.IsEnabled = isEnabled;
	}
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		if (Items == null || Items.Count == 0)
		{
			SnackbarHelper.ShowError("Chưa có thuốc nào trong toa");
			return;
		}

		try
		{
			ToggleUI(false);

			var req = Items.Select(x => new ChiTietToaThuocRequest
			{
				ThuocID = x.ThuocID,
				SoLuong = x.SoLuong,
				LieuDung = x.LieuDung
			}).ToList();

			var result = await _client.Update(_toaThuocId, req);

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
			SnackbarHelper.ShowError("Có lỗi xảy ra khi cập nhật toa thuốc");
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

