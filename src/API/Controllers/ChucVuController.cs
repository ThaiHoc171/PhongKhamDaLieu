using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/chucvu")]
[Authorize]
public class ChucVuController : ControllerBase
{
	private readonly ChucVuService _service;

	public ChucVuController(ChucVuService service)
	{
		_service = service;
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "ROLE_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] ChucVuRequest dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "ROLE_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] ChucVuRequest dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}


	// ==================== GET DETAIL ====================
	[Authorize(Policy = "ROLE_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<ChucVuReadModel>>> Detail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "ROLE_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<ChucVuListReadModel>>>> Paged([FromQuery] int page = 1, [FromQuery] int size = 10)
	{
		var result = await _service.GetPagedAsync(page, size);
		return Ok(result);
	}

	// ==================== SEARCH ====================
	[Authorize(Policy = "ROLE_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<ChucVuListReadModel>>>> Search([FromQuery] string keyword, [FromQuery] int page = 1, [FromQuery] int size = 10)
	{
		var result = await _service.SearchAsync(keyword, page, size);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== GET BY NHAN VIEN ====================
	[Authorize(Policy = "ROLE_VIEW")]
	[HttpGet("nhanvien/{nhanVienId}")]
	public async Task<ActionResult<ApiResponse<string?>>> GetByNhanVienId(int nhanVienId)
	{
		var result = await _service.GetByNhanVienIdAsync(nhanVienId);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== COMBOBOX ====================
	[Authorize(Policy = "ROLE_VIEW")]
	[HttpGet("combobox")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> Combobox()
	{
		var result = await _service.GetComboboxAsync();
		return Ok(result);
	}
}
