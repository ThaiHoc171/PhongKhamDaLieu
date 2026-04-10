using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class ThongTinCaNhanClient : AppClientBase
{
	private const string BASE = "api/thongtincanhan";
	public Task<ApiResult<PagedResult<ThongTinReadListModel>>> GetKhachList(int page = 1, int size = 15)
	{
		var url = $@"{BASE}?pageNumber={page}&pageSize={size}";
		return GetAsync<PagedResult<ThongTinReadListModel>>(url);
	}
	public Task<ApiResult<PagedResult<ThongTinReadListModel>>> Search(string keyword,int page = 1, int size = 15)
	{
		var url = $@"{BASE}/search?keyword={keyword}&pageNumber={page}&pageSize={size}";
		return GetAsync<PagedResult<ThongTinReadListModel>>(url);
	}
	public Task<ApiResult<ThongTinReadModel>> Detail(int id)
		=> GetAsync<ThongTinReadModel>($"{BASE}/{id}");
	public Task<ApiResult<bool>> Update(int id, ThongTinUpdateRequestDTO req)
		=> PutAsync<bool>($"{BASE}/{id}", req);
	public Task<ApiResult<bool>> LinkTaiKhoan(int thongTinId, int taiKhoanId, string? email)
	=> PutAsync<bool>($"{BASE}/{thongTinId}/taikhoan/{taiKhoanId}?email={email}",null);

}