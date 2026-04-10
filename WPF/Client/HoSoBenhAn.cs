using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class HoSoBenhAnClient : AppClientBase
{
	private const string BASE = "api/hosobenhan";

	public Task<ApiResult<bool>> Create(HoSoBenhAnRequest req)
		=> PostAsync<bool>(BASE, req);

	public Task<ApiResult<bool>> Update(int id, HoSoBenhAnUpdate req)
		=> PutAsync<bool>($@"{BASE}/{id}", req);

	public Task<ApiResult<HoSoBenhAnReadModel>> Detail(int id)
		=> GetAsync<HoSoBenhAnReadModel>($@"{BASE}/{id}");

	public Task<ApiResult<HoSoBenhAnReadModel?>> GetByBenhNhanId(int benhNhanId)
		=> GetAsync<HoSoBenhAnReadModel?>($@"{BASE}/benhnhan/{benhNhanId}");

	public Task<ApiResult<PagedResult<HoSoBenhAnListReadModel>>> GetPaged(int page = 1, int size = 10)
	{
		var url = $@"{BASE}?page={page}&size={size}";
		return GetAsync<PagedResult<HoSoBenhAnListReadModel>>(url);
	}

	public Task<ApiResult<PagedResult<HoSoBenhAnListReadModel>>> Search(string keyword, int page = 1, int size = 10)
	{
		var url = $@"{BASE}/search?keyword={keyword}&page={page}&size={size}";
		return GetAsync<PagedResult<HoSoBenhAnListReadModel>>(url);
	}
}