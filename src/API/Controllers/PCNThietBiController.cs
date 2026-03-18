using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/pcnthietbi")]
[Authorize]
public class PCNThietBiController : ControllerBase
{
	private readonly PCNThietBiService _service;

	public PCNThietBiController(PCNThietBiService service)
	{
		_service = service;
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "CSVC_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] PCNThietBiRequestDTO dto)
	{
		var result = await _service.TaoMoiAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "CSVC_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] PCNThietBiUpdateDTO dto)
	{
		var result = await _service.CapNhatAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== DELETE ====================
	[Authorize(Policy = "CSVC_UPDATE")]
	[HttpDelete("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
	{
		var result = await _service.XoaAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<PCNThietBiReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET BY PHONG ====================
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("phong/{phongId}")]
	public async Task<ActionResult<ApiResponse<List<PCNThietBiReadModel>>>> GetByPhong(int phongId)
	{
		var result = await _service.GetByPhongAsync(phongId);
		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<PCNThietBiListReadModel>>>> GetPaged(
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 15,
		[FromQuery] int? phongChucNangID = null)
	{
		var result = await _service.GetPagedAsync(pageNumber, pageSize, phongChucNangID);
		return Ok(result);
	}

	// ==================== SEARCH ====================
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<PCNThietBiListReadModel>>>> Search(
		[FromQuery] string keyword,
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 15,
		[FromQuery] int? phongChucNangID = null)
	{
		var result = await _service.SearchAsync(keyword, pageNumber, pageSize, phongChucNangID);
		return Ok(result);
	}
}