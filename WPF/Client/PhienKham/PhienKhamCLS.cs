using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class PhienKhamClsClient : AppClientBase
{
	private const string BASE = "api/phienkham-cls";

	// ==================== CREATE ====================
	public Task<ApiResult<bool>> Create(PkClsRequestDTO req)
		=> PostAsync<bool>(BASE, req);

	// ==================== ACCEPT CLS ====================
	public Task<ApiResult<bool>> Accept(int id, AcceptClsDTO req)
		=> PutAsync<bool>($@"{BASE}/{id}/accept", req);

	// ==================== COMPLETE CLS ====================
	public Task<ApiResult<bool>> Complete(int id, PkClsUpdateRequestDTO req)
		=> PutAsync<bool>($@"{BASE}/{id}/complete", req);

	// ==================== CANCEL CLS ====================
	public Task<ApiResult<bool>> Cancel(int id)
		=> PutAsync<bool>($@"{BASE}/{id}/cancel", null);

	// ==================== GET DETAIL ====================
	public Task<ApiResult<PhienKhamClsReadModel>> Detail(int id)
		=> GetAsync<PhienKhamClsReadModel>($@"{BASE}/{id}");

	// ==================== GET LIST ====================

	public Task<ApiResult<PagedResult<PhienKhamClsReadListModel>>> GetPaged(int page = 1, int size = 10, string? trangThai = null)
	{
		var url = $@"{BASE}?page={page}&size={size}";
		if (!string.IsNullOrEmpty(trangThai))
			url += $"&trangThai={trangThai}";
		return GetAsync<PagedResult<PhienKhamClsReadListModel>>(url);
	}

	public Task<ApiResult<PagedResult<PhienKhamClsReadListModel>>> Search(string keyword,string? trangThai, int page = 1, int size = 10)
	{
		var url = $@"{BASE}/search?keyword={keyword}&page={page}&size={size}";
		if (!string.IsNullOrEmpty(trangThai))
			url += $"&trangThai={trangThai}";
		return GetAsync<PagedResult<PhienKhamClsReadListModel>>(url);
	}
	// ==================== GET BY PHIEN KHAM ====================
	public Task<ApiResult<List<PhienKhamClsReadListModel>>> GetByPhienKham(int phienKhamId)
		=> GetAsync<List<PhienKhamClsReadListModel>>($@"{BASE}/phienkham/{phienKhamId}");
}