using System.Windows;
using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class TaiKhoanClient : AppClientBase
{
	private const string BASE = "api/taikhoan";

	public Task<ApiResult<int>> Create(TaiKhoanRequestDTO req)
		=> PostAsync<int>(BASE, req);

	public Task<ApiResult<bool>> ChangePassword(int id, ChangePasswordRequestDTO req)
		=> PutAsync<bool>($"{BASE}/{id}/password", req);


	public Task<ApiResult<bool>> ResetPassword(int id)
		=> PutAsync<bool>($"{BASE}/{id}/reset-password", null);

	public Task<ApiResult<bool>> UpdateStatus(int id, TaiKhoanUpdateRequestDTO req)
		=> PutAsync<bool>($"{BASE}/{id}/status", req);

	public Task<ApiResult<bool>> UpdateFcmToken(int id, string token)
	{
		var body = new { FCMToken = token };
		return PutAsync<bool>($"{BASE}/{id}/fcm-token", body);
	}
	public Task<ApiResult<TaiKhoanReadModel>> Detail(int id)
		=> GetAsync<TaiKhoanReadModel>($"{BASE}/{id}");

	public Task<ApiResult<PagedResult<TaiKhoanListReadModel>>>
		GetPaged(int page = 1, int size = 15, string? vaiTro = null, string? trangThai = null)
	{
		var url = $"{BASE}?page={page}&size={size}";

		if (!string.IsNullOrWhiteSpace(vaiTro))
			url += $"&vaiTro={vaiTro}";

		if (!string.IsNullOrWhiteSpace(trangThai))
			url += $"&trangThai={trangThai}";

		return GetAsync<PagedResult<TaiKhoanListReadModel>>(url);
	}
	public Task<ApiResult<PagedResult<TaiKhoanListReadModel>>>
	Search(int page = 1, int size = 15,string keyword = "", string? vaiTro = null, string? trangThai = null)
	{
		var url = $"{BASE}/search?keyword={keyword}&page={page}&size={size}";

		if (!string.IsNullOrWhiteSpace(vaiTro))
			url += $"&vaiTro={vaiTro}";

		if (!string.IsNullOrWhiteSpace(trangThai))
			url += $"&trangThai={trangThai}";

		return GetAsync<PagedResult<TaiKhoanListReadModel>>(url);
	}
}