using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Client;

public class DashboardClient : AppClientBase
{
	private const string BASE = "api/dashboard";
	public Task<ApiResult<DashboardKpiReadModel>> GetKpi()
		=> GetAsync<DashboardKpiReadModel>($"{BASE}/kpi");
	public Task<ApiResult<List<CaKhamTheoNgayReadModel>>> GetCaKhamTheoTuan()
		=> GetAsync<List<CaKhamTheoNgayReadModel>>($"{BASE}/ca-kham-tuan");
	public Task<ApiResult<List<TrangThaiCaKhamReadModel>>> GetTrangThaiCaKham(int? year = null, int? month = null)
	{
		var url = $"{BASE}/trang-thai-ca-kham?year={year}&month={month}";
		return GetAsync<List<TrangThaiCaKhamReadModel>>(url);
	}
	public Task<ApiResult<List<TopBenhReadModel>>> GetTopBenh(int? year = null, int? month = null)
	{
		var url = $"{BASE}/top-benh?year={year}&month={month}";
		return GetAsync<List<TopBenhReadModel>>(url);
	}
	public Task<ApiResult<List<TopBacSiReadModel>>> GetTopBacSi(int? year = null, int? month = null)
	{
		var url = $"{BASE}/top-bac-si?year={year}&month={month}";
		return GetAsync<List<TopBacSiReadModel>>(url);
	}
	public Task<ApiResult<List<LieuTrinhProgressReadModel>>> GetLieuTrinh()
		=> GetAsync<List<LieuTrinhProgressReadModel>>($"{BASE}/lieu-trinh");
	public Task<ApiResult<List<HoatDongReadModel>>> GetHoatDong()
		=> GetAsync<List<HoatDongReadModel>>($"{BASE}/hoat-dong");
}