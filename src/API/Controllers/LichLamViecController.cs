using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/lichlamviec")]
[Authorize]
public class LichLamViecController : ControllerBase
{
	private readonly LichLamViecService _service;
	public LichLamViecController(LichLamViecService service)
	{
		_service = service;
	}
	[Authorize(Policy = "LICH_READ")]
	[HttpGet]
	public async Task<IActionResult> List([FromQuery] int week = 0)
	{
		var response = await _service.GetWeekAsync(week);
		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}
	[Authorize(Policy = "LICH_READ")]
	[HttpGet("nhan-vien/{nhanVienID}")]
	public async Task<IActionResult> GetByNhanVien(int nhanVienID, [FromQuery] int week = 0)
	{
		var response = await _service.GetWeekByNhanVienAsync(nhanVienID, week);
		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}
	[Authorize(Policy = "LICH_WRITE")]
	[HttpPost("import/preview")]
	public async Task<ActionResult<ApiResponse<ExcelImportResult<LichLamViecImport>>>> 
		PreviewImport( IFormFile file, [FromQuery] string sheet)
	{
		if (file == null || file.Length == 0)
			return BadRequest(ApiResponse<string>.Fail("File không hợp lệ"));

		if (string.IsNullOrWhiteSpace(sheet))
			return BadRequest(ApiResponse<string>.Fail("Sheet không hợp lệ"));

		using var stream = file.OpenReadStream();

		var result = await _service.PreviewImport(stream, sheet);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== VALIDATE IMPORT ====================
	[Authorize(Policy = "LICH_WRITE")]
	[HttpPost("import/validate")]
	public async Task<ActionResult<ApiResponse<ExcelImportResult<LichLamViecImport>>>> 
		ValidateImport([FromBody] List<LichLamViecImport> list)
	{
		if (list == null || !list.Any())
			return BadRequest(ApiResponse<string>.Fail("Danh sách import rỗng"));

		var result = await _service.ValidateImport(list);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== CONFIRM IMPORT ====================
	[Authorize(Policy = "LICH_WRITE")]
	[HttpPost("import/confirm")]
	public async Task<ActionResult<ApiResponse<bool>>> Import(
		[FromBody] List<LichLamViecImport> list)
	{
		if (list == null || !list.Any())
			return BadRequest(ApiResponse<string>.Fail("Danh sách import rỗng"));

		var result = await _service.Import(list);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}
}