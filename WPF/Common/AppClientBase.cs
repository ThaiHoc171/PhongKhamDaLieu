using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace HoanMyClinic.Common;

public abstract class AppClientBase
{
	protected static readonly HttpClient _httpClient;

	private static readonly JsonSerializerOptions _jsonOptions = new()
	{
		PropertyNameCaseInsensitive = true
	};

	static AppClientBase()
	{
		_httpClient = new HttpClient
		{
			BaseAddress = new Uri("https://clinicjwt-api-bperhwd0dne7c9c0.southeastasia-01.azurewebsites.net/")
		};
	}

	protected async Task<bool> HasInternetAsync()
	{
		try
		{
			using var client = new HttpClient
			{
				Timeout = TimeSpan.FromSeconds(3)
			};

			var response = await client.GetAsync("https://www.google.com");
			return response.IsSuccessStatusCode;
		}
		catch
		{
			return false;
		}
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

	// ── Helper chung: deserialize 2 bước, tránh crash khi data: null ──
	private static ApiResult<T> ParseResponse<T>(string json)
	{
		var raw = JsonSerializer.Deserialize<ApiResponse<JsonElement>>(json, _jsonOptions);

		if (raw == null)
			return ApiResult<T>.Fail("Invalid server response");

		if (!raw.Success)
			return ApiResult<T>.Fail(raw.Message);

		T? data = default;
		if (raw.Data.ValueKind != JsonValueKind.Null &&
			raw.Data.ValueKind != JsonValueKind.Undefined)
		{
			data = JsonSerializer.Deserialize<T>(raw.Data.GetRawText(), _jsonOptions);
		}

		return ApiResult<T>.SuccessResult(data, raw.Message);
	}

	protected async Task<ApiResult<T>> GetAsync<T>(string url, bool attachToken = true)
	{
		try
		{
			if (!await HasInternetAsync())
				return ApiResult<T>.Fail("Không có kết nối internet");

			if (attachToken)
				AttachToken();

			var response = await _httpClient.GetAsync(url);
			var json = await response.Content.ReadAsStringAsync();

			return ParseResponse<T>(json);
		}
		catch (HttpRequestException)
		{
			return ApiResult<T>.Fail("Không thể kết nối đến server");
		}
		catch (TaskCanceledException)
		{
			return ApiResult<T>.Fail("Request timeout (mạng yếu hoặc mất kết nối)");
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
			if (!await HasInternetAsync())
				return ApiResult<T>.Fail("Không có kết nối internet");

			if (attachToken)
				AttachToken();

			var jsonBody = JsonSerializer.Serialize(body);
			var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(url, content);
			var json = await response.Content.ReadAsStringAsync();

			return ParseResponse<T>(json);
		}
		catch (HttpRequestException)
		{
			return ApiResult<T>.Fail("Không thể kết nối đến server");
		}
		catch (TaskCanceledException)
		{
			return ApiResult<T>.Fail("Request timeout (mạng yếu hoặc mất kết nối)");
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
			if (!await HasInternetAsync())
				return ApiResult<T>.Fail("Không có kết nối internet");

			AttachToken();

			var jsonBody = JsonSerializer.Serialize(body);
			var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

			var response = await _httpClient.PutAsync(url, content);
			var json = await response.Content.ReadAsStringAsync();

			return ParseResponse<T>(json);
		}
		catch (HttpRequestException)
		{
			return ApiResult<T>.Fail("Không thể kết nối đến server");
		}
		catch (TaskCanceledException)
		{
			return ApiResult<T>.Fail("Request timeout (mạng yếu hoặc mất kết nối)");
		}
		catch (Exception ex)
		{
			return ApiResult<T>.Fail(ex.Message);
		}
	}

	protected async Task<ApiResult<T>> DeleteAsync<T>(string url, bool attachToken = true)
	{
		try
		{
			if (!await HasInternetAsync())
				return ApiResult<T>.Fail("Không có kết nối internet");

			if (attachToken)
				AttachToken();

			var response = await _httpClient.DeleteAsync(url);
			var json = await response.Content.ReadAsStringAsync();

			return ParseResponse<T>(json);
		}
		catch (HttpRequestException)
		{
			return ApiResult<T>.Fail("Không thể kết nối đến server");
		}
		catch (TaskCanceledException)
		{
			return ApiResult<T>.Fail("Request timeout (mạng yếu hoặc mất kết nối)");
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
			if (!await HasInternetAsync())
				return ApiResult<T>.Fail("Không có kết nối internet");

			AttachToken();

			var content = new MultipartFormDataContent();
			var fileBytes = await File.ReadAllBytesAsync(filePath);
			var fileContent = new ByteArrayContent(fileBytes);

			fileContent.Headers.ContentType =
				new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

			content.Add(fileContent, "file", Path.GetFileName(filePath));

			var response = await _httpClient.PostAsync(url, content);
			var json = await response.Content.ReadAsStringAsync();

			return ParseResponse<T>(json);
		}
		catch (HttpRequestException)
		{
			return ApiResult<T>.Fail("Không thể kết nối đến server");
		}
		catch (TaskCanceledException)
		{
			return ApiResult<T>.Fail("Request timeout (mạng yếu hoặc mất kết nối)");
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
			fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

			content.Add(fileContent, "file", Path.GetFileName(filePath));
			content.Add(new StringContent(folder), "folder");

			var response = await _httpClient.PostAsync(url, content);
			var json = await response.Content.ReadAsStringAsync();

			var result = JsonSerializer.Deserialize<UploadResponse>(json, _jsonOptions);

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