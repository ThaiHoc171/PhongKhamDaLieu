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
using WPF.Windows.CaKham;

namespace WPF.Pages.CaKham;

public partial class CaKhamDaXacNhan : Page, INotifyPropertyChanged
{
	private readonly CaKhamClient _client = new();
	private readonly PhienKhamClient _phienkham = new();
	#region Paged
	public ObservableCollection<CaKhamListReadModel> Items { get; set; } = new();

	private int _page = 1;
	public int Page
	{
		get => _page;
		set { _page = value; OnPropertyChanged(); }
	}

	private int _sizePage = 12;
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
			await ApplyPageSize();
	}

	private async void SizePage_LostFocus(object sender, RoutedEventArgs e)
	{
		await ApplyPageSize();
	}

	// PAGING
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
	#endregion
	public CaKhamDaXacNhan()
	{
		InitializeComponent();

		DataContext = this;

		SetupDataGrid.ApplyStyle(GridContent);

		SetupColumns();

		dtpDate.SelectedDate = DateTime.Today;

		cbCategory.ItemsSource = new List<string>
		{
			"Khám",
			"Điều trị"
		};

		cbCategory.SelectedIndex = 0;

		Loaded += async (_, __) => await LoadData();

		PreviewMouseDown += async (_, __) =>
		{
			if (txtSizepage.IsKeyboardFocusWithin)
				await ApplyPageSize();
		};
	}

	private void SetupColumns()
	{
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("CaKhamID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Họ và tên",
			Binding = new Binding("HoTen"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Lý do",
			Binding = new Binding("LyDoKham"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày khám",
			Binding = new Binding("NgayKham")
			{
				StringFormat = "dd/MM/yyyy"
			},
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Khung giờ",
			Binding = new Binding("TenKhungGio"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});


		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Plus", OpenVisit_Click, "Tạo phiên khám"));
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Cancel", Cancel_Click, "Hủy phiên khám"));
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Minus", MarkNoShow_Click, "Bệnh nhân không đến"));
	}

	private async Task LoadData()
	{
		try
		{
			IsLoading = true;

			var ngay = dtpDate.SelectedDate ?? DateTime.Today;

			var loai = cbCategory.SelectedItem?.ToString() ?? "Khám";

			var res = await _client.GetPaged(ngay, "Đã xác nhận", loai, Page, SizePage);

			if (!res.Success)
			{
				await MessageHelper.ShowMessage(res.Message);
				return;
			}

			if (res.Data == null) return;

			Items.Clear();

			foreach (var item in res.Data.Items)
				Items.Add(item);

			TotalPages = (int)Math.Ceiling(
				(double)res.Data.TotalCount / res.Data.PageSize);

			var view = CollectionViewSource.GetDefaultView(GridContent.ItemsSource);

			view.SortDescriptions.Clear();

			view.SortDescriptions.Add(
				new SortDescription("NgayKham", ListSortDirection.Ascending));
		}
		finally
		{
			IsLoading = false;
		}
	}


	// REFRESH
	private async void Refresh_Click(object sender, RoutedEventArgs e)
	{
		Page = 1;
		await LoadData();
	}
	private async void OpenVisit_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is CaKhamListReadModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var confirm = await MessageHelper.Confirm("Xác nhận tạo phiên khám cho ca khám này?");
			if (!confirm)
			{
				if (overlay != null)
					overlay.Visibility = Visibility.Collapsed;
				return;
			}
			var result = await _phienkham.Create(item.CaKhamID);
			if (result.Success)
			{
				var req = new CaKhamTrangThaiDTO
				{
					TrangThai = "Đang khám",
					GhiChu = null
				};
				await _client.UpdateTrangThai(item.CaKhamID,req);
				SnackbarHelper.ShowSuccess("Phiên khám đã được tạo thành công!");
				await LoadData();
			}
			else
				SnackbarHelper.ShowError("Tạo phiên khám thất bại: " + result.Message);

			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}
	private async void Cancel_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is CaKhamListReadModel item)
		{
			var parentWindow = Window.GetWindow(this);
			var overlay = parentWindow.FindName("Overlay") as Border;

			if (overlay != null)
				overlay.Visibility = Visibility.Visible;

			var win = new TuChoiCaKham(item.CaKhamID)
			{
				Owner = parentWindow
			};

			var result = win.ShowDialog();

			if (result == true)
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Ca khám đã được hủy!");
			}

			if (overlay != null)
				overlay.Visibility = Visibility.Collapsed;
		}
	}
	private async void MarkNoShow_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is CaKhamListReadModel item)
		{
			var parts = item.TenKhungGio.Split('-');
			var startTimeStr = parts[0].Trim();
			if (!TimeSpan.TryParse(startTimeStr, out var startTime))
			{
				SnackbarHelper.ShowError("Không đọc được thời gian khung giờ!");
				return;
			}
			var startDateTime = item.NgayKham.Date + startTime;
			if (DateTime.Now < startDateTime.AddHours(1))
			{
				SnackbarHelper.ShowWarning("Chỉ có thể đánh dấu không đến sau 1 giờ kể từ thời gian khám!");
				return;
			}
			var confirm = await MessageHelper.Confirm("Xác nhận đánh dấu bệnh nhân không đến?");
			if (!confirm)
				return;

			var req = new CaKhamTrangThaiDTO
			{
				TrangThai = "Không đến",
				GhiChu = null
			};
			var result = await _client.UpdateTrangThai(item.CaKhamID, req);

			if (result.Success)
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Đã đánh dấu bệnh nhân không đến!");
			}
		}
	}
	private async void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		Page = 1;
		await LoadData();
	}

	private async void dtpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
	{
		Page = 1;
		await LoadData();
	}
}