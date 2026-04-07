using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/phienkham-thietbi")]
[Authorize]
public class PhienKhamThietBiController : ControllerBase
{
	private readonly PhienKhamThietBiService _service;

	public PhienKhamThietBiController(PhienKhamThietBiService service)
	{
		_service = service;
	}

	// ==================== GET BY PHIEN KHAM ====================
	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("phienkham/{phienKhamId}")]
	public async Task<ActionResult<ApiResponse<List<PhienKhamThietBiReadModel>>>> GetByPhienKham(int phienKhamId)
	{
		var result = await _service.GetByPhienKhamAsync(phienKhamId);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] PhienKhamThietBiRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] string? ghiChu)
	{
		var result = await _service.UpdateAsync(id, ghiChu);

		if (!result.Success)
			return result.Message.Contains("không tìm thấy")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}
}