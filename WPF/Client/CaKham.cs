using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class CaKhamClient : AppClientBase
{
	private readonly string BASE = "api/cakham";

	public Task<ApiResult<int>> Create(CaKhamGenerate req)
		=> PostAsync<int>(BASE, req);

	public Task<ApiResult<bool>> UpdateTrangThai(int id, CaKhamTrangThaiDTO req)
	{
		return PutAsync<bool>($"{BASE}/{id}/trang-thai", req);
	}

	public Task<ApiResult<CaKhamReadModel>> GetDetail(int id)
		=> GetAsync<CaKhamReadModel>($"{BASE}/{id}");

	public Task<ApiResult<PagedResult<CaKhamListReadModel>>> GetPaged(
		DateTime ngayKham,
		string trangThai,
		string loaiCaKham,
		int page = 1,
		int size = 15)
	{
		var url =
			$"{BASE}" +
			$"?ngayKham={ngayKham:yyyy-MM-dd}" +
			$"&trangThai={Uri.EscapeDataString(trangThai)}" +
			$"&loaiCaKham={Uri.EscapeDataString(loaiCaKham)}" +
			$"&pageNumber={page}" +
			$"&pageSize={size}";

		return GetAsync<PagedResult<CaKhamListReadModel>>(url);
	}

	public Task<ApiResult<PagedResult<CaKhamListReadModel>>> SearchByThongTin(
		int thongTinId,
		int page = 1,
		int size = 10)
	{
		var url = $"{BASE}/search/by-thongtin/{thongTinId}?pageNumber={page}&pageSize={size}";
		return GetAsync<PagedResult<CaKhamListReadModel>>(url);
	}
	public Task<ApiResult<PagedResult<CaKhamListReadModel>>> ChoXacNhan(int page = 1, int size = 10)
	{
		var url = $"{BASE}/choxacnhan?pageNumber={page}&pageSize={size}";
		return GetAsync<PagedResult<CaKhamListReadModel>>(url);
	}

	public Task<ApiResult<bool>> Register(int id, CaKhamRegister req)
		=> PutAsync<bool>($"{BASE}/{id}/register", req);

	public Task<ApiResult<bool>> Cancel(int id)
		=> PutAsync<bool>($"{BASE}/{id}/cancel", null);

	//GET CAKHAM TRONG THEO NHANVIEN
	public Task<ApiResult<List<int>>> GetKhungGioTrong(DateTime ngay, string loai, int nhanVienId)
	{
		var url = $"{BASE}/khunggio-trong?" +
				  $"ngayKham={ngay:yyyy-MM-dd}" +
				  $"&loaiCaKham={loai}" +
				  $"&nhanVienId={nhanVienId}";

		return GetAsync<List<int>>(url);
	}
	public Task<ApiResult<int>> GetCaKhamTrong(DateTime ngay, int khungGioId, string loai, int? nhanVienId)
	{
		var url = $"{BASE}/ca-trong?" +
				  $"ngayKham={ngay:yyyy-MM-dd}" +
				  $"&khungGioId={khungGioId}" +
				  $"&loaiCaKham={loai}" +
				  $"&nhanVienId={nhanVienId}";

		return GetAsync<int>(url);
	}
}