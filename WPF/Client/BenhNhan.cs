using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class BenhNhanClient : AppClientBase
{
	private const string BASE = "api/benhnhan";

	public Task<ApiResult<int>> Create(BenhNhanRequest req)
		=> PostAsync<int>(BASE, req);

	public Task<ApiResult<bool>> Update(int id, BenhNhanUpdateRequest req)
		=> PutAsync<bool>($@"{BASE}/{id}", req);

	public Task<ApiResult<BenhNhanReadModel>> Detail(int id)
		=> GetAsync<BenhNhanReadModel>($@"{BASE}/{id}");

	public Task<ApiResult<PagedResult<BenhNhanReadListModel>>> GetPaged(int page = 1, int size = 15)
	{
		var url = $@"{BASE}?pageNumber={page}&pageSize={size}";
		return GetAsync<PagedResult<BenhNhanReadListModel>>(url);
	}

	public Task<ApiResult<PagedResult<BenhNhanReadListModel>>> Search(string keyword, int page = 1, int size = 10)
	{
		var url = $@"{BASE}/search?keyword={keyword}&pageNumber={page}&pageSize={size}";
		return GetAsync<PagedResult<BenhNhanReadListModel>>(url);
	}

	public Task<ApiResult<List<NameHelper>>> GetCombobox()
		=> GetAsync<List<NameHelper>>($@"{BASE}/combobox");
}