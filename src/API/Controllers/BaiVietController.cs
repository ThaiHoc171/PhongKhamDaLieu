using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/baiviet")]
[Authorize]
public class BaiVietController : ControllerBase
{
	private readonly BaiVietService _service;

	public BaiVietController(BaiVietService service)
	{
		_service = service;
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "BAIVIET_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] ThemBaiVietDTO dto)
	{
		var result = await _service.ThemAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return CreatedAtAction(
			nameof(GetById),
			new { id = result.Data },
			result
		);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "BAIVIET_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] CapNhatBaiVietDTO dto)
	{
		var result = await _service.CapNhatAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== DELETE ====================
	[Authorize(Policy = "BAIVIET_UPDATE")]
	[HttpDelete("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
	{
		var result = await _service.XoaAsync(id);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "BAIVIET_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<BaiVietReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "BAIVIET_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<BaiVietListReadModel>>>> GetPaged(
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var result = await _service.GetPagedAsync(page, size);
		return Ok(result);
	}

	// ==================== FILTER ====================
	[Authorize(Policy = "BAIVIET_VIEW")]
	[HttpGet("loaibenh/{loaiBenhId}")]
	public async Task<ActionResult<ApiResponse<List<BaiVietListReadModel>>>> GetByLoaiBenh(int loaiBenhId)
	{
		var result = await _service.GetByLoaiBenhAsync(loaiBenhId);
		return Ok(result);
	}

	// ==================== TOP ====================
	[Authorize(Policy = "BAIVIET_VIEW")]
	[HttpGet("top")]
	public async Task<ActionResult<ApiResponse<List<BaiVietListReadModel>>>> GetTop(
		[FromQuery] int top = 5)
	{
		var result = await _service.GetTopLuotXemAsync(top);
		return Ok(result);
	}
}