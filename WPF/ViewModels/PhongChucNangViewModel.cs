using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WPF.Client;
using WPF.Common;
using WPF.Models;
using WPF.Pages;
using WPF.Windows;

namespace WPF.ViewModels;

public class PhongChucNangViewModel : PagedViewModel
{
	private readonly PhongChucNangClient _client = new();
	private readonly DebounceDispatcher _search = new();
	private readonly DebounceDispatcher _pageSize = new();

	public ObservableCollection<PhongChucNangReadListModel> Items { get; set; } = new();

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
			new WPF.Windows.PhongChucNang.ImportThietBiPhong
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Nhập excel thành công!");
			});

		OverlayHelper.Hide(overlay);
	});

	public ICommand AddCommand => new RelayCommand(async () =>
	{
		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new WPF.Windows.PhongChucNang.AddPhong
			{
				Owner = Application.Current.MainWindow
			},
			async () =>
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Thêm phòng thành công!");
			});

		OverlayHelper.Hide(overlay);
	});

	public ICommand ViewCommand =>
		new RelayCommandWithParam<PhongChucNangReadListModel>(item =>
		{
			if (item == null) return Task.CompletedTask;

			if (Application.Current.MainWindow is appClinic app)
			{
				app.OpenPage( new ViewDetailPhongPage(
						item.PhongChucNangID,
						item.TenPhong),
					$"Quản lý phòng chức năng / {item.TenPhong}"
				);
			}

			return Task.CompletedTask;
		});

	public ICommand EditCommand =>
		new RelayCommandWithParam<PhongChucNangReadListModel>(async item =>
		{
			if (item == null) return;

			var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
			OverlayHelper.Show(overlay);

			await DialogHelper.OpenDialogAsync(
				new WPF.Windows.PhongChucNang.UpdatePhong(item.PhongChucNangID)
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

	public ICommand ToggleStatusCommand =>
		new RelayCommandWithParam<PhongChucNangReadListModel>(async item =>
		{
			if (item == null) return;

			string newStatus;
			bool confirm;

			if (item.TrangThai == "Hoạt động")
			{
				newStatus = "Hỏng";
				confirm = await MessageHelper.Confirm($"Báo hỏng: {item.TenPhong}?");
			}
			else if (item.TrangThai == "Hỏng")
			{
				newStatus = "Bảo trì";
				confirm = await MessageHelper.Confirm($"Đang bảo trì: {item.TenPhong}?");
			}
			else
			{
				newStatus = "Hoạt động";
				confirm = await MessageHelper.Confirm($"Đã sửa xong: {item.TenPhong}?");
			}

			if (!confirm) return;

			var res = await _client.ChangeStatus(item.PhongChucNangID, newStatus);

			if (!res.Success)
			{
				SnackbarHelper.ShowError(res.Message);
				return;
			}

			SnackbarHelper.ShowSuccess("Đã cập nhật trạng thái!");
			await LoadData();
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