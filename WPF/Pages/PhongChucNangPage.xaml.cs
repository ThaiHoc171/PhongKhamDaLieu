using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WPF.Client;
using WPF.Common;
using WPF.Models;
using WPF.Windows;
using WPF.Windows.ChucVu;
using WPF.Windows.PhongChucNang;

namespace WPF.Pages;

public partial class PhongChucNangPage : Page, INotifyPropertyChanged
{
	public PhongChucNangPage()
	{
		InitializeComponent();
		DataContext = this;
		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns();
		Loaded += async (_, __) => await LoadData();
		PreviewMouseDown += async (_, __) =>
		{
			if (txtSizepage.IsKeyboardFocusWithin)
			{
				await ApplyPageSize();
			}
		};
	}
	private readonly PhongChucNangClient _client = new();

	public ObservableCollection<PhongChucNangReadListModel> Items { get; set; } = new();

	private int _page = 1;
	public int Page
	{
		get => _page;
		set { _page = value; OnPropertyChanged(); }
	}
	private int _sizePage = 15;
	public int SizePage
	{
		get => _sizePage;
		set { _sizePage = value; OnPropertyChanged(); }
	}
	private int _totalPages;
	public int TotalPages
	{
		get => _totalPages;
		set { _totalPages = value; OnPropertyChanged(); }
	}

	public string PageDisplay => $"{Page} / {TotalPages}";
	public bool CanGoNext => Page < TotalPages;

	private bool _isLoading;
	public bool IsLoading
	{
		get => _isLoading;
		set { _isLoading = value; OnPropertyChanged(); }
	}

	private string _keyword = "";
	public string Keyword
	{
		get => _keyword;
		set { _keyword = value; OnPropertyChanged(); }
	}
	private void SetupColumns()
	{
		GridContent.Columns.Clear();

		// ID
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Binding = new Binding("PhongChucNangID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		// NAME
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên phòng",
			Binding = new Binding("TenPhong"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});

		// STATUS
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		// BUTTON
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Eye", View_Click, "Xem chi tiết"));
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Pencil", Edit_Click, "Sửa thông tin"));
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Sync", ToggleStatus_Click, "Đổi trạng thái"));
	}

	private async Task LoadData()
	{
		try
		{
			IsLoading = true;

			var res = string.IsNullOrWhiteSpace(Keyword)
				? await _client.GetPaged(Page, SizePage)
				: await _client.Search(Keyword, Page, SizePage);

			if (!res.Success)
			{
				await MessageHelper.ShowMessage(res.Message);
				return;
			}

			if (res.Data == null) return;

			Items.Clear();

			foreach (var item in res.Data.Items)
				Items.Add(item);

			TotalPages = (int)Math.Ceiling((double)res.Data.TotalCount / res.Data.PageSize);

			var view = CollectionViewSource.GetDefaultView(GridContent.ItemsSource);
			view.SortDescriptions.Clear();
		}
		finally
		{
			IsLoading = false;
		}
	}

	// ===== SEARCH =====
	private async void Search_Click(object sender, RoutedEventArgs e)
	{
		Page = 1;
		await LoadData();
	}

	// ===== REFRESH =====
	private async void Refresh_Click(object sender, RoutedEventArgs e)
	{
		txt_Search.Text = "";
		await LoadData();
		Page = 1;
	}
	private async void Import_Click(object sender, RoutedEventArgs e)
	{
		var parentWindow = Window.GetWindow(this);
		var overlay = parentWindow.FindName("Overlay") as Border;

		if (overlay != null)
			overlay.Visibility = Visibility.Visible;

		var win = new NhapThietBiPhong
		{
			Owner = parentWindow
		};
		var result = win.ShowDialog();
		if (result == true)
		{
			await LoadData();
			SnackbarHelper.ShowSuccess("Nhập thiết bị từ excel thành công!");
		}

		if (overlay != null)
			overlay.Visibility = Visibility.Collapsed;
	}
	// ===== ADD =====
	private async void Add_Click(object sender, RoutedEventArgs e)
	{
		var parentWindow = Window.GetWindow(this);
		var overlay = parentWindow.FindName("Overlay") as Border;

		if (overlay != null)
			overlay.Visibility = Visibility.Visible;

		var win = new ThemPhongChucNang()
		{
			Owner = parentWindow
		};
		var result = win.ShowDialog();
		if (result == true)
		{
			await LoadData();
			SnackbarHelper.ShowSuccess("Thêm chức vụ thành công!");
		}

		if (overlay != null)
			overlay.Visibility = Visibility.Collapsed;
	}
	private void View_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PhongChucNangReadListModel item)
		{
			var parent = Window.GetWindow(this) as appClinic;

			parent?.OpenPage(
				new XemChiTietPhongPage(item.PhongChucNangID,item.TenPhong),
				$"Quản lý phòng chức năng / {item.TenPhong}"
			);
		}
	}

	private async void Edit_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PhongChucNangReadListModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var win = new CapNhatPhongChucNang(item.PhongChucNangID)
			{
				Owner = parentWindow
			};
			var result = win.ShowDialog();
			if (result == true)
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Cập nhật phòng chức năng thành công!");
			}

			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}

	private async void ToggleStatus_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PhongChucNangReadListModel item)
		{
			string newStatus = "";
			bool confirm = false;
			if (item.TrangThai == "Hoạt động")
			{
				newStatus = "Hỏng";
				confirm = await MessageHelper.Confirm(
					$"Bạn có chắc muốn báo hỏng: {item.TenPhong}?"
				);
			}
			else if(item.TrangThai == "Hỏng")
			{
				newStatus = "Bảo trì";
				confirm = await MessageHelper.Confirm(
					$"Phòng {item.TenPhong} đang được bảo trì?"
				);
			}
			else
			{
				newStatus = "Hoạt động";
				confirm = await MessageHelper.Confirm(
					$"Phòng {item.TenPhong} đã được sửa chữa xong?"
				);
			}
			if (!confirm) return;

			var res = await _client.ChangeStatus(item.PhongChucNangID, newStatus);

			if (!res.Success)
			{
				SnackbarHelper.ShowError(res.Message);
				return;
			}
			SnackbarHelper.ShowSuccess("Cập nhật trạng thái thành công!");
			await LoadData();
		}
	}


	private string _lastSizeText = "";
	private async Task ApplyPageSize()
	{
		if (txtSizepage.Text == _lastSizeText) return;

		if (int.TryParse(txtSizepage.Text, out int size) && size > 0)
		{
			_lastSizeText = txtSizepage.Text;
			SizePage = size;
			Page = 1;
			await LoadData();
		}
	}
	private async void SizePage_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Enter)
		{
			await ApplyPageSize();
		}
	}
	private async void SizePage_LostFocus(object sender, RoutedEventArgs e)
	{
		await ApplyPageSize();
	}
	private async void Next_Click(object sender, RoutedEventArgs e)
	{
		if (Page < TotalPages)
		{
			Page++;
			await LoadData();
		}
	}

	private async void Prev_Click(object sender, RoutedEventArgs e)
	{
		if (Page > 1)
		{
			Page--;
			await LoadData();
		}
	}

	private async void First_Click(object sender, RoutedEventArgs e)
	{
		Page = 1;
		await LoadData();
	}

	private async void Last_Click(object sender, RoutedEventArgs e)
	{
		Page = TotalPages;
		await LoadData();
	}

	public event PropertyChangedEventHandler? PropertyChanged;
	private void OnPropertyChanged([CallerMemberName] string name = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

		if (name == nameof(Page) || name == nameof(TotalPages))
		{
			OnPropertyChanged(nameof(PageDisplay));
			OnPropertyChanged(nameof(CanGoNext));
		}
	}
}
