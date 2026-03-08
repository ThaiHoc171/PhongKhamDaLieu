using Clinic.WinForms.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace Clinic.WinForms.Clients
{
	public class BenhNhanClient : ApiClientBase_old
	{
		public async Task<PagedResult<BenhNhanResponseDTO>> GetAllAsync(int pageNumber, int pageSize)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/BenhNhan?pageNumber={pageNumber}&pageSize={pageSize}");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			return Newtonsoft.Json.JsonConvert
				.DeserializeObject<PagedResult<BenhNhanResponseDTO>>(responseString);
		}
		public async Task<BenhNhanIdResponseDTO> GetByIdAsync(int id)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/BenhNhan/{id}");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			return Newtonsoft.Json.JsonConvert
				.DeserializeObject<BenhNhanIdResponseDTO>(responseString);
		}
		public async Task<bool> CreateAsync(BenhNhanRequestDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("api/BenhNhan", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateAsync(int id, CapNhatBenhNhanDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"api/BenhNhan/{id}", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<List<BenhNhanResponseDTO>> SearchAsync(string keyword)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/BenhNhan/Search?keyword={keyword}");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			return Newtonsoft.Json.JsonConvert
				.DeserializeObject<List<BenhNhanResponseDTO>>(responseString);
		}
		public async Task<List<ComboboxResult>> GetComboboxAsync()
		{
			AttachToken();
			var response = await _httpClient.GetAsync("api/BenhNhan/combobox");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			return Newtonsoft.Json.JsonConvert
				.DeserializeObject<List<ComboboxResult>>(responseString);
		}
	}
}