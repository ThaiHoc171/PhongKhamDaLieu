using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Client;

public class PhienKhamClient : AppClientBase
{
	private const string BASE = "api/phienkham";

	// ==================== CREATE ====================
	public Task<ApiResult<int>> Create(int caKhamId)
	{
		var url = $"{BASE}?caKhamId={caKhamId}";
		return PostAsync<int>(url, null);
	}

	// ==================== UPDATE ====================
	public Task<ApiResult<bool>> Update(int id, PhienKhamUpdateDTO req)
		=> PutAsync<bool>($"{BASE}/{id}", req);

	// ==================== COMPLETE ====================
	public Task<ApiResult<bool>> Complete(int id, string chanDoanCuoi)
		=> PutAsync<bool>($"{BASE}/{id}/complete", chanDoanCuoi);
	public Task<ApiResult<bool>> Start(int id)
		=> PutAsync<bool>($"{BASE}/{id}/start", null);
	public Task<ApiResult<bool>> Cancel(int id)
		=> PutAsync<bool>($"{BASE}/{id}/cancel", null);

	// ==================== GET BY ID ====================
	public Task<ApiResult<PhienKhamReadModel>> Detail(int id)
		=> GetAsync<PhienKhamReadModel>($"{BASE}/{id}");

	// ==================== GET BY CA KHAM ====================
	public Task<ApiResult<PhienKhamReadModel>> GetByCaKhamId(int caKhamId)
		=> GetAsync<PhienKhamReadModel>($"{BASE}/cakham/{caKhamId}");

	// ==================== GET BY BENH NHAN ====================
	public Task<ApiResult<PagedResult<PhienKhamReadListModel>>> GetByBenhNhan(
		int benhNhanId,
		int page = 1,
		int size = 10)
	{
		var url = $"{BASE}/benhnhan/{benhNhanId}?pageNumber={page}&pageSize={size}";
		return GetAsync<PagedResult<PhienKhamReadListModel>>(url);
	}

	// ==================== PAGED ====================
	public Task<ApiResult<PagedResult<PhienKhamReadListModel>>> GetPaged(
		int page = 1,
		int size = 15,
		int? nhanVienId = null,
		string? trangThai = null)
	{
		var url = $"{BASE}?pageNumber={page}&pageSize={size}&nhanVienID={nhanVienId}&trangThai={trangThai}";
		return GetAsync<PagedResult<PhienKhamReadListModel>>(url);
	}

	// ==================== SEARCH ====================
	public Task<ApiResult<PagedResult<PhienKhamReadListModel>>> Search(
		string keyword,
		int page = 1,
		int size = 15,
		int? nhanVienId = null)
	{
		var url = $"{BASE}/search?keyword={keyword}&pageNumber={page}&pageSize={size}&nhanVienID={nhanVienId}";
		return GetAsync<PagedResult<PhienKhamReadListModel>>(url);
	}
}