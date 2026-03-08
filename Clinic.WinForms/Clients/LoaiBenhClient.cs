using Clinic.WinForms.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.Clients
{
	public class LoaiBenhClient : ApiClientBase_old
	{
		public async Task<List<ComboboxResult>> GetComboboxAsync()
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/LoaiBenh/combobox");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComboboxResult>>(responseString);
			return result;
		}
		public async Task<PagedResult<LoaiBenhResponseDTO>> GetAllAsync(int pageNumber, int pageSize)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/LoaiBenh/paged?pageNumber={pageNumber}&pageSize={pageSize}");
			if (!response.IsSuccessStatusCode) return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PagedResult<LoaiBenhResponseDTO>>(responseString);
			return result;
		}
		public async Task<LoaiBenhResponseDTO> GetByIdAsync(int Id)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/LoaiBenh/{Id}");
			if (!response.IsSuccessStatusCode) return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<LoaiBenhResponseDTO>(responseString);
			return result;
		}
		public async Task<bool> CreateAsync(LoaiBenhRequestDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"api/LoaiBenh", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateAsync(int id, LoaiBenhRequestDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"api/LoaiBenh/{id}", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<List<LoaiBenhResponseDTO>> SearchAsync(string keyword)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/LoaiBenh/timkiem?ten={keyword}");
			if (!response.IsSuccessStatusCode) return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LoaiBenhResponseDTO>>(responseString);
			return result;
		}
	}
}
