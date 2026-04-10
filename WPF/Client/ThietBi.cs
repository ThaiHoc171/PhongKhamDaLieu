using WPF.Common;
using WPF.Models;
namespace WPF.Client;

public class ThietBiClient : AppClientBase
{
	private const string BASE = "api/thietbi";

	public Task<ApiResult<bool>> Create(ThietBiRequest dto)
	{
		return PostAsync<bool>(BASE, dto);
	}

	public Task<ApiResult<bool>> Update(int id, ThietBiRequest dto)
	{
		return PutAsync<bool>($"{BASE}/{id}", dto);
	}

	public Task<ApiResult<ThietBiReadModel>> GetDetail(int id)
	{
		return GetAsync<ThietBiReadModel>($"{BASE}/{id}");
	}

	public Task<ApiResult<PagedResult<ThietBiReadListModel>>> GetPaged(int page, int size)
	{
		return GetAsync<PagedResult<ThietBiReadListModel>>($"{BASE}?page={page}&size={size}");
	}

	public Task<ApiResult<PagedResult<ThietBiReadListModel>>> Search(string keyword, int page, int size)
	{
		return GetAsync<PagedResult<ThietBiReadListModel>>($"{BASE}/search?keyword={keyword}&page={page}&size={size}");
	}
	public Task<ApiResult<List<NameHelper>>> GetCombobox()
		=> GetAsync<List<NameHelper>>($@"{BASE}/combobox");
	public Task<ApiResult<ExcelImportResult<ThietBiRequest>>> PreviewImport(string filePath, string sheet)
	{
		return PostFileAsync<ExcelImportResult<ThietBiRequest>>(
			$"{BASE}/import/preview?sheet={sheet}",
			filePath);
	}
	public Task<ApiResult<bool>> ConfirmImport(List<ThietBiRequest> list)
	{
		return PostAsync<bool>($"{BASE}/import/confirm", list);
	}
}