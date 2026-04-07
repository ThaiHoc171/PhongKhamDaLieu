using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/taikhoan")]
public class TaiKhoanController : ControllerBase
{
	private readonly TaiKhoanService _service;

	public TaiKhoanController(TaiKhoanService service)
	{
		_service = service;
	}

	// ==================== CREATE ====================
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] TaiKhoanRequestDTO dto)
	{
		var result = await _service.CreateAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return CreatedAtAction(nameof(Detail), new { id = result.Data }, result);
	}

	// ==================== CHANGE PASSWORD ====================
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

	// ==================== RESET PASSWORD ====================
	[Authorize(Policy = "USER_UPDATE")]
	[HttpPut("{id}/reset-password")]
	public async Task<ActionResult<ApiResponse<bool>>> ResetPassword(int id)
	{
		var result = await _service.ResetPasswordAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}
    // ==================== GET ID ====================
    [HttpGet("getIdByEmail")]
    public async Task<ActionResult<ApiResponse<int>>> GetIdByEmail([FromQuery] string email)
    {
        var response = await _service.GetIdByEmailAsync(email);
        if (!response.Success)
            return BadRequest(response);
        return Ok(response);
    }
    // ==================== UPDATE STATUS ====================
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

	// ==================== UPDATE FCM TOKEN ====================
	[HttpPut("{id}/fcm-token")]
	public async Task<ActionResult<ApiResponse<bool>>> UpdateFcmToken(
		int id,
		[FromBody] UpdateFcmTokenDto dto)
	{
		var result = await _service.UpdateFcmTokenAsync(id, dto.FCMToken);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "USER_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<TaiKhoanReadModel>>> Detail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "USER_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<TaiKhoanListReadModel>>>> Paged(
		[FromQuery] int page = 1,
		[FromQuery] int size = 15,
		[FromQuery] string? vaiTro = null,
		[FromQuery] string? trangThai = null)
	{
		var result = await _service.GetPagedAsync(page, size, vaiTro, trangThai);

		return Ok(result);
	}
	[Authorize(Policy = "USER_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<TaiKhoanListReadModel>>>>
	Search([FromQuery] string keyword, [FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string? vaiTro = null, [FromQuery] string? trangThai = null)
	{
		var result = await _service.SearchAsync(page, size,keyword,vaiTro,trangThai);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}
}