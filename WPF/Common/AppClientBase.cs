using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.IO;

namespace WPF.Common;

public abstract class AppClientBase
{
	protected static readonly HttpClient _httpClient;

	static AppClientBase()
	{
		_httpClient = new HttpClient
		{
			BaseAddress = new Uri("https://clinicjwt-api-bperhwd0dne7c9c0.southeastasia-01.azurewebsites.net/")
		};
	}

	private void AttachToken()
	{
		_httpClient.DefaultRequestHeaders.Authorization = null;

		if (!string.IsNullOrEmpty(Session.Token))
		{
			_httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", Session.Token);
		}
	}

	protected async Task<ApiResult<T>> GetAsync<T>(string url, bool attachToken = true)
	{
		try
		{
			if (attachToken)
				AttachToken();

			var response = await _httpClient.GetAsync(url);

			var json = await response.Content.ReadAsStringAsync();

			var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(json,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

			if (apiResponse == null)
				return ApiResult<T>.Fail("Invalid server response");

			if (!apiResponse.Success)
				return ApiResult<T>.Fail(apiResponse.Message);

			return ApiResult<T>.SuccessResult(apiResponse.Data, apiResponse.Message);
		}
		catch (Exception ex)
		{
			return ApiResult<T>.Fail(ex.Message);
		}
	}

	protected async Task<ApiResult<T>> PostAsync<T>(string url, object body, bool attachToken = true)
	{
		try
		{
			if (attachToken)
				AttachToken();

			var jsonBody = JsonSerializer.Serialize(body);
			var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(url, content);

			var json = await response.Content.ReadAsStringAsync();

			var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(json,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

			if (apiResponse == null)
				return ApiResult<T>.Fail("Invalid server response");

			if (!apiResponse.Success)
				return ApiResult<T>.Fail(apiResponse.Message);
			return ApiResult<T>.SuccessResult(apiResponse.Data, apiResponse.Message);
		}
		catch (Exception ex)
		{
			return ApiResult<T>.Fail(ex.Message);
		}
	}
	protected async Task<ApiResult<T>> PutAsync<T>(string url, object? body)
	{
		try
		{
			AttachToken();

			var jsonBody = JsonSerializer.Serialize(body);
			var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

			var response = await _httpClient.PutAsync(url, content);

			var json = await response.Content.ReadAsStringAsync();

			var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(json,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

			if (apiResponse == null)
				return ApiResult<T>.Fail("Invalid server response");

			if (!apiResponse.Success)
				return ApiResult<T>.Fail(apiResponse.Message);

			return ApiResult<T>.SuccessResult(apiResponse.Data, apiResponse.Message);
		}
		catch (Exception ex)
		{
			return ApiResult<T>.Fail(ex.Message);
		}
	}
	protected async Task<ApiResult<T>> PostFileAsync<T>(string url, string filePath)
	{
		try
		{
			AttachToken();

			var content = new MultipartFormDataContent();

			var fileBytes = await File.ReadAllBytesAsync(filePath);
			var fileContent = new ByteArrayContent(fileBytes);

			fileContent.Headers.ContentType =
				new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

			content.Add(fileContent, "file", Path.GetFileName(filePath));

			var response = await _httpClient.PostAsync(url, content);

			var json = await response.Content.ReadAsStringAsync();

			var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(json,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

			if (apiResponse == null)
				return ApiResult<T>.Fail("Invalid server response");

			if (!apiResponse.Success)
				return ApiResult<T>.Fail(apiResponse.Message);

			return ApiResult<T>.SuccessResult(apiResponse.Data, apiResponse.Message);
		}
		catch (Exception ex)
		{
			return ApiResult<T>.Fail(ex.Message);
		}
	}
	private string GetContentType(string filePath)
	{
		var ext = Path.GetExtension(filePath).ToLower();

		return ext switch
		{
			".jpg" or ".jpeg" => "image/jpeg",
			".png" => "image/png",
			".gif" => "image/gif",
			".webp" => "image/webp",
			".bmp" => "image/bmp",

			".pdf" => "application/pdf",

			".doc" => "application/msword",
			".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",

			".xls" => "application/vnd.ms-excel",
			".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",

			".txt" => "text/plain",

			_ => "application/octet-stream"
		};
	}
	protected async Task<ApiResult<string>> PostAllFileAsync(string url, string filePath, string folder)
	{
		try
		{
			AttachToken();

			using var content = new MultipartFormDataContent();

			var fileBytes = await File.ReadAllBytesAsync(filePath);
			var fileContent = new ByteArrayContent(fileBytes);

			var contentType = GetContentType(filePath);

			fileContent.Headers.ContentType =
				new MediaTypeHeaderValue(contentType);

			content.Add(fileContent, "file", Path.GetFileName(filePath));

			content.Add(new StringContent(folder), "folder");

			var response = await _httpClient.PostAsync(url, content);

			var json = await response.Content.ReadAsStringAsync();

			var result = JsonSerializer.Deserialize<UploadResponse>(
				json,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

			if (result == null)
				return ApiResult<string>.Fail("Invalid server response");

			return ApiResult<string>.SuccessResult(result.Url);
		}
		catch (Exception ex)
		{
			return ApiResult<string>.Fail(ex.Message);
		}
	}
	private class UploadResponse
	{
		public string? Url { get; set; }
	}
}
