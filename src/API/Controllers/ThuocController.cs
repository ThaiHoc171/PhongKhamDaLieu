using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/thuoc")]
[Authorize]
public class ThuocController : ControllerBase
{
	private readonly ThuocService _service;

	public ThuocController(ThuocService service)
	{
		_service = service;
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "HETHONG_WRITE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] ThuocRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "HETHONG_WRITE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] ThuocUpdateDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("Không tìm thấy")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== DELETE ====================
	[Authorize(Policy = "HETHONG_WRITE")]
	[HttpPut("delete/{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
	{
		var result = await _service.DeleteAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "HETHONG_READ")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<ThuocReadModel>>> Detail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "HETHONG_READ")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<ThuocReadModel>>>> Paged(
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var result = await _service.GetPagedAsync(page, size);

		return Ok(result);
	}

	// ==================== SEARCH ====================
	[Authorize(Policy = "HETHONG_READ")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<ThuocReadModel>>>> Search(
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
	public async Task<ActionResult<ApiResponse<ExcelImportResult<ThuocRequestDTO>>>> ValidateImport(
		[FromBody] List<ThuocRequestDTO> list)
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
	public async Task<IActionResult> Import([FromBody] List<ThuocRequestDTO> list)
	{
		if (list == null || !list.Any())
			return BadRequest(ApiResponse<string>.Fail("Danh sách import rỗng"));

		var response = await _service.ImportAsync(list);

		if (!response.Success)
			return BadRequest(response);

		return Ok(response);
	}
}