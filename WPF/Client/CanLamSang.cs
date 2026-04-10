using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class CanLamSangClient : AppClientBase
{
	private readonly string BASE = "api/canlamsang";
	public Task<ApiResult<bool>> Create(CanLamSangRequest req)
		=> PostAsync<bool>(BASE, req);
	public Task<ApiResult<ExcelImportResult<CanLamSangRequest>>> PreviewImport(string filePath, string sheet)
	{
		return PostFileAsync<ExcelImportResult<CanLamSangRequest>>(
			$"{BASE}/import/preview?sheet={sheet}",
			filePath);
	}
	public Task<ApiResult<bool>> ConfirmImport(List<CanLamSangRequest> list)
	{
		return PostAsync<bool>($"{BASE}/import/confirm", list);
	}
	public Task<ApiResult<bool>> Update(int id, CanLamSangRequest req)
		=> PutAsync<bool>($"{BASE}/{id}", req);

	public Task<ApiResult<CanLamSangReadModel>> GetDetail(int id)
		=> GetAsync<CanLamSangReadModel>($"{BASE}/{id}");

	public Task<ApiResult<PagedResult<CanLamSangListReadModel>>> GetPaged(int page = 1, int size = 10)
	{
		var url = $"{BASE}?pageNumber={page}&pageSize={size}";
		return GetAsync<PagedResult<CanLamSangListReadModel>>(url);
	}	
	public Task<ApiResult<PagedResult<CanLamSangListReadModel>>> Search(string keyword, int page = 1, int size = 10)
	{
		var url = $"{BASE}/search?keyword={keyword}&pageNumber={page}&pageSize={size}";
		return GetAsync<PagedResult<CanLamSangListReadModel>>(url);
	}
	public Task<ApiResult<List<CanLamSangListReadModel>>> GetByLoai(string loai)
		=> GetAsync<List<CanLamSangListReadModel>>($"{BASE}/loai?loai={loai}");
	public Task<ApiResult<List<NameHelper>>> GetCombobox()
		=> GetAsync<List<NameHelper>>($"{BASE}/combobox");
}
