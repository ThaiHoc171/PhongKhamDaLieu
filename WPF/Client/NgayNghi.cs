using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Client;

public class NgayNghiNhanVienClient : AppClientBase
{
	private const string BASE = "api/ngaynghi";

	public Task<ApiResult<bool>> Create(NgayNghiRequestDTO req)
		=> PostAsync<bool>(BASE, req);

	public Task<ApiResult<bool>> Update(int id, NgayNghiUpdateRequestDTO req)
		=> PutAsync<bool>($"{BASE}/{id}", req);

	public Task<ApiResult<bool>> Delete(int id)
		=> DeleteAsync<bool>($"{BASE}/{id}");

	public Task<ApiResult<NgayNghiReadModel>> GetById(int id)
		=> GetAsync<NgayNghiReadModel>($"{BASE}/{id}");

	public Task<ApiResult<PagedResult<NgayNghiReadModel>>> GetPaged(int page = 1, int size = 10)
		=> GetAsync<PagedResult<NgayNghiReadModel>>($"{BASE}?pageNumber={page}&pageSize={size}");

	public Task<ApiResult<PagedResult<NgayNghiReadModel>>> Search(string keyword, int page = 1, int size = 10)
		=> GetAsync<PagedResult<NgayNghiReadModel>>($"{BASE}/search?keyword={keyword}&pageNumber={page}&pageSize={size}");

	public Task<ApiResult<List<NgayNghiRequestDTO>>> PreviewImport(string filePath)
		=> PostFileAsync<List<NgayNghiRequestDTO>>($"{BASE}/preview", filePath);

	public Task<ApiResult<List<NgayNghiRequestDTO>>> ValidateImport(List<NgayNghiRequestDTO> list)
		=> PostAsync<List<NgayNghiRequestDTO>>($"{BASE}/validate", list);

	public Task<ApiResult<bool>> Import(List<NgayNghiRequestDTO> list)
		=> PostAsync<bool>($"{BASE}/import", list);
}