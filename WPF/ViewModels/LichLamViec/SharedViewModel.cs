using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.ViewModels.LichLamViec;

public class SharedViewModel : PagedViewModel
{
	private readonly LichLamViecClient _client = new();

	public ObservableCollection<string> Headers { get; set; } = new();
	public ObservableCollection<DayShiftViewsModel> Morning { get; set; } = new();
	public ObservableCollection<DayShiftViewsModel> Afternoon { get; set; } = new();

	private List<LichLamViecReadModel> _allData = new();

	#region WEEK

	public ObservableCollection<WeekItem> Weeks { get; set; } = new();

	private int _page;
	public int Page
	{
		get => _page;
		set
		{
			if (_page == value) return;
			_page = value;
			OnPropertyChanged();

			LoadCommand.Execute(null);
		}
	}

	public bool CanGoPrev => Page > -4;
	public bool CanGoNext => Page < 4;

	#endregion

	#region COMMANDS

	public ICommand LoadCommand => new RelayCommand(async () =>
	{
		await LoadData();
	});

	public ICommand RefreshCommand => new RelayCommand(async () =>
	{
		await LoadData();
	});

	public ICommand FirstCommand => new RelayCommand(() =>
	{
		Page = -4;
		return Task.CompletedTask;
	});

	public ICommand LastCommand => new RelayCommand(() =>
	{
		Page = 4;
		return Task.CompletedTask;
	});

	public ICommand PrevCommand => new RelayCommand(() =>
	{
		if (!CanGoPrev) return Task.CompletedTask;
		Page--;
		return Task.CompletedTask;
	});

	public ICommand NextCommand => new RelayCommand(() =>
	{
		if (!CanGoNext) return Task.CompletedTask;
		Page++;
		return Task.CompletedTask;
	});

	#endregion

	public async Task Init()
	{
		BuildWeeks();
		Page = 0;
		await LoadData();
	}

	private void BuildWeeks()
	{
		Weeks.Clear();

		for (int i = -4; i <= 4; i++)
		{
			Weeks.Add(new WeekItem
			{
				Page = i,
				Display = WeekHelper.GetWeekDisplay(i)
			});
		}
	}

	protected override async Task LoadData()
	{
		try
		{
			IsLoading = true;

			var res = await _client.GetWeek(Page);

			if (!res.Success || res.Data == null)
			{
				Clear();
				return;
			}

			_allData = res.Data;

			Render();
		}
		finally
		{
			IsLoading = false;
		}
	}

	private void Clear()
	{
		Headers.Clear();
		Morning.Clear();
		Afternoon.Clear();
	}

	private void Render()
	{
		var days = GetWeekDays();

		Headers.Clear();
		foreach (var d in days)
			Headers.Add(d.ToString("ddd dd/MM"));

		BuildShift(days, 1, Morning, "#2196F3");
		BuildShift(days, 2, Afternoon, "#4CAF50");
	}

	private List<DateTime> GetWeekDays()
	{
		var today = DateTime.Today;

		int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
		var monday = today.AddDays(-diff).Date.AddDays(Page * 7);

		return Enumerable.Range(0, 7)
			.Select(i => monday.AddDays(i))
			.ToList();
	}

	private void BuildShift(List<DateTime> days, int ca,
		ObservableCollection<DayShiftViewsModel> target, string color)
	{
		target.Clear();

		foreach (var day in days)
		{
			var employees = _allData
				.Where(x => x.Ngay.ToLocalTime().Date == day && x.CaLamViec == ca)
				.ToList();

			target.Add(new DayShiftViewsModel
			{
				Date = day,
				NhanViens = employees,
				Color = new SolidColorBrush(
					(Color)ColorConverter.ConvertFromString(
						employees.Any() ? color : "#B0BEC5"
					))
			});
		}
	}
}