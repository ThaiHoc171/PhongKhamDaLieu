using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Client;

public class Auth: AppClientBase
{
	public Task<ApiResult<LoginResponseDTO>> Login(LoginRequestDTO req)
	{
		return PostAsync<LoginResponseDTO>("api/auth/login", req, false);
	}
}
