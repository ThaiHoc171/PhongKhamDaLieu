using Clinic.WinForms.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.Clients
{
	public class NhanVienClient : ApiClientBase_old
	{
		public async Task<PagedResult<NhanVienResponseDTO>> GetNhanVienPagedAsync(int pageNumber, int pageSize)
		{
			AttachToken();

			var response = await _httpClient.GetAsync(
				$"api/NhanVien/paged?pageNumber={pageNumber}&pageSize={pageSize}");

			if (!response.IsSuccessStatusCode)
				return new PagedResult<NhanVienResponseDTO>();

			var responseString = await response.Content.ReadAsStringAsync();

			var result = Newtonsoft.Json.JsonConvert
				.DeserializeObject<PagedResult<NhanVienResponseDTO>>(responseString);

			return result ?? new PagedResult<NhanVienResponseDTO>();
		}

		public async Task<ChiTietNhanVienResponseDTO> GetNhanVienByIdAsync(int id)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/NhanVien/{id}");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert
				.DeserializeObject<ChiTietNhanVienResponseDTO>(responseString);
			return result;
		}


		public async Task<PagedResult<NhanVienResponseDTO>> SearchNhanVienAsync(string keyword, int pageNumber, int pageSize)
		{
			AttachToken();
			var response = await _httpClient.GetAsync(
				$"api/NhanVien/search?keyword={keyword}&pageNumber={pageNumber}&pageSize={pageSize}");
			if (!response.IsSuccessStatusCode)
				return new PagedResult<NhanVienResponseDTO>();
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert
				.DeserializeObject<PagedResult<NhanVienResponseDTO>>(responseString);
			return result ?? new PagedResult<NhanVienResponseDTO>();
		}

		public async Task<bool> CreateNhanVienAsync(NhanVienRequestDTO dto)
		{
			AttachToken();

			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);

			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("api/NhanVien", content);

			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateNhanVienAsync(int id, CapNhatNhanVienDTO dto)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"api/NhanVien/{id}", content);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateSatusAsync(int id, string trangthai)
		{
			AttachToken();
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(trangthai);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"api/NhanVien/{id}/trangthai", content);
			return response.IsSuccessStatusCode;
		}

		public async Task<List<ComboboxResult>> GetComboboxAsync(int chucVuId)
		{
			AttachToken();
			var response = await _httpClient.GetAsync($"api/NhanVien/Combobox?chucVuId={chucVuId}");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComboboxResult>>(responseString);
			return result;
		}
	}
}
