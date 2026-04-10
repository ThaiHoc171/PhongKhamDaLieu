using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class LoaiBenhClient : AppClientBase
{
	private const string BASE = "api/loaibenh";

	// ==================== CREATE ====================
	public Task<ApiResult<bool>> Create(LoaiBenhRequest dto)
		=> PostAsync<bool>(BASE, dto);

	// ==================== UPDATE ====================
	public Task<ApiResult<bool>> Update(int id, LoaiBenhRequest dto)
		=> PutAsync<bool>($"{BASE}/{id}", dto);

	// ==================== DETAIL ====================
	public Task<ApiResult<LoaiBenhReadModel>> Detail(int id)
		=> GetAsync<LoaiBenhReadModel>($"{BASE}/{id}");

	// ==================== PAGED ====================
	public Task<ApiResult<PagedResult<LoaiBenhListReadModel>>> Paged(int page = 1, int size = 10)
		=> GetAsync<PagedResult<LoaiBenhListReadModel>>($"{BASE}?page={page}&size={size}");

	// ==================== SEARCH ====================
	public Task<ApiResult<PagedResult<LoaiBenhListReadModel>>> Search(string keyword, int page = 1, int size = 10)
		=> GetAsync<PagedResult<LoaiBenhListReadModel>>($"{BASE}/search?keyword={keyword}&page={page}&size={size}");

	// ==================== COMBOBOX ====================
	public Task<ApiResult<List<NameHelper>>> Combobox()
		=> GetAsync<List<NameHelper>>($"{BASE}/combobox");

	// ==================== GET TEN BENH ====================
	public Task<ApiResult<string?>> GetTenBenh(int id)
		=> GetAsync<string?>($"{BASE}/{id}/ten");

	// ==================== IMPORT PREVIEW ====================
	public Task<ApiResult<ExcelImportResult<LoaiBenhRequest>>> PreviewImport(string filePath, string sheet)
		=> PostFileAsync<ExcelImportResult<LoaiBenhRequest>>(
			$"{BASE}/import/preview?sheet={sheet}", filePath);

	// ==================== IMPORT VALIDATE ====================
	public Task<ApiResult<ExcelImportResult<LoaiBenhRequest>>> ValidateImport(List<LoaiBenhRequest> list)
		=> PostAsync<ExcelImportResult<LoaiBenhRequest>>($"{BASE}/import/validate", list);

	// ==================== IMPORT CONFIRM ====================
	public Task<ApiResult<bool>> ConfirmImport(List<LoaiBenhRequest> list)
		=> PostAsync<bool>($"{BASE}/import/confirm", list);
}