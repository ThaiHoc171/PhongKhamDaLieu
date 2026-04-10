using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class PhienKhamBenhClient : AppClientBase
{
	private const string BASE = "api/phienkhambenh";

	// ==================== CREATE ====================
	public Task<ApiResult<bool>> Create(PhienKhamBenhRequestDTO req)
		=> PostAsync<bool>(BASE, req);

	// ==================== UPDATE ====================
	public Task<ApiResult<bool>> Update(int id, PhienKhamBenhRequestDTO req)
		=> PutAsync<bool>($@"{BASE}/{id}", req);
	public Task<ApiResult<bool>> Delete(int id)
		=> PutAsync<bool>($@"{BASE}/delete/{id}", null);
	// ==================== GET DETAIL ====================
	public Task<ApiResult<PhienKhamBenhResponseDTO>> Detail(int id)
		=> GetAsync<PhienKhamBenhResponseDTO>($@"{BASE}/{id}");

	// ==================== GET BY PHIEN KHAM ====================
	public Task<ApiResult<List<PhienKhamBenhReadModel>>> GetByPhienKhamId(int phienKhamId)
		=> GetAsync<List<PhienKhamBenhReadModel>>($@"{BASE}/phienkham/{phienKhamId}");
}
