using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;
using HoanMyClinic.Windows.ChucVu;
using HoanMyClinic.Windows.Public;

namespace HoanMyClinic.ViewModels;

public class BaiVietViewModel : PagedViewModel
{
	private readonly BaiVietClient _client = new();
	private readonly DebounceDispatcher _search = new();
	private readonly DebounceDispatcher _pageSize = new();

	public ObservableCollection<BaiVietListReadModel> Items { get; set; } = new();

	#region SEARCH
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

			_pageSize.Debounce(400, async () =>
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
	public List<string> StatusList { get; } =
	new() { "Tất cả", "Đang điều trị", "Hoàn thành", "Đã hủy" };

	private string _selectedStatus = "Tất cả";
	public string SelectedStatus
	{
		get => _selectedStatus;
		set
		{
			_selectedStatus = value;
			OnPropertyChanged();

			Ui.RunAsync(async () =>
			{
				Page = 1;
				await LoadData();
			});
		}
	}

	private string? GetStatus()
		=> SelectedStatus == "Tất cả" ? null : SelectedStatus;
	#region COMMANDS

	public ICommand RefreshCommand => new RelayCommand(() =>
	{
		Keyword = "";
		Page = 1;
		return Task.CompletedTask;
	});


	public ICommand AddCommand => new RelayCommand(async () =>
	{
		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new AddBaiViet
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Thêm bài viết thành công!");
			});

		OverlayHelper.Hide(overlay);
	});

	public ICommand EditCommand => new RelayCommandWithParam<BaiVietListReadModel>(async item =>
	{
		if (item == null) return;

		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new UpdateBaiViet(item.BaiVietID)
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Cập nhật thành công!");
			});

		OverlayHelper.Hide(overlay);
	});


	#endregion

	#region LOAD DATA
	protected override async Task LoadData()
	{
		try
		{
			IsLoading = true;
			var status = GetStatus();

			var res = string.IsNullOrWhiteSpace(Keyword)
				? await _client.GetPaged(Page, SizePage,status)
				: await _client.Search(Keyword, Page, SizePage, status);

			if (!res.Success)
			{
				await MessageHelper.ShowMessage(res.Message);
				return;
			}

			await Ui.Run(() =>
			{
				Items.Clear();

				if (res.Data == null) return;

				foreach (var item in res.Data.Items)
					Items.Add(item);
			});

			if (res.Data != null)
			{
				TotalPages = (int)Math.Ceiling(
					(double)res.Data.TotalCount / res.Data.PageSize);
			}
		}
		finally
		{
			IsLoading = false;
		}
	}
	#endregion
}