using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WPF.Client;
using WPF.Common;
using WPF.Models;
using WPF.Windows.ChucVu;

namespace WPF.Pages;

public partial class ChucVuPage : Page, INotifyPropertyChanged
{
	public ChucVuPage()
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
	private readonly ChucVuClient _client = new();
	#region paged
	public ObservableCollection<ChucVuListReadModel> Items { get; set; } = new();

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
	public bool CanGoPrev => Page > 1;
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
			OnPropertyChanged(nameof(CanGoPrev));
		}
	}
	#endregion
	private void SetupColumns()
	{
		GridContent.Columns.Clear();

		// ID
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("ChucVuID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		// NAME
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên chức vụ",
			Binding = new Binding("TenChucVu"),
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
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Pencil", Edit_Click, "Sửa"));
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("AccountKey", Authorize_Click, "Phân quyền chức vụ"));
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
			view.SortDescriptions.Add(new SortDescription("ChucVuID", ListSortDirection.Ascending));
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

	// ===== ADD =====
	private async void Add_Click(object sender, RoutedEventArgs e)
	{
		var parentWindow = Window.GetWindow(this);
		var overlay = parentWindow.FindName("Overlay") as Border;

		if (overlay != null)
			overlay.Visibility = Visibility.Visible;

		var win = new ThemChucVu
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
	private async void Import_Click(object sender, RoutedEventArgs e)
	{
		var parentWindow = Window.GetWindow(this);
		var overlay = parentWindow.FindName("Overlay") as Border;

		if (overlay != null)
			overlay.Visibility = Visibility.Visible;

		var win = new ExcelChucVu
		{
			Owner = parentWindow
		};
		var result = win.ShowDialog();
		if (result == true)
		{
			await LoadData();
			SnackbarHelper.ShowSuccess("Thêm chức vụ từ excel thành công!");
		}

		if (overlay != null)
			overlay.Visibility = Visibility.Collapsed;
	}
	// ===== EDIT =====
	private async void Edit_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is ChucVuListReadModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var win = new CapNhatChucVu(item.ChucVuID)
			{
				Owner = parentWindow
			};
			var result = win.ShowDialog();
			if (result == true)
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Cập nhật chức vụ thành công!");
			}

			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}

	private async void Authorize_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is ChucVuListReadModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var win = new PhanQuyenChucVu(item.ChucVuID)
			{
				Owner = parentWindow
			};
			var result = win.ShowDialog();
			if (result == true)
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Cập nhật chức vụ thành công!");
			}

			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}

}