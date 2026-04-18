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
	[Authorize(Policy = "KHAMBENH_READ")]
	[HttpGet("phienkham/{phienKhamId}")]
	public async Task<ActionResult<ApiResponse<List<PhienKhamThietBiReadModel>>>> GetByPhienKham(int phienKhamId)
	{
		var result = await _service.GetByPhienKhamAsync(phienKhamId);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== CREATE ====================
	[Authorize(Policy = "KHAMBENH_WRITE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] PhienKhamThietBiRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== UPDATE ====================
	[Authorize(Policy = "KHAMBENH_WRITE")]
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
	[Authorize(Policy = "KHAMBENH_WRITE")]
	[HttpPut("delete/{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
	{
		var result = await _service.DeleteAsync(id);

		if (!result.Success)
			return result.Message.Contains("không tìm thấy")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}
}