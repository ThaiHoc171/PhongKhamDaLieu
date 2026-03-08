using Clinic.WinForms.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.Clients
{
	public class ThietBiClient: ApiClientBase_old
	{
		public async Task<List<ComboboxResult>> GetComboboxAsync()
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/ThietBi/combobox");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComboboxResult>>(responseString);
			return result;
		}
		public async Task<List<ThietBiResponseDTO>> GetAllAsync()
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/ThietBi");
			if (!response.IsSuccessStatusCode) return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ThietBiResponseDTO>>(responseString);
			return result;
		}
		public async Task<ThietBiResponseDTO> GetByIdAsync(int Id)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/ThietBi/{Id}");
			if (!response.IsSuccessStatusCode) return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ThietBiResponseDTO>(responseString);
			return result;
		}
		public async Task<bool> CreateAsync(ThietBiRequestDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"api/ThietBi",content);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateAsync(int id, ThietBiRequestDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"api/ThietBi/{id}", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<List<ThietBiResponseDTO>> SearchAsync(string keyword)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/ThietBi/timkiem?tenTB={keyword}");
			if (!response.IsSuccessStatusCode) return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ThietBiResponseDTO>>(responseString);
			return result;
		}
	}	
}
