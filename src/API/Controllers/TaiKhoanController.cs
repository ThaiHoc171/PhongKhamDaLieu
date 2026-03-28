using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/taikhoan")]
[Authorize]
public class TaiKhoanController : ControllerBase
{
	private readonly TaiKhoanService _service;
	public TaiKhoanController(TaiKhoanService service)
	{
		_service = service;
	}
	[Authorize(Policy = "USER_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create(
		[FromBody] TaiKhoanRequestDTO dto)
	{
		var result = await _service.CreateAsync(dto);
		if (!result.Success)
			return BadRequest(result);
		return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
	}
	[Authorize(Policy = "USER_UPDATE")]
	[HttpPut("{id}/password")]
	public async Task<ActionResult<ApiResponse<bool>>> ChangePassword(
		int id,
		[FromBody] ChangePasswordRequestDTO dto)
	{
		var result = await _service.ChangePasswordAsync(id, dto);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
	[Authorize(Policy = "USER_UPDATE")]
	[HttpPut("{id}/reset-password")]
	public async Task<ActionResult<ApiResponse<bool>>> ResetPassword(int id)
	{
		var result = await _service.ResetPasswordAsync(id);
		if (!result.Success)
			return NotFound(result);
		return Ok(result);
	}
	[Authorize(Policy = "USER_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<TaiKhoanListReadModel>>>> GetPaged(
		[FromQuery] int page = 1,
		[FromQuery] int size = 15,
		[FromQuery] string? vaiTro = null,
		[FromQuery] string? trangThai = null)
	{
		var result = await _service.GetPagedAsync(page, size, vaiTro, trangThai);
		return Ok(result);
	}
	[Authorize(Policy = "USER_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<TaiKhoanReadModel>>> GetById(int id)
	{
		if (User.IsInRole("Bệnh nhân"))
		{
			var benhNhanId = int.Parse(User.FindFirst("BenhNhanID")!.Value);
			if (benhNhanId != id)
				return Forbid();
		}
		var result = await _service.GetByIdAsync(id);
		if (!result.Success)
			return NotFound(result);
		return Ok(result);
	}
	[Authorize(Policy = "USER_UPDATE")]
	[HttpPut("{id}/status")]
	public async Task<ActionResult<ApiResponse<bool>>> UpdateStatus(
		int id,
		[FromBody] TaiKhoanUpdateRequestDTO dto)
	{
		var result = await _service.UpdateStatusAsync(id, dto);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
    [HttpPut("fcm-token")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateFcmToken(
    [FromBody] UpdateFcmTokenDto dto)
    {
        var taiKhoanId = int.Parse(User.FindFirst("TaiKhoanID")!.Value);
        var result = await _service.UpdateFcmTokenAsync(taiKhoanId, dto.FCMToken);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }
}