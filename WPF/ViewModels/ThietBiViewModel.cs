using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.ViewModels;

public class ThietBiViewModel : PagedViewModel
{
	private readonly ThietBiClient _client = new();
	private readonly DebounceDispatcher _search = new();
	private readonly DebounceDispatcher _pageSize = new();

	public ObservableCollection<ThietBiReadListModel> Items { get; set; } = new();

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

	#region PageSize

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

	public ICommand ImportCommand => new RelayCommand(async () =>
	{
		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new HoanMyClinic.Windows.ThietBi.ImportThietBi
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Nhập thiết bị thành công!");
			});

		OverlayHelper.Hide(overlay);
	});

	public ICommand AddCommand => new RelayCommand(async () =>
	{
		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new HoanMyClinic.Windows.ThietBi.AddThietBi
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Thêm thiết bị thành công!");
			});

		OverlayHelper.Hide(overlay);
	});

	public ICommand EditCommand =>
		new RelayCommandWithParam<ThietBiReadListModel>(async item =>
		{
			if (item == null) return;

			var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
			OverlayHelper.Show(overlay);

			await DialogHelper.OpenDialogAsync(
				new HoanMyClinic.Windows.ThietBi.UpdateThietBi(item.ThietBiID)
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

	#region LOAD

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

	#endregion
}