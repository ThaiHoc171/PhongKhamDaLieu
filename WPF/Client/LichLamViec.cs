using System.Diagnostics;
using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class LichLamViecClient : AppClientBase
{
	private const string BASE = "api/lichlamviec";

	public Task<ApiResult<List<LichLamViecReadModel>>> GetWeek(int week = 0)
		=> GetAsync<List<LichLamViecReadModel>>($@"{BASE}?week={week}");

	public Task<ApiResult<LichLamViecReadWeekModel>> GetByNhanVien(int nhanVienId, int week = 0)
	{
		var url = $@"{BASE}/nhan-vien/{nhanVienId}?week={week}";
		return GetAsync<LichLamViecReadWeekModel>(url);
	}

	public Task<ApiResult<ExcelImportResult<LichLamViecRequest>>> PreviewImport(string filePath, string sheet)
		=> PostFileAsync<ExcelImportResult<LichLamViecRequest>>($"{BASE}/import/preview?sheet={sheet}",filePath);

	public Task<ApiResult<ExcelImportResult<LichLamViecRequest>>> ValidateImport(List<LichLamViecRequest> list)
		=> PostAsync<ExcelImportResult<LichLamViecRequest>>($"{BASE}/import/validate", list);
	public Task<ApiResult<bool>> ConfirmImport(List<LichLamViecRequest> list)
		=> PostAsync<bool>($"{BASE}/import/confirm", list);
}