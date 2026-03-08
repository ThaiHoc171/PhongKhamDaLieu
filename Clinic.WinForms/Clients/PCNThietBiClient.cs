using Clinic.WinForms.DTOs;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.Clients
{
	public class PCNThietBiClient: ApiClientBase_old
	{
		public async Task<List<PCNThietBiResponseDTO>> GetByPhongAsync(int Id)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"/api/pcnThietBi/{Id}");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PCNThietBiResponseDTO>>(responseString);
			return result;
		}
		public async Task<bool> CreateAsync(PCNThietBiCreateDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"api/pcnThietBi", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> DeleteAsync(int id)
		{
			AttachToken();
			var response = await _httpClient.DeleteAsync($"api/pcnThietBi/{id}");
			return response.IsSuccessStatusCode;
		}
	}
}
