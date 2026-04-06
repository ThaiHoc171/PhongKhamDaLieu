using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/hosobenhan")]
[Authorize]
public class HoSoBenhAnController : ControllerBase
{
	private readonly HoSoBenhAnService _service;

	public HoSoBenhAnController(HoSoBenhAnService service)
	{
		_service = service;
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "HOSO_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] HoSoBenhAnRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "HOSO_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] HoSoBenhAnUpdateDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "HOSO_DETAIL")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<HoSoBenhAnReadModel>>> Detail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET BY BENH NHAN ====================
	[Authorize(Policy = "HOSO_DETAIL")]
	[HttpGet("benhnhan/{benhNhanId}")]
	public async Task<ActionResult<ApiResponse<HoSoBenhAnReadModel?>>> GetByBenhNhanId(int benhNhanId)
	{
		var result = await _service.GetByBenhNhanIdAsync(benhNhanId);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "HOSO_LIST")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<HoSoBenhAnListReadModel>>>> Paged(
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var result = await _service.GetPagedAsync(page, size);
		return Ok(result);
	}

	// ==================== SEARCH ====================
	[Authorize(Policy = "HOSO_LIST")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<HoSoBenhAnListReadModel>>>> Search(
		[FromQuery] string keyword,
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var result = await _service.SearchAsync(keyword, page, size);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}
}