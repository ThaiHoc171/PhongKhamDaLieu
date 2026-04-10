using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Pages.LichLamViec;

public partial class XemLichCaNhan : Page, INotifyPropertyChanged
{
	private readonly LichLamViecClient _client = new();
	private LichLamViecReadWeekModel _personalData = new();
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

	public XemLichCaNhan()
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
			int nhanVienId = Session.NhanVienId ?? 0;
			if (nhanVienId == 0)
			{
				//IsLoading = false;
				ClearCalendar();
				return;
			}

			var result = await _client.GetByNhanVien(nhanVienId, page);

			if (!result.Success || result.Data == null)
			{
				//IsLoading = false;
				ClearCalendar();
				return;
			}

			_personalData = result.Data;
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
		if (_personalData?.LichLamViecs == null) return;

		int page = GetSelectedPage();
		var today = DateTime.Today;
		int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
		var currentMonday = today.AddDays(-diff).Date;
		var monday = currentMonday.AddDays(page * 7);

		var days = Enumerable.Range(0, 7).Select(i => monday.AddDays(i)).ToList();

		icHeader.ItemsSource = days.Select(d => d.ToString("ddd dd/MM")).ToList();

		var morning = new List<DayShiftViewModel>();
		var afternoon = new List<DayShiftViewModel>();

		foreach (var day in days)
		{
			var dayShifts = _personalData.LichLamViecs
				.Where(x => x.Ngay.ToLocalTime().Date == day)
				.ToList();

			var morningShift = dayShifts.FirstOrDefault(x => x.CaLamViec == 1);
			var afternoonShift = dayShifts.FirstOrDefault(x => x.CaLamViec == 2);

			morning.Add(new DayShiftViewModel
			{
				Date = day,
				ShiftDisplay = morningShift != null ? "Có ca làm việc" : "Trống",
				Color = morningShift != null
					? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2196F3"))
					: new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B0BEC5"))
			});

			afternoon.Add(new DayShiftViewModel
			{
				Date = day,
				ShiftDisplay = afternoonShift != null ? "Có ca làm việc" : "Trống",
				Color = afternoonShift != null
					? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50"))
					: new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B0BEC5"))
			});
		}

		icMorning.ItemsSource = morning;
		icAfternoon.ItemsSource = afternoon;
	}

	private void cbWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (cbWeek.SelectedValue is int page)
		{
			Page = page;
			_ = LoadDataAsync();
		}
	}
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