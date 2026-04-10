using WPF.Common;
using WPF.Models;
namespace WPF.Client;

public class ChucVuQuyenClient : AppClientBase
{
	public Task<ApiResult<List<QuyenChecklistDTO>>> GetChecklist(int chucVuId)
		=> GetAsync<List<QuyenChecklistDTO>>($"api/chucvuquyen/checklist/{chucVuId}");
	public Task<ApiResult<bool>> Update(ChucVuQuyenDTO dto)
		=> PutAsync<bool>("api/chucvuquyen/update", dto);
}
