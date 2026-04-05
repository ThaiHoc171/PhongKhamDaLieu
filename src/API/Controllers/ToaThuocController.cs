using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/toathuoc")]
[Authorize]
public class ToaThuocController : ControllerBase
{
	private readonly ToaThuocService _service;

	public ToaThuocController(ToaThuocService service)
	{
		_service = service;
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] ToaThuocRequestDTO dto)
	{
		var result = await _service.CreateAsync(dto);
		if (!result.Success)
			return BadRequest(result);

		return CreatedAtAction(
			nameof(GetByPhienKham),
			new { phienKhamId = dto.PhienKhamID },
			result);
	}

	// ==================== KIỂM TRA TOA ====================
	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("phienkham/exits/{phienKhamId}")]
	public async Task<IActionResult> Exists(int phienKhamId)
	{
		var exists = await _service.KiemTraTonTaiAsync(phienKhamId);
		return Ok(exists);
	}

	// ==================== LẤY TOA THEO PHIÊN KHÁM ====================
	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("phienkham/{phienKhamId}")]
	public async Task<ActionResult<ApiResponse<ToaThuocReadModel>>> GetByPhienKham(int phienKhamId)
	{
		var result = await _service.GetByPhienKhamAsync(phienKhamId);
		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET PAGED ====================
	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<ToaThuocListReadModel>>>> GetPaged(
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var result = await _service.GetPagedAsync(page, size);
		return Ok(result);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{toaThuocId}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(
		int toaThuocId,
		[FromBody] List<ChiTietToaThuocRequestDTO> chiTiet)
	{
		if (chiTiet == null)
			chiTiet = new List<ChiTietToaThuocRequestDTO>();

		var result = await _service.UpdateAsync(toaThuocId, chiTiet);
		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}
}