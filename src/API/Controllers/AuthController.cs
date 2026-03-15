using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
	private readonly AuthService _service;
	public AuthController(AuthService service)
	{
		_service = service;
	}
	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<ActionResult<ApiResponse<LoginResponseDTO>>> Login(
		[FromBody] LoginRequestDTO dto)
	{
		var result = await _service.DangNhapAsync(dto);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
	[AllowAnonymous]
	[HttpPost("refresh")]
	public async Task<ActionResult<ApiResponse<LoginResponseDTO>>> Refresh(
		[FromBody] RefreshTokenRequestDTO dto)
	{
		var result = await _service.RefreshTokenAsync(dto.RefreshToken);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
	[Authorize]
	[HttpPost("logout")]
	public async Task<ActionResult<ApiResponse<bool>>> Logout(
		[FromBody] RefreshTokenRequestDTO dto)
	{
		var result = await _service.LogoutAsync(dto.RefreshToken);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
}