using Clinic.WinForms.Common;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Clinic.WinForms.Clients
{
	public abstract class ApiClientBase_old
	{
		protected static readonly HttpClient _httpClient;

		static ApiClientBase_old()
		{
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri("https://clinicjwt-api-bperhwd0dne7c9c0.southeastasia-01.azurewebsites.net/");
		}

		protected void AttachToken()
		{
			if (!string.IsNullOrEmpty(Session.Token))
			{
				_httpClient.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", Session.Token);
			}
		}
	}
	// Ver 1.1
	public abstract class ApiClientBase
	{
		protected static readonly HttpClient _httpClient;

		static ApiClientBase()
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

		protected async Task<ApiResult<T>> GetAsync<T>(string url)
		{
			try
			{
				AttachToken();

				var response = await _httpClient.GetAsync(url);

				if (!response.IsSuccessStatusCode)
				{
					return ApiResult<T>.Fail($"Server error: {response.StatusCode}");
				}

				var json = await response.Content.ReadAsStringAsync();

				var data = JsonSerializer.Deserialize<T>(json,
					new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					});

				return ApiResult<T>.Success(data);
			}
			catch (Exception ex)
			{
				return ApiResult<T>.Fail(ex.Message);
			}
		}

		protected async Task<ApiResult<T>> PostAsync<T>(string url, object body)
		{
			try
			{
				AttachToken();

				var jsonBody = JsonSerializer.Serialize(body);
				var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

				var response = await _httpClient.PostAsync(url, content);

				if (!response.IsSuccessStatusCode)
				{
					return ApiResult<T>.Fail($"Server error: {response.StatusCode}");
				}

				var json = await response.Content.ReadAsStringAsync();

				var data = JsonSerializer.Deserialize<T>(json,
					new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					});

				return ApiResult<T>.Success(data);
			}
			catch (Exception ex)
			{
				return ApiResult<T>.Fail(ex.Message);
			}
		}
		protected async Task<ApiResult<T>> PutAsync<T>(string url, object body)
		{
			try
			{
				AttachToken();

				var jsonBody = JsonSerializer.Serialize(body);
				var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

				var response = await _httpClient.PutAsync(url, content);

				if (!response.IsSuccessStatusCode)
				{
					return ApiResult<T>.Fail($"Server error: {response.StatusCode}");
				}

				var json = await response.Content.ReadAsStringAsync();

				var data = JsonSerializer.Deserialize<T>(json,
					new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					});

				return ApiResult<T>.Success(data);
			}
			catch (Exception ex)
			{
				return ApiResult<T>.Fail(ex.Message);
			}
		}
	}
}