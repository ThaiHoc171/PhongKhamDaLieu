using HoanMyClinic.Common;

public class UploadClient : AppClientBase
{
	private const string BASE = "api/upload";

	public Task<ApiResult<string>> UploadImage(string filePath, string folder)
	{
		var url = $"{BASE}/image";
		return PostAllFileAsync(url, filePath, folder);
	}
	public Task<ApiResult<string>> UploadFiles(string filePath, string folder)
	{
		var url = $"{BASE}/files";
		return PostAllFileAsync(url, filePath, folder);
	}
}