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
	[Authorize(Policy = "CHUCVU_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] ChucVuRequestDTO dto)
	{
		var result = await _service.ThemAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "CHUCVU_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] ChucVuRequestDTO dto)
	{
		var result = await _service.CapNhatAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== UPDATE TRANG THAI ====================
	[Authorize(Policy = "CHUCVU_UPDATE")]
	[HttpPut("{id}/trang-thai")]
	public async Task<ActionResult<ApiResponse<bool>>> UpdateTrangThai(int id, [FromBody] string trangThai)
	{
		var result = await _service.CapNhatTrangThaiAsync(id, trangThai);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "CHUCVU_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<ChucVuReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "CHUCVU_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<ChucVuListReadModel>>>> GetPaged(
		[FromQuery] int page = 1,
		[FromQuery] int size = 10,
		[FromQuery] string? trangThai = null)
	{
		var result = await _service.GetPagedAsync(page, size, trangThai);
		return Ok(result);
	}

	// ==================== SEARCH ====================
	[Authorize(Policy = "CHUCVU_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<ChucVuListReadModel>>>> Search(
		[FromQuery] string keyword,
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var result = await _service.SearchAsync(keyword, page, size);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== GET NAME ====================
	[Authorize(Policy = "CHUCVU_VIEW")]
	[HttpGet("{id}/name")]
	public async Task<ActionResult<ApiResponse<string?>>> GetNameById(int id)
	{
		var result = await _service.GetNameByIdAsync(id);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== GET BY NHAN VIEN ====================
	[Authorize(Policy = "CHUCVU_VIEW")]
	[HttpGet("nhanvien/{nhanVienId}")]
	public async Task<ActionResult<ApiResponse<string?>>> GetByNhanVienId(int nhanVienId)
	{
		var result = await _service.GetByNhanVienIdAsync(nhanVienId);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== COMBOBOX ====================
	[Authorize(Policy = "CHUCVU_VIEW")]
	[HttpGet("combobox")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetCombobox()
	{
		var result = await _service.GetIdAndNameAsync();
		return Ok(result);
	}
}