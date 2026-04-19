using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WPF.Client;
using WPF.Common;
using WPF.Models;
using WPF.Windows.Khach;

namespace WPF.ViewModels;

public class KhachViewModel : PagedViewModel
{
	private readonly ThongTinCaNhanClient _thongTin = new();
	private readonly BenhNhanClient _bn = new();

	private readonly DebounceDispatcher _search = new();
	private readonly DebounceDispatcher _pageSize = new();

	public ObservableCollection<ThongTinReadListModel> Items { get; set; } = new();

	// ================= SEARCH =================
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

	// ================= COMMANDS =================

	public ICommand RefreshCommand => new RelayCommand(() =>
	{
		Keyword = "";
		Page = 1;
		return Task.CompletedTask;
	});

	public ICommand SearchCommand => new RelayCommand(() =>
	{
		Page = 1;
		return LoadData();
	});

	public ICommand EditCommand => new RelayCommandWithParam<ThongTinReadListModel>(async item =>
	{
		if (item == null) return;

		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		await DialogHelper.OpenDialogAsync(
			new UpdateKhach(item.ThongTinID)
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

	public ICommand CreateBenhNhanCommand => new RelayCommandWithParam<ThongTinReadListModel>(async item =>
	{
		if (item == null) return;

		var confirm = await MessageHelper.Confirm($"Tạo Bệnh Nhân từ khách: {item.HoTen}?");
		if (!confirm) return;

		var detail = await _thongTin.Detail(item.ThongTinID);
		if (!detail.Success || detail.Data == null)
		{
			SnackbarHelper.ShowError("Không lấy được thông tin!");
			return;
		}

		var data = detail.Data;

		var req = new BenhNhanRequest
		{
			HoTen = data.HoTen,
			GioiTinh = data.GioiTinh,
			NgaySinh = data.NgaySinh,
			SDT = data.SDT,
			EmailLienHe = data.EmailLienHe,
			DiaChi = data.DiaChi,
			Avatar = data.Avatar
		};

		var result = await _bn.Create(req);

		if (!result.Success)
		{
			SnackbarHelper.ShowError(result.Message);
			return;
		}

		SnackbarHelper.ShowSuccess("Tạo bệnh nhân thành công!");
		await LoadData();
	});

	// ================= LOAD DATA =================
	protected override async Task LoadData()
	{
		try
		{
			IsLoading = true;

			var res = string.IsNullOrWhiteSpace(Keyword)
				? await _thongTin.GetKhachList(Page, SizePage)
				: await _thongTin.Search(Keyword, Page, SizePage);

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