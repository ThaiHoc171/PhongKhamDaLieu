using System.Text;
using System.Collections.Generic;
using System.Linq;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System.Threading.Tasks;
using System.Net.Http;

namespace Clinic.WinForms.Clients
{
	public class ChucVuClient : ApiClientBase_old
	{
		public async Task<List<ChucVuResponseDTO>> GetAllChucVuAsync()
		{
			AttachToken();
			var response = await _httpClient.GetAsync("api/ChucVu");
			if (!response.IsSuccessStatusCode)
				return new List<ChucVuResponseDTO>();
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ChucVuResponseDTO>>(responseString);
			return result;
		}
		public async Task<bool> CreateChucVuAsync(ChucVuRequestDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("api/ChucVu", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateChucVuAsync(int id, ChucVuRequestDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"api/ChucVu/{id}", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateStatusAsync(int id, string trangThai)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(trangThai);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"api/ChucVu/{id}/trangthai", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<ChucVuResponseDTO> GetByIdAsync(int id)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/ChucVu/{id}");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ChucVuResponseDTO>(responseString);
			return result;
		}
		public async Task<List<ComboboxResult>> GetComboboxAsync()
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/ChucVu/combobox");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComboboxResult>>(responseString);
			return result;
		}
	}
}
