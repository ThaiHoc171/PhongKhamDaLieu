using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class PhongChucNangClient : AppClientBase
{
	private const string BASE = "api/phongchucnang";

	public Task<ApiResult<bool>> Create(PhongChucNangRequestDTO dto)
		=> PostAsync<bool>(BASE, dto);

	public Task<ApiResult<bool>> Update(int id, PhongChucNangRequestDTO dto)
		=> PutAsync<bool>($@"{BASE}/{id}", dto);

	public Task<ApiResult<bool>> ChangeStatus(int id, string trangThaiMoi)
		=> PutAsync<bool>($@"{BASE}/{id}/status", trangThaiMoi);

	public Task<ApiResult<PhongChucNangReadModel>> GetById(int id)
		=> GetAsync<PhongChucNangReadModel>($@"{BASE}/{id}");

	public Task<ApiResult<PagedResult<PhongChucNangReadListModel>>> GetPaged(int page = 1, int size = 10, string? trangThai = null)
	{
		var url = $@"{BASE}?page={page}&size={size}";
		if (!string.IsNullOrWhiteSpace(trangThai))
			url += $"&trangThai={trangThai}";
		return GetAsync<PagedResult<PhongChucNangReadListModel>>(url);
	}

	public Task<ApiResult<PagedResult<PhongChucNangReadListModel>>> Search(string? keyword, int page = 1, int size = 10)
	{
		var url = $@"{BASE}/search?page={page}&size={size}";
		if (!string.IsNullOrWhiteSpace(keyword))
			url += $"&keyword={keyword}";
		return GetAsync<PagedResult<PhongChucNangReadListModel>>(url);
	}

	public Task<ApiResult<List<NameHelper>>> GetCombobox()
		=> GetAsync<List<NameHelper>>($@"{BASE}/combobox");
}