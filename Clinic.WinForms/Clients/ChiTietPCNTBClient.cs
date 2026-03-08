using Clinic.WinForms.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.Clients
{
	public class ChiTietPCNTBClient : ApiClientBase_old
	{
		public async Task<List<ChiTietPCNThietBiResponseDTO>> GetByPhongAsync(int Id)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/chitiet-pcntb/pcn-tb/{Id}");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ChiTietPCNThietBiResponseDTO>>(responseString);
			return result;
		}
		public async Task<bool> CreateAsync(ChiTietPCNThietBiCreateDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"api/chitiet-pcntb", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> DeleteAsync(int id)
		{
			AttachToken();
			var response = await _httpClient.DeleteAsync($"api/chitiet-pcntb/{id}");
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateAsync(int id, ChiTietPCNThietBiUpdateDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"api/chitiet-pcntb/{id}", content);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> UpdateStatusAsync(int id, string tinhTrang)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(tinhTrang);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"api/chitiet-pcntb/{id}/tinh-trang", content);
			return response.IsSuccessStatusCode;
		}
	}
}
