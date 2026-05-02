using HoanMyClinic.Common;
using HoanMyClinic.Models;
namespace HoanMyClinic.Client;
public class ExcelClient: AppClientBase
{
	public Task<ApiResult<List<string>>> GetSheets(string filePath)
	{
		return PostFileAsync<List<string>>("api/excel/sheets", filePath);
	}
}
