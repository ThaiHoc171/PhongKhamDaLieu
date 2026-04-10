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

namespace WPF.Pages.PhienKham;

public partial class PhienKhamCaNhanPage : Page
{
	public PhienKhamCaNhanPage()
	{
		InitializeComponent();
		DataContext = this;

		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns();

		GridContent.ItemsSource = Items;

		Loaded += async (_, __) =>
		{
			LoadCombobox();
			await LoadData();
		};

		PreviewMouseDown += async (_, __) =>
		{
			if (txtSizepage.IsKeyboardFocusWithin)
				await ApplyPageSize();
		};
	}

	#region PAGING

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
			await ApplyPageSize();
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

	public ObservableCollection<PhienKhamReadListModel> Items { get; set; } = new();

	private readonly PhienKhamClient _client = new();
	private readonly NhanVienClient _nhanvien = new();

	private void SetupColumns()
	{
		GridContent.Columns.Clear();

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("PhienKhamID")
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Bệnh nhân",
			Binding = new Binding("BenhNhan"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Bác sĩ",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("NhanVien"),
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
			Header = "Chẩn đoán cuối",
			Binding = new Binding("ChanDoanCuoi"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});

		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Play", Start_Click, "Khám", "Đang chờ,Đang khám"));
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Cancel", Cancel_Click, "Hủy", "Đang chờ,Đang khám"));
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Eye", View_Click, "Xem"));

	}

	private void LoadCombobox()
	{
		var listStatus = new List<string>
		{
			"Tất cả",
			"Đang chờ",
			"Đang khám",
			"Hoàn thành",
			"Đã hủy"
		};

		cboStatus.ItemsSource = listStatus;
		cboStatus.SelectedIndex = 0;
	}

	private async Task LoadData()
	{
		try
		{
			IsLoading = true;

			Keyword = txt_Search.Text;

			int? nhanvienid = Session.NhanVienId;
			string? trangthai = null;

			if (cboStatus.SelectedValue is string status && status != "Tất cả")
				trangthai = status;

			var res = string.IsNullOrWhiteSpace(Keyword)
				? await _client.GetPaged(Page, SizePage, nhanvienid, trangthai)
				: await _client.Search(Keyword, Page, SizePage, nhanvienid);

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
		Page = 1;
		await LoadData();
	}

	private async void cboDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (!IsLoaded) return;
		Page = 1;
		await LoadData();
	}

	private async void cboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (!IsLoaded) return;
		Page = 1;
		await LoadData();
	}
	private async void Cancel_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PhienKhamReadListModel item)
		{
			if (item.TrangThai == "Đang chờ" || item.TrangThai == "Đang khám")
			{
				await MessageHelper.ShowMessage("Xác nhận hủy phiên khám?");
			}
			else
			{
				await MessageHelper.ShowMessage("Không thể hủy.");
			}
		}
	}
	private async void Start_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PhienKhamReadListModel item)
		{
			var parent = Window.GetWindow(this) as appClinic;
			if (item.TrangThai == "Đang chờ")
			{
				var confirm = await MessageHelper.Confirm($"Xác nhận bắt đầu khám? \n Bệnh nhân: {item.BenhNhan}");
				if (!confirm) return;
				var res = await _client.Start(item.PhienKhamID);
				if (!res.Success)
				{
					SnackbarHelper.ShowError(res.Message);
					return;
				}
				parent?.OpenPage(new KhamPage(item.PhienKhamID), $"Khám bệnh phiên: {item.PhienKhamID}");
			}
			else if (item.TrangThai == "Đang khám")
			{
				parent?.OpenPage(new KhamPage(item.PhienKhamID),$"Khám bệnh phiên: {item.PhienKhamID}");
			}
			else
			{
				SnackbarHelper.ShowError("Không thể khám.");
			}
		}
	}


	private void View_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is PhienKhamReadListModel item)
		{
			var parent = Window.GetWindow(this) as appClinic;
			parent?.OpenPage(new XemPhienKham(item.PhienKhamID), $"Xem phiên khám: {item.PhienKhamID}");
		}
	}

}
