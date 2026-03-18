using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/loaibenh")]
[Authorize]
public class LoaiBenhController : ControllerBase
{
	private readonly LoaiBenhService _service;

	public LoaiBenhController(LoaiBenhService service)
	{
		_service = service;
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "BENH_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] LoaiBenhRequestDTO dto)
	{
		var result = await _service.TaoMoiAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "BENH_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] LoaiBenhUpdateDTO dto)
	{
		var result = await _service.CapNhatAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "BENH_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<LoaiBenhReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "BENH_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<LoaiBenhListReadModel>>>> GetPaged(
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 15)
	{
		var result = await _service.GetPagedAsync(pageNumber, pageSize);
		return Ok(result);
	}

	// ==================== SEARCH ====================
	[Authorize(Policy = "BENH_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<LoaiBenhListReadModel>>>> Search(
		[FromQuery] string keyword,
		[FromQuery] int pageNumber = 1,
		[FromQuery] int pageSize = 15)
	{
		var result = await _service.SearchAsync(keyword, pageNumber, pageSize);
		return Ok(result);
	}

	// ==================== COMBOBOX ====================
	[Authorize(Policy = "BENH_VIEW")]
	[HttpGet("combobox")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetCombobox()
	{
		var result = await _service.GetComboboxAsync();
		return Ok(result);
	}

	// ==================== GET TEN BENH ====================
	[Authorize(Policy = "BENH_VIEW")]
	[HttpGet("{id}/ten")]
	public async Task<ActionResult<ApiResponse<string>>> GetTenBenh(int id)
	{
		var result = await _service.GetTenBenhAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}
}