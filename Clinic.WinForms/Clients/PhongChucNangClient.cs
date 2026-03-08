using Clinic.WinForms.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.Clients
{
	public class PhongChucNangClient: ApiClientBase_old
	{
		public async Task<List<ComboboxResult>> GetComboboxAsync()
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/PhongChucNang/combobox");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComboboxResult>>(responseString);
			return result;
		}
		public async Task<List<PhongChucNangResponseDTO>> GetPhongAsync() 
		{ 			
			AttachToken();
			var response = await _httpClient.GetAsync($"api/PhongChucNang");
			if (!response.IsSuccessStatusCode)
				return null;	
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PhongChucNangResponseDTO>>(responseString);
			return result;
		}
		public async Task<PhongChucNangResponseDTO> GetPhongByIdAsync(int id)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/PhongChucNang/{id}");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PhongChucNangResponseDTO>(responseString);
			return result;
		}
		public async Task<bool> CreatePhongAsync(PhongChucNangRequestDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"api/PhongChucNang", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateAsync(int id, PCNUpdateDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"api/PhongChucNang/{id}", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateSatusAsync(int id, string trangthai)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(trangthai);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"api/PhongChucNang/{id}/trangthai", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<List<PhongChucNangResponseDTO>> SearchAsync(string keyword)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/PhongChucNang/timkiem?keyword={keyword}");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PhongChucNangResponseDTO>>(responseString);
			return result;
		}
	}
}
