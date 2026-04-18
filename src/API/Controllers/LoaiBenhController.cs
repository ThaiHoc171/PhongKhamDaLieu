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
	[Authorize(Policy = "HETHONG_WRITE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] LoaiBenhRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "HETHONG_WRITE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] LoaiBenhUpdateDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tìm thấy")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "HETHONG_READ")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<LoaiBenhReadModel>>> Detail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "HETHONG_READ")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<LoaiBenhListReadModel>>>> Paged(
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var result = await _service.GetPagedAsync(page, size);
		return Ok(result);
	}

	// ==================== SEARCH ====================
	[Authorize(Policy = "HETHONG_READ")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<LoaiBenhListReadModel>>>> Search(
		[FromQuery] string keyword,
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var result = await _service.SearchAsync(keyword, page, size);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== COMBOBOX ====================
	[Authorize(Policy = "HETHONG_READ")]
	[HttpGet("combobox")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> Combobox()
	{
		var result = await _service.GetComboboxAsync();
		return Ok(result);
	}

	// ==================== GET TEN BENH ====================
	[Authorize(Policy = "HETHONG_READ")]
	[HttpGet("{id}/ten")]
	public async Task<ActionResult<ApiResponse<string?>>> GetTenBenh(int id)
	{
		var result = await _service.GetTenBenhAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== IMPORT PREVIEW ====================
	[Authorize(Policy = "HETHONG_WRITE")]
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

	// ==================== IMPORT VALIDATE ====================
	[Authorize(Policy = "HETHONG_WRITE")]
	[HttpPost("import/validate")]
	public async Task<ActionResult<ApiResponse<ExcelImportResult<LoaiBenhRequestDTO>>>>
	ValidateImport([FromBody] List<LoaiBenhRequestDTO> list)
	{
		if (list == null || !list.Any())
			return BadRequest(ApiResponse<string>.Fail("Danh sách import rỗng"));

		var result = await _service.ValidateImport(list);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== IMPORT CONFIRM ====================
	[Authorize(Policy = "HETHONG_WRITE")]
	[HttpPost("import/confirm")]
	public async Task<IActionResult> Import([FromBody] List<LoaiBenhRequestDTO> list)
	{
		if (list == null || !list.Any())
			return BadRequest(ApiResponse<string>.Fail("Danh sách import rỗng"));

		var response = await _service.ImportAsync(list);

		if (!response.Success)
			return BadRequest(response);

		return Ok(response);
	}
}