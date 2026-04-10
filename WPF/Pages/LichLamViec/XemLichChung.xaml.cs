	using System.ComponentModel;
	using System.Runtime.CompilerServices;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using WPF.Client;
	using WPF.Common;
	using WPF.Models;

	namespace WPF.Pages.LichLamViec;

	public partial class XemLichChung : Page, INotifyPropertyChanged
	{
		private readonly LichLamViecClient _client = new();
		private List<LichLamViecReadModel> _allData = new();
		private int _page = 0;
		private int MinPage => (_allWeeks?.FirstOrDefault()?.Page) ?? -4;
		private int MaxPage => (_allWeeks?.LastOrDefault()?.Page) ?? 4;
		private List<WeekItem>? _allWeeks;
		public int Page
		{
			get => _page;
			set
			{
				if (_page != value)
				{
					_page = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(CanGoPrev));
					OnPropertyChanged(nameof(CanGoNext));
				}
			}
		}
		public bool CanGoPrev => Page > MinPage;
		public bool CanGoNext => Page < MaxPage;

		private bool _isLoading;
		public bool IsLoading
		{
			get => _isLoading;
			set { _isLoading = value; OnPropertyChanged(); }
		}

		public XemLichChung()
		{
			InitializeComponent();
			DataContext = this;
			SetupWeekComboBox();
			_ = LoadDataAsync();
		}

		private void SetupWeekComboBox()
		{
			_allWeeks = Enumerable.Range(-4, 9)
				.Select(p => new WeekItem
				{
					Page = p,
					Display = WeekHelper.GetWeekDisplay(p)
				})
				.ToList();

			cbWeek.ItemsSource = _allWeeks;
			cbWeek.DisplayMemberPath = "Display";
			cbWeek.SelectedValuePath = "Page";
			Page = 0;
			cbWeek.SelectedValue = Page;
		}

		private int GetSelectedPage() => cbWeek.SelectedItem is WeekItem w ? w.Page : 0;

		private async Task LoadDataAsync()
		{
			IsLoading = true;
			try
			{
				int page = GetSelectedPage();

				var result = await _client.GetWeek(page);

				if (!result.Success || result.Data == null)
				{
					ClearCalendar();
					return;
				}

				_allData = result.Data;
				RenderCalendar();
			}
			catch
			{
				ClearCalendar();
			}
			finally
			{
				IsLoading = false;
			}
		}

		private void ClearCalendar()
		{
			icHeader.ItemsSource = null;
			icMorning.ItemsSource = null;
			icAfternoon.ItemsSource = null;
		}

		private void RenderCalendar()
		{
			if (_allData == null) return;

			var days = GetWeekDays();
			icHeader.ItemsSource = days.Select(d => d.ToString("ddd dd/MM"));

			icMorning.ItemsSource = BuildShift(days, 1, "#2196F3");
			icAfternoon.ItemsSource = BuildShift(days, 2, "#4CAF50");
		}
		private List<DateTime> GetWeekDays()
		{
			int page = GetSelectedPage();
			var today = DateTime.Today;

			int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
			var monday = today.AddDays(-diff).Date.AddDays(page * 7);

			return Enumerable.Range(0, 7)
				.Select(i => monday.AddDays(i))
				.ToList();
		}
		private List<DayShiftViewsModel> BuildShift(List<DateTime> days, int ca, string color)
		{
			var result = new List<DayShiftViewsModel>();

			foreach (var day in days)
			{
				var employees = _allData
					.Where(x => x.Ngay.ToLocalTime().Date == day && x.CaLamViec == ca).ToList();

				result.Add(new DayShiftViewsModel
				{
					Date = day,
					NhanViens = employees,
					Color = new SolidColorBrush(
						(Color)ColorConverter.ConvertFromString(
							employees.Any() ? color : "#B0BEC5"
						))
				});
			}

			return result;
		}
		private string FormatEmployee(LichLamViecReadModel x)
		{
			var text = $"{x.ChucVu} {x.NhanVien.Name}";

			if (!string.IsNullOrWhiteSpace(x.TenPhong))
				text += $" • {x.TenPhong}";

			if (!string.IsNullOrWhiteSpace(x.GhiChu))
				text += $" ({x.GhiChu})";

			return text;
		}

		private void cbWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (cbWeek.SelectedValue is int page)
			{
				Page = page;
				_ = LoadDataAsync();
			}
		}

		private void Refresh_Click(object sender, RoutedEventArgs e) => _ = LoadDataAsync();

		private async void First_Click(object sender, RoutedEventArgs e)
		{
			Page = MinPage;
			cbWeek.SelectedValue = Page;
			await LoadDataAsync();
		}

		private async void Last_Click(object sender, RoutedEventArgs e)
		{
			Page = MaxPage;
			cbWeek.SelectedValue = Page;
			await LoadDataAsync();
		}

		private async void Prev_Click(object sender, RoutedEventArgs e)
		{
			if (CanGoPrev)
			{
				Page--;
				cbWeek.SelectedValue = Page;
				await LoadDataAsync();
			}
		}

		private async void Next_Click(object sender, RoutedEventArgs e)
		{
			if (CanGoNext)
			{
				Page++;
				cbWeek.SelectedValue = Page;
				await LoadDataAsync();
			}
		}
		public event PropertyChangedEventHandler? PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string name = "")
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}