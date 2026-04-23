using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class BaiVietClient : AppClientBase
{
	private const string BASE = "api/baiviet";

	// ================= CREATE =================
	public Task<ApiResult<int>> Create(ThemBaiVietDTO req)
		=> PostAsync<int>(BASE, req);

	// ================= UPDATE =================
	public Task<ApiResult<bool>> Update(int id, CapNhatBaiVietDTO req)
		=> PutAsync<bool>($"{BASE}/{id}", req);

	// ================= DELETE =================
	public Task<ApiResult<bool>> Delete(int id)
		=> PutAsync<bool>($"{BASE}/delete/{id}",null);

	// ================= STATUS ACTION =================
	public Task<ApiResult<bool>> Post(int id)
		=> PutAsync<bool>($"{BASE}/post/{id}", null);

	public Task<ApiResult<bool>> Hide(int id)
		=> PutAsync<bool>($"{BASE}/hide/{id}", null);

	public Task<ApiResult<bool>> Save(int id)
		=> PutAsync<bool>($"{BASE}/save/{id}", null);

	// ================= DETAIL =================
	public Task<ApiResult<BaiVietReadModel>> Detail(int id)
		=> GetAsync<BaiVietReadModel>($"{BASE}/{id}");

	// ================= PAGED =================
	public Task<ApiResult<PagedResult<BaiVietListReadModel>>> GetPaged(
		int page = 1,
		int size = 10,
		string? trangThai = null)
	{
		var url = $"{BASE}?page={page}&size={size}";

		if (!string.IsNullOrEmpty(trangThai))
			url += $"&trangThai={Uri.EscapeDataString(trangThai)}";

		return GetAsync<PagedResult<BaiVietListReadModel>>(url);
	}

	// ================= SEARCH =================
	public Task<ApiResult<PagedResult<BaiVietListReadModel>>> Search(
		string keyword,
		int page = 1,
		int size = 10,
		string? trangThai = null)
	{
		var url = $"{BASE}/search?keyword={Uri.EscapeDataString(keyword)}&page={page}&size={size}";

		if (!string.IsNullOrEmpty(trangThai))
			url += $"&trangThai={Uri.EscapeDataString(trangThai)}";

		return GetAsync<PagedResult<BaiVietListReadModel>>(url);
	}

	// ================= FILTER =================
	public Task<ApiResult<List<BaiVietListReadModel>>> GetByLoaiBenh(int loaiBenhId)
		=> GetAsync<List<BaiVietListReadModel>>($"{BASE}/loaibenh/{loaiBenhId}");

	// ================= TOP =================
	public Task<ApiResult<List<BaiVietListReadModel>>> GetTop(int top = 5)
	{
		var url = $"{BASE}/top?top={top}";
		return GetAsync<List<BaiVietListReadModel>>(url);
	}
}