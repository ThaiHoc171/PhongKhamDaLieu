using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.ViewModels;

public class TaiKhoanViewModel : PagedViewModel
{
	private readonly TaiKhoanClient _client = new();
	private readonly DebounceDispatcher _search = new();
	private readonly DebounceDispatcher _pageSize = new();

	public ObservableCollection<TaiKhoanListReadModel> Items { get; set; } = new();

	#region FILTER

	public List<string> RoleList { get; } =
		new() { "Tất cả", "Admin", "Bệnh nhân", "Khách", "Nhân viên" };

	public List<string> StatusList { get; } =
		new() { "Tất cả", "Hoạt động", "Bị khóa" };

	private string _selectedRole = "Tất cả";
	public string SelectedRole
	{
		get => _selectedRole;
		set
		{
			_selectedRole = value;
			OnPropertyChanged();

			Ui.RunAsync(async () =>
			{
				Page = 1;
				await LoadData();
			});
		}
	}

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

	private string GetRole()
		=> SelectedRole == "Tất cả" ? "" : SelectedRole;

	private string GetStatus()
		=> SelectedStatus == "Tất cả" ? "" : SelectedStatus;

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
		SelectedRole = "Tất cả";
		SelectedStatus = "Tất cả";
		Page = 1;
		return Task.CompletedTask;
	});

	public ICommand ResetCommand =>
		new RelayCommandWithParam<TaiKhoanListReadModel>(async item =>
		{
			if (item == null) return;

			var confirm = await MessageHelper.Confirm(
				$"Reset mật khẩu:\n{item.Email}?"
			);
			if (!confirm) return;

			var res = await _client.ResetPassword(item.Id);

			if (!res.Success)
			{
				SnackbarHelper.ShowError(res.Message);
				return;
			}

			SnackbarHelper.ShowSuccess("Reset thành công!");
		});

	public ICommand ToggleStatusCommand =>
		new RelayCommandWithParam<TaiKhoanListReadModel>(async item =>
		{
			if (item == null) return;

			string newStatus;
			bool confirm;

			if (item.TrangThai == "Hoạt động")
			{
				newStatus = "Bị khóa";
				confirm = await MessageHelper.Confirm(
					$"Khóa tài khoản:\n{item.Email}?"
				);
			}
			else
			{
				newStatus = "Hoạt động";
				confirm = await MessageHelper.Confirm(
					$"Kích hoạt lại:\n{item.Email}?"
				);
			}

			if (!confirm) return;

			var res = await _client.UpdateStatus(item.Id,
				new TaiKhoanUpdateRequestDTO
				{
					TrangThai = newStatus
				});

			if (!res.Success)
			{
				SnackbarHelper.ShowError(res.Message);
				return;
			}

			SnackbarHelper.ShowSuccess("Cập nhật thành công!");
			await LoadData();
		});

	#endregion

	#region LOAD

	protected override async Task LoadData()
	{
		try
		{
			IsLoading = true;

			var role = GetRole();
			var status = GetStatus();

			var res = string.IsNullOrWhiteSpace(Keyword)
				? await _client.GetPaged(Page, SizePage, role, status)
				: await _client.Search(Page, SizePage, Keyword, role, status);

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