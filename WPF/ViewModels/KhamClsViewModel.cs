using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.ViewModels;

public class PhienKhamCLSViewModel : PagedViewModel
{
	private readonly PhienKhamClsClient _client = new();
	private readonly DebounceDispatcher _search = new();
	private readonly DebounceDispatcher _pageSize = new();

	public ObservableCollection<PhienKhamClsReadListModel> Items { get; set; } = new();

	#region FILTER

	public List<string> StatusList { get; } =
		new() { "Tất cả", "Đang chờ", "Đang thực hiện", "Hoàn thành", "Đã hủy" };

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

	#endregion

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
		SelectedStatus = "Tất cả";
		Page = 1;
		return Task.CompletedTask;
	});

	public ICommand AcceptCommand =>
		new RelayCommandWithParam<PhienKhamClsReadListModel>(async item =>
		{
			if (item == null) return;

			var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
			OverlayHelper.Show(overlay);

			try
			{
				// accept
				if (item.TrangThai == "Đang chờ")
				{
					var confirm = await MessageHelper.Confirm("Nhận thực hiện CLS?");
					if (!confirm) return;

					var res = await _client.Accept(item.PhienKhamCLSID,
						new AcceptClsDTO
						{
							NhanVienThucHienID = Session.NhanVienId!.Value
						});

					if (!res.Success)
					{
						await MessageHelper.ShowMessage(res.Message);
						return;
					}
				}

				// open window
				if (item.TrangThai is "Đang chờ" or "Đang thực hiện")
				{
					var win = new WPF.Windows.KhamBenh.ThucHienCLS(item.PhienKhamCLSID)
					{
						Owner = Application.Current.MainWindow
					};

					if (win.ShowDialog() == true)
					{
						await LoadData();
						SnackbarHelper.ShowSuccess("Cập nhật thành công!");
					}
				}
			}
			finally
			{
				OverlayHelper.Hide(overlay);
			}
		});

	public ICommand ViewCommand =>
		new RelayCommandWithParam<PhienKhamClsReadListModel>(item =>
		{
			if (item == null) return Task.CompletedTask;

			var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
			OverlayHelper.Show(overlay);

			try
			{
				new WPF.Windows.KhamBenh.ViewCls(item.PhienKhamCLSID)
				{
					Owner = Application.Current.MainWindow
				}.ShowDialog();
			}
			finally
			{
				OverlayHelper.Hide(overlay);
			}

			return Task.CompletedTask;
		});

	public ICommand CancelCommand =>
		new RelayCommandWithParam<PhienKhamClsReadListModel>(async item =>
		{
			if (item == null) return;

			var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
			OverlayHelper.Show(overlay);

			try
			{
				var confirm = await MessageHelper.Confirm($"Hủy CLS: {item.TenCLS}?");
				if (!confirm) return;

				var res = await _client.Cancel(item.PhienKhamCLSID);

				if (!res.Success)
				{
					SnackbarHelper.ShowError(res.Message);
					return;
				}

				await LoadData();
				SnackbarHelper.ShowSuccess("Đã hủy!");
			}
			finally
			{
				OverlayHelper.Hide(overlay);
			}
		});

	#endregion

	#region LOAD

	protected override async Task LoadData()
	{
		try
		{
			IsLoading = true;

			var status = GetStatus();

			var res = string.IsNullOrWhiteSpace(Keyword)
				? await _client.GetPaged(Page, SizePage, status)
				: await _client.Search(Keyword, status, Page, SizePage);

			if (!res.Success)
			{
				await MessageHelper.ShowMessage(res.Message);
				return;
			}

			await Ui.Run(() =>
			{
				Items.Clear();
				foreach (var i in res.Data!.Items)
					Items.Add(i);
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