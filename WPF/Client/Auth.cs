using WPF.Common;
using WPF.Models;

namespace WPF.Client;

public class Auth: AppClientBase
{
	public Task<ApiResult<LoginResponseDTO>> Login(LoginRequestDTO req)
	{
		return PostAsync<LoginResponseDTO>("api/auth/login", req, false);
	}
}
