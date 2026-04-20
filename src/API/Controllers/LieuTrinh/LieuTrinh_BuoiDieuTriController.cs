using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/buoidieutri")]
[Authorize]
public class BuoiDieuTriController : ControllerBase
{
	private readonly BuoiDieuTriService _service;
	public BuoiDieuTriController(BuoiDieuTriService service)
	{
		_service = service;
	}
	[Authorize(Policy = "KHAMBENH_WRITE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] BuoiDieuTriRequestDTO dto)
	{
		var result = await _service.CreateAsync(dto);
		if (!result.Success)
			return BadRequest(result);
		return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
	}
	[Authorize(Policy = "KHAMBENH_READ")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<BuoiDieuTriReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);
		if (!result.Success)
			return NotFound(result);
		return Ok(result);
	}
	[Authorize(Policy = "KHAMBENH_READ")]
	[HttpGet("lieutrinh/{lieuTrinhId}")]
	public async Task<ActionResult<ApiResponse<List<BuoiDieuTriListReadModel>>>> GetByLieuTrinh(int lieuTrinhId)
	{
		var result = await _service.GetByLieuTrinhAsync(lieuTrinhId);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
	[Authorize(Policy = "KHAMBENH_READ")]
	[HttpGet("lieutrinh/{lieuTrinhId}/count-complete")]
	public async Task<ActionResult<ApiResponse<int>>> CountComplete(int lieuTrinhId)
	{
		var result = await _service.CountCompleteAsync(lieuTrinhId);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
}