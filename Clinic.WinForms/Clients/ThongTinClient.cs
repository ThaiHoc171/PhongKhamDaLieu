using Clinic.WinForms.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clinic.WinForms.Clients
{
	public class ThongTinClient:ApiClientBase_old
	{
		public async Task<bool> UpdateThongTinAsync(int id, CapNhatThongTinCaNhanDTO dto)
		{
			AttachToken();

			var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PutAsync(
				$"/api/ThongTinCaNhan/{id}",
				content);

			return response.IsSuccessStatusCode;
		}
		public async Task<List<ComboboxResult>> GetComboboxAsync()
		{
			AttachToken();
			var response = await _httpClient.GetAsync("api/ThongTinCaNhan/BenhNhan/Combobox");
			if (!response.IsSuccessStatusCode)
				return null;
			var responseString = await response.Content.ReadAsStringAsync();
			return Newtonsoft.Json.JsonConvert
				.DeserializeObject<List<ComboboxResult>>(responseString);
		}

	}
}
