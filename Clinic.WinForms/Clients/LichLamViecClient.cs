using Clinic.WinForms.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.Clients
{
	public class LichLamViecClient : ApiClientBase_old
	{
		public async Task<LichLamViecCaNhanResponseDTO>	GetByNhanVienIdAsync(int nhanVienId, int page = 0)
		{
			AttachToken();
			var response = await _httpClient.GetAsync(
				$"api/LichLamViec/GetByNhanVien/{nhanVienId}?page={page}");

			if (!response.IsSuccessStatusCode)
				return null;

			var responseString = await response.Content.ReadAsStringAsync();

			var result = JsonConvert
				.DeserializeObject<LichLamViecCaNhanResponseDTO>(responseString);

			return result;
		}
		public async Task<List<LichLamViecResponseDTO>> GetByWeekAsync(int page)
		{
			AttachToken();

			var response = await _httpClient.GetAsync(
				$"api/LichLamViec/GetByWeek?page={page}");

			if (!response.IsSuccessStatusCode)
				return null;

			var json = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<List<LichLamViecResponseDTO>>(json);
		}

		public async Task<bool> CreateLichLamViecAsync(LichLamViecRequestDTO dto)
		{
			AttachToken();

			var json = JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("api/LichLamViec/TaoLich", content);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync();
				throw new Exception($"API Error: {error}");
			}

			return true;
		}
	}
}
