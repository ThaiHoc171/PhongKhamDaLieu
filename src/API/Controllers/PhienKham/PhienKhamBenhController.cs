using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/phienkhambenh")]
[Authorize]
public class PhienKhamBenhController : ControllerBase
{
	private readonly PhienKhamBenhService _service;

	public PhienKhamBenhController(PhienKhamBenhService service)
	{
		_service = service;
	}

	// ==================== CREATE ====================

	[Authorize(Policy = "PHIENKHAM_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] PhienKhamBenhRequestDTO dto)
	{
		var result = await _service.AddAsync(dto);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}

	// ==================== UPDATE ====================

	[Authorize(Policy = "PHIENKHAM_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] PhienKhamBenhRequestDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);

		if (!result.Success)
			return result.Message.Contains("không tồn tại")
				? NotFound(result)
				: BadRequest(result);

		return Ok(result);
	}
	[Authorize(Policy = "PHIENKHAM_UPDATE")]
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

	// ==================== GET BY ID ====================

	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<PhienKhamBenhResponseDTO>>> Detail(int id)
	{
		var result = await _service.GetDetailAsync(id);

		if (!result.Success)
			return NotFound(result);

		return Ok(result);
	}

	// ==================== GET BY PHIEN KHAM ====================

	[Authorize(Policy = "PHIENKHAM_VIEW")]
	[HttpGet("phienkham/{phienKhamId}")]
	public async Task<ActionResult<ApiResponse<List<PhienKhamBenhReadModel>>>> GetByPhienKham(int phienKhamId)
	{
		var result = await _service.GetByPhienKhamIdAsync(phienKhamId);

		if (!result.Success)
			return BadRequest(result);

		return Ok(result);
	}
}
