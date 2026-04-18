using Application.Common;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/lieutrinh")]
[Authorize]
public class LieuTrinhDieuTriController : ControllerBase
{
	private readonly LieuTrinhDieuTriService _service;
	public LieuTrinhDieuTriController(LieuTrinhDieuTriService service)
	{
		_service = service;
	}
	[Authorize(Policy = "LIEUTRINH_CREATE")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] LieuTrinhDieuTriRequestDTO dto)
	{
		var result = await _service.CreateAsync(dto);
		if (!result.Success)
			return BadRequest(result);
		return CreatedAtAction(
			nameof(GetById),
			new { id = result.Data },
			result);
	}
	[Authorize(Policy = "LIEUTRINH_UPDATE")]
	[HttpPut("{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> Update( int id, [FromBody] LieuTrinhDieuTriUpdateDTO dto)
	{
		var result = await _service.UpdateAsync(id, dto);	
		if (!result.Success)
			return NotFound(result);
		return Ok(result);
	}
	[Authorize(Policy = "LIEUTRINH_UPDATE")]
	[HttpPut("{id}/complete")]
	public async Task<ActionResult<ApiResponse<bool>>> Complete(int id)
	{
		var result = await _service.CompleteAsync(id);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
	[Authorize(Policy = "LIEUTRINH_UPDATE")]
	[HttpPut("{id}/cancel")]
	public async Task<ActionResult<ApiResponse<bool>>> Cancel( int id, [FromBody] string? ghiChu)
	{
		var result = await _service.CancelAsync(id, ghiChu);
		if (!result.Success)
			return BadRequest(result);
		return Ok(result);
	}
	[Authorize(Policy = "LIEUTRINH_UPDATE")]
	[HttpPut("{id}/status")]
	public async Task<ActionResult<ApiResponse<bool>>> UpdateStatus( int id, [FromBody] LieuTrinhStatusDTO dto)
	{
		var result = await _service.UpdateStatusAsync(id, dto.GhiChu);
		if (!result.Success)
			return NotFound(result);
		return Ok(result);
	}
	[Authorize(Policy = "LIEUTRINH_VIEW")]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<LieuTrinhDieuTriReadModel>>> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);
		if (!result.Success)
			return NotFound(result);
		return Ok(result);
	}
	[Authorize(Policy = "LIEUTRINH_VIEW")]
	[HttpGet("exist/{id}")]
	public async Task<ActionResult<ApiResponse<bool>>> ExistByPhienKham(int id)
	{
		var result = await _service.ExistByPhienKham(id);
		if (!result.Success)
			return NotFound(result);
		return Ok(result);
	}

	[Authorize(Policy = "LIEUTRINH_VIEW")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<LieuTrinhDieuTriListReadModel>>>> 
		GetPaged( [FromQuery] int page = 1, [FromQuery] int size = 15, [FromQuery] string? trangThai = null)
	{
		var result = await _service.GetPagedAsync(page, size, trangThai);
		return Ok(result);
	}
	[Authorize(Policy = "LIEUTRINH_VIEW")]
	[HttpGet("search")]
	public async Task<ActionResult<ApiResponse<PagedResult<LieuTrinhDieuTriListReadModel>>>> 
		Search( [FromQuery] string keyword, [FromQuery] int page = 1, [FromQuery] int size = 15)
	{
		var result = await _service.SearchAsync(keyword, page, size);
		return Ok(result);
	}
	[Authorize(Policy = "LIEUTRINH_VIEW")]
	[HttpGet("benhnhan/{benhNhanId}")]
	public async Task<ActionResult<ApiResponse<PagedResult<LieuTrinhDieuTriListReadModel>>>> 
		GetByBenhNhan( int benhNhanId, [FromQuery] int page = 1, [FromQuery] int size = 15)
	{
		var result = await _service.GetByBenhNhanAsync(benhNhanId, page, size);
		return Ok(result);
	}
}