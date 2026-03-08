using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.Clients
{
	public class AuthClient : ApiClientBase_old
	{
		public async Task<LoginResponseDTO> LoginAsync(LoginDTO dto)
		{
			var json = JsonConvert.SerializeObject(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("api/TaiKhoan/dangnhap", content);

			if (!response.IsSuccessStatusCode)
				return null;

			var responseString = await response.Content.ReadAsStringAsync();

			var result = JsonConvert.DeserializeObject<LoginResponseDTO>(responseString);

			if (result != null && !string.IsNullOrEmpty(result.AccessToken))
			{
				Session.Token = result.AccessToken;
				Session.UserId = result.Id;
				Session.NhanVienId = result.NhanVienId;
			}

			return result;
		}
	}
}
