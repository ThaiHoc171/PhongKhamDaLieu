using System.IO;
using WPF.Common;
using WPF.Models;
namespace WPF.Client;
public class ChiTietPCNThietBiClient : AppClientBase
{
	private const string BASE = "api/chitiet-pcntb";
	public Task<ApiResult<int>> Create(ChiTietPCNThietBiRequest dto)
		=> PostAsync<int>(BASE, dto);
	public Task<ApiResult<bool>> Update(int id, ChiTietPCNThietBiUpdate dto)
		=> PutAsync<bool>($"{BASE}/{id}", dto);
	public Task<ApiResult<bool>> Delete(int id)
		=> PutAsync<bool>($"{BASE}/delete/{id}", null);
	public Task<ApiResult<ChiTietPCNThietBiReadModel>> GetById(int id)
		=> GetAsync<ChiTietPCNThietBiReadModel>($"{BASE}/{id}");
	public Task<ApiResult<List<ChiTietPCNThietBiListReadModel>>> GetList(int pcnTbId)
		=> GetAsync<List<ChiTietPCNThietBiListReadModel>>($"{BASE}?pcnTbId={pcnTbId}");
	public Task<ApiResult<List<NameHelper>>> GetCombobox(int pcnId, int tbId)
		=> GetAsync<List<NameHelper>>($"{BASE}/combobox?pcnId={pcnId}&tbId={tbId}");
	public Task<ApiResult<ExcelImportResult<ChiTietPCNThietBiRequest>>> PreviewImport(string filePath, string sheet)
		=> PostFileAsync<ExcelImportResult<ChiTietPCNThietBiRequest>>($"{BASE}/import/preview?sheet={sheet}", filePath);
	public Task<ApiResult<ExcelImportResult<ChiTietPCNThietBiRequest>>> ValidateImport(List<ChiTietPCNThietBiRequest> list)
		=> PostAsync<ExcelImportResult<ChiTietPCNThietBiRequest>>($"{BASE}/import/validate", list);
	public Task<ApiResult<bool>> ConfirmImport(List<ChiTietPCNThietBiRequest> list)
		=> PostAsync<bool>($"{BASE}/import/confirm", list);
}