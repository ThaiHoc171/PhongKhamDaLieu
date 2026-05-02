using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

public class PersonalViewModel : PagedViewModel
{
	private readonly LichLamViecClient _client = new();

	public ObservableCollection<WeekItem> Weeks { get; set; } = new();
	public ObservableCollection<string> Headers { get; set; } = new();
	public ObservableCollection<DayShiftViewModel> Morning { get; set; } = new();
	public ObservableCollection<DayShiftViewModel> Afternoon { get; set; } = new();

	#region WEEK

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

			int nhanVienId = Session.NhanVienId ?? 0;
			if (nhanVienId == 0)
			{
				Clear();
				return;
			}

			var res = await _client.GetByNhanVien(nhanVienId, Page);

			if (!res.Success || res.Data == null)
			{
				Clear();
				return;
			}

			Render(res.Data);
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

	private void Render(LichLamViecReadWeekModel data)
	{
		Clear();

		var days = GetWeekDays();

		foreach (var d in days)
			Headers.Add(d.ToString("ddd dd/MM"));

		foreach (var day in days)
		{
			var shifts = data.LichLamViecs
				.Where(x => x.Ngay.ToLocalTime().Date == day)
				.ToList();

			var morning = shifts.FirstOrDefault(x => x.CaLamViec == 1);
			var afternoon = shifts.FirstOrDefault(x => x.CaLamViec == 2);

			Morning.Add(CreateShift(day, morning, "#2196F3"));
			Afternoon.Add(CreateShift(day, afternoon, "#4CAF50"));
		}
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

	private DayShiftViewModel CreateShift(DateTime day, object? shift, string color)
	{
		return new DayShiftViewModel
		{
			Date = day,
			ShiftDisplay = shift != null ? "Có ca làm việc" : "Trống",
			Color = new SolidColorBrush(
				(Color)ColorConverter.ConvertFromString(
					shift != null ? color : "#B0BEC5"
				))
		};
	}
}
