using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.ViewModels;

public class HistoryViewModel : PagedViewModel
{
	private readonly CaKhamClient _client = new();

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

	public List<string> Statuses { get; } = new()
	{
		"Đang khám",
		"Hoàn thành",
		"Đã hủy",
		"Không đến"
	};

	private string _status = "Đang khám";
	public string Status
	{
		get => _status;
		set
		{
			_status = value;
			OnPropertyChanged();
			Reload();
		}
	}

	private void Reload()
	{
		Page = 1;
		_ = LoadData();
	}

	#endregion

	#region PageSize

	private string _pageSizeInput = "12";
	public string PageSizeInput
	{
		get => _pageSizeInput;
		set
		{
			if (_pageSizeInput == value) return;

			_pageSizeInput = value;
			OnPropertyChanged();

			if (!int.TryParse(value, out int size) || size <= 0)
				return;

			SizePage = size;
			Page = 1;
			_ = LoadData();
		}
	}

	#endregion

	#region COMMANDS

	public ICommand RefreshCommand => new RelayCommand(() =>
	{
		Page = 1;
		return Task.CompletedTask;
	});

	public ICommand ViewCommand => new RelayCommandWithParam<CaKhamListReadModel>(async item =>
	{
		if (item == null) return;

		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new HoanMyClinic.Windows.CaKham.View(item.CaKhamID)
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

	#region LOAD

	protected override async Task LoadData()
	{
		try
		{
			IsLoading = true;

			var res = await _client.GetPaged(
				SelectedDate,
				Status,
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

	#endregion
}