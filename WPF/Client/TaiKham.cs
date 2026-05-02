using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Client;

public class TaiKhamClient : AppClientBase
{
	private const string BASE = "api/taikham";

	public Task<ApiResult<int>> Create(TaiKhamRequestDTO req)
		=> PostAsync<int>(BASE, req);

	public Task<ApiResult<bool>> Update(int id, TaiKhamUpdateRequestDTO req)
		=> PutAsync<bool>($"{BASE}/{id}", req);

	public Task<ApiResult<bool>> Complete(int id)
		=> PutAsync<bool>($"{BASE}/{id}/complete", null);

	public Task<ApiResult<bool>> Cancel(int id)
		=> PutAsync<bool>($"{BASE}/{id}/cancel", null);

	public Task<ApiResult<bool>> AssignCaKham(int id, int caKhamId)
		=> PutAsync<bool>($"{BASE}/{id}/cakham/{caKhamId}", null);

	public Task<ApiResult<TaiKhamReadModel>> Detail(int id)
		=> GetAsync<TaiKhamReadModel>($"{BASE}/{id}");
	public Task<ApiResult<int>> GetId(int id)
		=> GetAsync<int>($"{BASE}/GetId/{id}");

	public Task<ApiResult<PagedResult<TaiKhamReadListModel>>>
		GetPaged(int page = 1, int size = 10, string? trangThai = null)
	{
		var url = $"{BASE}?page={page}&size={size}&trangThai={trangThai}";
		return GetAsync<PagedResult<TaiKhamReadListModel>>(url);
	}

	public Task<ApiResult<PagedResult<TaiKhamReadListModel>>>
		Search(string? keyword, int page = 1, int size = 10)
	{
		var url = $"{BASE}/search?keyword={keyword}&page={page}&size={size}";
		return GetAsync<PagedResult<TaiKhamReadListModel>>(url);
	}

	public Task<ApiResult<PagedResult<TaiKhamReadListModel>>>
		GetByBenhNhan(int benhNhanId, int page = 1, int size = 10)
	{
		var url = $"{BASE}/benhnhan/{benhNhanId}?page={page}&size={size}";
		return GetAsync<PagedResult<TaiKhamReadListModel>>(url);
	}
}