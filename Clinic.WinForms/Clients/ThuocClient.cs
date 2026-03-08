using Clinic.WinForms.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace Clinic.WinForms.Clients
{
	public class ThuocClient : ApiClientBase_old
	{
		public async Task<List<ComboboxResult>> GetComboboxAsync()
		{
			AttachToken();
			var response = await _httpClient.GetAsync("api/Thuoc/combobox");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			return Newtonsoft.Json.JsonConvert
				.DeserializeObject<List<ComboboxResult>>(responseString);
		}
		public async Task<PagedResult<ThuocResponseDTO>> GetAllAsync(int pageNumber, int pageSize)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/Thuoc?pageNumber={pageNumber}&pageSize={pageSize}");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			return Newtonsoft.Json.JsonConvert
				.DeserializeObject<PagedResult<ThuocResponseDTO>>(responseString);
		}
		public async Task<ThuocResponseDTO> GetByIdAsync(int id)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/Thuoc/{id}");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			return Newtonsoft.Json.JsonConvert
				.DeserializeObject<ThuocResponseDTO>(responseString);
		}
		public async Task<bool> CreateAsync(ThuocRequestDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("api/Thuoc", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateAsync(int id, ThuocRequestDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"api/Thuoc/{id}", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<List<ThuocResponseDTO>> SearchAsync(string keyword)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/Thuoc/timkiem?kw={keyword}");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			return Newtonsoft.Json.JsonConvert
				.DeserializeObject<List<ThuocResponseDTO>>(responseString);
		}
	}
}