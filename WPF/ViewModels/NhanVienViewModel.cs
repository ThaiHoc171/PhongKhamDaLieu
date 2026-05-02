using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;
using HoanMyClinic.Windows.NhanVien;
using HoanMyClinic.Windows.Public;
using HoanMyClinic.Windows.TaiKham;

namespace HoanMyClinic.ViewModels;

public class NhanVienViewModel : PagedViewModel
{
	private readonly NhanVienClient _client = new();
	private readonly BacSiProfileClient _bacsi = new();
	private readonly DebounceDispatcher _searchDebounce = new();
	private readonly DebounceDispatcher _sizeDebounce = new();

	public ObservableCollection<NhanVienReadListModel> Items { get; set; } = new();

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
			new AddNhanVien
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Thêm nhân viên thành công!");
			});

		OverlayHelper.Hide(overlay);
	});

	public ICommand EditCommand => new RelayCommandWithParam<NhanVienReadListModel>(async item =>
	{
		if (item == null) return;

		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new UpdateNhanVien(item.NhanVienID)
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Cập nhật nhân viên thành công!");
			});

		OverlayHelper.Hide(overlay);
	});
	public ICommand ToogleCommand => new RelayCommandWithParam<NhanVienReadListModel>(async item =>
	{
		if (item == null) return;
		bool confirm = false;
		string status = string.Empty;
		if(item.TrangThai == "Đang làm việc")
		{
			confirm = await MessageHelper.Confirm(
				$"Bạn có chắc muốn cho nhân viên'{item.HoTen}' thôi việc không?"
			);
			status = "Nghỉ việc";
		}
		if(item.TrangThai == "Nghỉ việc")
		{
			confirm = await MessageHelper.Confirm(
				$"Bạn có chắc muốn cho nhân viên'{item.HoTen}' vào làm việc lại không?"
			);
			status = "Đang làm việc";
		}

		if (!confirm) return;
		var res = await _client.Status(item.NhanVienID, status);

		if (!res.Success)
		{
			SnackbarHelper.ShowError(res.Message);
			return;
		}

		SnackbarHelper.ShowSuccess("Cập nhật thành công!");
		await LoadData();
	});
	public ICommand PublicCommand => new RelayCommandWithParam<NhanVienReadListModel>(async item =>
	{
		if (item == null) return;
		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);
		var res = await _bacsi.GetByNhanVien(item.NhanVienID);
		if (!res.Success)
		{
			await DialogHelper.OpenDialogAsync(
			new AddBacSi(item.NhanVienID,item.HoTen)
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Thêm thông tin bác sĩ công khai thành công!");
			});
		}
		else
		{
			await DialogHelper.OpenDialogAsync(
			new UpdateBacSi(item.NhanVienID)
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
			});
		}
		OverlayHelper.Hide(overlay);
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