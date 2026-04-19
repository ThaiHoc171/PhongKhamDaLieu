using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class LieuTrinhDieuTriClient : AppClientBase
{
	private const string BASE = "api/lieutrinh";
	public Task<ApiResult<int>> Create(LieuTrinhDieuTriRequestDTO req)
		=> PostAsync<int>(BASE, req);
	public Task<ApiResult<bool>> Update(int id, LieuTrinhDieuTriUpdateDTO req)
		=> PutAsync<bool>($"{BASE}/{id}", req);
	public Task<ApiResult<bool>> Complete(int id)
		=> PutAsync<bool>($"{BASE}/{id}/complete", null);
	public Task<ApiResult<bool>> Cancel(int id, string? ghiChu)
		=> PutAsync<bool>($"{BASE}/{id}/cancel", ghiChu);
	public Task<ApiResult<bool>> UpdateStatus(int id, LieuTrinhStatusDTO dto)
		=> PutAsync<bool>($"{BASE}/{id}/status", dto);
	public Task<ApiResult<LieuTrinhDieuTriReadModel>> Detail(int id)
		=> GetAsync<LieuTrinhDieuTriReadModel>($"{BASE}/{id}");
	public Task<ApiResult<LieuTrinhDieuTriReadModel>> Exist(int phienKhamId)
		=> GetAsync<LieuTrinhDieuTriReadModel>($"{BASE}/exist/{phienKhamId}");
	public Task<ApiResult<PagedResult<LieuTrinhDieuTriListReadModel>>>
		GetPaged(int page = 1, int size = 15, string? trangThai = null)
	{
		var url = $"{BASE}?page={page}&size={size}&trangThai={trangThai}";
		return GetAsync<PagedResult<LieuTrinhDieuTriListReadModel>>(url);
	}
	public Task<ApiResult<PagedResult<LieuTrinhDieuTriListReadModel>>>
		Search(string keyword, int page = 1, int size = 15)
	{
		var url = $"{BASE}/search?keyword={keyword}&page={page}&size={size}";
		return GetAsync<PagedResult<LieuTrinhDieuTriListReadModel>>(url);
	}

	public Task<ApiResult<PagedResult<LieuTrinhDieuTriListReadModel>>>
		GetByBenhNhan(int benhNhanId, int page = 1, int size = 15)
	{
		var url = $"{BASE}/benhnhan/{benhNhanId}?page={page}&size={size}";
		return GetAsync<PagedResult<LieuTrinhDieuTriListReadModel>>(url);
	}
}