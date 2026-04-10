using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class PhienKhamThietBiClient : AppClientBase
{
	private const string BASE = "api/phienkham-thietbi";

	// ==================== CREATE ====================
	public Task<ApiResult<bool>> Create(PhienKhamThietBiRequestDTO req)
		=> PostAsync<bool>(BASE, req);

	// ==================== UPDATE ====================
	public Task<ApiResult<bool>> Update(int id, string? ghiChu)
		=> PutAsync<bool>($@"{BASE}/{id}", ghiChu);
	public Task<ApiResult<bool>> Delete(int id)
		=> PutAsync<bool>($@"{BASE}/delete/{id}", null);

	// ==================== GET BY PHIEN KHAM ====================
	public Task<ApiResult<List<PhienKhamThietBiReadModel>>> GetByPhienKham(int phienKhamId)
		=> GetAsync<List<PhienKhamThietBiReadModel>>($@"{BASE}/phienkham/{phienKhamId}");
}