using WPF.Common;
using WPF.Models;
namespace WPF.Client;

public class ToaThuocClient : AppClientBase
{
	private const string BASE = "api/toathuoc";

	public Task<ApiResult<int>> Create(ToaThuocRequest req)
		=> PostAsync<int>(BASE, req);

	public Task<ApiResult<bool>> Exists(int phienKhamId)
		=> GetAsync<bool>($@"{BASE}/phienkham/exists/{phienKhamId}");

	public Task<ApiResult<ToaThuocReadModel>> GetByPhienKham(int phienKhamId)
		=> GetAsync<ToaThuocReadModel>($@"{BASE}/phienkham/{phienKhamId}");

	public Task<ApiResult<PagedResult<ToaThuocListReadModel>>> GetPaged(int page = 1, int size = 10)
	{
		var url = $@"{BASE}?page={page}&size={size}";
		return GetAsync<PagedResult<ToaThuocListReadModel>>(url);
	}

	public Task<ApiResult<bool>> Update(int toaThuocId, List<ChiTietToaThuocRequest> chiTiet)
		=> PutAsync<bool>($@"{BASE}/{toaThuocId}", chiTiet);
}