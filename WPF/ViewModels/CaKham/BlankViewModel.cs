using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;
using HoanMyClinic.Windows.CaKham;

namespace HoanMyClinic.ViewModels.CaKham;

public class BlankViewModel : PagedViewModel
{
	private readonly CaKhamClient _client = new();

	public ObservableCollection<CaKhamListReadModel> Items { get; set; } = new();

	#region FILTER

	private int? _selectedDoctorId;
	public int? SelectedDoctorId
	{
		get => _selectedDoctorId;
		set
		{
			_selectedDoctorId = value;
			OnPropertyChanged();
			Reload();
		}
	}
	private DateTime _selectedDate = DateTime.Today;
	public DateTime SelectedDate
	{
		get => _selectedDate;
		set
		{
			_selectedDate = value;
			OnPropertyChanged();
			Reload();
		}
	}

	public List<string> Categories { get; } = new()
	{
		"Khám",
		"Điều trị"
	};

	private string _category = "Khám";
	public string Category
	{
		get => _category;
		set
		{
			_category = value;
			OnPropertyChanged();
			Reload();
		}
	}

	#endregion

	#region COMMANDS

	public ICommand RefreshCommand => new RelayCommand(async () =>
	{
		Page = 1;
		await LoadData();
	});

	public ICommand AddCommand => new RelayCommand(async () =>
	{
		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new AddCaKham
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Tạo ca khám thành công!");
			});

		OverlayHelper.Hide(overlay);
	});

	public ICommand RegisterCommand => new RelayCommandWithParam<CaKhamListReadModel>(async item =>
	{
		if (item == null) return;

		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new Register(item.CaKhamID, item.TenKhungGio, item.NgayKham, item.NhanVien.Name)
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Đăng ký ca khám thành công!");
			});

		OverlayHelper.Hide(overlay);
	});

	#endregion

	#region LOAD

	protected override async Task LoadData()
	{
		try
		{
			IsLoading = true;

			var res = await _client.GetPaged(
				SelectedDate,
				"Trống",
				Category,
				Page,
				SizePage);

			if (!res.Success)
			{
				await MessageHelper.ShowMessage(res.Message);
				return;
			}

			await Ui.Run(() =>
			{
				Items.Clear();
				foreach (var item in res.Data!.Items)
					Items.Add(item);
			});

			TotalPages = (int)Math.Ceiling(
				(double)res.Data!.TotalCount / res.Data.PageSize);
		}
		finally
		{
			IsLoading = false;
		}
	}

	private async void Reload()
	{
		Page = 1;
		await LoadData();
	}

	#endregion
}