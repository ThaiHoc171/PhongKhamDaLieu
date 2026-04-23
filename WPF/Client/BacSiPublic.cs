using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class BacSiProfileClient : AppClientBase
{
	private const string BASE = "api/bacsi";
	public Task<ApiResult<int>> Create(BacSiProfileRequestDTO req)
			=> PostAsync<int>(BASE, req);

	public Task<ApiResult<bool>> Update(int id, BacSiProfileUpdateDTO req)
	=> PutAsync<bool>($@"{BASE}/{id}", req);
	public Task<ApiResult<BacSiProfileReadModel>> GetById(int id)
	=> GetAsync<BacSiProfileReadModel>($@"{BASE}/{id}");

	public Task<ApiResult<PagedResult<BacSiProfileListReadModel>>> GetPaged(int page = 1, int size = 10)
	{
		var url = $@"{BASE}?pageNumber={page}&pageSize={size}";
		return GetAsync<PagedResult<BacSiProfileListReadModel>>(url);
	}
	public Task<ApiResult<PagedResult<BacSiProfileListReadModel>>> Search(string keyword, int page = 1, int size = 10)
	{
		var url = $@"{BASE}/search?keyword={keyword}&pageNumber={page}&pageSize={size}";
		return GetAsync<PagedResult<BacSiProfileListReadModel>>(url);
	}
	public Task<ApiResult<BacSiProfileReadModel>> GetByNhanVien(int nhanVienId)
	=> GetAsync<BacSiProfileReadModel>($@"{BASE}/nhanvien/{nhanVienId}");

}