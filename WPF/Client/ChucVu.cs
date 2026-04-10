using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class ChucVuClient : AppClientBase
{
	private const string BASE = "api/chucvu";
	public Task<ApiResult<bool>> Create(ChucVuRequest req)
		=> PostAsync<bool>(BASE, req);

	public Task<ApiResult<bool>> Update(int id, ChucVuRequest req)
		=> PutAsync<bool>($@"{BASE}/{id}", req);

	public Task<ApiResult<ChucVuReadModel>> Detail(int id)
		=> GetAsync<ChucVuReadModel>($@"{BASE}/{id}");

	public Task<ApiResult<PagedResult<ChucVuListReadModel>>> GetPaged(int page = 1, int size = 10)
	{
		var url = $@"{BASE}?page={page}&size={size}";
		return GetAsync<PagedResult<ChucVuListReadModel>>(url);
	}

	public Task<ApiResult<PagedResult<ChucVuListReadModel>>> Search(string keyword, int page = 1, int size = 10)
	{
		var url = $@"{BASE}/search?keyword={keyword}&page={page}&size={size}";
		return GetAsync<PagedResult<ChucVuListReadModel>>(url);
	}

	public Task<ApiResult<string?>> GetNameById(int id)
		=> GetAsync<string?>($@"{BASE}/{id}/name");

	public Task<ApiResult<string?>> GetByNhanVienId(int nhanVienId)
		=> GetAsync<string?>($@"{BASE}/nhanvien/{nhanVienId}");

	public Task<ApiResult<List<NameHelper>>> GetCombobox()
		=> GetAsync<List<NameHelper>>($@"{BASE}/combobox");
	public Task<ApiResult<ExcelImportResult<ChucVuRequest>>> PreviewImport(string filePath, string sheet)
	{
		return PostFileAsync<ExcelImportResult<ChucVuRequest>>(
			$"{BASE}/import/preview?sheet={sheet}",
			filePath);
	}
	public Task<ApiResult<bool>> ConfirmImport(List<ChucVuRequest> list)
	{
		return PostAsync<bool>($"{BASE}/import/confirm", list);
	}
}