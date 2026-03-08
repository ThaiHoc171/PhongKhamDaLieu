using Clinic.WinForms.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.WinForms.Clients
{
	public class CanLamSangClient : ApiClientBase_old
	{
		public async Task<List<ComboboxResult>> GetComboboxAsync()
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/CanLamSang/combobox");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComboboxResult>>(responseString);
			return result;
		}
		public async Task<PagedResult<CanLamSangResponseDTO>> GetAllAsync(int pageNumber, int pageSize)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/CanLamSang/paged?pageNumber={pageNumber}&pageSize={pageSize}");
			if (!response.IsSuccessStatusCode) return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PagedResult<CanLamSangResponseDTO>>(responseString);
			return result;
		}
		public async Task<CanLamSangResponseDTO> GetByIdAsync(int Id)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/CanLamSang/{Id}");
			if (!response.IsSuccessStatusCode) return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<CanLamSangResponseDTO>(responseString);
			return result;
		}
		public async Task<bool> CreateAsync(CanLamSangRequestDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"api/CanLamSang", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateAsync(int id, CanLamSangRequestDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"api/CanLamSang/{id}", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<List<CanLamSangResponseDTO>> SearchAsync(string keyword)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/CanLamSang/timkiem?tenCLS={keyword}");
			if (!response.IsSuccessStatusCode) return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CanLamSangResponseDTO>>(responseString);
			return result;
		}
		public async Task<bool> ActiveAsync(int id)
		{
			var response = await _httpClient.PutAsync($"api/CanLamSang/{id}/kichhoat",null);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> InActiveAsync(int id)
		{
			var response = await _httpClient.PutAsync($"api/CanLamSang/{id}/ngungsudung", null);
			return response.IsSuccessStatusCode;
		}
	}
}
