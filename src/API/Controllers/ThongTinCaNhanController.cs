using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/thongtincanhan")]
[Authorize]
public class ThongTinCaNhanController : ControllerBase
{
	private readonly ThongTinCaNhanService _service;

	public ThongTinCaNhanController(ThongTinCaNhanService service)
	{
		_service = service;
	}

	// ==================== CREATE KHACH ====================
	[Authorize(Policy = "KHACH_CREATE")]
	[HttpPost("khach")]
	public async Task<ActionResult<ApiResponse<bool>>> CreateKhach([FromBody] ThongTinRequestDTO dto)
	{
		var result = await _service.AddKhachAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "USER_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] ThongTinUpdateRequestDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("Không tìm thấy")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "USER_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<ThongTinReadModel>>> Detail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	[Authorize(Policy = "KHACH_VIEW")]
	[HttpGet("khach")]
	public async Task<ActionResult<ApiResponse<List<ThongTinReadListModel>>>> ListKhach()
	{
		var result = await _service.DanhSachKhachAsync();
		return Ok(result);
	}

	[Authorize(Policy = "USER_UPDATE")]
	[HttpPut("{thongTinId}/taikhoan/{taiKhoanId}")]
	public async Task<ActionResult<ApiResponse<bool>>> LinkTaiKhoan(int thongTinId, int taiKhoanId)
	{
		var result = await _service.CapNhatTaiKhoanAsync(thongTinId, taiKhoanId);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}
}