using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.ViewModels.CaKham;

public class AcceptedViewModel : PagedViewModel
{
	private readonly CaKhamClient _client = new();
	private readonly PhienKhamClient _phien = new();

	public ObservableCollection<CaKhamListReadModel> Items { get; set; } = new();

	#region FILTER
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

	public ICommand OpenVisitCommand => new RelayCommandWithParam<CaKhamListReadModel>(async item =>
	{
		if (item == null) return;

		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);
		if (item.LoaiCaKham == "Khám")
		{
			var confirm = await MessageHelper.Confirm("Tạo phiên khám?");
			if (!confirm)
			{
				OverlayHelper.Hide(overlay);
				return;
			}

			var result = await _phien.Create(item.CaKhamID);

			if (result.Success)
			{
				await _client.UpdateTrangThai(item.CaKhamID,
					new CaKhamTrangThaiDTO { TrangThai = "Đang khám" });

				SnackbarHelper.ShowSuccess("Tạo phiên khám thành công");
				await LoadData();
			}
			else
				SnackbarHelper.ShowError(result.Message);
		}
		if(item.LoaiCaKham == "Điều trị")
		{
			SnackbarHelper.ShowWarning("Tính năng đang phát triển");
		}

		OverlayHelper.Hide(overlay);
	});

	public ICommand CancelCommand =>
		new RelayCommandWithParam<CaKhamListReadModel>(async item =>
		{
			if (item == null) return;

			var confirm = await MessageHelper.Confirm("Bạn có chắc muốn hủy ca khám này?");
			if (!confirm) return;

			var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
			OverlayHelper.Show(overlay);

			try
			{
				var result = await _client.Cancel(item.CaKhamID);

				if (result.Success)
				{
					await LoadData();
					SnackbarHelper.ShowSuccess("Đã hủy ca khám!");
				}
				else
				{
					SnackbarHelper.ShowError(result.Message);
				}
			}
			finally
			{
				OverlayHelper.Hide(overlay);
			}
		});

	public ICommand NoShowCommand => new RelayCommandWithParam<CaKhamListReadModel>(async item =>
	{
		if (item == null) return;

		var parts = item.TenKhungGio.Split('-');
		if (!TimeSpan.TryParse(parts[0].Trim(), out var start))
		{
			SnackbarHelper.ShowError("Sai format giờ");
			return;
		}

		var startTime = item.NgayKham.Date + start;

		if (DateTime.Now < startTime.AddHours(1))
		{
			SnackbarHelper.ShowWarning("Chưa đủ 1h để đánh dấu không đến");
			return;
		}

		var confirm = await MessageHelper.Confirm("Đánh dấu không đến?");
		if (!confirm) return;

		var res = await _client.UpdateTrangThai(item.CaKhamID,
			new CaKhamTrangThaiDTO { TrangThai = item.TrangThai, GhiChu = "Không đến" });
		if (res.Success)
		{
			await LoadData();
			SnackbarHelper.ShowSuccess("Đã hủy ca khám");
		}
		await _client.Cancel(item.CaKhamID);
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
				"Đã xác nhận",
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