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
using WPF.Windows.KhamBenh;
using WPF.Windows.LoaiBenh;

namespace WPF.Pages;

public partial class PhienKhamCLSPage : Page, INotifyPropertyChanged
{
    public PhienKhamCLSPage()
    {
		InitializeComponent();
		DataContext = this;
		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns();
		LoadCombobox();
		Loaded += async (_, __) => await LoadData();
		PreviewMouseDown += async (_, __) =>
		{
			if (txtSizepage.IsKeyboardFocusWithin)
			{
				await ApplyPageSize();
			}
		};
	}
	#region paged
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
	public ObservableCollection<PhienKhamClsReadListModel> Items { get; set; } = new();

	private readonly PhienKhamClsClient _client = new();
	private void LoadCombobox()
	{
		var list = new List<string> { "Tất cả", "Đang chờ", "Đang thực hiện", "Hoàn thành", "Đã hủy" };
		cboStatus.ItemsSource = list;
		cboStatus.SelectedIndex = 0;
	}
	private void SetupColumns()
	{
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("PhienKhamCLSID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên CLS",
			Binding = new Binding("TenCLS"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Kết quả",
			Binding = new Binding("KetQua"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày thực hiện",
			Binding = new Binding("NgayThucHien")
			{
				StringFormat = "dd/MM/yyyy"
			},
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Play", Accept_Click, "Nhận thực hiện", "Đang chờ,Đang thực hiện"));
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Eye", View_Click, "Xem chi tiết"));
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Cancel", Cancel_Click, "Hủy", "Đang chờ,Đang thực hiện"));
	}
	private string? GetStatus()
	{
		if (cboStatus.SelectedItem == null)
			return null;

		var status = cboStatus.SelectedItem.ToString();

		if (status == "Tất cả")
			return null;

		return status;
	}
	private async Task LoadData()
	{
		try
		{
			IsLoading = true;
			string? status = GetStatus();

			var res = string.IsNullOrWhiteSpace(Keyword)
				? await _client.GetPaged(Page, SizePage, status)
				: await _client.Search(Keyword, status, Page, SizePage);

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
	private async void cboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		Page = 1;
		await LoadData();
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
	// ===== EDIT =====
	private async void Accept_Click(object sender, RoutedEventArgs e)
	{
		if (sender is not Button btn || btn.Tag is not PhienKhamClsReadListModel item)
			return;

		var parentWindow = Window.GetWindow(this);
		var overlay = parentWindow?.FindName("Overlay") as Border;

		if (overlay != null)
			overlay.Visibility = Visibility.Visible;

		try
		{
			// ===== Nếu đang chờ -> Accept =====
			if (item.TrangThai == "Đang chờ")
			{
				var confirm = await MessageHelper.Confirm("Bạn có chắc chắn muốn nhận thực hiện CLS này?");
				if (!confirm)
					return;

				if (Session.NhanVienId == null)
				{
					await MessageHelper.ShowMessage("Không xác định được nhân viên thực hiện!");
					return;
				}

				var req = new AcceptClsDTO
				{
					NhanVienThucHienID = Session.NhanVienId.Value
				};

				var res = await _client.Accept(item.PhienKhamCLSID, req);

				if (!res.Success)
				{
					await MessageHelper.ShowMessage(res.Message);
					return;
				}
			}
			if (item.TrangThai == "Đang thực hiện" || item.TrangThai == "Đang chờ")
			{
				var win = new ThucHienCLS(item.PhienKhamCLSID)
				{
					Owner = parentWindow
				};

				var result = win.ShowDialog();

				if (result == true)
				{
					await LoadData();
					SnackbarHelper.ShowSuccess("Cập nhật CLS thành công!");
				}
			}
		}
		finally
		{
			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}
	private void View_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PhienKhamClsReadListModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			try
			{
				if (overlay != null)
					overlay.Visibility = Visibility.Visible;

				var win = new XemPhienKhamCLS(item.PhienKhamCLSID)
				{
					Owner = parentWindow
				};
				win.ShowDialog();
			}
			finally
			{
				if (overlay != null)
					overlay.Visibility = Visibility.Collapsed;
			}
		}
	}
	private async void Cancel_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PhienKhamClsReadListModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var confirm = await MessageHelper.Confirm($"Bạn có chắc muốn hủy CLS: {item.TenCLS} không?");
			if (!confirm)
			{
				if (overlay != null)
					overlay.Visibility = Visibility.Collapsed;
				return;
			}
			var res = await _client.Cancel(item.PhienKhamCLSID);
			if (!res.Success)
			{
				SnackbarHelper.ShowError(res.Message);
				if (overlay != null)
					overlay.Visibility = Visibility.Collapsed;
				return;
			}
			await LoadData();
			SnackbarHelper.ShowSuccess("Đã hủy yêu cầu CLS!");

			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}

}
