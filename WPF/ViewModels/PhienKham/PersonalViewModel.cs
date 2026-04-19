using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WPF.Client;
using WPF.Common;
using WPF.Models;
using WPF.Pages.PhienKham;
using WPF.Windows;

namespace WPF.ViewModels.PhienKham;

public class PersonalViewModel : PagedViewModel
{
	private readonly PhienKhamClient _client = new();
	private readonly DebounceDispatcher _searchDebounce = new();
	private readonly DebounceDispatcher _pageSizeDebounce = new();

	public ObservableCollection<PhienKhamReadListModel> Items { get; set; } = new();

	#region SEARCH
	private string _keyword = "";
	public string Keyword
	{
		get => _keyword;
		set
		{
			if (_keyword == value) return;

			_keyword = value;
			OnPropertyChanged();

			_searchDebounce.Debounce(400, async () =>
			{
				await Ui.RunAsync(async () =>
				{
					Page = 1;
					await LoadData();
				});
			});
		}
	}
	#endregion

	#region PAGE SIZE
	private string _pageSizeInput = "15";
	public string PageSizeInput
	{
		get => _pageSizeInput;
		set
		{
			if (_pageSizeInput == value) return;

			_pageSizeInput = value;
			OnPropertyChanged();

			_pageSizeDebounce.Debounce(400, async () =>
			{
				if (!int.TryParse(_pageSizeInput, out int size) || size <= 0)
					return;

				await Ui.RunAsync(async () =>
				{
					SizePage = size;
					Page = 1;
					await LoadData();
				});
			});
		}
	}
	#endregion

	#region FILTER
	private string _status = "Tất cả";
	public string Status
	{
		get => _status;
		set
		{
			if (_status == value) return;

			_status = value;
			OnPropertyChanged();

			_ = RefreshAsync();
		}
	}

	public ObservableCollection<string> Statuses { get; set; } = new()
	{
		"Tất cả",
		"Đang chờ",
		"Đang khám",
		"Hoàn thành",
		"Đã hủy"
	};
	#endregion

	#region COMMANDS

	private async Task RefreshAsync()
	{
		Keyword = "";
		Page = 1;
		await LoadData();
	}

	public ICommand RefreshCommand => new RelayCommand(async () =>
	{
		Status = "Tất cả";
		await RefreshAsync();
	});

	public ICommand StartCommand => new RelayCommandWithParam<PhienKhamReadListModel>(async item =>
	{
		if (item == null) return;

		if (item.TrangThai != "Đang chờ" && item.TrangThai != "Đang khám")
		{
			SnackbarHelper.ShowError("Không thể khám.");
			return;
		}
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
		}
		if (Application.Current.MainWindow is appClinic parent)
		{
			parent.OpenPage(
				new ConsultationPage(item.PhienKhamID),
				$"Khám bệnh phiên: {item.PhienKhamID}");
		}
	});

	public ICommand ViewCommand => new RelayCommandWithParam<PhienKhamReadListModel>(item =>
	{
		if (item == null) return Task.CompletedTask;

		if (Application.Current.MainWindow is appClinic parent)
		{
			parent.OpenPage(
				new ViewPage(item.PhienKhamID),
				$"Xem phiên khám: {item.PhienKhamID}");
		}

		return Task.CompletedTask;
	});

	public ICommand CancelCommand => new RelayCommandWithParam<PhienKhamReadListModel>(async item =>
	{
		if (item == null) return;

		if (item.TrangThai != "Đang chờ" && item.TrangThai != "Đang khám")
		{
			SnackbarHelper.ShowError("Không thể hủy.");
			return;
		}

		var confirm = await MessageHelper.Confirm("Xác nhận hủy phiên khám?");
		if (!confirm) return;

		var res = await _client.Cancel(item.PhienKhamID);
		if (!res.Success)
		{
			SnackbarHelper.ShowError(res.Message);
			return;
		}

		SnackbarHelper.ShowSuccess("Đã hủy phiên khám.");
		await LoadData();
	});

	#endregion

	#region LOAD

	protected override async Task LoadData()
	{
		try
		{
			IsLoading = true;

			int? nhanvienid = Session.NhanVienId;
			string? trangthai = Status == "Tất cả" ? null : Status;

			var res = string.IsNullOrWhiteSpace(Keyword)
				? await _client.GetPaged(Page, SizePage, nhanvienid, trangthai)
				: await _client.Search(Keyword, Page, SizePage, nhanvienid);

			if (!res.Success)
			{
				await MessageHelper.ShowMessage(res.Message);
				return;
			}

			await Ui.Run(() =>
			{
				Items.Clear();
				foreach (var x in res.Data!.Items)
					Items.Add(x);
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