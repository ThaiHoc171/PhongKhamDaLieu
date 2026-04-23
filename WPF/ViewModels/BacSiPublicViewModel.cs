using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WPF.Client;
using WPF.Common;
using WPF.Models;
using WPF.Windows.ChucVu;
using WPF.Windows.Public;

namespace WPF.ViewModels;

public class BacSiPublicViewModel : PagedViewModel
{
	private readonly BacSiProfileClient _client = new();
	private readonly DebounceDispatcher _search = new();
	private readonly DebounceDispatcher _pageSize = new();

	public ObservableCollection<BacSiProfileListReadModel> Items { get; set; } = new();

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

	#region COMMANDS

	public ICommand RefreshCommand => new RelayCommand(() =>
	{
		Keyword = "";
		Page = 1;
		return Task.CompletedTask;
	});

	public ICommand EditCommand => new RelayCommandWithParam<BacSiProfileListReadModel>(async item =>
	{
		if (item == null) return;

		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new UpdateBacSi(item.NhanVienID)
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
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

			var res = string.IsNullOrWhiteSpace(Keyword)
				? await _client.GetPaged(Page, SizePage)
				: await _client.Search(Keyword, Page, SizePage);

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