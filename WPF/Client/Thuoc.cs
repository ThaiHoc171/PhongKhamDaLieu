using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class ThuocClient : AppClientBase
{
	private const string BASE = "api/thuoc";

	// ==================== CREATE ====================
	public Task<ApiResult<bool>> Create(ThuocRequest dto)
		=> PostAsync<bool>(BASE, dto);

	// ==================== UPDATE ====================
	public Task<ApiResult<bool>> Update(int id, ThuocRequest dto)
		=> PutAsync<bool>($"{BASE}/{id}", dto);

	// ==================== DELETE ====================
	public Task<ApiResult<bool>> Delete(int id)
		=> PutAsync<bool>($"{BASE}/delete/{id}", null);

	// ==================== DETAIL ====================
	public Task<ApiResult<ThuocReadModel>> Detail(int id)
		=> GetAsync<ThuocReadModel>($"{BASE}/{id}");

	// ==================== PAGED ====================
	public Task<ApiResult<PagedResult<ThuocReadModel>>> Paged(int page = 1, int size = 10)
		=> GetAsync<PagedResult<ThuocReadModel>>($"{BASE}?page={page}&size={size}");

	// ==================== SEARCH ====================
	public Task<ApiResult<PagedResult<ThuocReadModel>>> Search(string keyword, int page = 1, int size = 10)
		=> GetAsync<PagedResult<ThuocReadModel>>($"{BASE}/search?keyword={keyword}&page={page}&size={size}");

	// ==================== COMBOBOX ====================
	public Task<ApiResult<List<NameHelper>>> Combobox()
		=> GetAsync<List<NameHelper>>($"{BASE}/combobox");

	// ==================== IMPORT PREVIEW ====================
	public Task<ApiResult<ExcelImportResult<ThuocRequest>>> PreviewImport(string filePath, string sheet)
		=> PostFileAsync<ExcelImportResult<ThuocRequest>>(
			$"{BASE}/import/preview?sheet={sheet}", filePath);

	// ==================== IMPORT VALIDATE ====================
	public Task<ApiResult<ExcelImportResult<ThuocRequest>>> ValidateImport(List<ThuocRequest> list)
		=> PostAsync<ExcelImportResult<ThuocRequest>>($"{BASE}/import/validate", list);

	// ==================== IMPORT CONFIRM ====================
	public Task<ApiResult<bool>> ConfirmImport(List<ThuocRequest> list)
		=> PostAsync<bool>($"{BASE}/import/confirm", list);
}