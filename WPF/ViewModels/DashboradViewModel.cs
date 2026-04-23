using System.Collections.ObjectModel;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.ViewModels;

public class DashboardViewModel : BaseViewModel
{
	private readonly DashboardClient _client = new();

	public DashboardKpiReadModel? Kpi { get; set; }

	public ObservableCollection<CaKhamTheoNgayReadModel> CaKhamTuan { get; set; } = new();
	public ObservableCollection<TrangThaiCaKhamReadModel> TrangThai { get; set; } = new();
	public ObservableCollection<TopBenhReadModel> TopBenh { get; set; } = new();
	public ObservableCollection<TopBacSiReadModel> TopBacSi { get; set; } = new();
	public ObservableCollection<HoatDongReadModel> HoatDong { get; set; } = new();
	public ObservableCollection<LieuTrinhProgressReadModel> LieuTrinh { get; set; } = new();

	public async Task Init()
	{
		await LoadData();
	}

	private async Task LoadData()
	{
		IsLoading = true;

		var kpiTask = _client.GetKpi();
		var tuanTask = _client.GetCaKhamTheoTuan();
		var ttTask = _client.GetTrangThaiCaKham();
		var benhTask = _client.GetTopBenh();
		var bsTask = _client.GetTopBacSi();
		var ltTask = _client.GetLieuTrinh();
		var hdTask = _client.GetHoatDong();

		await Task.WhenAll(kpiTask, tuanTask, ttTask, benhTask, bsTask, ltTask, hdTask);

		if (kpiTask.Result.Success) Kpi = kpiTask.Result.Data;

		Fill(CaKhamTuan, tuanTask.Result.Data);
		Fill(TrangThai, ttTask.Result.Data);
		Fill(TopBenh, benhTask.Result.Data);
		Fill(TopBacSi, bsTask.Result.Data);
		Fill(LieuTrinh, ltTask.Result.Data);
		Fill(HoatDong, hdTask.Result.Data);

		OnPropertyChanged(nameof(Kpi));
		IsLoading = false;
	}

	private void Fill<T>(ObservableCollection<T> col, List<T>? data)
	{
		col.Clear();
		if (data == null) return;
		foreach (var i in data) col.Add(i);
	}
}