using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WPF.Client;
using WPF.Common;
using WPF.Models;
using WPF.Windows;

namespace WPF.ViewModels;

public class ViewDetailPhongViewModel : PagedViewModel
{
	private readonly PCNThietBiClient _client = new();
	private readonly ChiTietPCNThietBiClient _chiTiet = new();
	private readonly DebounceDispatcher _search = new();
	private readonly DebounceDispatcher _pageSize = new();

	private readonly int _id;
	private readonly string _name;

	public ViewDetailPhongViewModel(int id, string name)
	{
		_id = id;
		_name = name;
	}

	public ObservableCollection<PCNThietBiReadModel> Items { get; set; } = new();
	public ObservableCollection<ChiTietPCNThietBiListReadModel> ChiTietItems { get; set; } = new();

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

	#region SELECTED
	private PCNThietBiReadModel? _selectedItem;
	public PCNThietBiReadModel? SelectedItem
	{
		get => _selectedItem;
		set
		{
			_selectedItem = value;
			OnPropertyChanged();

			if (value != null)
				_ = LoadChiTiet(value.PCN_TB_ID);
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

	public ICommand AddCommand => new RelayCommand(async () =>
	{
		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new WPF.Windows.ChiTietPhong.AddThietBiPhong(_id, _name)
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

	public ICommand ViewCommand => new RelayCommandWithParam<PCNThietBiReadModel>(async item =>
	{
		if (item == null) return;
		await LoadChiTiet(item.PCN_TB_ID);
	});

	public ICommand EditCommand => new RelayCommandWithParam<ChiTietPCNThietBiListReadModel>(async item =>
	{
		if (item == null) return;

		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new WPF.Windows.ChiTietPhong.UpdateDetailThietBi(item.ChiTietID)
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				await LoadChiTiet(item.ChiTietID);
				SnackbarHelper.ShowSuccess("Cập nhật thành công!");
			});

		OverlayHelper.Hide(overlay);
	});

	public ICommand DeleteCommand => new RelayCommandWithParam<ChiTietPCNThietBiListReadModel>(async item =>
	{
		if (item == null) return;

		bool confirm = await MessageHelper.Confirm("Xóa thiết bị?");
		if (!confirm) return;

		var res = await _chiTiet.Delete(item.ChiTietID);

		if (res.Success)
		{
			await LoadData();
			await LoadChiTiet(item.ChiTietID);
			SnackbarHelper.ShowSuccess("Xóa thành công!");
		}
		else
		{
			SnackbarHelper.ShowError(res.Message);
		}
	});

	public ICommand BackCommand => new RelayCommand(() =>
	{
		var parent = Application.Current.MainWindow as appClinic;
		parent?.OpenPage(new WPF.Pages.PhongChucNangPage(), "Quản lý phòng chức năng");
		return Task.CompletedTask;
	});

	#endregion

	#region LOAD

	protected override async Task LoadData()
	{
		try
		{
			IsLoading = true;

			var res = string.IsNullOrWhiteSpace(Keyword)
				? await _client.GetPaged(Page, SizePage, _id)
				: await _client.Search(Keyword, Page, SizePage, _id);

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

			ChiTietItems.Clear();
		}
		finally
		{
			IsLoading = false;
		}
	}

	private async Task LoadChiTiet(int id)
	{
		try
		{
			IsLoading = true;

			var res = await _chiTiet.GetList(id);

			if (!res.Success)
			{
				SnackbarHelper.ShowError(res.Message);
				return;
			}

			await Ui.Run(() =>
			{
				ChiTietItems.Clear();
				foreach (var item in res.Data!)
					ChiTietItems.Add(item);
			});
		}
		finally
		{
			IsLoading = false;
		}
	}

	#endregion
}