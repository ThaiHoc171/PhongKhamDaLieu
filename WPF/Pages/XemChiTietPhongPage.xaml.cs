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
using WPF.Windows;
using WPF.Windows.ChiTietPhong;

namespace WPF.Pages;

public partial class XemChiTietPhongPage : Page, INotifyPropertyChanged
{
	public XemChiTietPhongPage(int id,string name)
	{
		InitializeComponent();
		_id = id;
		_name = name;
		DataContext = this;
		SetupDataGrid.ApplyStyle(GridThietBi);
		SetupDataGrid.ApplyStyle(GridChiTietThietBi);
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
	private readonly PCNThietBiClient _client = new();
	private readonly ChiTietPCNThietBiClient _chitietclient = new();
	private readonly int _id;
	private readonly string _name;
	public ObservableCollection<PCNThietBiReadModel> Items { get; set; } = new();
	public ObservableCollection<ChiTietPCNThietBiListReadModel>  ChiTietItems { get; set; } = new();
	#region Paging
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
	#endregion
	private void SetupColumns()
	{
		//GridThietBi
		GridThietBi.Columns.Clear();
		GridThietBi.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("PCN_TB_ID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridThietBi.Columns.Add(new DataGridTextColumn
		{
			Header = "Phòng chức năng",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("PhongChucNang"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});
		GridThietBi.Columns.Add(new DataGridTextColumn
		{
			Header = "Thiết bị",
			Binding = new Binding("ThietBi"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});
		GridThietBi.Columns.Add(new DataGridTextColumn
		{
			Header = "Số lượng",
			Binding = new Binding("TongSoLuong"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridThietBi.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Eye", View_Click, "View"));
		//GirdChiTietThietBi

		GridChiTietThietBi.Columns.Clear();
		GridThietBi.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("ChiTietID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridChiTietThietBi.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã tài sản",
			Binding = new Binding("MaTaiSan"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridChiTietThietBi.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày nhập",
			Binding = new Binding("NgayNhap")
			{
				StringFormat = "dd/MM/yyyy"
			},
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridChiTietThietBi.Columns.Add(new DataGridTextColumn
		{
			Header = "Tình trạng",
			Binding = new Binding("TinhTrang"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridChiTietThietBi.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Pencil", Edit_Click, "Cập nhật"));
		GridChiTietThietBi.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Minus", Delete_Click, "Xóa"));
	}

	private async Task LoadData()
	{
		try
		{
			IsLoading = true;

			var res = string.IsNullOrWhiteSpace(Keyword)
				? await _client.GetPaged(Page, SizePage, _id)
				: await _client.Search(Keyword, Page, SizePage,_id);

			if (!res.Success)
			{
				SnackbarHelper.ShowError(res.Message);
				return;
			}

			if (res.Data == null) return;

			Items.Clear();

			foreach (var item in res.Data.Items)
				Items.Add(item);

			TotalPages = (int)Math.Ceiling((double)res.Data.TotalCount / res.Data.PageSize);

			var view = CollectionViewSource.GetDefaultView(GridThietBi.ItemsSource);
			view.SortDescriptions.Clear();
			await LoadChiTietThietBi(0);
		}
		finally
		{
			IsLoading = false;
		}
	}
	private async Task LoadChiTietThietBi(int pcnTbId)
	{
		if (pcnTbId == 0) 
		{
			ChiTietItems.Clear();
			return;
		}
		try
		{
			IsLoading = true;
			var res = await _chitietclient.GetList(pcnTbId);
			if (!res.Success)
			{
				SnackbarHelper.ShowError(res.Message);
				return;
			}
			if (res.Data == null) return;
			ChiTietItems.Clear();
			foreach (var item in res.Data)
				ChiTietItems.Add(item);


			var chitietview = CollectionViewSource.GetDefaultView(GridChiTietThietBi.ItemsSource);
			chitietview.SortDescriptions.Clear();
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

		var win = new ThemThietBiPhong(_id,_name)
		{
			Owner = parentWindow
		};
		var result = win.ShowDialog();
		if (result == true)
		{
			await LoadData();
			SnackbarHelper.ShowSuccess("Thêm thiết bị thành công!");
		}

		if (overlay != null)
			overlay.Visibility = Visibility.Collapsed;
	}
	private async void View_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PCNThietBiReadModel item)
		{
			if (item == null) return;
			await LoadChiTietThietBi(item.PCN_TB_ID);
		}
	}
	// ===== EDIT =====
	private async void Edit_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is ChiTietPCNThietBiListReadModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var win = new CapNhatChiTietThietBi(item.ChiTietID)
			{
				Owner = parentWindow
			};
			var result = win.ShowDialog();
			if (result == true)
			{
				await LoadData();
				await LoadChiTietThietBi(item.ChiTietID);
				SnackbarHelper.ShowSuccess("Cập nhật thiết bị thành công!");
			}

			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}
	private async void Delete_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is ChiTietPCNThietBiListReadModel item)
		{
			bool confirm = await MessageHelper.Confirm("Bạn có chắc muốn xóa thiết bị này không?");
			if (!confirm) return;
			var result = await _chitietclient.Delete(item.ChiTietID);
			if (result.Success)
			{
				await LoadData();
				await LoadChiTietThietBi(item.ChiTietID);
				SnackbarHelper.ShowSuccess("Xóa thiết bị thành công!");
			}
			else
			{
				SnackbarHelper.ShowError(result.Message);
			}
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

	private void Back_Click(object sender, RoutedEventArgs e)
	{
		var parent = Window.GetWindow(this) as appClinic;
		parent?.OpenPage(new PhongChucNangPage(), "Quản lý phòng chức năng");
	}

}

