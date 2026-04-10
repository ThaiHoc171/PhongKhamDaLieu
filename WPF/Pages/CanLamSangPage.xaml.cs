using Microsoft.Win32;
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
using WPF.Windows.CanLamSang;
using WPF.Windows.ThietBi;

namespace WPF.Pages;

public partial class CanLamSangPage : Page, INotifyPropertyChanged
{
    public CanLamSangPage()
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
	private readonly CanLamSangClient _client = new();
	public ObservableCollection<CanLamSangListReadModel> Items { get; set; } = new();

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
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("CanLamSangID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên cận lâm sàng",
			Binding = new Binding("TenCLS"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Loại",
			Binding = new Binding("LoaiXetNghiem"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Pencil", Edit_Click, "Sửa"));
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
			view.SortDescriptions.Add(new SortDescription("CanLamSangID", ListSortDirection.Ascending));
		}
		finally
		{
			IsLoading = false;
		}
	}

	private async void Search_Click(object sender, RoutedEventArgs e)
	{
		Page = 1;
		await LoadData();
	}

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

		var win = new ExcelCls
		{
			Owner = parentWindow
		};
		var result = win.ShowDialog();
		if (result == true)
		{
			await LoadData();
			SnackbarHelper.ShowSuccess("Thêm cận lâm sàng từ excel thành công!");
		}

		if (overlay != null)
			overlay.Visibility = Visibility.Collapsed;
	}
	private async void Add_Click(object sender, RoutedEventArgs e)
	{
		var parentWindow = Window.GetWindow(this);
		var overlay = parentWindow.FindName("Overlay") as Border;

		if (overlay != null)
			overlay.Visibility = Visibility.Visible;

		var win = new ThemCls
		{
			Owner = parentWindow
		};
		var result = win.ShowDialog();
		if (result == true)
		{
			await LoadData();
			SnackbarHelper.ShowSuccess("Thêm cận lâm sàng thành công!");
		}

		if (overlay != null)
			overlay.Visibility = Visibility.Collapsed;
	}

	private async void Edit_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is CanLamSangListReadModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var win = new CapNhatCls(item.CanLamSangID)
			{
				Owner = parentWindow
			};
			var result = win.ShowDialog();
			if (result == true)
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Cập nhật cận lâm sàng thành công!");
			}

			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
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
