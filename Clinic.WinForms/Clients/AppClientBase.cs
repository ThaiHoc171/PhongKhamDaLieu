using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Clinic.WinForms.Clients
{
	public class ApiClientBase
	{
		protected readonly HttpClient _httpClient;

		public ApiClientBase()
		{
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback =
					(message, cert, chain, errors) => true
			};

			_httpClient = new HttpClient(handler);
			_httpClient.BaseAddress = new Uri("https://clinicjwt-api-bperhwd0dne7c9c0.southeastasia-01.azurewebsites.net/");
		}

		protected void SetToken(string token)
		{
			_httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", token);
		}
	}
}
