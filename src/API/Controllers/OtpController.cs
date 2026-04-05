using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/otp")]
public class OtpController : ControllerBase
{
    private readonly OtpService _service;

    public OtpController(OtpService service)
    {
        _service = service;
    }

    [HttpPost("tao")]
    public async Task<ActionResult<ApiResponse<bool>>> TaoOtp([FromBody] TaoOtpRequestDTO dto)
    {
        var result = await _service.TaoOtpAsync(dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("xac-thuc")]
    public async Task<ActionResult<ApiResponse<bool>>> XacThucOtp([FromBody] XacThucOtpRequestDTO dto)
    {
        var result = await _service.XacThucOtpAsync(dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}