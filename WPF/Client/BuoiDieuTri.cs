using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class BuoiDieuTriClient : AppClientBase
{
	private const string BASE = "api/buoidieutri";

	// ==================== CREATE ====================
	public Task<ApiResult<int>> Create(BuoiDieuTriRequestDTO req)
		=> PostAsync<int>(BASE, req);

	// ==================== UPDATE IMAGE ====================
	public Task<ApiResult<bool>> UpdateImage(int id, string? hinhAnhJson)
		=> PutAsync<bool>($"{BASE}/{id}/image", hinhAnhJson);

	// ==================== DETAIL ====================
	public Task<ApiResult<BuoiDieuTriReadModel>> Detail(int id)
		=> GetAsync<BuoiDieuTriReadModel>($"{BASE}/{id}");

	// ==================== BY LIEU TRINH ====================
	public Task<ApiResult<List<BuoiDieuTriListReadModel>>>
		GetByLieuTrinh(int lieuTrinhId)
		=> GetAsync<List<BuoiDieuTriListReadModel>>($"{BASE}/lieutrinh/{lieuTrinhId}");

	// ==================== COUNT COMPLETE ====================
	public Task<ApiResult<int>> CountComplete(int lieuTrinhId)
		=> GetAsync<int>($"{BASE}/lieutrinh/{lieuTrinhId}/count-complete");
}