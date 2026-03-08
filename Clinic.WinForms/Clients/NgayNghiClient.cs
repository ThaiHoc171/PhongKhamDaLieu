using Clinic.WinForms.DTOs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.Clients
{
	public class NgayNghiClient:ApiClientBase_old
	{
		public async Task<List<NgayNghiResponseDTO>> GetByMonth(int month,int year)
		{
			AttachToken();

			var url = $"api/ngaynghi/thang?thang={month}&nam={year}";
			var response = await _httpClient.GetAsync(url);
			response.EnsureSuccessStatusCode();
			var json = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<List<NgayNghiResponseDTO>>(json)
				   ?? new List<NgayNghiResponseDTO>();
		}
		public async Task<bool> CreateNgayNghiAsync(NgayNghiRequestDTO dto)
		{
			AttachToken();
			var json = JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("api/ngaynghi", content);
			response.EnsureSuccessStatusCode();
			return true;
		}
	}
}
