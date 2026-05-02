using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;
using HoanMyClinic.Pages.PhienKham;
using HoanMyClinic.Windows;
namespace HoanMyClinic.ViewModels.PhienKham;
public class SharedViewModel : PagedViewModel
{
	private readonly PhienKhamClient _client = new();
	private readonly NhanVienClient _nhanvien = new();

	private readonly DebounceDispatcher _search = new();

	public ObservableCollection<PhienKhamReadListModel> Items { get; set; } = new();
	public ObservableCollection<NameHelper> Doctors { get; set; } = new();
	public ObservableCollection<string> Statuses { get; set; } = new();

	#region FILTER

	private string _keyword = "";
	public string Keyword
	{
		get => _keyword;
		set
		{
			_keyword = value;
			OnPropertyChanged();

			_search.Debounce(400, async () =>
			{
				await Ui.RunAsync(async () =>
				{
					Page = 1;
					await LoadData();
				});
			});
		}
	}

	private int? _selectedDoctorId;
	public int? SelectedDoctorId
	{
		get => _selectedDoctorId;
		set
		{
			_selectedDoctorId = value;
			OnPropertyChanged();
			_ = Reload();
		}
	}

	private string? _selectedStatus;
	public string? SelectedStatus
	{
		get => _selectedStatus;
		set
		{
			_selectedStatus = value;
			OnPropertyChanged();

			_ = Reload();
		}
	}

	#endregion

	#region COMMAND

	public ICommand RefreshCommand => new RelayCommand(async() =>
	{
		Keyword = "";
		SelectedDoctorId = 0;
		SelectedStatus = Statuses.FirstOrDefault();
		Page = 1;
		await LoadData();
	});

	public ICommand ViewCommand => new RelayCommandWithParam<PhienKhamReadListModel>(item =>
	{
		if (item == null) return Task.CompletedTask;
		if (Application.Current.MainWindow is appClinic app)
		{
			app.OpenPage(new ViewPage(item.PhienKhamID),
			$"Xem phiên khám: {item.PhienKhamID}"
			);
		}

		return Task.CompletedTask;
	});

	#endregion

	#region LOAD

	public async Task Init()
	{
		await LoadCombobox();
		await LoadData();
	}

	private async Task Reload()
	{
		Page = 1;
		await LoadData();
	}

	private async Task LoadCombobox()
	{
		var res = await _nhanvien.GetComboboxDoctor();

		if (res.Success && res.Data != null)
		{
			res.Data.Insert(0, new NameHelper { Id = 0, Name = "Tất cả" });

			Doctors.Clear();
			foreach (var d in res.Data)
				Doctors.Add(d);
		}

		Statuses.Clear();
		Statuses.Add("Tất cả");
		Statuses.Add("Đang chờ");
		Statuses.Add("Đang khám");
		Statuses.Add("Hoàn thành");
		Statuses.Add("Đã hủy");
	}

	protected override async Task LoadData()
	{
		try
		{
			IsLoading = true;

			int? doctorId = SelectedDoctorId == 0 ? null : SelectedDoctorId;
			string? status = SelectedStatus == "Tất cả" ? null : SelectedStatus;

			var res = string.IsNullOrWhiteSpace(Keyword)
				? await _client.GetPaged(Page, SizePage, doctorId, status)
				: await _client.Search(Keyword, Page, SizePage, doctorId);

			if (!res.Success)
			{
				await MessageHelper.ShowMessage(res.Message);
				return;
			}

			await Ui.Run(() =>
			{
				Items.Clear();
				foreach (var i in res.Data!.Items)
					Items.Add(i);
			});

			TotalPages = (int)Math.Ceiling(
				(double)res.Data!.TotalCount / res.Data.PageSize);
		}
		finally
		{
			IsLoading = false;
		}
	}

	#endregion
}