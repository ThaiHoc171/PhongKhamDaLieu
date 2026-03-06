using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]

public class RefreshTokenController : ControllerBase
{
    private readonly RefreshTokenService _service;

    public RefreshTokenController(RefreshTokenService refreshTokenService)
    {
        _service = refreshTokenService;
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] string tokenHash)
    {
        await _service.RevokeAsync(tokenHash);
        return Ok(new { message = "Đăng xuất thành công" });
    }
}
