using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Clinic.WinForms.DTOs;

namespace Clinic.WinForms.Clients
{
	public class AuthClient : ApiClientBase
	{
		public async Task<LoginResponseDTO> LoginAsync(LoginDTO dto)
		{
			var json = JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("/api/TaiKhoan/dangnhap", content);

			if (!response.IsSuccessStatusCode)
				return null;

			var responseString = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<LoginResponseDTO>(responseString);
		}
	}
}
