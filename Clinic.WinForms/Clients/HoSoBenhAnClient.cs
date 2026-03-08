using Clinic.WinForms.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace Clinic.WinForms.Clients
{
	public class HoSoBenhAnClient : ApiClientBase_old
	{
		//public async Task<List<HoSoBenhAnResponeDTO>> GetAllAsync()
		//{
		//	AttachToken();
		//	var response = await _httpClient.GetAsync("api/HoSoBenhAn");
		//	if (!response.IsSuccessStatusCode)
		//		return null;
		//	var responseString = await response.Content.ReadAsStringAsync();
		//	return Newtonsoft.Json.JsonConvert
		//		.DeserializeObject<List<HoSoBenhAnResponeDTO>>(responseString);
		//}
		//public async Task<HoSoBenhAnResponeDTO> GetByIdAsync(int id)
		//{
		//	AttachToken();
		//	var response = await _httpClient.GetAsync($"api/HoSoBenhAn/{id}");
		//	if (!response.IsSuccessStatusCode)
		//		return null;
		//	var responseString = await response.Content.ReadAsStringAsync();
		//	return Newtonsoft.Json.JsonConvert
		//		.DeserializeObject<HoSoBenhAnResponeDTO>(responseString);
		//}
		public async Task<HoSoBenhAnResponeDTO> GetByBenhNhanAsync(int benhNhanID)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/HoSoBenhAn/benhnhan/{benhNhanID}");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			return Newtonsoft.Json.JsonConvert
				.DeserializeObject<HoSoBenhAnResponeDTO>(responseString);
		}
		public async Task<bool> CreateAsync(HoSoBenhAnRequestDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("api/HoSoBenhAn", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateAsync(int id, HoSoBenhAnUpdateDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"api/HoSoBenhAn/{id}", content);
			return response.IsSuccessStatusCode;
		}
	}
}