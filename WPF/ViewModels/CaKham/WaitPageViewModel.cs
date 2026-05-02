using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;
using HoanMyClinic.Windows.CaKham;

namespace HoanMyClinic.ViewModels.CaKham;

public class WaitPageViewModel : PagedViewModel
{
	private readonly CaKhamClient _client = new();
	private readonly DebounceDispatcher _pageSize = new();

	public ObservableCollection<CaKhamListReadModel> Items { get; set; } = new();

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

	public ICommand RefreshCommand => new RelayCommand(async () =>
	{
		Page = 1;
		await LoadData();
	});

	public ICommand AcceptCommand =>
		new RelayCommandWithParam<CaKhamListReadModel>(async item =>
		{
			if (item == null) return;

			var confirm = await MessageHelper.Confirm("Xác nhận đăng ký ca khám?");
			if (!confirm) return;

			var req = new CaKhamTrangThaiDTO
			{
				TrangThai = "Đã xác nhận"
			};

			var result = await _client.UpdateTrangThai(item.CaKhamID, req);

			if (result.Success)
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Đã xác nhận!");
			}
		});

	public ICommand CancelCommand =>
	new RelayCommandWithParam<CaKhamListReadModel>(async item =>
	{
		if (item == null) return;

		var confirm = await MessageHelper.Confirm("Bạn có chắc muốn hủy ca khám này?");
		if (!confirm) return;

		var overlay = OverlayHelper.GetOverlay(Application.Current.MainWindow!);
		OverlayHelper.Show(overlay);

		try
		{
			var result = await _client.Cancel(item.CaKhamID);

			if (result.Success)
			{
				await LoadData();
				SnackbarHelper.ShowSuccess("Đã hủy ca khám!");
			}
			else
			{
				SnackbarHelper.ShowError(result.Message);
			}
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

			var res = await _client.ChoXacNhan(Page, SizePage);

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