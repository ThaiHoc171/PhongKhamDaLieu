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
	[Authorize(Policy = "ROLE_CREATE")]
	[HttpPost("import/preview")]
	public async Task<IActionResult> PreviewImport(IFormFile file, [FromQuery] string sheet)
	{
		if (file == null || file.Length == 0)
			return BadRequest(ApiResponse<string>.Fail("File không hợp lệ"));

		if (string.IsNullOrWhiteSpace(sheet))
			return BadRequest(ApiResponse<string>.Fail("Sheet không hợp lệ"));

		using var stream = file.OpenReadStream();

		var response = await _service.PreviewImport(stream, sheet);

		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}
	[Authorize(Policy = "ROLE_CREATE")]
	[HttpPost("import/confirm")]
	public async Task<IActionResult> Import([FromBody] List<ChucVuImport> list)
	{
		if (list == null || !list.Any())
			return BadRequest(ApiResponse<string>.Fail("Danh sách import rỗng"));

		var response = await _service.Import(list);

		if (!response.Success)
			return BadRequest(response);

		return Ok(response);
	}
}
