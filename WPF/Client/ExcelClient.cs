using WPF.Common;
using WPF.Models;
namespace WPF.Client;
public class ExcelClient: AppClientBase
{
	public Task<ApiResult<List<string>>> GetSheets(string filePath)
	{
		return PostFileAsync<List<string>>("api/excel/sheets", filePath);
	}
}
