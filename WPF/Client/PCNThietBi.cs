using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class PCNThietBiClient : AppClientBase
{
	private const string BASE = "api/pcnthietbi";
	public Task<ApiResult<PagedResult<PCNThietBiReadModel>>> GetPaged(int page = 1, int size = 15, int? phongChucNangID = null)
	{
		var url = $@"{BASE}?page={page}&size={size}";
		if (phongChucNangID.HasValue)
			url += $"&phongChucNangID={phongChucNangID}";
		return GetAsync<PagedResult<PCNThietBiReadModel>>(url);
	}

	public Task<ApiResult<PagedResult<PCNThietBiReadModel>>> Search(string keyword, int page = 1, int size = 15, int? phongChucNangID = null)
	{
		var url = $@"{BASE}/search?keyword={keyword}&page={page}&size={size}";
		if (phongChucNangID.HasValue)
			url += $"&phongChucNangID={phongChucNangID}";
		return GetAsync<PagedResult<PCNThietBiReadModel>>(url);
	}
	public Task<ApiResult<List<NameHelper>>> GetCombobox(int pcnId)
		=> GetAsync<List<NameHelper>>($"{BASE}/combobox?pcnId={pcnId}");
}