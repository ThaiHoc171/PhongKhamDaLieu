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

namespace WPF.Pages;
public partial class TaiKhoanPage : Page, INotifyPropertyChanged
{
	public TaiKhoanPage()
	{
		InitializeComponent();
		DataContext = this;
		SetupDataGrid.ApplyStyle(GridContent);
		SetupColumns();
		SetupCombobox();
		Loaded += async (_, __) => await LoadData();
		PreviewMouseDown += async (_, __) =>
		{
			if (txtSizepage.IsKeyboardFocusWithin)
			{
				await ApplyPageSize();
			}
		};
	}
	private readonly TaiKhoanClient _client = new();

	public ObservableCollection<TaiKhoanListReadModel> Items { get; set; } = new();

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

	private void SetupCombobox()
	{
		cboRole.ItemsSource = new List<string> { "Tất cả","Admin", "Bệnh nhân", "Khách", "Nhân viên" };
		cboRole.SelectedIndex = 0;
		cboStatus.ItemsSource = new List<string> { "Tất cả", "Hoạt động", "Bị khóa" };
		cboStatus.SelectedIndex = 0;
	}
	private void SetupColumns()
	{
		GridContent.Columns.Clear();
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Mã",
			Visibility = Visibility.Collapsed,
			Binding = new Binding("ID"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Email",
			Binding = new Binding("Email"),
			Width = new DataGridLength(3, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Vai trò",
			Binding = new Binding("VaiTro"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridContent.Columns.Add(new DataGridTextColumn
		{
			Header = "Trạng thái",
			Binding = new Binding("TrangThai"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		// BUTTON
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Sync", Reset_Click, "Reset mật khẩu"));
		GridContent.Columns.Add(SetupDataGrid.CreateIconButtonColumn("Power", ToggleStatus_Click, "Đổi trạng thái"));
	}

	private async Task LoadData()
	{
		try
		{
			IsLoading = true;


			string vaiTro = "";
			string trangThai = "";
			if (cboRole.SelectedIndex > 0)
			{
				vaiTro = cboRole.SelectedItem.ToString()!;
			}
			if (cboStatus.SelectedIndex > 0)
			{
				trangThai = cboStatus.SelectedItem.ToString()!;
			}
			var res = string.IsNullOrWhiteSpace(Keyword)
				? await _client.GetPaged(Page, SizePage, vaiTro, trangThai)
				: await _client.Search(Page,SizePage, Keyword, vaiTro, trangThai);
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

	private async void Reset_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is TaiKhoanListReadModel item)
		{
			bool confirm = await MessageHelper.Confirm(
				$"Bạn có chắc muốn reset mật khẩu tài khoản:\n{item.Email} về mặc định?"
			);
			if (!confirm) return;
			var res = await _client.ResetPassword(item.Id);
			if (!res.Success)
			{
				SnackbarHelper.ShowError(res.Message);
				return;
			}
			SnackbarHelper.ShowSuccess("Reset mật khẩu thành công!");
		}
	}	


	private async void ToggleStatus_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button btn && btn.Tag is TaiKhoanListReadModel item)
		{
			string newStatus = "";
			bool confirm = false;
			if (item.TrangThai == "Hoạt động")
			{
				newStatus = "Bị khóa";
				confirm = await MessageHelper.Confirm(
					$"Bạn có chắc muốn vô hiệu hóa tài khoản:\n{item.Email}?"
				);
			}
			else
			{
				newStatus = "Hoạt động";
				confirm = await MessageHelper.Confirm(
					$"Bạn có chắc muốn kích hoạt lại tài khoản:\n{item.Email}?"
				);
			}
			if (!confirm) return;

			var req = new TaiKhoanUpdateRequestDTO
			{
				TrangThai = newStatus
			};
			var res = await _client.UpdateStatus(item.Id, req);

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

	private async void cboRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		Page = 1;
		await LoadData();
	}

	private async void cboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		Page = 1;
		await LoadData();
	}
}

