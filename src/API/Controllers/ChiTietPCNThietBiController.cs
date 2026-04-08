using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/chitiet-pcntb")]
[Authorize]
public class ChiTietPCNThietBiController : ControllerBase
{
	private readonly ChiTietPCNThietBiService _service;

	public ChiTietPCNThietBiController(ChiTietPCNThietBiService service)
	{
		_service = service;
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "CSVC_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] ChiTietPCNThietBiRequestDTO dto)
	{
		var result = await _service.CreateAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "CSVC_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] ChiTietPCNThietBiUpdateDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== DELETE ====================
	[Authorize(Policy = "CSVC_UPDATE")]
	[HttpPut("delete/{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
	{
		var result = await _service.DeleteAsync(id);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}

	// ==================== GET DETAIL ====================
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<ChiTietPCNThietBiReadModel>>> Detail(int id)
	{
		var result = await _service.GetByIdAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET LIST ====================
	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<List<ChiTietPCNThietBiListReadModel>>>> List([FromQuery] int pcnTbId)
	{
		var result = await _service.GetListAsync(pcnTbId);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== IMPORT PREVIEW ====================
	[Authorize(Policy = "CSVC_CREATE")]
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
	[Authorize(Policy = "CSVC_CREATE")]
	[HttpPost("import/validate")]
	public async Task<ActionResult<ApiResponse<ExcelImportResult<ChiTietPCNThietBiImport>>>>
	ValidateImport([FromBody] List<ChiTietPCNThietBiImport> list)
	{
		if (list == null || !list.Any())
			return BadRequest(ApiResponse<string>.Fail("Danh sách import rỗng"));

		var result = await _service.ValidateImport(list);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}
	// ==================== IMPORT CONFIRM ====================
	[Authorize(Policy = "CSVC_CREATE")]
	[HttpPost("import/confirm")]
	public async Task<IActionResult> Import([FromBody] List<ChiTietPCNThietBiImport> list)
	{
		if (list == null || !list.Any())
			return BadRequest(ApiResponse<string>.Fail("Danh sách import rỗng"));

		var response = await _service.ImportAsync(list);

		if (!response.Success)
			return BadRequest(response);

		return Ok(response);
	}

	[Authorize(Policy = "CSVC_VIEW")]
	[HttpGet("combobox")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> Combobox([FromQuery] int pcnId, [FromQuery] int tbId)
	{
		var result = await _service.GetComboboxAsync(pcnId,tbId);

		return Ok(result);
	}
}