using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;
using HoanMyClinic.Windows.NgayNghi;

namespace HoanMyClinic.ViewModels;

public class NgayNghiViewModel : PagedViewModel
{
	private readonly NgayNghiNhanVienClient _client = new();
	private readonly DebounceDispatcher _searchDebounce = new();
	private readonly DebounceDispatcher _sizeDebounce = new();

	public ObservableCollection<NgayNghiReadModel> Items { get; set; } = new();

	// ================= SEARCH =================
	private string _keyword = "";
	public string Keyword
	{
		get => _keyword;
		set
		{
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

	// ================= PAGE SIZE =================
	private string _pageSizeInput = "15";
	public string PageSizeInput
	{
		get => _pageSizeInput;
		set
		{
			if (_pageSizeInput == value) return;

			_pageSizeInput = value;
			OnPropertyChanged();

			_sizeDebounce.Debounce(400, async () =>
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

	// ================= COMMANDS =================

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
			new AddNgayNghi
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Thêm ngày nghỉ thành công!");
			});

		OverlayHelper.Hide(overlay);
	});
	public ICommand ImportCommand => new RelayCommand(async () =>
	{
		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new ImportNgayNghi
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Nhập ngày nghỉ thành công!");
			});

		OverlayHelper.Hide(overlay);
	});

	public ICommand EditCommand => new RelayCommandWithParam<NgayNghiReadModel>(async item =>
	{
		if (item == null) return;

		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new UpdateNgayNghi(item.NgayNghiID)
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Cập nhật ngày nghỉ thành công!");
			});

		OverlayHelper.Hide(overlay);
	});

	public ICommand DeleteCommand => new RelayCommandWithParam<NgayNghiReadModel>(async item =>
	{
		if (item == null) return;

		bool confirm = await MessageHelper.Confirm(
			$"Bạn có chắc muốn xoá ngày nghỉ của '{item.NhanVien.Name}' không?"
		);
		if (!confirm) return;

		var res = await _client.Delete(item.NgayNghiID);
		if (!res.Success)
		{
			SnackbarHelper.ShowError(res.Message);
			return;
		}

		SnackbarHelper.ShowSuccess("Xoá thành công!");
		await LoadData();
	});

	// ================= LOAD DATA =================
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

			TotalPages = (int)Math.Ceiling((double)res.Data!.TotalCount / res.Data.PageSize);
		}
		finally
		{
			IsLoading = false;
		}
	}
}